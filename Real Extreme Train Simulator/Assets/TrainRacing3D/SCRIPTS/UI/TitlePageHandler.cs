using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitlePageHandler : MonoBehaviour
{
	private static TitlePageHandler _instance = null;
	public static TitlePageHandler Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(TitlePageHandler)) as TitlePageHandler; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	[SerializeField] float loadTime;
	[SerializeField] string sceneName;


	void Start()
	{
		SetToInitialValue ();
	}

	IEnumerator RequestToLoadNextScene(float lTime){
		
		yield return new WaitForSeconds (lTime);
		Application.LoadLevel (sceneName);
	}


	void SetToInitialValue(){
		StartCoroutine(RequestToLoadNextScene(loadTime));
	}


}
