using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TrainMultiPlayerManager : MonoBehaviour
{

//	private static TrainMultiPlayerManager _instance = null;

//	public static TrainMultiPlayerManager Instance {		
//		get {			
//			if (_instance == null) {
//				_instance = GameObject.FindObjectOfType (typeof(TrainMultiPlayerManager)) as TrainMultiPlayerManager; 	
//			}			
//			return _instance; 
//		}
//		set {			
//			_instance = value;			
//		}	
//	}

//	[SerializeField] GameObject[] _DiffMultiPlayerTrains;

//	#if CUSTOM_CLIENT
//	public static bool IsMasterClient;
//	private bool isStateInitialized;
//	#endif

//	void Awake ()
//	{
//		#if CUSTOM_CLIENT
//		IsMasterClient = false;
//		isStateInitialized = false;
//		#endif
//		//OnJoinedRoom ();
//	}

//	void Start ()
//	{
		
//		//Invoke ("InitAI", 1);
//	}


//	int InstantiateType = 0;

//	public GameObject newPlayerObject;
//	public GameObject newPlayerObject2;
//	[Space (5)]
//	[SerializeField] Transform Player;
//	[SerializeField] Transform Opponent;
//	[Space (5)]
//	[SerializeField] CameraSmoothFollowWithRotation _smoothFollow;
//	[SerializeField] GamePlayManager _GamePlayManager;

//	public void RequestToSearching ()
//	{
//		Debug.Log ("-->> RequestToSearching ");
//		InvokeRepeating ("searchingProgress", 1, 0.5f);
//		searchingTxt.text = "Searching";
//	}

//	bool isMasterOnRoom;

//	void OnJoinedRoom ()
//	{			
	

//		//GlobalVariables.mGameState = eGAME_STATE.searchMultiplay;
		
//		#if CUSTOM_CLIENT

//		if (isStateInitialized) {
//			return;
//		}
		
//		Debug.Log ("---->>> MasterClient :" + IsMasterClient);
////		if (PhotonNetwork.room.playerCount < 2) {
////			return;
////		}
//		#else
//	//	Debug.Log ("MasterClient :" + PhotonNetwork.isMasterClient);
//		#endif

//		switch (InstantiateType) {
//		case 0:

//			#if CUSTOM_CLIENT

//		//	Debug.Log("-- TotalPlayers"+PhotonNetwork.room.playerCount);
//			if (IsMasterClient) {
//				#else
//			//if (PhotonNetwork.isMasterClient) {
//			//	#endif
//			////	newPlayerObject = PhotonNetwork.Instantiate (_DiffMultiPlayerTrains [GlobalVariables.i_CurrentTrainSelected].name, Player.transform.position, Quaternion.identity, 0);
//			//	newPlayerObject.name = "Player";
//			//	newPlayerObject.tag = "TrainDriver";

//			//} else {
//			//	GlobalVariables.i_CurrentTrainOppSelected = GlobalVariables.i_CurrentTrainSelected;
//			////	newPlayerObject2 = PhotonNetwork.Instantiate (_DiffMultiPlayerTrains [GlobalVariables.i_CurrentTrainOppSelected].name, Opponent.transform.position, Quaternion.identity, 0);
//			//	newPlayerObject2.name = "Opponent";
//			//	newPlayerObject2.tag = "TrainDriver";
//			//	int themesIndex = Random.Range (0, 5);
//			////	OnNetworkReceiver.Instance.RequestToMethod (themesIndex);
//			//}

//			InitialState ();
//			break;

//		case 1:
//		//	PhotonNetwork.InstantiateSceneObject (_DiffMultiPlayerTrains [GlobalVariables.i_CurrentTrainSelected].name, InputToEvent.inputHitPos + new Vector3 (0, 5f, 0), Quaternion.identity, 0, null);
//			break;
//		}
//	}


//	//public Vector3 getAvailPos(){
//	//	//Vector3 pos;
//	////	if (IsMasterClient) {
//	////		pos = Opponent.transform.position;
//	////	}else{
//	////		pos = Player.transform.position;
//	////	}
//	////	return pos;
//	//}


//	[Space (5)]
//	[SerializeField] Text searchingTxt;

//	IEnumerator ieRequestToLoadScene (string sceneName, int mIndex)
//	{

//	//	PhotonNetwork.Disconnect ();
//		yield return new WaitForSeconds (0);
//		GlobalVariables.iMenuEnableIndex = mIndex;
//		Application.LoadLevel (sceneName);
//	}

//	public void RequestToDisconnect ()
//	{
//		StartCoroutine (ieRequestToLoadScene ("GameUI", 3));
//	}

//	int searchingTime = 30;

//	void searchingProgress ()
//	{		
//		//if (isMasterOnRoom) {
//		//if (PhotonNetwork.connected && PhotonNetwork.room != null && PhotonNetwork.room.playerCount >= 2) {
//		//	CancelInvoke ("searchingProgress");
//		//	return;
//		//}

//		searchingTime--;
//		//Debug.Log ("--searchingTime... "+searchingTime);
//		if (searchingTime < 0) {
//			//RequestToDisconnect ();
//			searchingTime = 10;
//			Debug.Log ("--Cancel searching... Introduce Ai Here..");
//		//	InitAI ();
//			CancelInvoke ("searchingProgress");
//		}
//		//}
//		searchingTxt.text = searchingTxt.text + ".";

//		if (searchingTxt.text.Length > 12) {
//			searchingTxt.text = "Searching";
//		}
//	}

//	void InitialState ()
//	{
		
//		//#if CUSTOM_CLIENT
//		//if (IsMasterClient) {
//		//	#else
//		//if (PhotonNetwork.isMasterClient) {
//		//	#endif

//		//	_smoothFollow.target = newPlayerObject.transform;
//		//	if (PointerHandler.Instance) {
//		//		PointerHandler.Instance._player = newPlayerObject;
//		//	}
//		//	isMasterOnRoom = true;
//		//} else {
			
//		//	_smoothFollow.target = newPlayerObject2.transform;
//		//	if (PointerHandler.Instance) {
//		//		PointerHandler.Instance._player = newPlayerObject2;
//		//	}
//		//}
//		//isStateInitialized = true;
//	}

//	void InitialAIState ()
//	{
//		_smoothFollow.target = newPlayerObject.transform;
//		if (PointerHandler.Instance) {
//			PointerHandler.Instance._player = newPlayerObject;
//			PointerHandler.Instance._opponent = newPlayerObject2;
//		}
//	}

//	public void RequestToStartRace ()
//	{
		
//		Invoke ("RequestToStartGame", 1.0f);
//	}

//	void RequestToStartGame ()
//	{
//		Debug.Log ("RequestToStartGame");

//		CancelInvoke ("searchingProgress");

//		#if CUSTOM_CLIENT
//		if (IsMasterClient) {
//			#else
//		//if (PhotonNetwork.isMasterClient) {
//		//	#endif

//		//	newPlayerObject.GetComponent<TrainMovementScript> ().enabled = true;
//		//	if (isAIEnabled) {
//		//		newPlayerObject2.GetComponent<TrainMovementScript> ().enabled = true;
//		//	}

//		//	CameraSmoothFollowWithRotation.Instance.cameraPositions = newPlayerObject.GetComponent<TrainMovementScript> ().cameraPositions;
//		//	_smoothFollow.enabled = true;
//		//	_GamePlayManager.gameObject.SetActive (true);
//		//	_GamePlayManager._trainMovementScript = newPlayerObject.GetComponent<TrainMovementScript> ();
//		//} else {

//		//	newPlayerObject2.GetComponent<TrainMovementScript> ().enabled = true;
//		//	if (isAIEnabled) {
//		//		newPlayerObject.GetComponent<TrainMovementScript> ().enabled = true;
//		//	}
//		//	CameraSmoothFollowWithRotation.Instance.cameraPositions = newPlayerObject2.GetComponent<TrainMovementScript> ().cameraPositions;
//		//	_smoothFollow.enabled = true;
//		//	_GamePlayManager.gameObject.SetActive (true);
//		//	_GamePlayManager._trainMovementScript = newPlayerObject2.GetComponent<TrainMovementScript> ();
//		//}
//		//PointerHandler.Instance.pointerPlayer.sprite = PointerHandler.Instance.pointerTextures [0];
//		//PointerHandler.Instance.StartToProcess ();
//	}

//	#region MultiplayerAI

//	[HideInInspector] public float[] inteligenceSpeeds = new float[]{ 0.5f, 0.7f, 1, 1, 1 };
//	[HideInInspector] public int inteligenceLevel = 0;
//	public Text infoTxt;


//	[HideInInspector]public bool isAIEnabled = false;

//	//void InitAI ()
//	//{
//	//	inteligenceSpeeds = new float[]{ 0.5f, 0.7f, 1, 1, 1};

//	//	GlobalVariables.isMultiPlayerMode = true;
//	//	TrainMultiPlayerManager.IsMasterClient = true;
//	//	//if (GlobalVariables.isMultiPlayerMode) {
//	//	//	PhotonNetwork.LeaveRoom ();
//	//	//	if (PhotonNetwork.connected)
//	//	//		PhotonNetwork.Disconnect ();
//	//	//}
//	//	inteligenceLevel = Random.Range (1, 5);
//	//	Debug.Log ("--**** AI player inteligenceLvl::" + inteligenceLevel);
//	//	_GamePlayManager.f_DiffInSpeed_AI = inteligenceSpeeds [inteligenceLevel];
//	//	newPlayerObject2 = Instantiate (_DiffMultiPlayerTrains [Random.Range (0, _DiffMultiPlayerTrains.Length)], Opponent.transform.position, Quaternion.identity) as GameObject;
//	//	newPlayerObject2.name = "AIPlayer";
//	//	newPlayerObject2.tag = "TrainDriver";
//	//	newPlayerObject2.GetComponent<TrainCollisionMultiPlayerHandler> ().isAI = true;
//	//	isAIEnabled = true;
//	//	//_smoothFollow.target = newPlayerObject2.transform;
//	//	//OnNetworkReceiver network = newPlayerObject2.GetComponent<OnNetworkReceiver> ();
//	////	Destroy (network);
//	//	//PhotonView pw = newPlayerObject2.GetComponent<PhotonView> ();
//	////	Destroy (pw);


//	//	newPlayerObject = Instantiate (_DiffMultiPlayerTrains [GlobalVariables.i_CurrentTrainSelected], Player.transform.position, Quaternion.identity) as GameObject;
//	//	newPlayerObject.name = "Player";
//	//	newPlayerObject.tag = "TrainDriver";
//	//	newPlayerObject.GetComponent<TrainCollisionMultiPlayerHandler> ().isAI = false;
//	//	_smoothFollow.target = newPlayerObject.transform;
//	////	network = newPlayerObject.GetComponent<OnNetworkReceiver> ();
//	//	//Destroy (network);
//	//	//pw = newPlayerObject.GetComponent<PhotonView> ();
//	//	//Destroy (pw);

//	//	int themeIndex = Random.Range (0, 5);
//	//	ThemesPathHandler.Instance.RequestToSetTheme (themeIndex);

//	//	InitialAIState ();

//	//	//Invoke ("ShowAiInfo", 3);
//	//}

//	void ShowAiInfo(){
//		Text txt = Instantiate (_GamePlayManager._gSpeed) as Text;
//		txt.transform.SetParent (_GamePlayManager._gSpeed.transform.parent);
//		txt.transform.localPosition = Vector3.zero;//txt.alignment = TextAnchor.MiddleCenter;
//		txt.name = "AiText";
//		txt.text = "IsAI"+"\n("+inteligenceLevel+")";
//		txt.color = Color.yellow;
//		txt.transform.localScale = Vector3.one;
//	}


//	#endregion

}


