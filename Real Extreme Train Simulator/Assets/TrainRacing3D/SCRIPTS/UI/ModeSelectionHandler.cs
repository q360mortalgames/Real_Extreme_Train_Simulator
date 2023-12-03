using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Net;

public class ModeSelectionHandler : MonoBehaviour
{
	private static ModeSelectionHandler _instance = null;
	public static ModeSelectionHandler Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(ModeSelectionHandler)) as ModeSelectionHandler; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	[Space(10)]
	[SerializeField] Text _totalCoins;
	[Space(10)]
	[SerializeField] Image btnMultiplayer;
	[SerializeField] Color active;
	[SerializeField] Color deactive;
	bool isConnection;

	int tempTotalCoins;
	void TotalCoinInfo(){

		if (GameManager.Instance && GameManager.Instance.totalCoins != tempTotalCoins) {
			tempTotalCoins = GameManager.Instance.totalCoins;
			_totalCoins.text = ""+tempTotalCoins.ToString();
			PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);
		}


	}

//	void Update () {
//
//		if (GlobalVariables.isBackNavigation && Input.GetKeyUp (KeyCode.Escape)) {
//			//UIHandler.Instance.RequestToEnableObject (0);
//		}
//	}

	void OnEnable()
	{
		TotalCoinInfo ();
	}

	#region OnButtonAction
	public void OnClick (string _click)
	{
		StartCoroutine (ieOnClick (_click));
	}

	IEnumerator	ieOnClick(string _click){
		SoundController.Instance.OnButtonClick ();
		yield return new WaitForSeconds (UIHandler.Instance.navigationTime);
		OnClickComplete (_click);
	}

	IEnumerator RequestToLoadNextScene (float lTime)
	{
		yield return new WaitForSeconds (lTime);
		Application.LoadLevel ("TrainSelectionPage");
	}

	void OnClickComplete (string _click)
	{
		switch (_click) {

		case "BTN_BACK":
			UIHandler.Instance.RequestToEnableObject (0);
			break;



		case "BTN_Levels":

			Debug.Log ("BTN_Levels");

			GlobalVariables.loadScene = "Game_levels";
			GlobalVariables.isMultiPlayerMode = false;
			levelManager.LevelMode = true;
			UIHandler.Instance.RequestToEnableObject (5);
			//StartCoroutine (RequestToLoadNextScene (0.3f));
			break;


		case "BTN_LevelsStart":
			Debug.Log ("BTN_LevelsStart");

			GlobalVariables.loadScene = "Game_levels";
			GlobalVariables.isMultiPlayerMode = false;
			levelManager.LevelMode = true;

			StartCoroutine (RequestToLoadNextScene (0.3f));

			break;

		case "BTN_ENDLESS":
			
			Debug.Log ("BTN_ENDLESS");

			GlobalVariables.loadScene = "FreeMode-Unlimited";
			levelManager.LevelMode = false;
			GlobalVariables.isMultiPlayerMode = false;
			StartCoroutine (RequestToLoadNextScene (0.3f));
			break;



		case "BTN_MULTIPLAYER":
			
			Debug.Log ("BTN_MULTIPLAYER");	

			GlobalVariables.loadScene = "MultiPlayerMode";
			GlobalVariables.isMultiPlayerMode = true;
			levelManager.LevelMode = false;
			UIHandler.Instance.RequestToEnableObject (4);
			break;	

		case "TEX_COINS_PLUS":
			UIHandler.Instance.RequestToEnableObject (2);
			break;	

		case "BTN_GIFT":
			Debug.Log("BTN_GIFT");
			UIHandler.Instance.RequestToEnableObject(6);
			FreeCoinsPage.atPage = 3;
			break;
		}
	}
	#endregion

	#region HasConnection
	bool HasConnection()
	{
		try
		{
			using (var client = new WebClient())
			using (var stream = new WebClient().OpenRead("http://www.google.com"))
			{
				return true;
			}
		}
		catch
		{
			return false;
		}
	}

	/*

	isConnection = HasConnection ();
	btnMultiplayer.color = isConnection ? active : deactive;
		
	 if (isConnection) {
				GlobalVariables.loadScene = "MultiPlayerMode";
				GlobalVariables.isMultiPlayerMode = true;
				UIHandler.Instance.RequestToEnableObject (4);
			} else {
				Debug.Log("Please Check Connection");
			}

	*/
	#endregion
}