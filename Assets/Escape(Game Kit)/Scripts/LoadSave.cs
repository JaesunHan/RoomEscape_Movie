using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadSave : MonoBehaviour {
	public Transform Player;
	public CaptueCamera _CaptueCamera;
	public Vector3 ZeroPosition;
	public Text[] ButtonsSaved;
	public SavedTriggers _SavedTriggers;
	public Menu _Menu;
	// Use this for initialization
	void Start () {
		_Menu = Camera.main.GetComponent<Menu> ();
		int NmSav = PlayerPrefs.GetInt ("NmSav");
		//print (System.Convert.ToString(System.DateTime.Now));
		if (PlayerPrefs.GetInt ("Load") == 1) {
			transform.position = new Vector3 (PlayerPrefs.GetFloat ("LoadX"+System.Convert.ToString(NmSav)), PlayerPrefs.GetFloat ("LoadY"+System.Convert.ToString(NmSav)), PlayerPrefs.GetFloat ("LoadZ"+System.Convert.ToString(NmSav)));
		} else {
			transform.position = ZeroPosition;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void PlayerSaved (int numSaved) {
		_Menu._AudioSource.clip = _Menu.ClickClip;
		_Menu._AudioSource.Play ();
		PlayerPrefs.SetFloat ("LoadX"+System.Convert.ToString(numSaved), Player.transform.position.x);
		PlayerPrefs.SetFloat ("LoadY"+System.Convert.ToString(numSaved), Player.transform.position.y);
		PlayerPrefs.SetFloat ("LoadZ"+System.Convert.ToString(numSaved), Player.transform.position.z);
		PlayerPrefs.SetString ("TimeSave"+System.Convert.ToString(numSaved), System.Convert.ToString(System.DateTime.Now));
		PlayerPrefs.SetInt ("Saved"+System.Convert.ToString(numSaved), 1);
		PlayerPrefs.SetInt ("Sav", 1);
		for (int i = 0; i < _SavedTriggers.TriggerObjects.Length; i++) {
			if(_SavedTriggers.TriggerObjects[i] == null)
			{
				_SavedTriggers._NumsSaveds[numSaved].SaveTrig[i] = 1;
			}
		}
		_SavedTriggers.SaveNums (numSaved);
		ButtonsSaved[numSaved].text = "Saved: "+PlayerPrefs.GetString ("TimeSave"+System.Convert.ToString(numSaved));
	}
}
