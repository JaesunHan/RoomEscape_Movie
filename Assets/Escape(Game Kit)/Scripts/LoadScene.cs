using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {
	
	private AsyncOperation async = null;
	public bool isLoading = false;
	public string levelName = "";
	public Image lineBar;
	
	
	private IEnumerator _Start(){
		
		async = Application.LoadLevelAsync( levelName );
		while( !async.isDone ){
			
			yield return null;
		}
		
		isLoading = false;
		yield return async;
	}
	void Update () {
		if (isLoading == true) {
			lineBar.rectTransform.sizeDelta = new Vector2 (async.progress * 722, lineBar.rectTransform.sizeDelta.y);
		}
	}
}