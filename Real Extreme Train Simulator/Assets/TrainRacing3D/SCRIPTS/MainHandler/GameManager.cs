using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance = null;

	public static GameManager Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(GameManager)) as GameManager; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	public delegate void CurrentState ();

	[HideInInspector]
	public bool isMusicPause;
	[HideInInspector]
	public bool isSoundPause;
	[HideInInspector]
	public bool isCharaterAnimationEnable;
	[HideInInspector]
	public bool isMultiPlayerRaceStart;

	[HideInInspector]
	public int totalCoins;

	public int[] Prize;

	[HideInInspector]
	public int stationCount;
	[HideInInspector]
	public int distance;
	[HideInInspector]
	public int unlockTrain;

	[HideInInspector]
	public int themesIndex;
	[HideInInspector]
	public int pathIndex;
	[HideInInspector]
	public int iShareRateCounter;
	[HideInInspector]
	public int evenCount;
	[HideInInspector]
	public int oddCount;

	Transform myTransform;

	void Awake ()
	{
		
		if (_instance != null && _instance != this) {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
			_instance = this;
		}

		myTransform = transform;
		RegisterGameStates ();
		GetFromPlayerPrefs ();	

		Screen.sleepTimeout = SleepTimeout.NeverSleep;	
	}

	void SetToPlayerPrefs ()
	{		
		if (PlayerPrefs.GetInt ("PlayerPref") == 0) {

			PlayerPrefs.SetInt ("TutorialComplete", 0); 
			PlayerPrefs.SetInt ("totalCoins", 5000);
			PlayerPrefs.SetInt ("iShareRateCounter", 0);
			PlayerPrefs.SetInt ("stationCount", 0);
			PlayerPrefs.SetInt ("distance", 0);
			PlayerPrefs.SetInt ("unlockTrain", 0);

			PlayerPrefs.SetInt ("PlayerPref", -1);

		}
	}

	void GetFromPlayerPrefs ()
	{				
		SetToPlayerPrefs ();

		if (!PlayerPrefs.HasKey ("TRAINS")) {	
			SetInitialValueToPlayerPref ();
		} else {
			GetCurrentValueFromDB ();
		}

		totalCoins = PlayerPrefs.GetInt ("totalCoins");
		stationCount = PlayerPrefs.GetInt ("stationCount");
		iShareRateCounter = PlayerPrefs.GetInt ("iShareRateCounter");
		distance = PlayerPrefs.GetInt ("distance");
		unlockTrain = PlayerPrefs.GetInt ("unlockTrain");

		GameManager.Instance.isMultiPlayerRaceStart = false;

		GameStates.SetCurrentStateTO (GAME_STATE.MAIN_MENU);
	}

	void RegisterGameStates ()
	{		
		GameStates.RegisterStates (GAME_STATE.MAIN_MENU, MainMenu);				
		GameStates.RegisterStates (GAME_STATE.STORE_PAGE, StorePage);
		GameStates.RegisterStates (GAME_STATE.SETTINGS_PAGE, SettingPage);
		GameStates.RegisterStates (GAME_STATE.TRAIN_SELECTION, TrainSelectionPage);
		GameStates.RegisterStates (GAME_STATE.MODE_SELECTION, ModeSelectionPage);
	}

	void MainMenu ()
	{

	}

	void StorePage ()
	{
		
	}

	void SettingPage ()
	{

	}

	void TrainSelectionPage ()
	{
	
	}

	void ModeSelectionPage ()
	{
	
	}

	#region UpgradePurchase

	[HideInInspector]
	public List<string> TRAINS = new List<string> ();
	string allTrainIDs = "";

	void SetInitialValueToPlayerPref ()
	{	
		for (int i = 0; i < 7; i++) {
			if (i == 0) {
				TRAINS.Add ("1");
			} else {
				TRAINS.Add ("0");
			}
		} 


		string _currentTrainID = "";
		foreach (string id in TRAINS) {
			_currentTrainID += id + "#";
		}

		allTrainIDs = _currentTrainID;
		allTrainIDs = _currentTrainID.TrimEnd (new char[]{ '#' });	
		PlayerPrefs.SetString ("TRAINS", allTrainIDs);



	}

	string[] currentTrainIDs;

	void GetCurrentValueFromDB ()
	{
		TRAINS.Clear ();
		if (PlayerPrefs.HasKey ("TRAINS")) {
			
			allTrainIDs = PlayerPrefs.GetString ("TRAINS");
			currentTrainIDs = allTrainIDs.Split ('#');
			foreach (string id in currentTrainIDs) {
				TRAINS.Add (id);
			}
		}
	}

	void SetCurrentValueToDB (int pIndex, GAME_STATE _gState)
	{
		switch (_gState) {
		case GAME_STATE.TRAIN_SELECTION:
			
			TRAINS [pIndex] = "1";
			string _currentTrainID = "";
			foreach (string id in TRAINS) {
				_currentTrainID += id + "#";
			}

			allTrainIDs = _currentTrainID;
			allTrainIDs = allTrainIDs.TrimEnd (new char[]{ '#' });		
			PlayerPrefs.SetString ("TRAINS", allTrainIDs);

			if (TrainSelectionHandler.Instance) {
				TrainSelectionHandler.Instance.ResetData ();
			}

			// for Achivement
			if (pIndex == 1 || pIndex == 4) {
				if (GameManager.Instance) {
					
					GameManager.Instance.unlockTrain = GameManager.Instance.unlockTrain + 10;
					PlayerPrefs.SetInt ("unlockTrain", GameManager.Instance.unlockTrain);			 
				}
			}

			break;

		case GAME_STATE.MODE_SELECTION:
			
			break;
		}
	}

	public void RequestToBuyTrain (int buyCost, int _trainID, GAME_STATE _gState)
	{

		if (GameManager.Instance.totalCoins >= buyCost) {
			GameManager.Instance.totalCoins = GameManager.Instance.totalCoins - buyCost;
			PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);
			SetCurrentValueToDB (_trainID, _gState);
		} else {
			Debug.Log ("Sorry......... krishna");
//			UIHandler.Instance.RequestToEnableObject (2);
		}
	}

	IEnumerator ieRequestToLoadNextScene (float lTime)
	{
		Debug.Log ("Go To TrainSelectionPage");
		yield return new WaitForSeconds (lTime);
		Application.LoadLevel ("TrainSelectionPage");
	}

	[HideInInspector]
	public int multiplayerEntryFee;

	public void RequestTopayEntryFee (int buyCost, int mpVersion, int pindex)
	{

		if (GameManager.Instance.totalCoins >= buyCost) {
		//	OnConnectionHandler.Version = mpVersion;
			multiplayerEntryFee = buyCost;
			pathIndex = pindex;
			StartCoroutine (ieRequestToLoadNextScene (1.0f));
		} else {
			UIHandler.Instance.RequestToEnableObject (2);
		}
	}

	public void RequestToUnlockTrain(int _trainID, GAME_STATE _gState){
		SetCurrentValueToDB (_trainID, _gState);
	}

	#endregion

}

