
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrainSelectionHandler : MonoBehaviour {
	
	private static TrainSelectionHandler _instance = null;
	public static TrainSelectionHandler Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(TrainSelectionHandler)) as TrainSelectionHandler; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	[SerializeField] TrainSelectionCamera _cameraScr;
	[SerializeField] SelectionTrainScript[] _selectionTrainScrs;
	[SerializeField] Image[] TEX_TRAIN_INFO;
	[SerializeField] int[] TrainCost;
	[HideInInspector]
	public bool mb_IsCameraRotationEnable	= false;
	[Space(10)]
	[SerializeField] Button BTN_LEFT_ARROW;
	[SerializeField] Button BTN_RIGHT_ARROW;
	[SerializeField] Button BTN_UNLOCK_FB;
	[SerializeField] Button BTN_BUY;
	[SerializeField] Button BTN_PLAY;
	[SerializeField] Button BTN_VideoUnlock;
	[SerializeField] Image TEX_LOCK;
	[SerializeField] Image BTN_ULOCK_ALL_TRAINS;

	[Space(10)]
	[SerializeField] Text _totalCoins;
	[SerializeField] Text _trainCost;

	int tempTotalCoins;
	void TotalCoinInfo(){

		if (GameManager.Instance && GameManager.Instance.totalCoins != tempTotalCoins) {
			tempTotalCoins = GameManager.Instance.totalCoins;
			_totalCoins.text = tempTotalCoins.ToString();
			PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);

		}
	}

	void Update(){

		TotalCoinInfo ();

//		if (GlobalVariables.isBackNavigation && Input.GetKeyUp (KeyCode.Escape)) {
//			GlobalVariables.iMenuEnableIndex = 3;
//			StartCoroutine (RequestToLoadNextScene (0f,"GameUI","GameUI"));
//		}
	}

	void OnEnable()
	{
		GlobalVariables.mCurrentScene	= eCURRENT_SCENE.TrainSelection;
		GlobalVariables.i_CurrentTrainSelected = 0;
		OnButtonClickForTrainChange ();
		selectedVideoType = 0;
	}

	void SetCameraToCurrentlySelectedTrain ()
	{
		mb_IsCameraRotationEnable	= false;
		_cameraScr.ChangeTrainCamera (_selectionTrainScrs [GlobalVariables.i_CurrentTrainSelected].TrainFrontViewCameraPos, _selectionTrainScrs [GlobalVariables.i_CurrentTrainSelected].TrainEngineTarget);
		ResetToOriginalPos ();






		foreach(Transform t in _selectionTrainScrs [GlobalVariables.i_CurrentTrainSelected].gameObject.GetComponentsInChildren<Transform>(true)) //include inactive
		{
			t.gameObject.layer = 15;
		}
	}

	void enableCameraTouched ()
	{
		mb_IsCameraRotationEnable	= true;
	}

	void OnButtonClickForTrainChange ()
	{
		ResetData ();

		mb_IsCameraRotationEnable = false;
		Invoke ("SetCameraToCurrentlySelectedTrain", 0f);
		Invoke ("enableCameraTouched", 1f);

		CheckForVideoAvailablity ();
	}

	IEnumerator ieRequestToLockTexture(){
		TEX_LOCK.gameObject.SetActive (false);
		yield return new WaitForSeconds (0.1f);
		TEX_LOCK.gameObject.SetActive (!isPlayBtnEnable);
	}

	bool IsUnlockAllTrains(){
		
		if (GameManager.Instance.TRAINS [2].Equals ("1") && GameManager.Instance.TRAINS [3].Equals ("1") && GameManager.Instance.TRAINS [4].Equals ("1") && GameManager.Instance.TRAINS [5].Equals ("1") && GameManager.Instance.TRAINS [6].Equals ("1")) {
			return false;
		}
		return true;
	}

	bool isPlayBtnEnable;
	public void ResetData()
	{
		if (GameManager.Instance) {
			_totalCoins.text = GameManager.Instance.totalCoins.ToString ();
		}

		BTN_ULOCK_ALL_TRAINS.gameObject.SetActive (IsUnlockAllTrains ());

		#region PLAY_BUY BTN Handle
		//Debug.Log ("tcount : "+GameManager.Instance.TRAINS.Count);

//		print("GlobalVariables.i_CurrentTrainSelected : "+GlobalVariables.i_CurrentTrainSelected);
		print("getting isplayen:"+GameManager.Instance.TRAINS [GlobalVariables.i_CurrentTrainSelected].Equals ("1"));
		//print("getting isplayen:"+GlobalVariables.i_CurrentTrainSelected);


		isPlayBtnEnable = (GameManager.Instance.TRAINS [GlobalVariables.i_CurrentTrainSelected].Equals ("1"));

		StartCoroutine(ieRequestToLockTexture());

		BTN_PLAY.gameObject.SetActive (isPlayBtnEnable);
		BTN_BUY.gameObject.SetActive (GlobalVariables.i_CurrentTrainSelected !=1?!isPlayBtnEnable:false);

		BTN_UNLOCK_FB.gameObject.SetActive (GlobalVariables.i_CurrentTrainSelected == 1 && GameManager.Instance.TRAINS [GlobalVariables.i_CurrentTrainSelected].Equals ("0")?true:false);

		_trainCost.text = TrainCost[GlobalVariables.i_CurrentTrainSelected].ToString();

		#endregion

		RequestToEnableObject (GlobalVariables.i_CurrentTrainSelected);
		BTN_LEFT_ARROW.gameObject.SetActive (GlobalVariables.i_CurrentTrainSelected < 1?false:true);
		BTN_RIGHT_ARROW.gameObject.SetActive (GlobalVariables.i_CurrentTrainSelected > _selectionTrainScrs.Length-2?false:true);
	}

	void RequestToEnableObject(int _currentID)
	{
		for (int i = 0; i < TEX_TRAIN_INFO.Length; i++) {
			if (i == _currentID) {
				TEX_TRAIN_INFO[i].gameObject.SetActive (true);
			} else {
				TEX_TRAIN_INFO[i].gameObject.SetActive (false);
			}
		}
	}

	void ResetToOriginalPos ()
	{
		if (GlobalVariables.i_CurrentTrainSelected > 0 && GlobalVariables.i_CurrentTrainSelected < (GlobalVariables.iTotalTrainsAvalaiable - 1)) {
			_selectionTrainScrs [GlobalVariables.i_CurrentTrainSelected - 1].ResetToOriginalPos ();
			_selectionTrainScrs [GlobalVariables.i_CurrentTrainSelected + 1].ResetToOriginalPos ();
		} else if (GlobalVariables.i_CurrentTrainSelected == 0)
			_selectionTrainScrs [GlobalVariables.i_CurrentTrainSelected + 1].ResetToOriginalPos ();
		else if (GlobalVariables.i_CurrentTrainSelected == (GlobalVariables.iTotalTrainsAvalaiable - 1))
			_selectionTrainScrs [GlobalVariables.i_CurrentTrainSelected - 1].ResetToOriginalPos ();
           

	}

	#region OnButtonAction
	public void OnClick(string _click)
	{
		StartCoroutine (ieOnClick (_click));
	}

	IEnumerator RequestToLoadNextScene (float lTime, string levelName, string nextLevelLoad)
	{
		GameManager.Instance.isCharaterAnimationEnable = true;
		GlobalVariables.sSceneToBeLoaded = nextLevelLoad;
		yield return new WaitForSeconds (lTime);
		Application.LoadLevel (levelName);
	}


	IEnumerator	ieOnClick(string _click){
		SoundController.Instance.OnButtonClick ();
		yield return new WaitForSeconds (0.25f);
		OnClickComplete (_click);
	}

	void LateUpgrade(){

		GameManager.Instance.RequestToBuyTrain (0, GlobalVariables.i_CurrentTrainSelected, GAME_STATE.TRAIN_SELECTION);


	}

	void OnClickComplete(string _click){
		switch(_click)
		{
		case "BTN_BUY":	
			
			Debug.Log ("BTN_BUY :" + GlobalVariables.i_CurrentTrainSelected);
			GameManager.Instance.RequestToBuyTrain (TrainCost [GlobalVariables.i_CurrentTrainSelected], GlobalVariables.i_CurrentTrainSelected, GAME_STATE.TRAIN_SELECTION);
            


			break;

		case "BTN_BACK":			
			GlobalVariables.iMenuEnableIndex = 0;
			StartCoroutine (RequestToLoadNextScene (0f,"GameUI","GameUI"));
			break;
			
		case "BTN_UNLOCK_ALL_TRAINS":
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToInApp (7);
			//}
			Debug.Log ("BTN_UNLOCK_ALL_TRAINS");
			break;

		case "BTN_LEFT_ARROW":
			if (GlobalVariables.i_CurrentTrainSelected > 0) {
				GlobalVariables.i_CurrentTrainSelected--;
			} 
			OnButtonClickForTrainChange ();


			if(GlobalVariables.i_CurrentTrainSelected!=7){
				foreach(Transform g in _selectionTrainScrs [GlobalVariables.i_CurrentTrainSelected+1].gameObject.GetComponentsInChildren<Transform>(true)) //include inactive
				{
					g.gameObject.layer = 8;
				}
			}

			break;

		case "BTN_RIGHT_ARROW":
			if (GlobalVariables.i_CurrentTrainSelected < _selectionTrainScrs.Length-1) {
				GlobalVariables.i_CurrentTrainSelected++;
			} 
			OnButtonClickForTrainChange ();

			if(GlobalVariables.i_CurrentTrainSelected!=0){
				foreach(Transform g in _selectionTrainScrs [GlobalVariables.i_CurrentTrainSelected-1].gameObject.GetComponentsInChildren<Transform>(true)) //include inactive
				{
					g.gameObject.layer = 8;
				}
			}
			break;



		case "BTN_UNLOCK_FB":
//			if (AdsManager.Instance) {
		//		AdsManager.Instance.RequestToFBShare ("Yes! In Train Racing 3D, I got S.F.Express!!! Play for free...");
//			}
			Debug.Log ("BTN_UNLOCK_FB");
			Invoke ("LateUpgrade", 1f);
			//GameConfigs2015.share ();

			break;

		case "BTN_PLAY":			
			StartCoroutine (RequestToLoadNextScene (0f,"LoadingPage",GlobalVariables.loadScene));
			break;

		case "TEX_COINS_PLUS":
			GlobalVariables.iMenuEnableIndex = 2;
			StartCoroutine (RequestToLoadNextScene (0f,"GameUI","GameUI"));
			break;

		case "BTN_Gift":
			
			break;
		}

	}
	#endregion



	#region VideoAd
	public static string videosKey = "videosWatched";
	int trainToUnlock = 3;
	void CheckForVideoAvailablity(){
		SetVideoCount ();
		BTN_VideoUnlock.gameObject.SetActive(false);
		//Debug.Log (BTN_VideoUnlock+"slected:: " + GlobalVariables.i_CurrentTrainSelected);
		if (BTN_VideoUnlock && GlobalVariables.i_CurrentTrainSelected == trainToUnlock && PlayerPrefs.GetInt (videosKey,5) >0) {
			BTN_VideoUnlock.gameObject.SetActive (true);
			Debug.Log ("Show Video Button with Count:: " + PlayerPrefs.GetInt (videosKey, 5));
		}
	}

	void SetVideoCount(){
		if(BTN_VideoUnlock.GetComponentInChildren<Text>())BTN_VideoUnlock.GetComponentInChildren<Text>().text = "Watch "+PlayerPrefs.GetInt (videosKey, 5)+" video's to unlock";
		//Debug.Log ("Videos To watch:: " + PlayerPrefs.GetInt (videosKey, 5));
	}

	public static int selectedVideoType = 0;
	public static int ingameVideo = 0;
	public void WatchVideo(int type){
		//watchSuccess ();
		selectedVideoType = type;
	//	GoogleMobileAdsDemoScript.mee.ShowRewardBasedVideo();
	}

	public string watchSuccess(){
		Debug.Log ("watchSuccess::");
		int val = PlayerPrefs.GetInt (videosKey,5);
		val -= 1;
		PlayerPrefs.SetInt(videosKey,val);
		SetVideoCount ();
		Debug.Log ("Videos To watch:: " + PlayerPrefs.GetInt (videosKey, 5));
		if (PlayerPrefs.GetInt (videosKey,5) <= 0) {
			GameManager.Instance.RequestToUnlockTrain (GlobalVariables.i_CurrentTrainSelected, GAME_STATE.TRAIN_SELECTION);
			BTN_VideoUnlock.gameObject.SetActive(false);
		}
		return PlayerPrefs.GetInt (videosKey, 5)+"";
	}
	#endregion

}
