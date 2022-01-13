using UnityEngine;
using System.Collections;

public class Cursors : MonoBehaviour {
	public OnMenu _OnMenu;
	// Use this for initialization
	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if(_OnMenu.MenuCanvas.activeSelf == false)
			{
				Cursor.visible = true;
			}
			if(_OnMenu.MenuCanvas.activeSelf == true)
			{
				Cursor.visible = false;
			}
		}
	}
}
