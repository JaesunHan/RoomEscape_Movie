using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public Image Obvod, Obvod2;
	public LoadScene _LoadScene;
	public GameObject MenuParentObject, FoneLoad, LoadParentObject, OptionsParentObject, CreditsParentObject, SaveLoadParentObject;
	public Slider SliderSound, SliderGraph, SliderResolut;
	public Text TextTime;
	public AudioSource _AudioSource;
	public AudioClip UnderLineClip, ClickClip;
	public Text[] ButtonsSaved;
	// Use this for initialization
	void Start () {
		
		//int NmSav = PlayerPrefs.GetInt ("NmSav");
		for (int i = 0; i < ButtonsSaved.Length; i++) {
			if(PlayerPrefs.GetInt ("Saved"+System.Convert.ToString(i)) != 0)
			{
				ButtonsSaved[i].text = "Saved: "+PlayerPrefs.GetString ("TimeSave"+System.Convert.ToString(i));
			}
		}
		
		SliderSound.value = PlayerPrefs.GetFloat ("SliderSound");
		SliderGraph.value = PlayerPrefs.GetFloat ("SliderGraph");
		SliderResolut.value = PlayerPrefs.GetFloat ("SliderResolut");
		
		AudioListener.volume = PlayerPrefs.GetFloat ("SliderSound");
		QualitySettings.SetQualityLevel(System.Convert.ToInt32(PlayerPrefs.GetFloat ("SliderGraph")+1));
		if (PlayerPrefs.GetFloat ("SliderResolut") == 0) {
			Screen.SetResolution(800, 600, true);
		}
		if (PlayerPrefs.GetFloat ("SliderResolut") == 1) {
			Screen.SetResolution(1280, 720, true);
		}
		if (PlayerPrefs.GetFloat ("SliderResolut") == 2) {
			Screen.SetResolution(1600, 1900, true);
		}
		if (PlayerPrefs.GetFloat ("SliderResolut") == 3) {
			Screen.SetResolution(1920, 1080, true);
		}
	}
	
	// Update is called once per frame
	void Update () {
		TextTime.text = System.DateTime.Now.Hour + ":" + System.DateTime.Now.Minute;
	}
	
	public void OnLoadLevel () {
		_AudioSource.clip = ClickClip;
		_AudioSource.Play ();
		MenuParentObject.SetActive (false);
		SaveLoadParentObject.SetActive (false);
		CreditsParentObject.SetActive (false);
		OptionsParentObject.SetActive (false);
		_AudioSource.clip = ClickClip;
		_AudioSource.Play ();
		MenuParentObject.SetActive (false);
		LoadParentObject.SetActive (true);
		FoneLoad.SetActive (true);
		_LoadScene.isLoading = true;
		PlayerPrefs.SetInt ("Load", 0);
		_LoadScene.StartCoroutine("_Start");
	}
	public void OnOptions() {
		_AudioSource.clip = ClickClip;
		_AudioSource.Play ();
		MenuParentObject.SetActive (false);
		OptionsParentObject.SetActive (true);
	}
	public void OnCredits() {
		_AudioSource.clip = ClickClip;
		_AudioSource.Play ();
		MenuParentObject.SetActive (false);
		CreditsParentObject.SetActive (true);
	}
	public void OnExit () {
		_AudioSource.clip = ClickClip;
		_AudioSource.Play ();
		Application.Quit ();
	}
	public void OnExitCredits () {
		Obvod.rectTransform.anchoredPosition = new Vector2 (0, 0);
		_AudioSource.clip = ClickClip;
		_AudioSource.Play ();
		CreditsParentObject.SetActive (false);
		MenuParentObject.SetActive (true);
	}
	public void SaveOptions () {
		Obvod.rectTransform.anchoredPosition = new Vector2 (0, 0);
		PlayerPrefs.SetFloat ("SliderSound", SliderSound.value);
		PlayerPrefs.SetFloat ("SliderGraph", SliderGraph.value);
		PlayerPrefs.SetFloat ("SliderResolut", SliderResolut.value);
		AudioListener.volume = PlayerPrefs.GetFloat ("SliderSound");
		QualitySettings.SetQualityLevel(System.Convert.ToInt32(PlayerPrefs.GetFloat ("SliderGraph")+1));
		if (PlayerPrefs.GetFloat ("SliderResolut") == 0) {
			Screen.SetResolution(800, 600, true);
		}
		if (PlayerPrefs.GetFloat ("SliderResolut") == 1) {
			Screen.SetResolution(1280, 720, true);
		}
		if (PlayerPrefs.GetFloat ("SliderResolut") == 2) {
			Screen.SetResolution(1600, 1900, true);
		}
		if (PlayerPrefs.GetFloat ("SliderResolut") == 3) {
			Screen.SetResolution(1920, 1080, true);
		}
		_AudioSource.clip = ClickClip;
		_AudioSource.Play ();
		OptionsParentObject.SetActive (false);
		MenuParentObject.SetActive (true);
	}
	
	public void OnObvodka (Text _text) {
		Obvod.rectTransform.anchoredPosition = new Vector2 (_text.rectTransform.anchoredPosition.x, _text.rectTransform.anchoredPosition.y);
		_AudioSource.clip = UnderLineClip;
		_AudioSource.Play ();
	}
	public void OnObvodka2 (Text _text) {
		Obvod2.rectTransform.anchoredPosition = new Vector2 (_text.rectTransform.anchoredPosition.x, _text.rectTransform.anchoredPosition.y);
		_AudioSource.clip = UnderLineClip;
		_AudioSource.Play ();
	}
	public void OnSaveLoad () {
		_AudioSource.clip = ClickClip;
		_AudioSource.Play ();
		MenuParentObject.SetActive (false);
		SaveLoadParentObject.SetActive (true);
	}
	public void ExitSaveLoad () {
		_AudioSource.clip = ClickClip;
		_AudioSource.Play ();
		SaveLoadParentObject.SetActive (false);
		MenuParentObject.SetActive (true);
	}
	public void OnLoadGame (int num) {
		if (PlayerPrefs.GetInt ("Saved" + System.Convert.ToString (num)) != 0) {
			MenuParentObject.SetActive (false);
			SaveLoadParentObject.SetActive (false);
			CreditsParentObject.SetActive (false);
			OptionsParentObject.SetActive (false);
			PlayerPrefs.SetInt ("Load", 1);
			PlayerPrefs.SetInt ("NmSav", num);
			_AudioSource.clip = ClickClip;
			_AudioSource.Play ();
			MenuParentObject.SetActive (false);
			LoadParentObject.SetActive (true);
			FoneLoad.SetActive (true);
			_LoadScene.isLoading = true;
			
			_LoadScene.StartCoroutine ("_Start");
		}
	}
}
