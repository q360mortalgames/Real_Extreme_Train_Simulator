
using UnityEngine;
using System.Collections;

public class UIHandler : MonoBehaviour {

	private static UIHandler _instance = null;
	public static UIHandler Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(UIHandler)) as UIHandler; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	[Space(10)]
	public UIObject[] _UIObject;
	[Space(10)]
	[SerializeField] GameObject bgUI;
	[SerializeField] Camera rendererCamera;

	[HideInInspector]
	public int currentPage;
	[HideInInspector]
	public int previousPage;
	[Space(10)]
	public float navigationTime;
    
	public void RequestToEnableObject(int _currentPageID)
	{
		previousPage = currentPage;
		currentPage = _currentPageID;

		foreach (UIObject uiObject in _UIObject) {

			if (uiObject.ID == currentPage) {
				uiObject._object.SetActive (true);
			} else {
				uiObject._object.SetActive (false);
			}
		}

		bgUI.GetComponent<MeshRenderer> ().enabled =  (currentPage == 0?false:true);
		rendererCamera.enabled = (currentPage == 0?true:false);
        







        
	}

	[Space(10)]
	[SerializeField] EnvironmentInfo[] _environmentInfo;

	void Start () {

		RequestToEnableObject (GlobalVariables.iMenuEnableIndex);

		int randomGeneration = Random.Range (0, 100);

//		RequestToEnvironmentEnable (randomGeneration < 50 ? 0 : 1);

		//TimuzMoreGames.GamePage="notinthegame";
        
	}

	void RequestToEnvironmentEnable(int eIndex){

		_environmentInfo [eIndex].environmentObject.SetActive (true);
		_environmentInfo [eIndex].particleEffect.SetActive (true);
		RenderSettings.skybox = _environmentInfo [eIndex].skyBox;
        
	}

}
