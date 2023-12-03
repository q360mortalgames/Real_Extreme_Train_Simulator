using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuPageHandler : MonoBehaviour
{
	private static MenuPageHandler _instance = null;
	public static int FreeCoins = 500;

	public static MenuPageHandler Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(MenuPageHandler)) as MenuPageHandler; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	void Start ()
	{
        //if (AdsManager.Instance) {
        //	AdsManager.Instance.RequestToShowAds (ADS_STATE.MENU_PAGE, 0);
        //}

        //if (AdsManager.Instance) {
        //	AdsManager.Instance.RequestToAuthenticatePlayer ();
        //}
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

    }

	[SerializeField] Image GSignInOut;
	[SerializeField] Sprite[] GOOGLE;
	private int CoinsToAdd = 500;
	void Update ()
	{

//		if (GlobalVariables.isBackNavigation && Input.GetKeyUp (KeyCode.Escape)) {
//			RequestToExitPopUps ();
//		}
		//#if UNITY_ANDROID
		//GSignInOut.sprite = (PlayGameServices.isSignedIn() ? GOOGLE [1] : GOOGLE [0]);
		//#endif
	}



	#region OnButtonAction

	public void OnClick (string _click)
	{
		StartCoroutine (ieOnClick (_click));
	}

	IEnumerator	ieOnClick (string _click)
	{
		SoundController.Instance.OnButtonClick ();
		yield return new WaitForSeconds (UIHandler.Instance.navigationTime);
		OnClickComplete (_click);
	}

	public void freecoins()
    {
		Debug.Log("free");
		//GameManager.Instance.totalCoins = GameManager.Instance.totalCoins + CoinsToAdd;
		PlayerPrefs.SetInt("totalCoins", GameManager.Instance.totalCoins+CoinsToAdd);

	}
	void OnButtonActiveEffect (string _btnName)
	{
	
		if (!GameObject.Find (_btnName).GetComponent<iTween> ()) {
			iTween.PunchScale (GameObject.Find (_btnName).gameObject, iTween.Hash ("x", 0.25, "y", 0.25, "time", 0.55f));
		}
	}

	void OnRemoveiTween (GameObject _object)
	{
		if (_object.GetComponent<iTween> ())
			Destroy (_object.GetComponent<iTween> ());
	}

	void OnClickComplete (string _click)
	{
		switch (_click) {

		case "BTN_MORE_GAMES":
			Debug.Log ("BTN_MORE_GAMES");
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToMoreGames ();
			//}
			break;

		case "BTN_SETTING":
			Debug.Log ("BTN_SETTING");
			UIHandler.Instance.RequestToEnableObject (1);
			break;

		case "BTN_GIFT":
			Debug.Log ("BTN_GIFT");
			UIHandler.Instance.RequestToEnableObject (6);
			FreeCoinsPage.atPage = 0;
            break;

		case "BTN_ACHIEVEMENT":
			Debug.Log ("BTN_ACHIEVEMENT");
			//if (AdsManager.Instance) {

			//	ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.ShowAchievement);

			//	AdsManager.Instance.RequestToShowAchievements ();


			//}
			break;

//		case "BTN_FACEBOOK":
//			if (AdsManager.Instance) {
//				AdsManager.Instance.RequestToFBShare ();
//			}
//			Debug.Log ("BTN_FACEBOOK");
//			break;

		case "BTN_LEADERBOARD":
			Debug.Log ("BTN_LEADERBOARD");
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToShowLeaderBoard ();
			//}
			break;

		case "BTN_RATEUS":
			Debug.Log ("BTN_RATEUS");
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToRateMe ();
			//}
			break;

		case "BTN_SIGN_IN":
			Debug.Log ("BTN_SIGN_IN");
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToSignInOut ();
			//}
			break;

		case "BTN_PLAY":
			Debug.Log ("BTN_PLAY");
			if (PlayerPrefs.GetInt ("TutorialComplete") == 0) {
				StartCoroutine (RequestToLoadNextScene (0f, "LoadingPage", "TutorialMode"));
			} else {
				UIHandler.Instance.RequestToEnableObject (5);
			}

			break;

		case "BTN_STORE":
			UIHandler.Instance.RequestToEnableObject (2);
			Debug.Log ("BTN_STORE");
			break;



		}
	}

	IEnumerator RequestToLoadNextScene (float lTime, string levelName, string nextLevelLoad)
	{
		GlobalVariables.sSceneToBeLoaded = nextLevelLoad;
		yield return new WaitForSeconds (lTime);
		Application.LoadLevel (levelName);
	}

	#endregion


	#region NATIVE_POP_UP

	bool isExitPopUp;

	void ShowExitPopUp ()
	{
	//	MobileNativeDialog dialog = new MobileNativeDialog ("Indian Train Games 2017", "Are you sure you want to exit?");
	//	dialog.OnComplete += OnDialogClose;
		isExitPopUp = true;
	}

	//private void OnDialogClose (MNDialogResult result)
	//{

	//	switch (result) {
	//	case MNDialogResult.YES:
	//		Application.Quit ();
	//		Debug.Log ("Yes button pressed");
	//		break;
	//	case MNDialogResult.NO:
	//		isExitPopUp = false;
	//		Debug.Log ("No button pressed");
	//		break;			
	//	}
	//}

	void RequestToExitPopUps ()
	{
		if (!isExitPopUp) {
			ShowExitPopUp ();
		} else {
			//OnDialogClose (MNDialogResult.NO);
		}
	}

	#endregion
}
