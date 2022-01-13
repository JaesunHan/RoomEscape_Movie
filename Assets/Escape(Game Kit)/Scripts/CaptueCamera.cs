using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class CaptueCamera : MonoBehaviour {
	public Animation CamAnimation;
	public Camera _Camera;
	public AnimationClip animClipUp, animClipDown;
	private int UpDown = 0, canv = 0, yescam = 0, vsrich = 0, prosmotr = 0;
	public int NumI;
	public GameObject CameraObject, canvasObject, canvasPhotoObject, SpotLight;
	public string NameAnimUpCam;
	public float SpeedField;
	public Image ImageLineWT, ImageShowPhoto, IconFlash, IconPhoto, ImageBorder;
	public Sprite[] sprites;
	private int NumBut = 1;
	public AudioSource _AudioSource, _AudioSourceZoom;
	public AudioClip clipCameraUpDown, clipClipShot, clipCamBut, clipCamZoom;
	public int stZ, stZ2;
	public CharacterController _CharacterController;
	private int CountScreen;
	
	// Use this for initialization
	void  Start () { //PlayerPrefs.SetInt ("CountScreen", 0);
		CountScreen = PlayerPrefs.GetInt ("CountScreen");
		
		
		//var bytes = tex.EncodeToPNG ();
		for (int i = 0; i < CountScreen; i ++) {
			var path = System.IO.Path.Combine (Application.persistentDataPath, "IMG_" + i.ToString () + ".png");
			var bytes = System.IO.File.ReadAllBytes (path);
			if(bytes != null)
			{
				Texture2D tex = new Texture2D (Screen.width, Screen.height);
				tex.LoadImage (bytes);
				tex.Apply ();
				sprites [i] = Sprite.Create (tex, new Rect (0, 0, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
				Debug.Log (tex);
				tex = null;
			}
			
		}
		
	}
	
	
	void LateUpdate () {
		if (Input.GetKey (KeyCode.LeftControl)) {
			if (_CharacterController.height > 1) {
				_CharacterController.height -= 0.8f * Time.deltaTime;
			}
		} else {
			_CharacterController.height = 1.8f;	
		}
		ImageLineWT.rectTransform.sizeDelta = new Vector2 (((_Camera.fieldOfView - 60)*-1)*6.66f, ImageLineWT.rectTransform.sizeDelta.y);
		if (Input.GetMouseButton (1)) {
			if(yescam == 1)
			{
				stZ2 = 0;
				if (_Camera.fieldOfView > 30) {
					_Camera.fieldOfView -= SpeedField * Time.deltaTime;
					
					if(stZ == 0)
					{
						_AudioSourceZoom.Stop ();
						_AudioSourceZoom.clip = clipCamZoom;
						_AudioSourceZoom.Play ();
					}
					stZ = 1;
					
				}
				else
				{
					stZ = 0;
					_AudioSourceZoom.Stop ();
					
				}
			}
		} else {
			stZ = 0;
			if (_Camera.fieldOfView < 60) {
				_Camera.fieldOfView += SpeedField * Time.deltaTime;
				
				if(stZ2 == 0)
				{
					
					_AudioSourceZoom.Stop ();
					_AudioSourceZoom.clip = clipCamZoom;
					_AudioSourceZoom.Play ();
				}
				stZ2 = 1;
				
				
			}
			else
			{
				_AudioSourceZoom.Stop ();
				stZ2 = 0;
				
			}
			
		}
	}
	// Update is called once per frame
	void Update () {
		if (prosmotr == 1) {
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				for (int i = NumI+1; i < sprites.Length; i++) {
					if (sprites [i] != null) {
						//_AudioSource.clip = clipCamBut;
						//_AudioSource.Play ();
						ImageShowPhoto.sprite = sprites [i];
						if (i != sprites.Length) {
							NumI = i;
						}
						i = sprites.Length;
					}
				}
			}
			if (Input.GetKeyDown (KeyCode.LeftArrow)) {
				for (int i = NumI-1; i > -1; i--) {
					if (sprites [i] != null) {
						//	_AudioSource.clip = clipCamBut;
						//_AudioSource.Play ();
						ImageShowPhoto.sprite = sprites [i];
						if (i == 0) {
							NumI = i;
						}
						if (i > 0) {
							NumI = i;
						}
						i = 0;
					}
				}
			}
			
			if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.DownArrow)) {
				canvasPhotoObject.SetActive (false);
				canvasObject.SetActive (true);
				prosmotr = 0;
			}
			
		}
		if (Input.GetKeyDown (KeyCode.Return)) {
			_AudioSource.clip = clipCamBut;
			_AudioSource.Play ();
			if (NumBut == 0) {
				vsrich ++;
				if (vsrich > 1) {
					vsrich = 0;
				}
				if (vsrich == 0) {
					IconFlash.color = new Color (1, 1, 1, 0.3f);
				}
				if (vsrich == 1) {
					IconFlash.color = new Color (1, 1, 1, 1f);
				}
			}
			
			if (NumBut == 1) {
				canvasObject.SetActive (false);
				for (int i = NumI; i < sprites.Length; i++) {
					if (sprites [i] != null) {
						ImageShowPhoto.sprite = sprites [i];
						if (i != sprites.Length) {
							NumI = i;
						}
						i = sprites.Length;
					}
				}
				canvasPhotoObject.SetActive (true);
				prosmotr = 1;
			}
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			_AudioSource.clip = clipCamBut;
			_AudioSource.Play ();
			NumBut++;
			if (NumBut > 1) {
				NumBut = 0;
			}
			
			if (NumBut == 0) {
				ImageBorder.rectTransform.anchoredPosition = IconFlash.rectTransform.anchoredPosition;
			}
			if (NumBut == 1) {
				ImageBorder.rectTransform.anchoredPosition = IconPhoto.rectTransform.anchoredPosition;
			}
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			_AudioSource.clip = clipCamBut;
			_AudioSource.Play ();
			NumBut--;
			if (NumBut < 0) {
				NumBut = 1;
			}
			
			if (NumBut == 0) {
				ImageBorder.rectTransform.anchoredPosition = IconFlash.rectTransform.anchoredPosition;
			}
			if (NumBut == 1) {
				ImageBorder.rectTransform.anchoredPosition = IconPhoto.rectTransform.anchoredPosition;
			}
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			if (yescam == 1) {
				canvasObject.SetActive (false);
				if (vsrich == 1) {
					SpotLight.SetActive (true);
				}
				_AudioSource.clip = clipClipShot;
				_AudioSource.Play ();
				
				StartCoroutine ("TakeScrShot");
			}
		}
		if (!CamAnimation.isPlaying) {
			CameraObject.SetActive (false);
		}
		if (canv == 1) {
			if (!CamAnimation.IsPlaying (NameAnimUpCam)) {
				canvasObject.SetActive (true);
				canv = 0;
				yescam = 1;
			}
		}
		if(prosmotr != 1)
		if (Input.GetKeyDown (KeyCode.E)) {
			if (!CamAnimation.isPlaying) {
				_AudioSource.clip = clipCameraUpDown;
				_AudioSource.Play ();
				if (UpDown == 1) {
					CameraObject.SetActive (true);
					canvasObject.SetActive (false);
					yescam = 0;
					_Camera.fieldOfView = 60;
					CamAnimation.clip = animClipDown;
					_Camera.fieldOfView = 60;
				}
				if (UpDown == 0) {
					CameraObject.SetActive (true);
					canv = 1;
					CamAnimation.clip = animClipUp;
				}
				CamAnimation.Play ();
				UpDown++;
				if (UpDown > 1) {
					UpDown = 0;
				}
			}
		}
	}
	
	private IEnumerator TakeScrShot()
	{
		yield return new WaitForEndOfFrame ();
		
		
		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D (width, height, TextureFormat.RGB24, false);
		
		tex.ReadPixels (new Rect (0, 0, width, height), 0, 0);
		tex.Apply ();
		
		var bytes = tex.EncodeToPNG ();
		
		var path = System.IO.Path.Combine(Application.persistentDataPath, "IMG_"+CountScreen.ToString() + ".png");
		
		System.IO.File.WriteAllBytes (path, bytes);
		
		Debug.Log(path);
		CountScreen ++;
		
		PlayerPrefs.SetInt ("CountScreen", CountScreen);
		canvasObject.SetActive (true);
		SpotLight.SetActive (false);
		for (int i = 0; i < sprites.Length; i++) {
			if (sprites [i] == null) {
				sprites [i] = Sprite.Create (tex, new Rect (0, 0, tex.width, tex.height), new Vector2 (0.5f, 0.5f));
				i = sprites.Length;
			}
		}
	}
}
