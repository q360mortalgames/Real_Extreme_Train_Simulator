using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public enum e_POPUP_STATE
{
	Levels,
	LevelDetails,
	UnlockUsingStarsPopUp,
	UnlockAllLevelsPopUp,
	Shop,
	ratepop,
	None
}

public class LevelSelectionHandler : MonoBehaviour
{


	[SerializeField] bool isResetData = false;
	public static LevelSelectionHandler Instance;
	public e_POPUP_STATE mPopUpState	= e_POPUP_STATE.Levels;

	public ButtonAnims _LevelDetailsPopUp;
	public ButtonAnims _LevelPageUI;
	public ButtonAnims _UnlockUsingStarsPopUp;
	public ButtonAnims _UnlockLevelsPopUp;
	public GameObject AllLevels_obj;

	public GameObject[] _LevelsButton;
	public GameObject _UnlockLevel6;
	public GameObject _UnlockLevel11;
	public GameObject _UnlockLevel16;
	public GameObject _UnlockLevel21;
	public GameObject _UnlockLevel26;
	public GameObject _UnlockLevel31;
	public GameObject _UnlockLevel36;
	public GameObject _UnlockLevel41;
	public GameObject _UnlockLevel46;

	public Text _TimerText;
	public Text _StopsText;
	public Text _XP1;
	public Text _XP2;
	public Text _XP3;
	public Text _LevelNo;
	public Image _TrainSelected;
	public Image _ThemeSelected;
	public Text _StarsCount;
	public Text _CoinsText;

	//public Sprite[] _LevlNoTextures;
	public Sprite[] _TrainTextures;
	public Sprite[] _ThemeTextures;
	public LevelSelectionCamera _cameraScr;
	public GameObject _AnimationObj;

	[SerializeField]public  ButtonAnims _RatePopUp;
	public Text Rate_text;

	void DisablePages (bool ldetail = false, bool Lpag = false, bool unlockStars = false, bool unlockleve = false, bool all_lvl = false, bool Rate_lvl = false, bool _shopp = true)
	{
		//Debug.LogError("Disable apges");
		_LevelDetailsPopUp.gameObject.SetActive (ldetail);
		_LevelPageUI.gameObject.SetActive (Lpag);
		_UnlockUsingStarsPopUp.gameObject.SetActive (unlockStars);
		_UnlockLevelsPopUp.gameObject.SetActive (unlockleve);
		AllLevels_obj.gameObject.SetActive (all_lvl);
		_RatePopUp.gameObject.SetActive (Rate_lvl);
		_Shop.gameObject.SetActive (_shopp);
	}

	void EnablePages ()
	{
		//Debug.LogError("Enable  apges");

		_LevelDetailsPopUp.gameObject.SetActive (true);
		_LevelPageUI.gameObject.SetActive (true);
		_UnlockUsingStarsPopUp.gameObject.SetActive (true);
		_UnlockLevelsPopUp.gameObject.SetActive (true);
		AllLevels_obj.gameObject.SetActive (true);
		_RatePopUp.gameObject.SetActive (true);
		_Shop.gameObject.SetActive (false);


	}

	void Awake ()
	{
		//if (isResetData)
		//PlayerPrefs.DeleteAll ();

		if (!PlayerPrefs.HasKey (GlobalVariables.sTotalUnlockedLevels))
			PlayerPrefs.SetInt (GlobalVariables.sTotalUnlockedLevels, 1);


		GlobalVariables.iNextLevelToUnlock	= PlayerPrefs.GetInt ("TotalLevelsToUnlock");
		int unlockedLevelNo = PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels);

		_TimerText.text	= "";
		_StopsText.text	= "";

		_StarsCount.text	= PlayerPrefs.GetInt (GlobalVariables.sTotalStarsGained).ToString ();
		_CoinsText.text = PlayerPrefs.GetInt (GlobalVariables.sTotalCoinsAvaliable).ToString ();

		UnlockVisibility (unlockedLevelNo);
	}

	//void CheckForAchievements ()
	//{
	//	AchievementsHandler.Instance.CheckForAchievements (eACHIEVEMENTSTAG.Unlock_Level_6);
	//	AchievementsHandler.Instance.CheckForAchievements (eACHIEVEMENTSTAG.Unlock_Level_11);
	//	AchievementsHandler.Instance.CheckForAchievements (eACHIEVEMENTSTAG.Unlock_Level_16);
	//	AchievementsHandler.Instance.CheckForAchievements (eACHIEVEMENTSTAG.Unlock_Level_21);
	//	AchievementsHandler.Instance.CheckForAchievements (eACHIEVEMENTSTAG.Unlock_Level_26);
	//	AchievementsHandler.Instance.CheckForAchievements (eACHIEVEMENTSTAG.Unlock_Level_31);

	//	AchievementsHandler.Instance.CheckForAchievements (eACHIEVEMENTSTAG.Collect_50_Stars);
	//	AchievementsHandler.Instance.CheckForAchievements (eACHIEVEMENTSTAG.Collect_100_Stars);
	//	AchievementsHandler.Instance.CheckForAchievements (eACHIEVEMENTSTAG.Complete_All_Levels_With_3_Stars);
	//}

	void ReSetAnimationObject ()
	{
		Debug.Log ("Current level " + GlobalVariables.iCurrentLevel);
		if (GlobalVariables.iCurrentLevel <= 0) {
			GlobalVariables.iCurrentLevel = 1;
		}
		GameObject _selectedLevel	= _LevelsButton [(GlobalVariables.iCurrentLevel - 1)];
		_AnimationObj.transform.position	= _selectedLevel.transform.position + new Vector3 (-0.04f, -0.7f, 0f);
	}

	void Start ()
	{

		GlobalVariables.mCurrentScene	= eCURRENT_SCENE.LevelSelection;
		Instance	= this;
		inRatePop = false;
		//				if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) < GlobalVariables.iTotalLevels) {
		//						int count	= PlayerPrefs.GetInt ("UnlockLevelsPopUpCount");
		//						count++;
		//						PlayerPrefs.SetInt ("UnlockLevelsPopUpCount", count);
		//						if (count % 3 == 0) {
		//
		//								Invoke ("ShowUnlockLevelsPopUp", 0.1f);
		//			} else{
		//								Invoke ("CallAnimAfterDelay", 0.1f);
		//			}
		//
		//
		//		} else{
		//						Invoke ("CallAnimAfterDelay", 0.1f);
		//		}
		Invoke ("CallAnimAfterDelay", 0.1f);
		SetLockForImages ();
	//	CheckForAchievements ();

		Invoke ("hideprev", 0.5f);


	}

	void hideprev ()
	{

		prev_btn.SetActive (false);

		//OnShowRatePopUp();
		//OnShowRatePopUp();
		//Debug.LogError(GlobalVariables.iNextLevelToUnlock+ " clevel a "+GameConfigs2015.RateLevels_ex.Count);
		//GlobalVariables.iCurrentLevel=3;


//		for (int i = 0; i < GameConfigs2015.RateLevels_ex.Count; i++) {
//
//			//Debug.LogError("aa "+GameConfigs2015.RateLevels_ex[i]);
//
//			if (GameConfigs2015.RateLevels_ex [i] == (GlobalVariables.iCurrentLevel - 1)) {
//				//Debug.LogError(GameConfigs2015.RateLevels_ex[i]+" : rate "+GlobalVariables.iCurrentLevel);
//				//Time.timeScale=0;
//				OnShowRatePopUp ();
//			}
//
//		}


		Debug.Log ((PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels)) + " : " + GlobalVariables.iNextLevelToUnlock + " Auto open level>> " + GlobalVariables.iCurrentLevel);

		if (GlobalVariables.iCurrentLevel >= ((PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels))) && inRatePop == false) {
			Debug.Log ("AutoOpen " + (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels)));
			mPopUpState	= e_POPUP_STATE.Levels;
			OnButtonClick ("Level" + (GlobalVariables.iNextLevelToUnlock));
		}



	}

	void CallAnimAfterDelay ()
	{
		mPopUpState	= e_POPUP_STATE.None;

		Debug.Log (isShowUnlockUsingStarsPopUp + " : " + GlobalVariables.iCurrentLevel);

		if (isShowUnlockUsingStarsPopUp) {
			_state	= e_POPUP_STATE.UnlockUsingStarsPopUp;
			ShowUnlockPopUp ();
			isShowUnlockUsingStarsPopUp	= false;
		} else {
			_state	= e_POPUP_STATE.Levels;
			_LevelPageUI.CallAllAnims ();
		}
		//				SetLockForImages ();
		Invoke ("ChangeState", 0.5f);
	}

	RaycastHit hit;

	void Update ()
	{
		if (mPopUpState == e_POPUP_STATE.LevelDetails || mPopUpState == e_POPUP_STATE.Shop)
			return;

		if (Input.GetMouseButtonDown (0)) {
			Ray _ray	= Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast (_ray, out hit)) {
				string _rayName	= hit.collider.gameObject.name;
				Debug.Log (_rayName + " : " + PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) + " : " + GlobalVariables.iNextLevelToUnlock);
				CheckClicks (_rayName);

			}
		}
	}

	void CheckClicks (string _rayName)
	{
		if (inRatePop) {
			return;
		}

		//---------------

		if (_rayName.Contains ("Level"))
			OnButtonClick (_rayName);
		else if (_rayName == "Unlock6" && PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 5) {
			ShowUnlockPopUp ();
			SoundController.Instance.OnButtonClick ();
		} else if (_rayName == "Unlock11" && PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 10) {// && GlobalVariables.iNextLevelToUnlock == 11) {
			ShowUnlockPopUp ();
			SoundController.Instance.OnButtonClick ();
		} else if (_rayName == "Unlock16" && PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 15) {// && GlobalVariables.iNextLevelToUnlock == 16) {
			ShowUnlockPopUp ();
			SoundController.Instance.OnButtonClick ();
		} else if (_rayName == "Unlock21" && PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 20) {//  && GlobalVariables.iNextLevelToUnlock == 21) {
			ShowUnlockPopUp ();
			SoundController.Instance.OnButtonClick ();
			//inRatePop=true;
		} else if (_rayName == "Unlock26" && PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 25) {//  && GlobalVariables.iNextLevelToUnlock == 26) {
			ShowUnlockPopUp ();
			SoundController.Instance.OnButtonClick ();
		} else if (_rayName == "Unlock31" && PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 30) {//  && GlobalVariables.iNextLevelToUnlock == 31) {
			ShowUnlockPopUp ();
			SoundController.Instance.OnButtonClick ();
		} else if (_rayName == "Unlock36" && PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 35) {//  && GlobalVariables.iNextLevelToUnlock == 31) {
			ShowUnlockPopUp ();
			SoundController.Instance.OnButtonClick ();
		} else if (_rayName == "Unlock41" && PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 40) {//  && GlobalVariables.iNextLevelToUnlock == 31) {
			ShowUnlockPopUp ();
			SoundController.Instance.OnButtonClick ();
		} else if (_rayName == "Unlock46" && PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 45) {//  && GlobalVariables.iNextLevelToUnlock == 31) {
			ShowUnlockPopUp ();
			SoundController.Instance.OnButtonClick ();
		}

	}

	public GameObject contentViewer;
	//1465
	public GameObject prev_btn, Next_btn;
	public static bool inRatePop = false;

	public void CloseRatePopUp ()
	{
		inRatePop = false;
		//Debug.Log(this.transform.parent+" : "+_LevelComplete.transform.parent);
		//SetVisibleLayer (_LevelComplete.transform);
		EnablePages ();
		SetInviisbleLayer (_RatePopUp.transform);
		_RatePopUp.ReverseAll ();
		GlobalVariables.mGameState = eGAME_STATE.LevelComplete;
	}

	public void OnButtonClick (string _str)
	{

		Debug.Log (_str + " , " + inRatePop + " ,mstate, " + mPopUpState);

//		if(inRatePop){
//			if(_str == "Cancel"){
//				PlayerPrefs.SetInt (GlobalVariables._sRatingCancel, 1);
//				CloseRatePopUp ();
//
//			}
//			if(_str == "RateIt"){
//
//				PlayerPrefs.SetInt (GlobalVariables._sRatingString, 1);
//				Application.OpenURL("market://details?id=com.timuzsolutions.trainsimulator2016");
//				CloseRatePopUp ();
//
//				int coinVala = 0;
//
//				coinVala = PlayerPrefs.GetInt (GlobalVariables.sTotalCoinsAvaliable)+GameConfigs2015.Coins_rate;
//				PlayerPrefs.SetInt (GlobalVariables.sTotalCoinsAvaliable, coinVala);
//
//				SetCoinAndStarData ();
//
//			}
//			return;
//		}
		switch (mPopUpState) {

		case e_POPUP_STATE.ratepop:

			if (_str == "Cancel") {
				PlayerPrefs.SetInt (GlobalVariables._sRatingCancel, 1);
				mPopUpState	= e_POPUP_STATE.Levels;

				CloseRatePopUp ();

			}
			if (_str == "RateIt") {
				CloseRatePopUp ();
				PlayerPrefs.SetInt (GlobalVariables._sRatingString, 1);
				Application.OpenURL ("market://details?id=com.mtsfreegames.trainracingmultiplayer");
				mPopUpState	= e_POPUP_STATE.Levels;


				int coinVala = 0;

				coinVala = PlayerPrefs.GetInt (GlobalVariables.sTotalCoinsAvaliable);// + GameConfigs2015.Coins_rate;
				PlayerPrefs.SetInt (GlobalVariables.sTotalCoinsAvaliable, coinVala);

				SetCoinAndStarData ();

			}
			break;
		case e_POPUP_STATE.Levels:
			if (_str == "Back" || _str == "UnityBackButton") {
				mPopUpState	= e_POPUP_STATE.None;
				_cameraScr.FadeToLoading ();
				SoundController.Instance.OnButtonClick ();
				Invoke ("OnBackclick", 0.5f);
			} else if (_str.Contains ("Level")) {
				OnLevelClick (_str);
				SoundController.Instance.OnButtonClick2 ();
			} else if (_str == "Shop") {
				OnShopClick ();
				SoundController.Instance.OnButtonClick ();
			} else if (_str == "MoreGames") {
			//	ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.MoreGames);
				SoundController.Instance.OnButtonClick ();
			}
			//						else if (_str == "UnlockAllLels") {
			//								SoundController.Instance.OnButtonClick();
			//								ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.UnlockAllLevels, "", CallBackFromInterfaceHandler);
			//						}
			break;
		case e_POPUP_STATE.LevelDetails:
			if (_str == "Close" || _str == "UnityBackButton") {
				ClosePopUp ();
				SoundController.Instance.OnButtonClick ();
			} else if (_str == "Play" || _str == "Buy5False") {
				SoundController.Instance.OnButtonClick2 ();
				mPopUpState	= e_POPUP_STATE.None;
				_cameraScr.FadeToLoading ();
				Invoke ("OnPlayClick", 0.5f);
				//								OnPlayClick ();
			}
			if (_str == "RateIt") {

				SoundController.Instance.OnButtonClick2 ();
				mPopUpState	= e_POPUP_STATE.None;
				_cameraScr.FadeToLoading ();
				Invoke ("OnPlayClick", 0.5f);
			}
			break;
		case e_POPUP_STATE.UnlockUsingStarsPopUp:
			if (_str == "Cancel_star" || _str == "UnityBackButton") {
				closeUnlockUsingStarsPopUp ();
				SoundController.Instance.OnButtonClick ();
			} else if (_str == "UnlockUsingCoins") {
				OnUnlockUsingCoins ();
				SoundController.Instance.OnButtonClick ();
			} else if (_str == "UnlockUsingStars") {
				OnUnlockUsingStars ();
				SoundController.Instance.OnButtonClick ();
			}
			break;
		case e_POPUP_STATE.Shop:
			if (_str == "Close" || _str == "UnityBackButton") {
				OnShopClose ();
				SoundController.Instance.OnButtonClick ();
			} else if (_str == "NextStore") {
				SoundController.Instance.OnButtonClick ();
				iTween.MoveTo (contentViewer.gameObject, iTween.Hash ("x", -732, "islocal", true));

				prev_btn.SetActive (true);
				Next_btn.SetActive (false);
			} else if (_str == "PrevStore") {
				SoundController.Instance.OnButtonClick ();
				iTween.MoveTo (contentViewer.gameObject, iTween.Hash ("x", 254, "islocal", true));

				Next_btn.SetActive (true);
				prev_btn.SetActive (false);
			} else if (_str == "Buy1") {
				SoundController.Instance.OnButtonClick ();
				//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.MiniPack, "", CallBackFromInterfaceHandler);
			} else if (_str == "Buy2") {
				SoundController.Instance.OnButtonClick ();
				//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.BoosterPack, "", CallBackFromInterfaceHandler);
			} else if (_str == "Buy3") {
				SoundController.Instance.OnButtonClick ();
				//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.SuperPack, "", CallBackFromInterfaceHandler);
			} else if (_str == "Buy4") {
				SoundController.Instance.OnButtonClick ();
				//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.ProPack, "", CallBackFromInterfaceHandler);
			} else if (_str == "Buy5") {
				SoundController.Instance.OnButtonClick ();
			//	ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.MegaPack, "", CallBackFromInterfaceHandler);
			} else if (_str == "Buy6") {
				SoundController.Instance.OnButtonClick ();
				//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.UltraPack, "", CallBackFromInterfaceHandler);
			} else if (_str == "NoAds") {
				SoundController.Instance.OnButtonClick ();
				//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.RemoveAds, "", CallBackFromInterfaceHandler);
			} else if (_str == "UnlockAllLevels") {
				SoundController.Instance.OnButtonClick ();
				//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.UnlockAllLevels, "", CallBackFromInterfaceHandler);
			} else if (_str == "UnlockAllTrains") {
				SoundController.Instance.OnButtonClick ();
				//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.UnlockAllVehicles, "", CallBackFromInterfaceHandler);
			} else if (_str == "UnlockAllLevelsAndTrains") {
				SoundController.Instance.OnButtonClick ();
				//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.UnlockAllLevelsAndVehicles, "", CallBackFromInterfaceHandler);
			}
			break;
		case e_POPUP_STATE.UnlockAllLevelsPopUp:
			if (_str == "Close" || _str == "UnityBackButton") {
				closeUnlockLevelsPopUp ();
				SoundController.Instance.OnButtonClick ();

				inRatePop = false;

			} else if (_str	== "UnlockAllLevels") {
				//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.UnlockAllLevels, "", CallBackFromInterfaceHandler);
				SoundController.Instance.OnButtonClick ();
			}
			break;
		}
	}

	//public void CallBackFromInterfaceHandler (eBUTTON_CALL_STATE _state, string _status)
	//{

	//	#if UNITY_EDITOR || UNITY_ANDROID

	//	return;
	//	#endif
	//	Debug.Log ("adding normally ls");
	//	ButtonVisibilityForShopAndChangeText _obj	= GameObject.Find ("ShopCanvas").GetComponent<ButtonVisibilityForShopAndChangeText> ();
	//	int coinVal = 0;
	//	switch (_state) {
	//	case eBUTTON_CALL_STATE.RemoveAds:
	//		PlayerPrefs.SetString ("NoAdsPurchase", "Success");
	//		break;
	//	case eBUTTON_CALL_STATE.UnlockAllLevels:
	//		PlayerPrefs.SetInt (GlobalVariables.sTotalUnlockedLevels, GlobalVariables.iTotalLevels);
	//		UnlockAllLevels ();
	//		break;
	//	case eBUTTON_CALL_STATE.UnlockAllVehicles:
	//		PlayerPrefs.SetInt (GlobalVariables.sTotalTrainsUnlocked, GlobalVariables.iTotalTrainsAvalaiable);
	//		break;
	//	case eBUTTON_CALL_STATE.UnlockAllLevelsAndVehicles:
	//		PlayerPrefs.SetInt (GlobalVariables.sTotalUnlockedLevels, GlobalVariables.iTotalLevels);
	//		PlayerPrefs.SetInt (GlobalVariables.sTotalTrainsUnlocked, GlobalVariables.iTotalTrainsAvalaiable);
	//		UnlockAllLevels ();
	//		break;
	//	case eBUTTON_CALL_STATE.MiniPack:
	//		coinVal	= GlobalVariables.iCoinsForMiniPack;
	//		break;
	//	case eBUTTON_CALL_STATE.BoosterPack:
	//		coinVal	= GlobalVariables.iCoinsForBoosterPack;
	//		break;
	//	case eBUTTON_CALL_STATE.SuperPack:
	//		coinVal	= GlobalVariables.iCoinsForSuperPack;
	//		break;
	//	case eBUTTON_CALL_STATE.ProPack:
	//		coinVal	= GlobalVariables.iCoinsForProPack;
	//		break;
	//	case eBUTTON_CALL_STATE.MegaPack:
	//		coinVal	= GlobalVariables.iCoinsForMegaPack;
	//		break;
	//	case eBUTTON_CALL_STATE.UltraPack:
	//		coinVal	= GlobalVariables.iCoinsForUltraPack;
	//		break;
	//	}
	//	if (_obj)
	//		_obj.CheckButtonVisibility ();

	//	if (coinVal > 0) {
	//		coinVal	+= PlayerPrefs.GetInt (GlobalVariables.sTotalCoinsAvaliable);
	//		PlayerPrefs.SetInt (GlobalVariables.sTotalCoinsAvaliable, coinVal);
	//		SetCoinAndStarData ();
	//	}

	//	if (mPopUpState == e_POPUP_STATE.Shop)
	//		OnShopClose ();
	//}

	void SetCoinAndStarData ()
	{
		_CoinsText.text	= "" + PlayerPrefs.GetInt (GlobalVariables.sTotalCoinsAvaliable);
		_StarsCount.text	= "" + PlayerPrefs.GetInt (GlobalVariables.sTotalStarsGained);
	}

	void SetVisibleLayer (Transform _ObjToChangeLayer)
	{

		_ObjToChangeLayer.gameObject.GetComponent<Canvas> ().enabled	= true;
	}

	void SetInviisbleLayer (Transform _ObjToChangeLayer)
	{
		_ObjToChangeLayer.gameObject.GetComponent<Canvas> ().enabled	= false;
	}

	void ChangeStaterate ()
	{
		mPopUpState = e_POPUP_STATE.ratepop;
	}

	public static bool FromComplete = false;

	void OnShowRatePopUp ()
	{
		if (PlayerPrefs.GetInt (GlobalVariables._sRatingString) == 1)
			return;


		mPopUpState = e_POPUP_STATE.ratepop;

		DisablePages (false, false, false, false, false, true, false);
		Debug.LogError ("rate enable---- " + (PlayerPrefs.GetInt (GlobalVariables._sRatingString)));

		SetVisibleLayer (_RatePopUp.transform);
		_RatePopUp.CallAllAnims ();

		Invoke ("ChangeStaterate", 0.5f);

		GlobalVariables.mGameState = eGAME_STATE.Rate;
		inRatePop = true;

		FromComplete = false;

	}

	void OnLevelClick (string _str)
	{
		int selectedLevel	= 0;
		string newString = _str.Remove (0, 5);
		int.TryParse (newString, out selectedLevel);
		//				if (_str == "Level1") {
		//						selectedLevel	= 1;
		//				} else if (_str == "Level2") {
		//						selectedLevel	= 2;
		//				} else if (_str == "Level3") {
		//						selectedLevel	= 3;
		//				} else if (_str == "Level4") {
		//						selectedLevel	= 4;
		//				} else if (_str == "Level5") {
		//						selectedLevel	= 5;
		//				} else if (_str == "Level6") {
		//						selectedLevel	= 6;
		//				} else if (_str == "Level7") {
		//						selectedLevel	= 7;
		//				} else if (_str == "Level8") {
		//						selectedLevel	= 8;
		//				} else if (_str == "Level9") {
		//						selectedLevel	= 9;
		//				} else if (_str == "Level10") {
		//						selectedLevel	= 10;
		//				} else if (_str == "Level11") {
		//						selectedLevel	= 11;
		//				} else if (_str == "Level12") {
		//						selectedLevel	= 12;
		//				} else if (_str == "Level13") {
		//						selectedLevel	= 13;
		//				} else if (_str == "Level14") {
		//						selectedLevel	= 14;
		//				} else if (_str == "Level15") {
		//						selectedLevel	= 15;
		//				} else if (_str == "Level16") {
		//						selectedLevel	= 16;
		//				} else if (_str == "Level17") {
		//						selectedLevel	= 17;
		//				} else if (_str == "Level18") {
		//						selectedLevel	= 18;
		//				} else if (_str == "Level19") {
		//						selectedLevel	= 19;
		//				} else if (_str == "Level20") {
		//						selectedLevel	= 20;
		//				}



		if (selectedLevel > PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels))
			return;


		Debug.Log ("clicked on level " + selectedLevel);
		GlobalVariables.iCurrentLevel	= selectedLevel;
		mPopUpState	= e_POPUP_STATE.None;
		ReSetAnimationObject ();
		Invoke ("ShowPopUp", 0.5f);
	}

	void ShowPopUp ()
	{
		mPopUpState	= e_POPUP_STATE.None;
		_LevelPageUI.ReverseAll ();

		_cameraScr.ChangeCameraToSelected (_LevelsButton [GlobalVariables.iCurrentLevel - 1].transform.position.x, _LevelsButton [GlobalVariables.iCurrentLevel - 1].transform.position.y);

		_LevelNo.text = "Level  " + (GlobalVariables.iCurrentLevel);
		_TrainSelected.sprite	= _TrainTextures [(GlobalVariables.i_CurrentTrainSelected)];

		SetThemeBasedOnLevels ();

		Invoke ("setDataAfterDelay", 0.5f);
		Invoke ("setLevelDetailsAfterDelay", 0.5f);
	}

	void SetThemeBasedOnLevels ()
	{
		int selectThemeNo = 0;

		// 0,1 - Village    2,3 - Snow    4,5 - Desert  6,7 - City     8,9 - mountain 

		if (GlobalVariables.iCurrentLevel == 1 || GlobalVariables.iCurrentLevel == 11 || GlobalVariables.iCurrentLevel == 21 || GlobalVariables.iCurrentLevel == 31 || GlobalVariables.iCurrentLevel == 36 || GlobalVariables.iCurrentLevel == 44)
			selectThemeNo = 0;
		else if (GlobalVariables.iCurrentLevel == 3 || GlobalVariables.iCurrentLevel == 16 || GlobalVariables.iCurrentLevel == 26 || GlobalVariables.iCurrentLevel == 39 || GlobalVariables.iCurrentLevel == 46)
			selectThemeNo = 1;
		else if (GlobalVariables.iCurrentLevel == 4 || GlobalVariables.iCurrentLevel == 12 || GlobalVariables.iCurrentLevel == 24 || GlobalVariables.iCurrentLevel == 34 || GlobalVariables.iCurrentLevel == 38 || GlobalVariables.iCurrentLevel == 47)
			selectThemeNo = 2;
		else if (GlobalVariables.iCurrentLevel == 8 || GlobalVariables.iCurrentLevel == 19 || GlobalVariables.iCurrentLevel == 28 || GlobalVariables.iCurrentLevel == 43)
			selectThemeNo = 3;
		else if (GlobalVariables.iCurrentLevel == 2 || GlobalVariables.iCurrentLevel == 13 || GlobalVariables.iCurrentLevel == 22 || GlobalVariables.iCurrentLevel == 32 || GlobalVariables.iCurrentLevel == 37 || GlobalVariables.iCurrentLevel == 42)
			selectThemeNo = 4;
		else if (GlobalVariables.iCurrentLevel == 7 || GlobalVariables.iCurrentLevel == 18 || GlobalVariables.iCurrentLevel == 27 || GlobalVariables.iCurrentLevel == 47)
			selectThemeNo = 5;
		else if (GlobalVariables.iCurrentLevel == 5 || GlobalVariables.iCurrentLevel == 15 || GlobalVariables.iCurrentLevel == 25 || GlobalVariables.iCurrentLevel == 35 || GlobalVariables.iCurrentLevel == 40)
			selectThemeNo = 6;
		else if (GlobalVariables.iCurrentLevel == 9 || GlobalVariables.iCurrentLevel == 20 || GlobalVariables.iCurrentLevel == 29 || GlobalVariables.iCurrentLevel == 50)
			selectThemeNo = 7;
		else if (GlobalVariables.iCurrentLevel == 6 || GlobalVariables.iCurrentLevel == 14 || GlobalVariables.iCurrentLevel == 23 || GlobalVariables.iCurrentLevel == 33 || GlobalVariables.iCurrentLevel == 45 || GlobalVariables.iCurrentLevel == 47)
			selectThemeNo = 8;
		else if (GlobalVariables.iCurrentLevel == 10 || GlobalVariables.iCurrentLevel == 17 || GlobalVariables.iCurrentLevel == 30 || GlobalVariables.iCurrentLevel == 41)
			selectThemeNo = 9;

		_ThemeSelected.sprite	= _ThemeTextures [selectThemeNo];
	}

	void setLevelDetailsAfterDelay ()
	{
		_state	= e_POPUP_STATE.LevelDetails;

		_LevelDetailsPopUp.CallAllAnims ();
		Invoke ("ChangeState", 0.5f);
	}

	e_POPUP_STATE _state = e_POPUP_STATE.None;

	void ChangeState ()
	{
		mPopUpState	= _state;
		print ("Change State--- " + mPopUpState);
	}

	void setDataAfterDelay ()
	{
		resetTextDatas (true);
	}

	void resetTextDatas (bool isSetFromLevelSelect)
	{
		if (isSetFromLevelSelect) {
			_StopsText.text	= _LevelsButton [GlobalVariables.iCurrentLevel - 1].GetComponent<LevelDetailsInLevelSelection> ().mi_StopsCount.ToString ();
			_TimerText.text	= _LevelsButton [GlobalVariables.iCurrentLevel - 1].GetComponent<LevelDetailsInLevelSelection> ().mi_TimerCountinSec.ToString ();
			int timerVal	= _LevelsButton [GlobalVariables.iCurrentLevel - 1].GetComponent<LevelDetailsInLevelSelection> ().mi_TimerCountinSec;

			print ("timerVal : "+timerVal);
			int min	= Mathf.FloorToInt (timerVal / 60f);
			int sec	= timerVal % 60;
			_TimerText.text	= min.ToString ("00") + ":" + sec.ToString ("00");

			_XP1.text	= _LevelsButton [GlobalVariables.iCurrentLevel - 1].GetComponent<LevelDetailsInLevelSelection> ().mi_XP1.ToString ();
			_XP2.text	= _LevelsButton [GlobalVariables.iCurrentLevel - 1].GetComponent<LevelDetailsInLevelSelection> ().mi_XP2.ToString ();
			_XP3.text	= _LevelsButton [GlobalVariables.iCurrentLevel - 1].GetComponent<LevelDetailsInLevelSelection> ().mi_XP3.ToString ();

		} else {
			_StopsText.text	= "";
			_TimerText.text	= "";
		}
	}

	void ClosePopUp ()
	{
		_cameraScr.ChangeCameraToNormalMode ();
		resetTextDatas (false);
		mPopUpState	= e_POPUP_STATE.None;
		_LevelDetailsPopUp.ReverseAll ();
		Invoke ("CallLevelSelectionAfterDelay", 0.5f);
	}

	void CallLevelSelectionAfterDelay ()
	{
		_state = e_POPUP_STATE.Levels;
		_LevelPageUI.CallAllAnims ();
		Invoke ("ChangeState", 0.5f);
	}

	void OnPlayClick ()
	{

		CallPlayAfterDelay ();
		//				Invoke ("CallPlayAfterDelay ", 0.4f);
	}
	//private int[] DessertLevels_ary={8,2,3,4,5,6,7};

	private int[] DessertLevels_ary = { 2, 7, 13, 18, 22, 27, 32, 37, 42, 47 };
	private int[] Village_nightLevels_ary = { 1, 3, 6, 10, 14, 17, 23, 30, 33, 44, 45 };
	private int[] SnowLevels_ary = { 4, 8, 12, 19, 24, 28, 34, 38, 43, 48 };
	private int[] CityLevels_ary = { 5, 9, 15, 20, 25, 29, 35, 40, 50 };
	private int[] Village_DayLevels_ary = { 11, 16, 21, 26, 31, 36, 39, 41, 46, 49 };
	//private int[] Village_DayLevels_ary={1};


	public static string PlayingTheme;

	void CallPlayAfterDelay ()
	{





		for (int i = 0; i < DessertLevels_ary.Length; i++) {
			if (DessertLevels_ary [i] == GlobalVariables.iCurrentLevel) {

				//LoadGameNow("DessertTheme_main");
				LoadGameNow ("DesertTheme2");
				//LoadGameNow("ts2_dessert");
				return;
			}


		}



		for (int j = 0; j < Village_nightLevels_ary.Length; j++) {
			if (Village_nightLevels_ary [j] == GlobalVariables.iCurrentLevel) {

				LoadGameNow ("GreenIland_theme");//night
				//LoadGameNow("ts1_green");//night

				return;
			}
		}

		for (int i = 0; i < SnowLevels_ary.Length; i++) {
			if (SnowLevels_ary [i] == GlobalVariables.iCurrentLevel) {

				LoadGameNow ("snowTheme");
				//LoadGameNow("ts3_snow");//night

				return;
			}
		}

		for (int i = 0; i < CityLevels_ary.Length; i++) {
			if (CityLevels_ary [i] == GlobalVariables.iCurrentLevel) {

				LoadGameNow ("City theme");
				//LoadGameNow("ts4_city");//night

				return;
			}
		}

		for (int i = 0; i < Village_DayLevels_ary.Length; i++) {
			if (Village_DayLevels_ary [i] == GlobalVariables.iCurrentLevel) {


				//LoadGameNow("Level_01_bk");
				LoadGameNow ("Daytheme");
				//LoadGameNow("ts5_day");//night

				return;
			}
		}


		//				GlobalVariables.sSceneToBeLoaded	= "Level_" + GlobalVariables.iCurrentLevel.ToString ("00");
		//				Application.LoadLevel ("LoadingScene");
	}

	void LoadGameNow (string sceneName = "")
	{
		//		if(sceneName!="DessertTheme_main"){
		//			sceneName="DesertTheme2";
		//			GlobalVariables.iCurrentLevel=2;
		//		}

		//Debug.Log(sceneName);
		PlayingTheme = sceneName;
		GlobalVariables.sSceneToBeLoaded	= sceneName;// "Level_" + GlobalVariables.iCurrentLevel.ToString ("00");
		Application.LoadLevel ("LoadingScene");
	}

	void OnBackclick ()
	{
		GlobalVariables.sSceneToBeLoaded	= "TrainSelection";
		Application.LoadLevel ("LoadingScene");
	}

	[SerializeField] Image _gUnlockAllLevelsButton;


	bool isShowUnlockUsingStarsPopUp = false;

	void SetLockForImages ()
	{
		int totalUnlocked	= PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels);
		if (totalUnlocked < GlobalVariables.iNextLevelToUnlock && totalUnlocked < GlobalVariables.iTotalLevels) {
			if (GlobalVariables.iNextLevelToUnlock != 6 && GlobalVariables.iNextLevelToUnlock != 11 && GlobalVariables.iNextLevelToUnlock != 16 && GlobalVariables.iNextLevelToUnlock != 16 && GlobalVariables.iNextLevelToUnlock != 21 && GlobalVariables.iNextLevelToUnlock != 26 && GlobalVariables.iNextLevelToUnlock != 31 && GlobalVariables.iNextLevelToUnlock != 36 && GlobalVariables.iNextLevelToUnlock != 41 && GlobalVariables.iNextLevelToUnlock != 46) {
				totalUnlocked	= (GlobalVariables.iNextLevelToUnlock);
				Debug.LogError ("seting levels " + totalUnlocked);
				PlayerPrefs.SetInt (GlobalVariables.sTotalUnlockedLevels, totalUnlocked);
				isShowUnlockUsingStarsPopUp	= false;

				//GSConfig.SaveMyDataSavedGames ();

			} else {
				//							Invoke("ShowUnlockPopUp",1f);
				//								ShowUnlockPopUp();
				isShowUnlockUsingStarsPopUp	= true;

			}
		}
		Debug.Log ("Total Unlocked " + totalUnlocked);

		SetUnlockedLevelCount (totalUnlocked);

		if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) >= GlobalVariables.iTotalLevels) {
			_gUnlockAllLevelsButton.enabled	= false;
			_gUnlockAllLevelsButton.GetComponent<Button> ().interactable	= false;
			_gUnlockAllLevelsButton.transform.GetChild (0).gameObject.SetActive (false);
		}

		UnlockVisibility (totalUnlocked);
	}

	[SerializeField] Sprite[] _sprites;

	void SetUnlockedLevelCount (int totalUnlocked)
	{
		/*string levelStarsa = "";
		string StarData = "0" + "332311";
		for (int i = 0; i < StarData.Length; i++) {
			Debug.Log (i + "-----");


			//PlayerPrefs.SetInt ((GlobalVariables.sStarsGainedInEachLevel + (i).ToString ()), int.Parse (StarData.Substring (i)));


			levelStarsa = levelStarsa + int.Parse (StarData.Substring (i, 1));

			Debug.Log (i + " : " + int.Parse (StarData.Substring (i, 1)) + " : " + levelStarsa);

			PlayerPrefs.SetInt ((GlobalVariables.sStarsGainedInEachLevel + (i).ToString ()), int.Parse (levelStarsa.Substring (i)));



		}*/

		//PlayerPrefs.SetInt ((GlobalVariables.sStarsGainedInEachLevel + (1).ToString ()), (3));
		//PlayerPrefs.SetInt ((GlobalVariables.sStarsGainedInEachLevel + (3).ToString ()), (2));

		Debug.Log (PlayerPrefs.GetInt ((GlobalVariables.sStarsGainedInEachLevel + "1")) + ": stard ate of " + PlayerPrefs.GetInt ((GlobalVariables.sStarsGainedInEachLevel + "4")));


		string levelStars = "";
		for (int i = 0; i < PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels); i++) {
			levelStars = levelStars + PlayerPrefs.GetInt ((GlobalVariables.sStarsGainedInEachLevel + (i).ToString ()));

			PlayerPrefs.SetInt ((GlobalVariables.sStarsGainedInEachLevel + (i).ToString ()), int.Parse (levelStars.Substring (i)));

			Debug.Log (levelStars + " : " + i + " : " + levelStars.Substring (i));
		}


		Debug.Log (GlobalVariables.sStarsGainedInEachLevel + " : set unlock " + levelStars);
		for (int i = 0; i < _LevelsButton.Length; i++) {
			if (i >= totalUnlocked) {
				_LevelsButton [i].GetComponent<SpriteRenderer> ().sprite	= _sprites [1];//new Color(0.25f,0.25f,0.25f,0.75f); 
			} else {


				_LevelsButton [i].GetComponent<SpriteRenderer> ().sprite	= _sprites [0];//  new Color(1,1,1,1); 
				int totalStars	= PlayerPrefs.GetInt ((GlobalVariables.sStarsGainedInEachLevel + (i + 1).ToString ()));



				Debug.Log (PlayerPrefs.GetInt ((GlobalVariables.sStarsGainedInEachLevel + 1.ToString ())) + " :i: " + PlayerPrefs.GetInt ((GlobalVariables.sStarsGainedInEachLevel + (i + 1).ToString ())));
				for (int j = 0; j < _LevelsButton [i].transform.childCount; j++) {
					if (totalStars > j)
						(_LevelsButton [i].transform.GetChild (j)).gameObject.GetComponent<SpriteRenderer> ().enabled	= true;
				}
			}
		}

		UnlockVisibility (totalUnlocked);
		ReSetAnimationObject ();
		StarsCount ();

	}


	void StarsCount ()
	{
		string LevelStars = GlobalVariables.sStarsGainedInEachLevel + GlobalVariables.iCurrentLevel.ToString ();
		int oldStarCount	= PlayerPrefs.GetInt (LevelStars);
		int totalStarsInGame	= 0;
		for (int i = 1; i <= GlobalVariables.iTotalLevels; i++) {
			LevelStars	= GlobalVariables.sStarsGainedInEachLevel + i.ToString ();
			totalStarsInGame	+= PlayerPrefs.GetInt (LevelStars);
		}
		PlayerPrefs.SetInt (GlobalVariables.sTotalStarsGained, totalStarsInGame);
		Debug.Log ("Total Stars : " + totalStarsInGame);

		_StarsCount.text	= PlayerPrefs.GetInt (GlobalVariables.sTotalStarsGained).ToString ();
	}

	void UnlockVisibility (int totalUnlocked)
	{
		if (totalUnlocked >= 6)
			_UnlockLevel6.SetActive (false);
		if (totalUnlocked >= 11)
			_UnlockLevel11.SetActive (false);
		if (totalUnlocked >= 16)
			_UnlockLevel16.SetActive (false);
		if (totalUnlocked >= 21)
			_UnlockLevel21.SetActive (false);
		if (totalUnlocked >= 26)
			_UnlockLevel26.SetActive (false);
		if (totalUnlocked >= 31)
			_UnlockLevel31.SetActive (false);
		if (totalUnlocked >= 36)
			_UnlockLevel36.SetActive (false);
		if (totalUnlocked >= 41)
			_UnlockLevel41.SetActive (false);
		if (totalUnlocked >= 46)
			_UnlockLevel46.SetActive (false);
	}

	public Text _StarReqText;
	public Text _CoinReqText;

	void ShowUnlockPopUp ()
	{

		DisablePages (false, false, true, false, false, false, false);

		openCoinPageOnUnlock	= false;	
		mPopUpState	= e_POPUP_STATE.UnlockUsingStarsPopUp;
		_UnlockUsingStarsPopUp.CallAllAnims ();
		_LevelPageUI.ReverseAll ();

		int avaCoin	= PlayerPrefs.GetInt (GlobalVariables.sTotalCoinsAvaliable);
		int avaStar	= PlayerPrefs.GetInt (GlobalVariables.sTotalStarsGained);
		int reqStar	= 0;
		int reqCoin	= 0;
		if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 5) {
			//reqStar	= GlobalVariables.iRequiredStarsForLevel6;
			reqCoin	= GlobalVariables.iRequiredCoinsForLevel6;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 10) {
			//reqStar	= GlobalVariables.iRequiredStarsForLevel11;
			reqCoin	= GlobalVariables.iRequiredCoinsForLevel11;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 15) {
			//reqStar	= GlobalVariables.iRequiredStarsForLevel16;
			reqCoin	= GlobalVariables.iRequiredCoinsForLevel16;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 20) {
			//reqStar	= GlobalVariables.iRequiredStarsForLevel21;
			reqCoin	= GlobalVariables.iRequiredCoinsForLevel21;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 25) {
			//reqStar	= GlobalVariables.iRequiredStarsForLevel26;
			reqCoin	= GlobalVariables.iRequiredCoinsForLevel26;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 30) {
			//reqStar	= GlobalVariables.iRequiredStarsForLevel31;
			reqCoin	= GlobalVariables.iRequiredCoinsForLevel31;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 35) {
			//reqStar	= GlobalVariables.iRequiredStarsForLevel36;
			reqCoin	= GlobalVariables.iRequiredCoinsForLevel36;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 40) {
			//reqStar	= GlobalVariables.iRequiredStarsForLevel41;
			reqCoin	= GlobalVariables.iRequiredCoinsForLevel41;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 45) {
			//reqStar	= GlobalVariables.iRequiredStarsForLevel46;
			reqCoin	= GlobalVariables.iRequiredCoinsForLevel46;
		}
		_StarReqText.text	= "Req : " + reqStar + "\nAva : " + avaStar;
		_CoinReqText.text	= "Req : " + reqCoin + "\nAva : " + avaCoin;
	}

	public void closeUnlockUsingStarsPopUp ()
	{
		Debug.LogError ("close unlock stars ");
		EnablePages ();
		mPopUpState	= e_POPUP_STATE.None;
		_UnlockUsingStarsPopUp.ReverseAll ();

		if (!openCoinPageOnUnlock) {
			Invoke ("CallAnimAfterDelay", 0.5f);
		} else {
			Invoke ("OnShopClick", 0.25f);
		}

	}

	void OnUnlockUsingStars ()
	{
		int requiredStars	= 0;
		int leveToUnlock = 0;
		if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 5) {
			//requiredStars	= GlobalVariables.iRequiredStarsForLevel6;
			leveToUnlock = 6;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 10) {
			//requiredStars	= GlobalVariables.iRequiredStarsForLevel11;
			leveToUnlock = 11;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 15) {
			//requiredStars	= GlobalVariables.iRequiredStarsForLevel16;
			leveToUnlock = 16;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 20) {
			//requiredStars	= GlobalVariables.iRequiredStarsForLevel21;
			leveToUnlock = 21;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 25) {
			//requiredStars	= GlobalVariables.iRequiredStarsForLevel26;
			leveToUnlock = 26;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 30) {
			//requiredStars	= GlobalVariables.iRequiredStarsForLevel31;
			leveToUnlock = 31;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 35) {
			//requiredStars	= GlobalVariables.iRequiredStarsForLevel36;
			leveToUnlock = 36;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 40) {
			//requiredStars	= GlobalVariables.iRequiredStarsForLevel41;
			leveToUnlock = 41;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 45) {
			//requiredStars	= GlobalVariables.iRequiredStarsForLevel46;
			leveToUnlock = 46;
		}

		int totalStars	= PlayerPrefs.GetInt (GlobalVariables.sTotalStarsGained);
		Debug.LogError (GlobalVariables.iNextLevelToUnlock + " level to unlock " + leveToUnlock);
		if (totalStars >= requiredStars) {
			PlayerPrefs.SetInt (GlobalVariables.sTotalUnlockedLevels, leveToUnlock);
			closeUnlockUsingStarsPopUp ();
			GlobalVariables.iNextLevelToUnlock = leveToUnlock;
			SetUnlockedLevelCount (GlobalVariables.iNextLevelToUnlock);
		} else {
			Debug.Log ("jarToast");
//			gameConfigs.mee.jarToast ("Required more " + (requiredStars - totalStars) + " stars to unlock.!");

		}

	}

	void OnUnlockUsingCoins ()
	{
		int requiredCoins	= 0;
		int leveToUnlock = 0;
		if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 5) {
			requiredCoins	= GlobalVariables.iRequiredCoinsForLevel6;
			leveToUnlock = 6;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 10) {
			requiredCoins	= GlobalVariables.iRequiredCoinsForLevel11;
			leveToUnlock = 11;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 15) {
			requiredCoins	= GlobalVariables.iRequiredCoinsForLevel16;
			leveToUnlock = 16;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 20) {
			requiredCoins	= GlobalVariables.iRequiredCoinsForLevel21;
			leveToUnlock = 21;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 25) {
			requiredCoins	= GlobalVariables.iRequiredCoinsForLevel26;
			leveToUnlock = 26;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 30) {
			requiredCoins	= GlobalVariables.iRequiredCoinsForLevel31;
			leveToUnlock = 31;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 35) {
			requiredCoins	= GlobalVariables.iRequiredCoinsForLevel36;
			leveToUnlock = 36;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 40) {
			requiredCoins	= GlobalVariables.iRequiredCoinsForLevel41;
			leveToUnlock = 41;
		} else if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == 45) {
			requiredCoins	= GlobalVariables.iRequiredCoinsForLevel46;
			leveToUnlock = 46;
		}

		int totalCoins	= PlayerPrefs.GetInt (GlobalVariables.sTotalCoinsAvaliable);
		Debug.LogError ("level to unlock aaa:  " + leveToUnlock);

		if (totalCoins >= requiredCoins) {
			PlayerPrefs.SetInt (GlobalVariables.sTotalUnlockedLevels, leveToUnlock);
			closeUnlockUsingStarsPopUp ();
			GlobalVariables.iNextLevelToUnlock = leveToUnlock;

			SetUnlockedLevelCount (GlobalVariables.iNextLevelToUnlock);

			totalCoins	-= requiredCoins;
			PlayerPrefs.SetInt (GlobalVariables.sTotalCoinsAvaliable, totalCoins);
			SetCoinAndStarData ();
		} else {
			openCoinPageOnUnlock	= true;
			closeUnlockUsingStarsPopUp ();
			//						OnShopClick ();
		}
	}

	bool openCoinPageOnUnlock	= false;

	// ******************************
	// SHOP
	//*******************************
	[SerializeField] ButtonAnims _Shop;

	void OnShopClick ()
	{

		DisablePages ();

		mPopUpState	= e_POPUP_STATE.None;
		_LevelPageUI.ReverseAll ();
		Invoke ("SetShopAfterDelay", 0.5f);
	}

	void SetShopAfterDelay ()
	{
		_Shop.CallAllAnims ();
		_state	= e_POPUP_STATE.Shop;
		Invoke ("ChangeState", 0.5f);
	}

	void OnShopClose ()
	{

		EnablePages ();
		_Shop.ReverseAll ();
		mPopUpState	= e_POPUP_STATE.None;
		Invoke ("CallLevelSelectionAfterDelay", 0.5f);
	}

	[SerializeField] Text _AllLevelsText;

	void ShowUnlockLevelsPopUp ()
	{
		mPopUpState	= e_POPUP_STATE.None;
		_state	= e_POPUP_STATE.UnlockAllLevelsPopUp;
		_UnlockLevelsPopUp.CallAllAnims ();
	//	_AllLevelsText.text	= ExternalInterfaceHandler.Instance.getStringForBuyButtons (1);
		Invoke ("ChangeState", 0.5f);
	}

	void closeUnlockLevelsPopUp ()
	{
		_UnlockLevelsPopUp.ReverseAll ();
		mPopUpState	= e_POPUP_STATE.None;
		_state	= e_POPUP_STATE.Levels;
		Invoke ("CallAnimAfterDelay", 0.5f);
	}

	void UnlockAllLevels ()
	{
		ButtonVisibilityForShopAndChangeText _obj	= GameObject.Find ("ShopCanvas").GetComponent<ButtonVisibilityForShopAndChangeText> ();
		if (_obj)
			_obj.CheckButtonVisibility ();
		SetLockForImages ();
		if (mPopUpState == e_POPUP_STATE.UnlockAllLevelsPopUp)
			closeUnlockLevelsPopUp ();
	}
}
