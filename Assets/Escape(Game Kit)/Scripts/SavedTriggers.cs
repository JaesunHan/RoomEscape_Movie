using UnityEngine;
using System.Collections;

public class SavedTriggers : MonoBehaviour {

	public GameObject[] TriggerObjects;
	public string TagTrigger;
	[System.Serializable]
	public class NumsSaveds 
	{
		public int[] SaveTrig;
	}
	public NumsSaveds[] _NumsSaveds;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < _NumsSaveds.Length; i++) {
			for (int j = 0; j < _NumsSaveds[i].SaveTrig.Length; j++) {
				_NumsSaveds[i].SaveTrig[j] = PlayerPrefs.GetInt("NmN"+System.Convert.ToString(i)+System.Convert.ToString(j));
			}
		}

		int NmSav = PlayerPrefs.GetInt ("NmSav");
		if (PlayerPrefs.GetInt ("Load") == 1) {
			for (int i = 0; i < _NumsSaveds[NmSav].SaveTrig.Length; i++) {
				if(_NumsSaveds[NmSav].SaveTrig[i] == 1)
				{
					Destroy(TriggerObjects[i]);
				}
			}
		}
		for (int i = 0; i < _NumsSaveds.Length; i++) {
			for (int j = 0; j < _NumsSaveds[i].SaveTrig.Length; j++) {
				_NumsSaveds[i].SaveTrig[j] = 0;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void SaveNums (int nm) {
		for (int i = 0; i < _NumsSaveds[nm].SaveTrig.Length; i++) {
			PlayerPrefs.SetInt("NmN"+System.Convert.ToString(nm)+System.Convert.ToString(i), _NumsSaveds[nm].SaveTrig[i]);
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == TagTrigger) {
			//Destroy(other.gameObject);
		}
	}
}
