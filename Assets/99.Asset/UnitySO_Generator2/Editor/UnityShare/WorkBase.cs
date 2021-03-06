using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

#if !UNITY_EDITOR
using System.Windows.Forms;
#endif

namespace SpreadSheetParser
{
    abstract public class WorkBase
    {
        public int iWorkOrder;
        public Type pType;
        public bool bEnable;

        [NonSerialized]
        private Type pFormType;

#if !UNITY_EDITOR
        protected WorkBase()
        {
            OnCreateInstance(out pFormType, out pType);
        }

        public void ShowForm()
        {
            Form pForm = (Form)Activator.CreateInstance(pFormType);
            pForm.Show();
            pForm.Name = ToString();

            OnShowForm(pForm);
        }
#endif

        public WorkBase CopyInstance()
        {
            return (WorkBase)this.MemberwiseClone();
        }

#if !UNITY_EDITOR
        public void DoAutoSaveAsync()
        {
            SpreadSheetParser_MainForm.WriteConsole($"자동 저장 중.. {GetDisplayString()}");
            SaveDataManager.SaveSheet(SpreadSheetParser_MainForm.pSpreadSheet_CurrentConnected);
        }

        public void DoOpenFolder(string strPath)
        {
            SpreadSheetParser_MainForm.DoOpenFolder(GetRelative_To_AbsolutePath(strPath));
        }
#endif

        abstract public void DoWork(CodeFileBuilder pCodeFileBuilder, SpreadSheetConnector pConnector, IEnumerable<TypeData> listSheetData, System.Action<string> OnPrintWorkState);
#if !UNITY_EDITOR
        virtual public void DoWorkAfter() { }

        abstract protected void OnCreateInstance(out Type pFormType, out Type pType);
        abstract protected void OnShowForm(Form pFormInstance);
#endif

        abstract public string GetDisplayString();

        public override string ToString()
        {
            return GetDisplayString();
        }


        public string GetRelative_To_AbsolutePath(string strPath)
        {
            if (Path.IsPathRooted(strPath))
                return strPath;

            string strRelativePath = $"{new Uri(Directory.GetCurrentDirectory()).AbsolutePath}/";
#if !UNITY_EDITOR
            strRelativePath += "../";
#endif

            return strRelativePath + strPath;
        }

    }

    // https://stackoverflow.com/questions/20995865/deserializing-json-to-abstract-class
    public class WorkJsonConverter : JsonConverter
    {
        public class BaseSpecifiedConcreteClassConverter : DefaultContractResolver
        {
            protected override JsonConverter ResolveContractConverter(Type objectType)
            {
                if (typeof(WorkBase).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                    return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
                return base.ResolveContractConverter(objectType);
            }
        }

        static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new BaseSpecifiedConcreteClassConverter() };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(WorkBase));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            string strTypeName = jo["pType"].Value<string>();

            foreach(var pType in GetEnumerableOfType(typeof(WorkBase)))
            {
                if(strTypeName.Contains(pType.Name))
                {
                    return JsonConvert.DeserializeObject(jo.ToString(), pType, SpecifiedSubclassConversion);
                }
            }

            return null;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // won't be called because CanWrite returns false
        }

        public static IEnumerable<Type> GetEnumerableOfType(System.Type pBaseType)
        {
            List<Type> objects = new List<Type>();
            foreach (Type type in
                Assembly.GetAssembly(pBaseType).GetTypes()
                .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(pBaseType)))
            {
                objects.Add(type);
            }
            return objects;
        }
    }
}
