#region Header
/*	============================================
 *	�ۼ��� : Strix
 *	�ۼ��� : 2020-01-21 ���� 4:36:26
 *	���� : 
 *	�ø������¡ ������ ������Ʈ�� �����ϴ� ���
 *	��ȣȭ ���
 *	
 *	��ȣȭ ���� ��ũ : https://gist.github.com/ftvs/5299600
   ============================================ */
#endregion Header

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace UnityLocalSavePattern
{
    /// <summary>
    /// <see cref="UnityEngine.PlayerPrefs"/>���� ����� �߰��� Ŭ�����Դϴ�.
    /// </summary>
    public static class PlayerPrefsExtension
    {
        #region ��ȣȭ ����

        const string g_strPrivateKey = "��ȣȭ�ؾ���";
        const string const_strEncryption_Check = "_encryption_check";
        const string const_strUsedKey = "_used_key";

        static readonly System.Random g_pRandom = new System.Random(g_strPrivateKey.GetHashCode());
        static readonly UTF8Encoding g_Encoding = new UTF8Encoding();
        static readonly MD5CryptoServiceProvider g_MD5Provider = new MD5CryptoServiceProvider();
        static readonly TripleDESCryptoServiceProvider g_TripleDescryptoProvider = Generate_TripleDescryptoProvider();

        private static TripleDESCryptoServiceProvider Generate_TripleDescryptoProvider()
        {
            TripleDESCryptoServiceProvider pProvider = new TripleDESCryptoServiceProvider();
            pProvider.Mode = CipherMode.ECB;
            pProvider.Padding = PaddingMode.PKCS7;

            return pProvider;
        }

        public class EncryptWrapper
        {
            public string strKey;
            public string strData;

            public EncryptWrapper(string strKey, string strData)
            {
                this.strKey = strKey; this.strData = strData;
            }
        }

        public static string[] g_arrKeys = new string[] { "����ƮŰ", "�Դϴ�", "������ �����մϴ�" };

        public static byte[] Get_MD5Hash(string strToEncrypt)
        {
            return g_MD5Provider.ComputeHash(g_Encoding.GetBytes(strToEncrypt));
        }

        static void SaveEncryption(string strKey, string strType, string strValue)
        {
            if (g_arrKeys.Length == 0)
            {
                Debug.LogError(nameof(SaveEncryption) + "g_arrKeys.Length == 0");
            }

            try
            {
                int iKeyIndex_Random = g_pRandom.Next(int.MinValue, int.MaxValue) % g_arrKeys.Length;
                string strSecretKey = g_arrKeys[iKeyIndex_Random];
                byte[] arrKey = GetCheckSum(strType, strSecretKey);
                byte[] arrData = g_Encoding.GetBytes(strValue);

                g_TripleDescryptoProvider.Key = arrKey;
                ICryptoTransform transform = g_TripleDescryptoProvider.CreateEncryptor();
                byte[] arrEncryptedData = transform.TransformFinalBlock(arrData, 0, arrData.Length);

                PlayerPrefs.SetString(strKey, System.Convert.ToBase64String(arrEncryptedData));
                PlayerPrefs.SetInt(strKey + const_strUsedKey, iKeyIndex_Random);
            }
            catch
            {
            }
        }

        static bool GetEncryptedData(string strKey, string strType, out string strValue)
        {
            strValue = "";
            if (PlayerPrefs.HasKey(strKey) == false || PlayerPrefs.HasKey(strKey + const_strUsedKey) == false)
                return false;

            try
            {
                int iKeyIndex_Random = PlayerPrefs.GetInt(strKey + const_strUsedKey);
                string strSecretKey = g_arrKeys[iKeyIndex_Random];
                byte[] arrKey = GetCheckSum(strType, strSecretKey);

                string strEncryptedData = PlayerPrefs.GetString(strKey);
                byte[] arrEncryptedData = System.Convert.FromBase64String(strEncryptedData);

                g_TripleDescryptoProvider.Key = arrKey;
                ICryptoTransform transform = g_TripleDescryptoProvider.CreateDecryptor();
                byte[] results = transform.TransformFinalBlock(arrEncryptedData, 0, arrEncryptedData.Length);
                strValue = g_Encoding.GetString(results);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static void DeleteKey(string strKey)
        {
            PlayerPrefs.DeleteKey(strKey);
            PlayerPrefs.DeleteKey(strKey + const_strEncryption_Check);
            PlayerPrefs.DeleteKey(strKey + const_strUsedKey);
        }


        private static byte[] GetCheckSum(string strType, string strSecretKey)
        {
            return Get_MD5Hash(strType + "_" + g_strPrivateKey + "_" + strSecretKey);
        }


        #endregion ��ȣȭ ����

        public static void SetObject(string strKey, object pSerializeObject, System.Action<string> OnError = null)
        {
            string strJson = JsonUtility.ToJson(pSerializeObject);
            if (Check_IsInvalidJson(strJson))
            {
                OnError?.Invoke($"{nameof(SetObject)} - ToJson() Fail - {strKey}-{pSerializeObject.ToString()}");

                return;
            }

            PlayerPrefs.SetString(strKey, strJson);
        }

        public static void SetObject_Encrypt(string strKey, object pSerializeObject, System.Action<string> OnError = null)
        {
            string strJson = JsonUtility.ToJson(pSerializeObject);
            if (Check_IsInvalidJson(strJson))
            {
                OnError?.Invoke($"{nameof(SetObject)} - ToJson() Fail - {strKey}-{pSerializeObject.ToString()}");

                return;
            }

            SetObject(strKey, pSerializeObject, OnError);
            SaveEncryption(strKey, pSerializeObject.GetType().Name, strJson);
        }

        public static bool GetObject<T>(string strKey, ref T pGetObject, System.Action<string> OnError = null)
        {
            if (PlayerPrefs.HasKey(strKey) == false)
            {
                OnError?.Invoke($"{nameof(GetObject)} - PlayerPrefs.HasKey({strKey}) == false");

                return false;
            }

            string strJson = PlayerPrefs.GetString(strKey);
            if (Check_IsInvalidJson(strJson))
            {
                OnError?.Invoke($"{nameof(GetObject)} - ToJson() Fail - {strKey}");

                return false;
            }

            try
            {
                JsonUtility.FromJsonOverwrite(strJson, pGetObject);
            }
            catch (System.Exception e)
            {
                OnError?.Invoke($"{nameof(GetObject)} - FromJsonOverwrite Fail - {strKey} Value : \n{strJson}\n{e}");

                return false;
            }

            return true;
        }

        public static bool GetObject_Encrypted<T>(string strKey, ref T pGetObject, System.Action<string> OnError = null)
        {
            string strJson;

            if (GetEncryptedData(strKey, pGetObject.GetType().Name, out strJson) == false)
            {
                OnError?.Invoke($"{nameof(GetObject_Encrypted)} - Check Encrypt() Fail - {strKey}");

                return false;
            }
            JsonUtility.FromJsonOverwrite(strJson, pGetObject);

            return true;
        }


        private static bool Check_IsInvalidJson(string strJson)
        {
            if (string.IsNullOrEmpty(strJson) || strJson.Equals("{}") == false)
                return false;

            return true;
        }
    }
}