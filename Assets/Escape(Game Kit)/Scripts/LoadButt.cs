using UnityEngine;
using System.Collections;

public class LoadButt : MonoBehaviour {
	public GameObject TextLoad;
	// Use this for initialization
	void Start () {
		
		if (PlayerPrefs.HasKey ("Sav") == true) {
			TextLoad.SetActive(true);
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
