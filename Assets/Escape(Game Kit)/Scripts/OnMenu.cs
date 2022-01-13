using UnityEngine;
using System.Collections;

public class OnMenu : MonoBehaviour {
	public GameObject MenuCanvas;
	public Menu _Menu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			int sp = 0;
			if(MenuCanvas.activeSelf == true)
			{
				GameObject.FindWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
				MenuCanvas.SetActive(false);
				sp = 1;
			}
			if(sp == 0)
				if(MenuCanvas.activeSelf == false)
			{
				GameObject.FindWithTag("Player").GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
				_Menu.Obvod.rectTransform.anchoredPosition = new Vector2 (0, 0);
				MenuCanvas.SetActive(true);
				_Menu.OptionsParentObject.SetActive (false);
				_Menu.CreditsParentObject.SetActive (false);
				_Menu.SaveLoadParentObject.SetActive (false);
				_Menu.MenuParentObject.SetActive (true);
			}
		}
	}
}
