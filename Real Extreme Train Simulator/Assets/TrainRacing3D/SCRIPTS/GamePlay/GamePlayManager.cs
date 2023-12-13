using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class GamePlayManager : MonoBehaviour
{
	private static GamePlayManager _instance = null;

	public static GamePlayManager Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(GamePlayManager)) as GamePlayManager; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	
	// ************ SPEED CALCULATION ********
	float f_StartYPos = 0;
	float f_CurrentYPos = 0f;
	//[HideInInspector]
	public float f_DiffInSpeed = 0,f_DiffInSpeed_AI = 0;
	public Text _gSpeed;
	public Text _WarningText;
	public Text _SpeedLimitText;
	Text crashResetAlertText;
	public Text _StopsText;


	[Space (10)]
	public Sprite[] _SoundImgs;
	//[SerializeField] Sprite[] _LFTexture;

	[Space (10)]
	[SerializeField] Color _cColorForNormalText;
	[SerializeField] Color _cColorForWaringText;

	[Space (10)]
	public Slider _gAccelSlider;
	[SerializeField] Slider _gDirectionChangeSlider;

	LevelData _scrLevelData;
	private Vector3 _AccelInputPos;

	[HideInInspector] 
	public bool mb_IsTouchHasTexture = false;
	[HideInInspector] 
	public bool mb_IsStationLeftSide	= true;
	[HideInInspector] 
	public bool mb_IsMasterPalyer;
	[HideInInspector] 
	public bool IsGameResult;

	bool isLightsOn	= false;


	Image _SoundBtn;

	[HideInInspector]
	public int InGameCoins;
	[HideInInspector]
	public float InGameDistance;


	GameObject _LC_S1;
	GameObject _LC_S2;
	GameObject _LC_S3;

	GameObject _LC_XP;

	public GameObject dollerbutton;

	void Start ()
	{	
		//OnLevelCompleted ();
		if (LevelSelectionHandler.Instance) {
			LevelSelectionHandler.FromComplete = false;
		}
			
		ChangeInstructionState (e_INSTRUCTION_STATE.NoInstruction);
		GlobalVariables.mCurrentScene = eCURRENT_SCENE.GamePlay;

		SetDataOnStart ();

		f_DiffInSpeed	= 0;
		InGameCoins = 0;
		InGameDistance = 0;

		mb_IsMasterPalyer = false;
		IsGameResult = false;
		disableWaringingText ();

		SoundController.Instance.playTrainCrowd ();
		GAinedXp = 0;
	//	AdManager.instance.RequestInterstitial();
		ShowAdsOnGameStart ();
		IsInstructionAvlaiable	= LevelData.Instance.mi_Level == 1 ? true : false;

		//TimuzMoreGames.GamePage = "inthegame";
	}

	void SetDataOnStart ()
	{
		_gAccelSlider = GameObject.Find ("Accelerometer").GetComponent<Slider> ();
		_gDirectionChangeSlider = GameObject.Find ("DirectionChangeSlider").GetComponent<Slider> ();
		_WarningText = GameObject.Find ("WarningText").GetComponent<Text> ();
		_gSpeed = GameObject.Find ("SpeedText").GetComponent<Text> ();
		_SpeedLimitText = GameObject.Find ("MaxSpeedText").GetComponent<Text> ();




		_SoundBtn	= GameObject.Find ("Sound").GetComponent<Image> ();

		_rateText = GameObject.Find ("RateText").GetComponent<Text> ();
		_shareText = GameObject.Find ("ShareText").GetComponent<Text> ();

//		_gDirectChangeBase	= GameObject.Find("TrackChangerBase");
//		_gDirectChangeInput	= GameObject.Find("Slider");
//		_gHalt				= GameObject.Find("Halt");
//		_gHornImg			= GameObject.Find("Horn").GetComponent<GUITexture>();

		_LevelComplete = GameObject.Find ("LevelComplete").GetComponent<ButtonAnims> ();
		_LevelFailed = GameObject.Find ("LevelFailed").GetComponent<ButtonAnims> ();
		if(GameObject.Find ("LevelCrashed"))_LevelCrashed = GameObject.Find ("LevelCrashed").GetComponent<ButtonAnims> ();
		if (GameObject.Find ("CrashResetAlert"))crashResetAlertText = GameObject.Find ("CrashResetAlert").GetComponent<Text> ();
		if(crashResetAlertText)crashResetAlertText.enabled = false;
		_Pause = GameObject.Find ("PausePopUp").GetComponent<ButtonAnims> ();

		if (LevelData.Instance.mi_Level == 1) {
			_TutorialComplete = GameObject.Find ("TutorialComplete").GetComponent<ButtonAnims> ();
		}

		_GamePlayUI = GameObject.Find ("GamePlayUICanvas").transform;
		if (GlobalVariables.isMultiPlayerMode) {
			_ServerSearchingUI = GameObject.Find ("ServerSearching").transform;
			_MapUI = GameObject.Find ("MapPanel").transform;
		}

		_ExitPopUp = GameObject.Find ("ExitCanvas").GetComponent<ButtonAnims> ();
		_RatePopUp = GameObject.Find ("RateCanvas").GetComponent<ButtonAnims> ();
		_SharePopUp = GameObject.Find ("ShareCanvas").GetComponent<ButtonAnims> ();

		if (!GlobalVariables.isMultiPlayerMode)
			_KMText = GameObject.Find ("KM-Dist").GetComponent<Text> ();


		print (levelManager.LevelMode + " : lm");

		if (levelManager.LevelMode) {
			_StopsText = GameObject.Find ("AllStopsText").GetComponent<Text> ();
			_XPText = GameObject.Find ("XPGained").GetComponent<Text> ();

			_StopsText.text = levelManager.stationsreached + "/" + levelManager.mee.NumofStations;

			_star1 = GameObject.Find ("Star1");
			_star2 = GameObject.Find ("Star2");
			_star3 = GameObject.Find ("Star3");





			// Level Complete  
			_LC_S1 = GameObject.Find ("LC_Star1");
			_LC_S2 = GameObject.Find ("LC_Star2");
			_LC_S3 = GameObject.Find ("LC_Star3");
			_LC_S1.GetComponent<Image> ().enabled	= false;
			_LC_S2.GetComponent<Image> ().enabled	= false;
			_LC_S3.GetComponent<Image> ().enabled	= false;


			_LC_XP = GameObject.Find ("LC_XPEarned");
			_LC_Rewards = GameObject.Find ("LC_RewardEarned");

			_LC_Distance = GameObject.Find ("_LC_Distance");
			_LC_XP.GetComponent<Text> ().text	= "";
			_LC_Rewards.GetComponent<Text> ().text	= "";

			requiredXP1 = levelselectionNew.mee.mi_XP1;
			requiredXP2 = levelselectionNew.mee.mi_XP2;
			requiredXP3 = levelselectionNew.mee.mi_XP3;

		}


		print ("star data : " + requiredXP1 + ":" + requiredXP2);

		// Level Complete 
		if (GlobalVariables.isMultiPlayerMode) {
			_LC_Rewards = GameObject.Find ("LC_RewardEarned");
			_LC_TotalCoins = GameObject.Find ("LC_TotalCoins");
			_LC_Rewards.GetComponent<Text> ().text	= "";
			_LC_TotalCoins.GetComponent<Text> ().text	= "";
		}
		// LEVEL Failed
		if (!GlobalVariables.isMultiPlayerMode) {
			_LF_Coins = GameObject.Find ("LF_Coins");
			_LF_Distance = GameObject.Find ("LF_Distance");




		}

		_LF_TotalCoins = GameObject.Find ("LF_TotalCoins");

		if (GlobalVariables.isMultiPlayerMode) {
			SetInviisbleLayer (_ServerSearchingUI.transform);
		}

		SetInviisbleLayer (_LevelComplete.transform);
		SetInviisbleLayer (_LevelFailed.transform);
		SetInviisbleLayer (_Pause.transform);
		SetInviisbleLayer (_ExitPopUp.transform);
		SetInviisbleLayer (_SharePopUp.transform);
		SetInviisbleLayer (_RatePopUp.transform);
		if(_LevelCrashed)SetInviisbleLayer (_LevelCrashed.transform);
			
		_scrLevelData	= GameObject.Find ("LevelData").GetComponent<LevelData> ();

		GlobalVariables.iCurrentLevel	= _scrLevelData.mi_Level;

		if (!GlobalVariables.isMultiPlayerMode) {
			if (levelManager.LevelMode) {
				GameTimerHandler.Instance.UpdateTime (levelselectionNew.mee.Timeval [levelselectionNew.OpenLevelNUm]);

			} else {
				GameTimerHandler.Instance.UpdateTime (_scrLevelData.mf_TimeInSec [PatternManager.Instance.nextPatternId]);
			}
		}			

		if (GameTimerHandler.Instance && GlobalVariables.iCurrentLevel != 1)
			GameTimerHandler.Instance.isTimmerUpdate	= true;

//		requiredXP1 = _scrLevelData.mi_XPRequired1;
//		requiredXP2 = _scrLevelData.mi_XPRequired2;
//		requiredXP3 = _scrLevelData.mi_XPRequired3;

		isLightsOn	= false;

		CheckSoundImage ();

		#if ADS_SETUP
		_rateText.text = "Love Indian Train Games 2017! Please Rate with 5 stars. It'll help us a lot!";
		_shareText.text = "Love playing Indian Train Games 2017? Please share.";

		_rateText.text = GameConfigs2015.Rate_text;
		_shareText.text = GameConfigs2015.Share_text;
		#else
		_rateText.text = "Love Indian Train Games 2021! Please Rate with 5 stars. It'll help us a lot!";
		_shareText.text = "Love playing Indian Train Games 2021? Please share.";
		#endif

		//print ("GameConfigs2015.Rate_text : "+GameConfigs2015.Rate_text);
		if (!GlobalVariables.isMultiPlayerMode) {
			
			if (_LF_Title) {
				_LF_Title = GameObject.Find ("LF_Title");
				_LF_Title.GetComponent<Text> ().text = "";
			}
		}
	}

	//Sprite getTextureForCurrentLevel ()
	//{
	//	//Sprite _tex = new Sprite ();
	////	return _tex;
	//}

	void Update ()
	{
		if (GlobalVariables.isMultiPlayerMode && IsGameResult) {
			Debug.Log ("IsGameResult Show");
			if (mb_IsMasterPalyer) {
				OnLevelCompleted ();
			} else {
				OnLevelFailed ();
			}
			IsGameResult = false;
		}


		UpdatedSpeed ();
		TouchHandler ();
		if (levelManager.LevelMode) {
			OnUpdatedXPValues (); 
			//arjun
			//Edit By Vaibhav
		}
		OnDirectionChangeSlider ();

	}

	int xpToDisplay;
	int rewardsToDisplay;
	float speedOfUpdateScore;
	float _fValIncrementSpeed = 15;

	/*
	void LevelCompleteUpdate ()
	{
		if (xpToDisplay < mi_XPPoint) {
			xpToDisplay += (int)(mi_XPPoint / _fValIncrementSpeed);    
			if (xpToDisplay > mi_XPPoint)
				xpToDisplay	= mi_XPPoint;
		}
		if (rewardsToDisplay < coinsColledted) 
		{
			rewardsToDisplay += (int)(coinsColledted / _fValIncrementSpeed); 
			if (rewardsToDisplay > coinsColledted)
				rewardsToDisplay	= coinsColledted;
			_LC_Rewards.GetComponent<Text> ().text = "" + rewardsToDisplay;
		}
	}
	*/

	void TouchHandler ()
	{

		OnAccelerometerSlider ();

#if UNITY_EDITOR
		if (Input.GetMouseButtonDown (0))
			CheckForInputs (Input.mousePosition, TouchPhase.Began);
		else if (Input.GetMouseButtonUp (0)) {
			CheckForInputs (Input.mousePosition, TouchPhase.Ended);
		} else if (Input.GetMouseButton (0))
			CheckForInputs (Input.mousePosition, TouchPhase.Moved);


#elif UNITY_ANDROID
		if(Input.touchCount <= 0)
			return;
		
		Touch[] _touches	= Input.touches;
//		for(int i = 0 ; i < _touches.Length ; i++)
		{
			Touch _touch	= _touches[0];
			CheckForInputs(_touch.position, _touch.phase);

		}
#endif
	}


	#region UI

	[HideInInspector]
	public float yLowerLimitOfAccel = 0.1f;
	[HideInInspector]
	public float yHigherLimitOfAccel	= 0.35f;
	bool isHold	= false;
	[HideInInspector]
	public bool mb_IsBreak = false,mb_IsAIBreak = false;
	//	public Camera UICamera;

	// CHANGE DIRECTION
	[Space (10)]
	[SerializeField] Vector3 startButtonPos;
	Vector3 StartTouchPos;
	Vector3 CurrectTouchPos;
	float xLowerLimitOfDirectionChange = 0.05f;
	float xHigherLimitOfDirectionChange	= 0.23f;

	[HideInInspector]public bool isReverse = false;

	void CheckForInputs (Vector3 _touchPos, TouchPhase _touchPhase)
	{
		
	}

	void OnAccelerometerSlider ()
	{
		if (isTutorialCompletePopUpShow) {
			return;
		}
		if (_gAccelSlider && (!IsInstructionAvlaiable || (IsInstructionAvlaiable && GlobalVariables.mInsState == e_INSTRUCTION_STATE.Accelration)) && !isReverse) {
			f_DiffInSpeed	= _gAccelSlider.value;

			if (LevelEnabler.TrainDirection == -1 && _gAccelSlider.value <= 0f) {
				f_DiffInSpeed = 0f;
			}
			if (IsInstructionAvlaiable && f_DiffInSpeed > 0.5f) {
				ChangeInstructionState (e_INSTRUCTION_STATE.Break);
			}
		}
	}

	void OnDirectionChangeSlider ()
	{
		
		if (isTutorialCompletePopUpShow || mDirectionToChange == eDIRECTION_TO_CHANGE.None)
			return;

		if (_gDirectionChangeSlider.value > 0.1f) {
			CalculateDirectionChange (eDIRECTION_TO_CHANGE.Right);
		} else if (_gDirectionChangeSlider.value < -0.1f) {
			CalculateDirectionChange (eDIRECTION_TO_CHANGE.Left);
		}

		//Debug.Log ("--Directin Change::" + _gDirectionChangeSlider.value);
	}

	[HideInInspector]
	public bool isTutorialCompletePopUpShow;

	public void OnDirectionChnageSliderClick ()
	{
		OnDirectionChangeSlider ();
	}

	public void OnButtonClick (string _str)
	{
		Debug.Log (GlobalVariables.mGameState + " : " + _str + " : " + IsInstructionAvlaiable + " : " + GlobalVariables.mInsState);
		if (isTutorialCompletePopUpShow) {
			return;
		}
		switch (GlobalVariables.mGameState) {
		case eGAME_STATE.GamePlay:
			if (_str == "CameraButton" && (!IsInstructionAvlaiable || (IsInstructionAvlaiable && GlobalVariables.mInsState == e_INSTRUCTION_STATE.CameraButton))) {
				SoundController.Instance.OnButtonClick ();
				if (IsInstructionAvlaiable) {
					if (GlobalVariables.mInsSubState == e_INSTRUCTION_STATE.CameraFreeHandMode) {
						GlobalVariables.mInsSubState = e_INSTRUCTION_STATE.CameraDriverMode;
						ChangeInstructionState (e_INSTRUCTION_STATE.CameraButton);
					} else {
						iCameraCurrentView	= 3;
						GlobalVariables.mInsSubState = e_INSTRUCTION_STATE.CameraFreeHandMode;
						ChangeInstructionState (e_INSTRUCTION_STATE.TimeForLevels);
					}
				}
				OnChangeCameraButtonClick ();


			} else if (_str == "Horn" && (!IsInstructionAvlaiable || (IsInstructionAvlaiable && GlobalVariables.mInsState == e_INSTRUCTION_STATE.Horn)))
				OnHornClick ();
			else if (_str == "InsOkButton") {
				SoundController.Instance.OnButtonClick ();
				if (GlobalVariables.mInsState == e_INSTRUCTION_STATE.TimeForLevels)
					ChangeInstructionState (e_INSTRUCTION_STATE.SpeedLimit);
				else if (GlobalVariables.mInsState == e_INSTRUCTION_STATE.SpeedLimit)
					ChangeInstructionState (e_INSTRUCTION_STATE.Speed);
				else if (GlobalVariables.mInsState == e_INSTRUCTION_STATE.Speed)
					ChangeInstructionState (e_INSTRUCTION_STATE.XpPoints);
				else if (GlobalVariables.mInsState == e_INSTRUCTION_STATE.XpPoints)
					ChangeInstructionState (e_INSTRUCTION_STATE.Accelration);
//				else if (GlobalVariables.mInsState == e_INSTRUCTION_STATE.Reverse) {
//					GameTimerHandler.Instance.isTimmerUpdate	= true;
//					ChangeInstructionState (e_INSTRUCTION_STATE.NoInstruction);
//				}
				else if (GlobalVariables.mInsState == e_INSTRUCTION_STATE.Horn) {
					isTutorialCompletePopUpShow = true;
					Debug.Log ("TutorialComplete");
						//if (AdsManager.Instance) {
						//	AdsManager.Instance.RequestToShowAds (ADS_STATE.LEVEL_COMPLETE, 2.0f);
						//}
					//	Admanager.instance.ShowFullScreenAd();
					PlayerPrefs.SetInt ("TutorialComplete", 1);
					ChangeInstructionState (e_INSTRUCTION_STATE.NoInstruction);
					_TutorialComplete.CallAllAnims ();
					UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("GameUI");
				}

			} else if ((_str == "PauseButton") && !IsInstructionAvlaiable) {
				OnPauseClick ();
				SoundController.Instance.OnButtonClick ();
			} else if (_str == "Light" && !IsInstructionAvlaiable) {
				isLightsOn	= !isLightsOn;
				SoundController.Instance.OnButtonClick ();
				_trainMovementScript.SetLights (isLightsOn);
			} else if ((_str == "ReverseBtn") && !IsInstructionAvlaiable) {
				/*StopTrainImediatly ();*/
				SoundController.Instance.OnButtonClick ();

				AdsManagerRwd.Instance.ShowRewardedAd((bool status) => {
					StopTrainImediatly();
					TakeReverseDirection();
				});

					//#if VIDEOTESTING || UNITY_EDITOR
					//VWatchedContinue();
					//else
					//GoogleMobileAdsDemoScript.mee.ShowRewardBasedVideo();
					//#endif
					//	Admanager.instance.ShowRewardedAd();
					//  AdManager.instance.showRewardVideo();   venkat
				}
			else if((_str == "CloseCrash") && !IsInstructionAvlaiable) {
				//levelCrashed = false;
				TrainCollisionHandler.mee.lFailCall ();
                    //AdmobAds.instance.showVideoAd();
                  //  AdManager.instance.showInterstitial();  venkat
				}
//			else if (_str == "UnityBackButton") {
//				SoundController.Instance.OnButtonClick();
//
//				OnPauseClick ();
                
//			}

			else if (_str == "UnityBackButton" && !IsInstructionAvlaiable) {
				SoundController.Instance.OnButtonClick ();
				OnShowExitPopUp (eGAME_STATE.GamePlay);
			}
			break;
		case eGAME_STATE.LevelComplete:
			if (_str == "Retry") {
				OnRetryClick ();
				//	AdManager.instance.ShowInterstitial();
					SoundController.Instance.OnButtonClick ();
			} else if (_str == "Home") {
				OnHomeClick ();
				//	AdManager.instance.ShowInterstitial();
					SoundController.Instance.OnButtonClick ();
			} else if (_str == "UnityBackButton") {
				SoundController.Instance.OnButtonClick ();
				OnShowExitPopUp (eGAME_STATE.LevelComplete);
			} else if (_str == "MoreGames") {
				SoundController.Instance.OnButtonClick ();
				//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.MoreGames);
			} else if (_str == "Share") {
				Debug.Log ("share clicked ");
				SoundController.Instance.OnButtonClick ();
				OnShareButtonInPopUpClick ();
			} else if (_str == "RewardVideo") {
				SoundController.Instance.OnButtonClick ();
					//	GoogleMobileAdsDemoScript.mee.ShowRewardBasedVideo ();
					//AdmobAds.instance.showVideoAd();
				//	Admanager.instance.ShowRewardedAd();
			}
			break;
		case eGAME_STATE.LevelFailed:
			if (_str == "Retry") {
				SoundController.Instance.OnButtonClick ();
				OnRetryClick ();
					//	Admanager.instance.ShowFullScreenAd();
				//	AdManager.instance.ShowInterstitial();
			} else if (_str == "Home") {
				OnHomeClick ();
				SoundController.Instance.OnButtonClick ();
					//AdManager.instance.ShowInterstitial();
					//	Admanager.instance.ShowFullScreenAd();
				} else if (_str == "UnityBackButton") {
				SoundController.Instance.OnButtonClick ();
				OnShowExitPopUp (eGAME_STATE.LevelFailed);
			} else if (_str == "MoreGames") {
				SoundController.Instance.OnButtonClick ();
			//	ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.MoreGames);
			}else if ((_str == "ReverseBtn") && !IsInstructionAvlaiable) {
				/*StopTrainImediatly ();*/
				SoundController.Instance.OnButtonClick ();
				GlobalVariables.mGameState	= eGAME_STATE.GamePlay;
				StopTrainImediatly ();
				TakeReverseDirection ();
				//	Admanager.instance.ShowRewardedAd();
					//#if VIDEOTESTING || UNITY_EDITOR
					//VWatchedContinue();
					//#else
					//GoogleMobileAdsDemoScript.mee.ShowRewardBasedVideo();
					//#endif

				}
			else if((_str == "CloseCrash") && !IsInstructionAvlaiable) {
				//levelCrashed = false;
				TrainCollisionHandler.mee.lFailCall ();
				//	AdmobAds.instance.showVideoAd();
				}
			break;
		case eGAME_STATE.Instruction:
			if (_str == "InsOkButton") {
				SoundController.Instance.OnButtonClick ();
				Time.timeScale	= 1;
				GlobalVariables.mGameState	= eGAME_STATE.GamePlay;
				ChangeInstructionState (e_INSTRUCTION_STATE.NoInstruction);
			} else if (_str == "Halt") {
				SoundController.Instance.OnButtonClick ();
				Time.timeScale	= 1;
				Debug.Log ("Halt");
				GlobalVariables.mGameState	= eGAME_STATE.GamePlay;
				ChangeInstructionState (e_INSTRUCTION_STATE.NoInstruction);
			}
			break;
		case eGAME_STATE.Pause:
			if (_str == "Resume") {
				SoundController.Instance.OnButtonClick ();
				ClosePausePopUp ();
			} else if (_str == "Retry") {
				SoundController.Instance.OnButtonClick ();
				OnRetryClick ();
			} else if (_str == "Sound") {
				SoundController.Instance.OnButtonClick ();
				SoundController.Instance.ChangeSoundState ();
				CheckSoundImage ();
			} else if (_str == "Home") {
				SoundController.Instance.OnButtonClick ();
				OnHomeClick ();
			} else if (_str == "MoreGames") {
				SoundController.Instance.OnButtonClick ();
				//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.MoreGames);
			}
			break;
		case eGAME_STATE.Rate:
			if (_str == "UnityBackButton") {
				SoundController.Instance.OnButtonClick ();
				CloseRatePopUp ();
			} else if (_str == "Rate") {
				SoundController.Instance.OnButtonClick ();
				OnRateClick ();
			} else if (_str == "Cancel") {
				SoundController.Instance.OnButtonClick ();
				OnCancelRateClick ();
			} else if (_str == "Later") {
				SoundController.Instance.OnButtonClick ();
				OnLaterClick ();
			}
			break;
		case eGAME_STATE.ExitPopUp:
			if (_str == "UnityBackButton") {
				
				SoundController.Instance.OnButtonClick ();

				if (InPlayArea) {
					OnExitClick ();
				} else {
					OnPlayOnClick ();
				}
			} else if (_str == "Exit") {
				if (InPlayArea) {
					ShowFailRef ();
				} else {
					OnExitClick ();
				}
			} else if (_str == "PlayOn") {
				if (InPlayArea) {
					GamePlayManager.Instance.CloseExit ();
				} else {
					OnPlayOnClick ();
				}
				SoundController.Instance.OnButtonClick ();
			}
			break;
		case eGAME_STATE.Share:
			if (_str == "UnityBackButton") {
				SoundController.Instance.OnButtonClick ();
				OnShareCancelClick ();
			} else if (_str == "Share") {
				SoundController.Instance.OnButtonClick ();
				OnShareButtonInPopUpClick ();
			} else if (_str == "Cancel") {
				SoundController.Instance.OnButtonClick ();
				OnShareCancelClick ();
			}
			break;
		}
	}

	public void VWatchedContinue(){
		GlobalVariables.mGameState	= eGAME_STATE.GamePlay;
		StopTrainImediatly ();
		TakeReverseDirection ();
	}



	public void CheckSoundImage ()
	{
		if (PlayerPrefs.GetInt (GlobalVariables.sSoundsString) == 1) {
			_SoundBtn.sprite	= _SoundImgs [1];
		} else
			_SoundBtn.sprite	= _SoundImgs [0];
	}

	[HideInInspector]
	public int iCameraCurrentView	= 0;

	public void OnChangeCameraButtonClick ()
	{
		/* 0 - FreeHand
		   1 - Driver view
		   2 - Station View
		   3 - Train Full View
		   
		   10 - InitialCameraPos
		   50 - Crash View
		   51 - LevelComplete view
		*/
		if (isStaticCamera)
			return;


		iCameraCurrentView++;
		if (iCameraCurrentView > 3)
			iCameraCurrentView	= 0;

		if (iCameraCurrentView == 0) {
			GlobalVariables.mCameraMode	= e_CAMERA_MODE.FreeHandView;
		} else if (iCameraCurrentView == 1) {
			GlobalVariables.mCameraMode	= e_CAMERA_MODE.DriverView;
		} else if (iCameraCurrentView == 2) {
			GlobalVariables.mCameraMode	= e_CAMERA_MODE.StationView;
		} else if (iCameraCurrentView == 3)
			GlobalVariables.mCameraMode	= e_CAMERA_MODE.TrainFullView;
		CameraSmoothFollowWithRotation.Instance.SetTargetForCameraToFollow ();
	}

	void SetCameraBasedOnTag (int tag)
	{
		iCameraCurrentView	= tag;
		if (iCameraCurrentView == 0) {
			GlobalVariables.mCameraMode	= e_CAMERA_MODE.FreeHandView;
		} else if (iCameraCurrentView == 1) {
			GlobalVariables.mCameraMode	= e_CAMERA_MODE.DriverView;
		} else if (iCameraCurrentView == 2) {
			GlobalVariables.mCameraMode	= e_CAMERA_MODE.StationView;
		} else if (iCameraCurrentView == 3) {
			GlobalVariables.mCameraMode	= e_CAMERA_MODE.TrainFullView;
		} else if (iCameraCurrentView == 10) {
			GlobalVariables.mCameraMode	= e_CAMERA_MODE.CrashView;
		}
		CameraSmoothFollowWithRotation.Instance.SetTargetForCameraToFollow ();
	}

	public static bool goingSlow = false;
	public static bool goingSpeed = false;
	public static bool crowdSnd = false;

	public TrainMovementScript _trainMovementScript;

	void UpdatedSpeed ()
	{
		if (!_trainMovementScript) {
			return;
		}

		if (GlobalVariables.isMultiPlayerMode && PointerHandler.Instance) {
			PointerHandler.Instance.UpdateToProcess ();
		}

		if (_trainMovementScript.m_fTrainSpeed >= 50 && goingSpeed == false) {
			//Debug.Log("play fast");
			SoundController.Instance.TrainFastSound ();
			goingSpeed = true;
			goingSlow = false;
		} else if (_trainMovementScript.m_fTrainSpeed >= 1 && _trainMovementScript.m_fTrainSpeed <= 49 && goingSlow == false) {

			SoundController.Instance.TrainSlowSound ();
			goingSlow = true;
			goingSpeed = false;
			crowdSnd = true;
		} else if (_trainMovementScript.m_fTrainSpeed <= 0 && crowdSnd == true) {

			crowdSnd = false;
			SoundController.Instance.playTrainidle ();
			goingSlow = false;
			goingSpeed = false;
		}

		if (_trainMovementScript) {
			if (LevelEnabler.TrainDirection == 1) {
				_gSpeed.text	= "" + _trainMovementScript.m_fTrainSpeed;//+"\n Input Speed :"+f_DiffInSpeed;
			} else {

				_gSpeed.text	= "R" + _trainMovementScript.m_fTrainSpeed;//+"\n Input Speed :"+f_DiffInSpeed;
				if (_trainMovementScript.m_fTrainSpeed == 0) {
					
					LevelEnabler.ReverseDone = true;
				}

			}
			if (_trainMovementScript.m_fTrainSpeed > _trainMovementScript.m_iSpeedLimiterValue)
				_gSpeed.color	= _cColorForWaringText;
			else
				_gSpeed.color	= _cColorForNormalText;
		} else
			_gSpeed.text	= "" + 0;
	}

	float OnWarningTextYPosition	= 0;

	public void SetWarningText (string _str, Color _color, int delay = 0)
	{
		Debug.Log("show warning: "+ _str);

		_WarningText.text	= _str;
		_WarningText.gameObject.GetComponent<RectTransform> ().anchoredPosition3D	= Vector3.zero;
		OnWarningTextYPosition	= _WarningText.gameObject.transform.position.y;
		iTween.MoveTo (_WarningText.gameObject, iTween.Hash ("y", (OnWarningTextYPosition + 400), "time", 1.5f, "delay", delay, "easetype", iTween.EaseType.linear));
		_WarningText.color	= _color;
		Invoke ("disableWaringingText", 1.5f);
	}

	void disableWaringingText ()
	{
		_WarningText.text	= "";
		_WarningText.gameObject.GetComponent<RectTransform> ().anchoredPosition3D	= Vector3.zero;
	}

	public void SetSpeedLimit (int speedLimit)
	{
		_SpeedLimitText = GameObject.Find ("MaxSpeedText").GetComponent<Text> ();
		//Debug.Log (_SpeedLimitText + " ---" + speedLimit);
		_SpeedLimitText.text	= "" + speedLimit;
	}

	public void OnSignalBroke ()
	{	
		if (levelManager.LevelMode) {
			SetWarningText ("Signal broke -100xp", Color.red);
			mi_XPPoint	-= 100;

			if (mi_XPPoint <= 0)
				mi_XPPoint	= 0;
			calculateStarsCountInUpdate ();
		} else {

			if (GlobalVariables.isMultiPlayerMode) {
				SetWarningText ("Signal broke", Color.red);

			} else {
				SetWarningText ("Signal broke -100 Coins", Color.red);
			}
			InGameCoins = InGameCoins - 100;

			if (InGameCoins <= 0)
				InGameCoins	= 0;

			if (GameManager.Instance && GameManager.Instance.totalCoins >= 100) {
				GameManager.Instance.totalCoins = GameManager.Instance.totalCoins - 100;
				PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);
			}

		}



	}

	public void OnSignalBrokeMP ()
	{		
		SetWarningText ("Signal broke.", Color.red);
		StartCoroutine (ieRequestToFailLevel ());

	}


	IEnumerator ieRequestToFailLevel ()
	{
	
		yield return new WaitForSeconds (1.0f);

	//	#if CUSTOM_CLIENT
	//	if (TrainMultiPlayerManager.IsMasterClient) {
	//		#else
	//	if (PhotonNetwork.isMasterClient) {
	//		#endif
	//	//	if(OnNetworkReceiver.Instance)OnNetworkReceiver.Instance.OnRequestSetToBoolean (mGameResult.PlayerLose, false);
	//	} else {
	//		//OnNetworkReceiver.Instance.OnRequestSetToBoolean (mGameResult.OppoentLose, false);
	//	}
	}

	#endregion


	#region HORN AREA

	// ****************** HORN AREA *********************//
	bool mb_HornEnabled	= false;
	Texture _gHornImg;

	public void OnResetHornBool (bool isEnable)
	{
		if (isEnable == false && mb_HornEnabled) {


			if (levelManager.LevelMode) {
				SetWarningText ("- 100XP", Color.red);
				mi_XPPoint	-= 100;

				if (mi_XPPoint <= 0)
					mi_XPPoint	= 0;
				calculateStarsCountInUpdate ();
			} else {


			

				SetWarningText ("-50 Coins", Color.red);

				InGameCoins	= InGameCoins - 50;

				if (InGameCoins <= 0)
					InGameCoins	= 0;
			
				if (GameManager.Instance && GameManager.Instance.totalCoins >= 50) {
					GameManager.Instance.totalCoins = GameManager.Instance.totalCoins - 50;
					PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);
				}

			}
//			if (mi_XPPoint <= 0)
//				mi_XPPoint	= 0;
		}

		mb_HornEnabled	= isEnable;
	}

	public void OnHornClick ()
	{
		if (!mb_HornEnabled)
			return;

		mb_HornEnabled	= false;



		if (levelManager.LevelMode) {
			mi_XPPoint	+= 250;
			calculateStarsCountInUpdate ();

			SetWarningText ("+250XP", Color.white);
		} else {
			SetWarningText ("+250 Coins", Color.white);
			InGameCoins	= InGameCoins + 250;

			if (GameManager.Instance) {
				GameManager.Instance.totalCoins = GameManager.Instance.totalCoins + 250;
				PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);
			}

		}



	}

	#endregion

	#region DIRECTION CHANGE AREA

	// ****************** DIRECTION CHANGE *********************//
	[HideInInspector]
	public eDIRECTION_TO_CHANGE mDirectionToChange	= eDIRECTION_TO_CHANGE.None;
	int mi_DirectionChangeArrayVal	= 0,mi_DirectionChangeArrayValAI = 0;

	public void ResetDirectionChangeValue (eDIRECTION_TO_CHANGE changeInDirection)
	{
		//	Debug.Log ("reset direction " + changeInDirection);
		mDirectionToChange	= changeInDirection;
	}

	public void RequestToResetDirectionChangeValue ()
	{
		
		mi_DirectionChangeArrayVal = 0;

		RenderSettings.skybox = PatternManager.Instance.skyboxes [PatternManager.Instance.nextPatternId];

		foreach (GameObject lObject in PatternManager.Instance.lights) {
			lObject.SetActive (false);
		}

		PatternManager.Instance.lights [PatternManager.Instance.nextPatternId].SetActive (true);

	}

	//public static int indCount = 0;
	public GameObject PathChanger_obj;

	void CalculateDirectionChange(eDIRECTION_TO_CHANGE swipedDirection, bool fromAI = false)
	{
		if (mDirectionToChange == swipedDirection)
		{


			if (!GlobalVariables.isMultiPlayerMode && levelManager.LevelMode)
			{
				//print (PathChanger_obj.name+" a mDirectionToChange : "+mDirectionToChange);
				//PathChanger_obj.name = "zeroooo";
				PathChanger_obj.GetComponent<TrackChangeHandler>().OnChangeTrackClick();
				PathChanger_obj.transform.GetChild(0).gameObject.SetActive(false);
			}
			else if (!GlobalVariables.isMultiPlayerMode && mi_DirectionChangeArrayVal < PatternManager.Instance.Patterns[PatternManager.Instance.currentPatternId]._scrTrackChange.Length)
			{
				PatternManager.Instance.Patterns[PatternManager.Instance.currentPatternId]._scrTrackChange[mi_DirectionChangeArrayVal].trackChanger.OnChangeTrackClick();
				PatternManager.Instance.Patterns[PatternManager.Instance.currentPatternId]._scrTrackChange[mi_DirectionChangeArrayVal].trackChanger.transform.GetChild(0).gameObject.SetActive(false);
				mi_DirectionChangeArrayVal++;
				mDirectionToChange = eDIRECTION_TO_CHANGE.None;

			}
			//		#if CUSTOM_CLIENT
			//		else if (GlobalVariables.isMultiPlayerMode && TrainMultiPlayerManager.IsMasterClient && mi_DirectionChangeArrayVal < ThemesManager.Instance.trackChangePlayer1.Length) {
			//			#else
			//		else if (GlobalVariables.isMultiPlayerMode && PhotonNetwork.isMasterClient && mi_DirectionChangeArrayVal < ThemesManager.Instance.trackChangePlayer1.Length) {
			//			#endif
			//			Debug.Log ("--TrackChange Multiplayer Ai IsMaster--"+ThemesManager.Instance.name+" FromAI??"+fromAI+" swipedDirection:: "+swipedDirection);
			//			if(!fromAI){
			//				ThemesManager.Instance.trackChangePlayer1 [mi_DirectionChangeArrayVal].trackChanger.OnChangeTrackClick ();
			//				ThemesManager.Instance.trackChangePlayer1 [mi_DirectionChangeArrayVal].trackChanger.transform.GetChild (0).gameObject.SetActive (false);
			//			}else{
			//				if (TrainMultiPlayerManager.Instance.newPlayerObject2 && TrainMultiPlayerManager.Instance.newPlayerObject2.GetComponent <TrainCollisionMultiPlayerHandler> ().isAI) {					
			//					ThemesManager.Instance.trackChangePlayer2 [mi_DirectionChangeArrayValAI].trackChanger.OnChangeTrackClick ();
			//					ThemesManager.Instance.trackChangePlayer2 [mi_DirectionChangeArrayValAI].trackChanger.transform.GetChild (0).gameObject.SetActive (false);
			//					mi_DirectionChangeArrayValAI++;
			//				}
			//			}
			//			//if(OnNetworkReceiver.Instance)OnNetworkReceiver.Instance.OnRequestSetToInteger (PhotonTargets.Others, mi_DirectionChangeArrayVal);
			//			if(!fromAI){
			//				mi_DirectionChangeArrayVal++;
			//			}
			//			mDirectionToChange	= eDIRECTION_TO_CHANGE.None;

			//		}
			//		#if CUSTOM_CLIENT
			//		else if (GlobalVariables.isMultiPlayerMode && !TrainMultiPlayerManager.IsMasterClient && mi_DirectionChangeArrayVal < ThemesManager.Instance.trackChangePlayer2.Length) {
			//			#else
			//		else if (GlobalVariables.isMultiPlayerMode && !PhotonNetwork.isMasterClient && mi_DirectionChangeArrayVal < ThemesManager.Instance.trackChangePlayer2.Length) {
			//			#endif
			//			Debug.Log ("--TrackChange Multiplayer Ai IsNotMaster--"+ThemesManager.Instance.name+ " ");
			//			ThemesManager.Instance.trackChangePlayer2 [mi_DirectionChangeArrayVal].trackChanger.OnChangeTrackClick ();
			//			ThemesManager.Instance.trackChangePlayer2 [mi_DirectionChangeArrayVal].trackChanger.transform.GetChild (0).gameObject.SetActive (false);
			//			if (!TrainMultiPlayerManager.Instance.newPlayerObject2.GetComponent <TrainCollisionMultiPlayerHandler> ().isAI) {
			//			//	OnNetworkReceiver.Instance.OnRequestSetToInteger (PhotonTargets.MasterClient, mi_DirectionChangeArrayVal);
			//			}else{
			//				ThemesManager.Instance.trackChangePlayer1 [mi_DirectionChangeArrayVal].trackChanger.OnChangeTrackClick ();
			//				ThemesManager.Instance.trackChangePlayer1 [mi_DirectionChangeArrayVal].trackChanger.transform.GetChild (0).gameObject.SetActive (false);
			//			}

			//			Debug.Log ("--Is Player with AI--"+ThemesManager.Instance.name);

			//			mi_DirectionChangeArrayVal++;
			//			mDirectionToChange	= eDIRECTION_TO_CHANGE.None;
			//		}
			//	}
			//}
		}
	}
	#endregion

	//*****************************************************
	// STATIC CAMERA ON BRIDGE
	//*****************************************************
	bool isStaticCamera	= false;

	public void OnStaticCameraEntry (Transform _transform = null)
	{
		isStaticCamera	= true;
		cameraOldTag	= iCameraCurrentView;

		if (_transform != null)
			CameraSmoothFollowWithRotation.Instance.SetCameraToStatic (_transform, e_CAMERA_MODE.BridgeView_1);
		else
			CameraSmoothFollowWithRotation.Instance.SetCameraToStatic (_transform, e_CAMERA_MODE.DriverView);
	}

	//[HideInInspector]
	public bool isTrainOnTunnel;

	public void OnTunnelEnterTrain (bool isOnTunnel)
	{
		PatternManager.Instance.RequestToEnableEffect (PatternManager.Instance.currentPatternId, isOnTunnel);
		isTrainOnTunnel = isOnTunnel;
		if (isTrainOnTunnel) {
			GlobalVariables.mCameraMode	= e_CAMERA_MODE.FreeHandView;
			CameraSmoothFollowWithRotation.Instance.SetTargetForCameraToFollow ();
		}
	}


	public void OnStaticCameraExit ()
	{
		isStaticCamera	= false;
		iCameraCurrentView	= cameraOldTag;
		SetCameraBasedOnTag (iCameraCurrentView);
	}


	#region Game State Changes

	int cameraOldTag = 0;

	public void OnStationStop ()
	{
		cameraOldTag = iCameraCurrentView;
		SetWarningText ("Station Reached", Color.green);
		SetCameraBasedOnTag (2);
		GlobalVariables.mGameState	= eGAME_STATE.Station;
		_trainMovementScript.OpenDoor ();

		if (!GlobalVariables.isMultiPlayerMode) {
			OnStationReacheStartAniamtion ();

			if (GameManager.Instance) {
				GameManager.Instance.stationCount = GameManager.Instance.stationCount + 1;
				PlayerPrefs.SetInt ("stationCount", GameManager.Instance.stationCount);		

				levelManager.stationsreached++;
			}

			if (levelManager.LevelMode) {

				_StopsText.text = levelManager.stationsreached + "/" + levelManager.mee.NumofStations;

			}

			print (levelManager.stationsreached + " station count : " + GameManager.Instance.stationCount);
		}

		print ("station count : " + GameManager.Instance.stationCount);


		if (!GlobalVariables.isMultiPlayerMode && !levelManager.LevelMode && GameTimerHandler.Instance.isTimmerUpdate) {
			Debug.Log ("Reached in Time");

			InGameCoins = InGameCoins + 500;
			SetWarningText ("+500 Coins", Color.white);

			if (GameManager.Instance) 
			{
				GameManager.Instance.totalCoins = GameManager.Instance.totalCoins + 500;
				PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);

			}
			GameTimerHandler.Instance.isTimmerUpdate = false;
		}

		Invoke ("CheckForLevelComplete", 5f);
		//AdmobAds.instance.showVideoAd();
	}


	int _iStationNo = 0;

	public void OnLevelFailed ()
	{
		Debug.Log ("######### LEVEL FAIL ##############");

		if (GlobalVariables.mGameState != eGAME_STATE.GamePlay) {
			return;
		}
		if (mb_isTimeUp && GameObject.Find ("TimesUpText"))
			GameObject.Find ("TimesUpText").GetComponent<Text> ().enabled = true;
		
		if (!GlobalVariables.isMultiPlayerMode && !levelCrashed && !mb_isTimeUp) {
			OnLevelCrashed ();
			return;
		}
		//AdmobAds.instance.showVideoAd();
		//Debug.Log ("######### LEVEL FAIL ############## " + gameConfigs.LcAdDelay);
		//if (AdsManager.Instance) {
		//	if (GlobalVariables.isMultiPlayerMode) {
		//		AdsManager.Instance.RequestToShowAds (ADS_STATE.LEVEL_FAIL, gameConfigs.LfAdDelay);//1.0
		//	} else {
		//		AdsManager.Instance.RequestToShowAds (ADS_STATE.LEVEL_FAIL, gameConfigs.LfAdDelay);//1.0
		//	}
		//}

	//	Admanager.instance.ShowFullScreenAd();
		SetCameraBasedOnTag (10);

		//SoundController.Instance.PlayLevelFailed ();
		GlobalVariables.mGameState	= eGAME_STATE.LevelFailed;
		mb_isLevelWin	= false;

		Invoke ("EnableLevelFailedPopUp", 0.5f);
		mb_IsBreak	= true;

		Invoke ("RequestToDisconnect", 3.0f);

		print ("submittt.....w " + GameManager.Instance.totalCoins);
		


		//	GSConfig.SubmitScore (PlayerPrefs.GetInt ("totalCoins", (GameManager.Instance.totalCoins)));
	}

	[HideInInspector]public bool levelCrashed = false;
	public void OnLevelCrashed()
	{
		if (_LevelCrashed != null)
		{
			StopTrainImediatly();
			SetVisibleLayer(_LevelCrashed.transform);
			_LevelCrashed.CallAllAnims();
			levelCrashed = true;
		}
		Debug.Log ("--OnLevelCrashed");
		SoundController.Instance.StopTrainSource();

	}



	void CheckForLevelComplete ()
	{
		Debug.Log ("CheckPoint");
		_scrLevelData.mi_NoOfStops--;
		if (GlobalVariables.isMultiPlayerMode && _scrLevelData.mi_NoOfStops <= 0) {
			
			Debug.Log ("OnLevelCompleted");


		} else {

			if (levelManager.LevelMode && levelManager.stationsreached >= levelManager.mee.NumofStations) {

				GlobalVariables.mGameState = eGAME_STATE.LevelComplete;
				Levels_LevelCompleted ();

			} else {


			
				_trainMovementScript.CloseDoor ();
							
				//print ( levelManager.mee.previousPatternId+ "current patrenid :"+PatternManager.Instance.currentPatternId);
				if (levelManager.LevelMode) {

					PatternManager.Instance.Patterns [levelManager.mee.preId]._stationInfo._StationTrigger.enabled = false;
					PatternManager.Instance.Patterns [levelManager.mee.preId]._stationInfo._StationStopPoint.SetActive (false);
					PatternManager.Instance.Patterns [levelManager.mee.preId]._stationInfo._StationRoadBlocker.SetActive (false);
				} else {
					PatternManager.Instance.Patterns [PatternManager.Instance.currentPatternId]._stationInfo._StationTrigger.enabled = false;
					PatternManager.Instance.Patterns [PatternManager.Instance.currentPatternId]._stationInfo._StationStopPoint.SetActive (false);
					PatternManager.Instance.Patterns [PatternManager.Instance.currentPatternId]._stationInfo._StationRoadBlocker.SetActive (false);
				}

				_iStationNo++;
				SetCameraBasedOnTag (cameraOldTag);

				if (!levelManager.LevelMode) {

					if (GameTimerHandler.Instance)
						GameTimerHandler.Instance.UpdateTime (_scrLevelData.mf_TimeInSec [PatternManager.Instance.nextPatternId]);

					GameTimerHandler.Instance.isTimmerUpdate = true;

				}
			

				GlobalVariables.mGameState	= eGAME_STATE.GamePlay;
				Debug.Log ("GoAgain....");

				Invoke ("GoAgain", 1);
			}
		}
	}

	void GoAgain ()
	{
		SetWarningText ("Go Ahead for Next Station", Color.white, 3);
		Debug.Log ("GoAgain");
	}


	void calculateStarsAndCoins_levels ()
	{
		GlobalVariables.iCurrentLevel = levelselectionNew.OpenLevelNUm + 1;

		print ("current level " + GlobalVariables.iCurrentLevel);
		string LevelStars = GlobalVariables.sStarsGainedInEachLevel + GlobalVariables.iCurrentLevel.ToString ();
		int oldStarCount	= PlayerPrefs.GetInt (LevelStars);
		Debug.Log ("Previous stars :" + oldStarCount + "    current stars : " + unlockedStars);


		if (oldStarCount < unlockedStars) {
			oldStarCount	= unlockedStars;
			PlayerPrefs.SetInt (LevelStars, oldStarCount);
		}



		int totalStarsInGame	= 0;
		for (int i = 1; i <= GlobalVariables.iTotalLevels; i++) {
			LevelStars	= GlobalVariables.sStarsGainedInEachLevel + i.ToString ();
			totalStarsInGame	+= PlayerPrefs.GetInt (LevelStars);
		}
		PlayerPrefs.SetInt (GlobalVariables.sTotalStarsGained, totalStarsInGame);
		Debug.Log ("Total Stars : " + totalStarsInGame);


		//***** COINS 
		int coinsCoint	= 0;
		if (unlockedStars == 1) {
			coinsCoint	= _scrLevelData.mi_CoinReward1;
		} else if (unlockedStars == 2) {
			coinsCoint	= _scrLevelData.mi_CoinReward2;
		} else if (unlockedStars == 3) {
			coinsCoint	= _scrLevelData.mi_CoinReward3;
		}
		coinsColledted	= coinsCoint;

		int totalCoins	= PlayerPrefs.GetInt ("totalCoins");

		totalCoins	+= coinsCoint;
		GameManager.Instance.totalCoins = totalCoins;

		PlayerPrefs.SetInt ("totalCoins", totalCoins);

		print (" tcoins-- " + PlayerPrefs.GetInt ("totalCoins"));

		// ************ SCORE
		//int totalScore	= PlayerPrefs.GetInt (GlobalVariables.sTotalScoreInGame);
		//totalScore	+= (int)mi_XPPoint;
		//PlayerPrefs.SetInt (GlobalVariables.sTotalScoreInGame, totalCoins);
		if (levelManager.LevelMode) {
			Invoke ("SetXPandStarsCount", 0.5f);

		}

	}

	int coinsColledted	= 0;

	void SetXPandStarsCount ()
	{
		rewardsToDisplay	= 0;
		xpToDisplay = 0;


		//_LC_XP.GetComponent<Text> ().text = "0";//+ mi_XPPoint;

		_LC_XP.GetComponent<Text> ().text = "" + mi_XPPoint;

		_LC_Distance.GetComponent<Text> ().text = "" + InGameDistance; // + coinsColledted;
		_LC_Rewards.GetComponent<Text> ().text = "" + coinsColledted;


		PlayerPrefs.SetInt ("totalCoins", PlayerPrefs.GetInt ("totalCoins") + coinsColledted);
		//PlayerPrefs.SetInt ("totalCoins", (GameManager.Instance.totalCoins+coinsColledted));
		print ("distance trav " + InGameDistance);

		//RequestToCounter (_LC_Distance.gameObject, 0, InGameDistance, 1.0f, 0.5f, "OnUpdateDistance");



		switch (unlockedStars) {
		case 0:
			_LC_S1.GetComponent<Image> ().enabled	= false;
			_LC_S2.GetComponent<Image> ().enabled	= false;
			_LC_S3.GetComponent<Image> ().enabled	= false;
			break;
		case 1:
			_LC_S1.GetComponent<Image> ().enabled	= true;
			_LC_S2.GetComponent<Image> ().enabled	= false;
			_LC_S3.GetComponent<Image> ().enabled	= false;
			break;
		case 2:
			_LC_S1.GetComponent<Image> ().enabled	= true;
			_LC_S2.GetComponent<Image> ().enabled	= true;
			_LC_S3.GetComponent<Image> ().enabled	= false;
			break;
		case 3:
			_LC_S1.GetComponent<Image> ().enabled	= true;
			_LC_S2.GetComponent<Image> ().enabled	= true;
			_LC_S3.GetComponent<Image> ().enabled	= true;
			break;
		}
		iTween.ScaleFrom (_LC_S1, iTween.Hash ("scale", new Vector3 (1.5f, 1.5f, 0.5f), "time", 0.1f, "delay", 0f, "easetype", iTween.EaseType.spring));
		iTween.ScaleFrom (_LC_S2, iTween.Hash ("scale", new Vector3 (1.5f, 1.5f, 0.5f), "time", 0.1f, "delay", 0f, "easetype", iTween.EaseType.spring));
		iTween.ScaleFrom (_LC_S3, iTween.Hash ("scale", new Vector3 (1.5f, 1.5f, 0.5f), "time", 0.1f, "delay", 0f, "easetype", iTween.EaseType.spring));

		LevelSelectionHandler.FromComplete = true;
	}

	void Levels_LevelCompleted ()
	{
		Debug.Log ("On levelcomplete");

		GlobalVariables.mGameState	= eGAME_STATE.LevelComplete;

		//DeleteCharactersAfterDelay ();
		calculateStarsAndCoins_levels ();
		//GlobalVariables.iNextLevelToUnlock	= GlobalVariables.iCurrentLevel + 1;
		GlobalVariables.iNextLevelToUnlock	= (levelselectionNew.OpenLevelNUm + 1) + 1;
		if (GlobalVariables.iNextLevelToUnlock >= GlobalVariables.iTotalLevels)
			GlobalVariables.iNextLevelToUnlock	= GlobalVariables.iTotalLevels;

		if (PlayerPrefs.GetInt ("TotalLevelsToUnlock") <= GlobalVariables.iNextLevelToUnlock) {
			PlayerPrefs.SetInt ("TotalLevelsToUnlock", GlobalVariables.iNextLevelToUnlock);
		}


		print (GlobalVariables.iCurrentLevel + " : " + GlobalVariables.iNextLevelToUnlock + " : " + PlayerPrefs.GetInt ("TotalLevelsToUnlock"));


		//if (AdsManager.Instance)
		//{

		//    AdsManager.Instance.RequestToShowAds(ADS_STATE.LEVEL_COMPLETE, gameConfigs.LcAdDelay);
		//}

	
		EnabledLevelCompletePopUp ();

		//AdmobAds.instance.showVideoAd();

	}






	public void OnLevelCompleted ()
	{
		Debug.Log ("######### LEVEL COMPLETE ############## " + GlobalVariables.mGameState);

		/*if (GlobalVariables.mGameState != eGAME_STATE.GamePlay) {
			return;
		}*/

		SoundController.Instance.PlayLevelComplete ();

		//Debug.Log ("######### LEVEL COMPLETE ############## " + gameConfigs.LcAdDelay);

		//if (AdsManager.Instance) {
			
		//	AdsManager.Instance.RequestToShowAds (ADS_STATE.LEVEL_COMPLETE, gameConfigs.LcAdDelay);
		//}

		calculateStarsAndCoinsGained ();
		GlobalVariables.mGameState	= eGAME_STATE.LevelComplete;
		mb_isLevelWin = true;
		GameManager.Instance.totalCoins = GameManager.Instance.totalCoins + GameManager.Instance.Prize [GameManager.Instance.pathIndex];
		PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);

		print ("total score adding..............");
	

		EnabledLevelCompletePopUp ();
		Invoke ("RequestToDisconnect", 3.0f);

		print ("submittt.....q " + GameManager.Instance.totalCoins);

		//GSConfig.SubmitScore (PlayerPrefs.GetInt ("totalCoins", (GameManager.Instance.totalCoins)));

	}

	void RequestToDisconnect ()
	{
		
		//if (GlobalVariables.isMultiPlayerMode && PhotonNetwork.connected) {
		//	PhotonNetwork.Disconnect ();
		//}
	}

	void calculateStarsAndCoinsGained ()
	{
		RequestToCounter (_LC_TotalCoins.gameObject, 0, (float)GameManager.Instance.totalCoins, 0.5f, 1.0f, "OnUpdateTotalCoinLC");
		_LC_Rewards.GetComponent<Text> ().text = "" + GameManager.Instance.Prize [GameManager.Instance.pathIndex];
		print ("levelscore ............. krishna");
	}


	#endregion


	#region XP

	// ************************** XP ********************//
	float mf_TimerToCalculateXP	= 0f;
	float mf_TimerTargetToCalculateXP	= 1f;
	int mi_XPPoint	= 0;
	[Space (10)]
	public Text _XPText;
	public Text _KMText;

	Text _rateText;
	Text _shareText;

	int requiredXP1;
	int requiredXP2;
	int requiredXP3;
	int unlockedStars = 0;
	int previousStarsCount	= 0;

	GameObject _star1;
	GameObject _star2;
	GameObject _star3;

	public static int GAinedXp;

	void OnUpdatedXPValues ()
	{
		mf_TimerToCalculateXP	+= Time.deltaTime;

		if (mf_TimerToCalculateXP >= mf_TimerTargetToCalculateXP && _trainMovementScript.m_iSpeedLimiterValue >= _trainMovementScript.m_fTrainSpeed && _trainMovementScript.m_fTrainSpeed > 10f) {
			mf_TimerToCalculateXP	= 0;
			mi_XPPoint	+= 5;
			calculateStarsCountInUpdate ();
		}
	}


	void calculateStarsCountInUpdate ()
	{

		if (!levelManager.LevelMode) {
			return;
		}
		_XPText.text	= "" + mi_XPPoint;

		if (previousStarsCount >= 3)
			return;

		if (mi_XPPoint >= requiredXP3)
			previousStarsCount	= 3;
		else if (mi_XPPoint >= requiredXP2)
			previousStarsCount	= 2;
		else if (mi_XPPoint >= requiredXP1)
			previousStarsCount	= 1;
		else
			previousStarsCount	= 0;


		if (previousStarsCount != unlockedStars) {
			unlockedStars	= previousStarsCount;
			Debug.Log ("Enable Stars");
			if (unlockedStars == 1) {
				_star1.GetComponent<Image> ().enabled	= true;
				_star2.GetComponent<Image> ().enabled	= false;
				_star3.GetComponent<Image> ().enabled	= false;
			} else if (unlockedStars == 2) {
				_star1.GetComponent<Image> ().enabled	= true;
				_star2.GetComponent<Image> ().enabled	= true;
				_star3.GetComponent<Image> ().enabled	= false;
			} else if (unlockedStars == 3) {
				_star1.GetComponent<Image> ().enabled	= true;
				_star2.GetComponent<Image> ().enabled	= true;
				_star3.GetComponent<Image> ().enabled	= true;
			} else {
				_star1.GetComponent<Image> ().enabled	= false;
				_star2.GetComponent<Image> ().enabled	= false;
				_star3.GetComponent<Image> ().enabled	= false;
			}
				 
		}

		GAinedXp = mi_XPPoint;
	}

	#endregion

	#region POPUp

	[Space (10)]
	[SerializeField] Transform _GamePlayUI;
	[SerializeField] Transform _ServerSearchingUI;
	[SerializeField] Transform _MapUI;
	[SerializeField] ButtonAnims _LevelComplete;
	[SerializeField] ButtonAnims _LevelFailed,_LevelCrashed;
	[SerializeField] ButtonAnims _Pause;
	[SerializeField] ButtonAnims _TutorialComplete;
	[SerializeField] ButtonAnims _ExitPopUp;
	[SerializeField] ButtonAnims _RatePopUp;
	[SerializeField] ButtonAnims _SharePopUp;

	GameObject _LF_Title;
	GameObject _LC_Rewards;
	GameObject _LC_Distance;
	GameObject _LF_Coins;
	GameObject _LF_Distance;
	GameObject _LF_TotalCoins;
	GameObject _LC_TotalCoins;

	bool mb_isLevelWin = false;

	[HideInInspector] public bool mb_isTimeUp	= false;

	void EnabledLevelCompletePopUp ()
	{
		Debug.Log ("EnabledLevelCompletePopUp");//arj

		GlobalVariables.mGameState	= eGAME_STATE.LevelComplete;
		_ExitState = eGAME_STATE.LevelComplete;
		SetVisibleLayer (_LevelComplete.transform);

		if (GlobalVariables.isMultiPlayerMode) {
			SetInviisbleLayer (_MapUI.transform);
		}

		SetInviisbleLayer (_GamePlayUI);

		_LevelComplete.CallAllAnims ();

		_LevelFailed.gameObject.SetActive (false);
	//	Admanager.instance.ShowFullScreenAd();

		Invoke ("CheckForRateOrSharePopUp", 4f);




		//PlayGameServices.submitScore (LeaderBoardID, score);
		//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.SubmitScore);

	}

	private void delaysaves ()
	{
		//GSConfig.SaveMyDataSavedGames ();
	}

	public bool mb_isCrashed = false;

	void EnableLevelFailedPopUp ()
	{
		if (GlobalVariables.mGameState != eGAME_STATE.LevelFailed)
			return;

		if (GameManager.Instance && !GlobalVariables.isMultiPlayerMode) {
			GameManager.Instance.distance = GameManager.Instance.distance + System.Convert.ToInt16 (InGameDistance);
			PlayerPrefs.SetInt ("distance", GameManager.Instance.distance);			 
		}
		if (!GlobalVariables.isMultiPlayerMode) {
			RequestToCounter (_LF_Coins.gameObject, 0, (float)InGameCoins, 1.0f, 0, "OnUpdateInGame");
			RequestToCounter (_LF_Distance.gameObject, 0, InGameDistance, 1.0f, 0.5f, "OnUpdateDistance");
		} 


		if (!GlobalVariables.isMultiPlayerMode) {

			if (_LF_Title){
				_LF_Title.GetComponent<Text> ().text = "";
		}
			if (mb_isCrashed) {
				//_LF_Title.GetComponent<Image> ().sprite = _LFTexture [1];
				_LF_Title.GetComponent<Text> ().text = "Time Out!";

			}

		}


	

		RequestToCounter (_LF_TotalCoins.gameObject, 0, (float)GameManager.Instance.totalCoins, 1.0f, 1.0f, "OnUpdateTotalCoin");

		InPlayArea = false;
		SetVisibleLayer (_LevelFailed.transform);
		SetInviisbleLayer (_GamePlayUI);

		if (GlobalVariables.isMultiPlayerMode) {
			SetInviisbleLayer (_MapUI.transform);
		}

		GlobalVariables.mGameState	= eGAME_STATE.LevelFailed;
		_ExitState	= eGAME_STATE.LevelFailed;
		_LevelFailed.CallAllAnims ();
	//	Admanager.instance.ShowFullScreenAd();

		float delayval = 1.0f;
		delayval -= 0.25f;
		if (delayval <= 0) {
			delayval = 0;
		}

		if (!levelManager.LevelMode) {
			Invoke ("CheckForRateOrSharePopUp", 4f);
		}

		//PushNotificator.setPushTag ("coins", PlayerPrefs.GetInt ("CoinsAvalaibleInGame").ToString ());

		/*if (mb_isTimeUp) {
			GameObject timeUpTxt = new GameObject ("timeUpText");
			timeUpTxt.transform.parent = _LevelFailed.transform;
			Text txt = timeUpTxt.AddComponent<Text> ();
			/*txt.rectTransform.pivot = new Vector2 (0.5f, 0.5f);
			txt.rectTransform.anchorMin = new Vector2 (0.5f, 1);
			txt.rectTransform.anchorMax = new Vector2 (0.5f, 1);*//*
			txt.rectTransform.localPosition = Vector3.zero;
			txt.rectTransform.sizeDelta = new Vector2 (500, 100);
			txt.rectTransform.position = Vector3.up * 300;
			txt.alignment = TextAnchor.MiddleCenter;
			txt.fontSize = 30;
			txt.font = _LevelFailed.GetComponentInChildren<Text> ().font;

			txt.text = "OOps... time's up!!!";
		}*/
	}

	#region COUNTER_UPDATE

	void RequestToCounter (GameObject textObject, float _from, float _to, float _time, float _delay, string OnUpdateProcess)
	{

		iTween.ValueTo (gameObject, iTween.Hash ("from", _from, "to", _to, "time", _time, "delay", _delay, "onupdate", OnUpdateProcess, "easetype", iTween.EaseType.linear, "onupdatetarget", gameObject));
	}

	void OnUpdateTotalCoin (int _value)
	{
		_LF_TotalCoins.GetComponent<Text> ().text = _value.ToString ();
	}

	void OnUpdateTotalCoinLC (int _value)
	{
		_LC_TotalCoins.GetComponent<Text> ().text = _value.ToString ();
	}

	void OnUpdateDistance (float _value)
	{
		_LF_Distance.GetComponent<Text> ().text = _value.ToString ("0.00") + " Km";
	}

	void OnUpdateInGame (int _value)
	{
		_LF_Coins.GetComponent<Text> ().text = _value.ToString ();
	}

	#endregion

	void EnablePausePopUp ()
	{
		Debug.Log (this.transform.name + " : " + this.transform.parent);
		_Pause.CallAllAnims ();
		SetVisibleLayer (_Pause.transform);
		SetInviisbleLayer (_GamePlayUI);

		if (GlobalVariables.isMultiPlayerMode) {
			SetInviisbleLayer (_MapUI.transform);
		}

		GlobalVariables.mGameState	= eGAME_STATE.Station;
		_ExitState	= eGAME_STATE.Pause;
		Invoke ("SetDelayOnPause", 0.8f);
	}




	void SetDelayOnPause ()
	{
		Time.timeScale	= 0;
		GlobalVariables.mGameState	= _ExitState;
	}

	public void ClosePausePopUp ()
	{
		GlobalVariables.mGameState	= eGAME_STATE.GamePlay;
		Time.timeScale	= 1;
		SetVisibleLayer (_GamePlayUI);

		if (GlobalVariables.isMultiPlayerMode) {
			SetVisibleLayer (_MapUI.transform);
		}
		SetInviisbleLayer (_Pause.transform);
		_Pause.ReverseAll ();
	}

	private int[] AutoShareLevels = { 1, 10, 20, 30, 40, 50 };

	public void OnNextLevelClick ()
	{
		GlobalVariables.iCurrentLevel++;

		if (GlobalVariables.iCurrentLevel > GlobalVariables.iTotalLevels)
			GlobalVariables.iCurrentLevel = 1;
		
		Debug.Log (" OnNextLevelClick :" + GlobalVariables.iCurrentLevel);
	}


	IEnumerator ieRequestToLoadScene (string sceneName, int mIndex)
	{
		
		//if (GlobalVariables.isMultiPlayerMode && PhotonNetwork.connected) {
		//	PhotonNetwork.Disconnect ();
		//}
		yield return new WaitForSeconds (0);
		GlobalVariables.iMenuEnableIndex = mIndex;
		Application.LoadLevel (sceneName);
	}

	public void OnRetryClick ()
	{
		Debug.Log ("OnRetryClick");
		Time.timeScale	= 1;
		if (GlobalVariables.isMultiPlayerMode) {
			StartCoroutine (ieRequestToLoadScene ("GameUI", 3));
		} else {
			Application.LoadLevel (Application.loadedLevelName);
		}

	}

	public void OnHomeClick ()
	{
		Debug.Log ("OnHomeClick");
		Time.timeScale	= 1;
		StartCoroutine (ieRequestToLoadScene ("GameUI", 0));
	}

	public void OnNextClick ()
	{
		Debug.Log ("OnNextClick");
		Time.timeScale	= 1;
		StartCoroutine (ieRequestToLoadScene ("GameUI", 5));
	}

	public void OnHomeworldClick ()
	{
		Debug.Log ("OnHomeClick");
		Time.timeScale	= 1;
		StartCoroutine (ieRequestToLoadScene ("GameUI", 4));
	}

	public void OnPauseClick ()
	{
		EnablePausePopUp ();
	}


	Rigidbody[] allrigidBodies;
	List<Rigidbody> selectedBodies = new List<Rigidbody>();
	public void StopTrainImediatly(){

		TrainBreakScript.Instance.UseBreak ();
		TrainBreakScript.Instance.PostBreak();	

		freezeRigidBodies ();

	}
	void freezeRigidBodies(){
		allrigidBodies = TrainMovementScript.Instance.transform.GetComponentsInChildren<Rigidbody> ();
		foreach (Rigidbody rgb in allrigidBodies) 
		{
			//if (rgb.isKinematic == false) {
			selectedBodies.Add (rgb);
			rgb.velocity = Vector3.zero;
			rgb.angularVelocity = Vector3.zero;
			rgb.drag = 1000;
			rgb.angularDrag = 1000;

			//}
		}
		foreach (Rigidbody body in selectedBodies) 
		{
			//body.isKinematic = true;
		}


	}
	void UnFreezeRigidBodies(){
		foreach (Rigidbody body in selectedBodies)
		{
			//body.isKinematic = false;
			body.velocity=Vector3.zero;
			body.angularVelocity=Vector3.zero;

			body.drag = 0;
			body.angularDrag = 0;
		}
	}


	public static int normalDirectionDuration = 13;
	public void TakeReverseDirection(){
		//TrainCollisionHandler.mee.mb_IsStationTriggered = false;
		SetInviisbleLayer (_LevelCrashed.transform);
		Debug.Log("--Change Direction to backwards");
		SoundController.Instance.EnableTrainSource();
		UnFreezeRigidBodies ();
		isReverse = true;
		f_DiffInSpeed = 1f;
		LevelEnabler.TrainDirection = -1;
		//TrainBreakScript.Instance.Invoke("PostBreak",1);
		//*/
		Invoke ("ChangeToNormalDirection", normalDirectionDuration);
		Debug.Log ("TrainDir: "+LevelEnabler.TrainDirection+" isReverse: "+isReverse+" f_DiffInSpeed: "+f_DiffInSpeed);
		GameObject[] explossions =  GameObject.FindGameObjectsWithTag ("Explosions");
		foreach (GameObject exp in explossions) {
			Destroy (exp);
		}

		if (TrainCollisionHandler.AiMovingTrainCollider) {
			TrainCollisionHandler.AiMovingTrainCollider.transform.parent.GetComponent<AIWayPointMovement> ().OnRequestToSetInitialPosition ();
		}
		if(crashResetAlertText)crashResetAlertText.enabled = true;
	}

	void ChangeToNormalDirection(){
		Debug.Log("--Change Direction to Normal..");
		levelCrashed = false;
		TrainBreakScript.Instance.UseBreak ();
		TrainBreakScript.Instance.PostBreak();
		freezeRigidBodies ();
		UnFreezeRigidBodies ();
		LevelEnabler.TrainDirection = 1;
		isReverse = false;
		TrainCollisionHandler.disabledCollider.enabled = true;
		TrainCollisionHandler.mee.EnableCollider ();// for last stop Collider
		levelCrashed = false;

		if (TrainCollisionHandler.AiMovingTrainCollider) {
			TrainCollisionHandler.AiMovingTrainCollider.transform.parent.GetComponent<AIWayPointMovement> ().OnRequestToSetInitialPosition ();
			TrainCollisionHandler.AiMovingTrainCollider.transform.parent.GetComponent<AIWayPointMovement> ().mb_IsStart = false;
			TrainCollisionHandler.AiMovingTrainCollider = null;
		}
		if(crashResetAlertText)crashResetAlertText.enabled = false;
	}


	void RequestToCheckShareRatePopUp ()
	{

		//GameManager.Instance.iShareRateCounter = 10;
		print (levelManager.LevelMode + " : " + ((GameManager.Instance.iShareRateCounter) % 3));



		if (!levelManager.LevelMode && (GameManager.Instance.iShareRateCounter) % 5 == 0 && (GameManager.Instance.iShareRateCounter) != 0) {

			OnSharePopUp ();
			return;
		} 
		if (!levelManager.LevelMode && (GameManager.Instance.iShareRateCounter) % 3 == 0 && (GameManager.Instance.iShareRateCounter) != 0) {
			print ("--------------------------------------------------");
			OnShowRatePopUp ();
		}
		


		//oldsetupShare ();
	}


	void oldsetupShare ()
	{
		

	}

	void CheckForRateOrSharePopUp ()
	{

		GameManager.Instance.iShareRateCounter++;
		PlayerPrefs.SetInt ("iShareRateCounter", GameManager.Instance.iShareRateCounter);

		RequestToCheckShareRatePopUp ();

		print ("submittt....." + GameManager.Instance.totalCoins);

		//GSConfig.SubmitScore (PlayerPrefs.GetInt ("totalCoins", (GameManager.Instance.totalCoins)));
		
	}

	void OnShowRatePopUp ()
	{
		
		if (PlayerPrefs.GetInt (GlobalVariables._sRatingString) == 1)
			return;
		
		SetVisibleLayer (_RatePopUp.transform);
		SetInviisbleLayer (mb_isLevelWin ? _LevelComplete.transform : _LevelFailed.transform);
		_RatePopUp.CallAllAnims ();

		GlobalVariables.mGameState = eGAME_STATE.Rate;
	}

	public void OnRateClick ()
	{
	Application.OpenURL ("");
		Debug.Log ("Rate here>>>>");

		PlayerPrefs.SetInt (GlobalVariables._sRatingString, 1);

		#if ADS_SETUP
		purchasedAmount (GameConfigs2015.Rate_Coins);
		#endif
		CloseRatePopUp ();
	}

	void purchasedAmount (int _amount)
	{
		
		GameManager.Instance.totalCoins = GameManager.Instance.totalCoins + _amount;
		PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);

		PlayerPrefs.SetInt ("totalCoinsLDB", PlayerPrefs.GetInt ("totalCoinsLDB") + _amount);
		RequestToCounter (_LF_TotalCoins.gameObject, 0, (float)GameManager.Instance.totalCoins, 1.0f, 1.0f, "OnUpdateTotalCoin");
	}

	public void OnCancelRateClick ()
	{
		//arj PlayerPrefs.SetInt (GlobalVariables._sRatingCancel, 1);
		CloseRatePopUp ();
	}

	public void OnLaterClick ()
	{
		CloseRatePopUp ();
	}

	public void CloseRatePopUp ()
	{	

		SetVisibleLayer (mb_isLevelWin ? _LevelComplete.transform : _LevelFailed.transform);
		SetInviisbleLayer (_RatePopUp.transform);
		_RatePopUp.ReverseAll ();
		print (_ExitState + " _exitt");

		if (_ExitState == eGAME_STATE.LevelComplete) {
			GlobalVariables.mGameState = eGAME_STATE.LevelComplete;
		} else {
			GlobalVariables.mGameState = eGAME_STATE.LevelFailed;


		}
		//GlobalVariables.mGameState = eGAME_STATE.LevelComplete;
	}

	eGAME_STATE _ExitState	= eGAME_STATE.Pause;

	public void OnShowExitPopUp (eGAME_STATE _state)
	{
		if (_state == eGAME_STATE.LevelFailed || _state == eGAME_STATE.LevelComplete) {
			return;
		}
		_ExitState	= _state;

		SetVisibleLayer (_ExitPopUp.transform);


		InPlayArea = false;

		if (_ExitState == eGAME_STATE.LevelComplete) {
			SetInviisbleLayer (_LevelComplete.transform);
		} else if (_ExitState == eGAME_STATE.LevelFailed) {
			SetInviisbleLayer (_LevelFailed.transform);
		} else if (_ExitState == eGAME_STATE.GamePlay) {
			InPlayArea = true;
			Invoke ("makedelaypause", 0.5f);
		}

		GlobalVariables.mGameState = eGAME_STATE.ExitPopUp;
		_ExitPopUp.CallAllAnims ();
		Debug.Log ("Exit pop up --- " + InPlayArea);
	}

	void makedelaypause ()
	{
		Time.timeScale = 0;
	}

	public void CloseExit ()
	{
		SetInviisbleLayer (_ExitPopUp.transform);
		_ExitPopUp.ReverseAll ();
		InPlayArea = false;
		Time.timeScale = 1;
		GlobalVariables.mGameState = eGAME_STATE.GamePlay;
	}

	public static bool InPlayArea = false;

	public void ShowFailRef ()
	{
		Time.timeScale = 1;

		SetInviisbleLayer (_ExitPopUp.transform);
		_ExitPopUp.ReverseAll ();

		GlobalVariables.mGameState	= eGAME_STATE.LevelFailed;
		mb_isLevelWin	= false;
		OnLevelFailed ();
	}

	public void OnExitClick ()
	{
		//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.Exit);
	}

	public void OnPlayOnClick ()
	{		
		SetInviisbleLayer (_ExitPopUp.transform);

		_ExitPopUp.ReverseAll ();

		if (_ExitState == eGAME_STATE.LevelComplete) {
			SetVisibleLayer (_LevelComplete.transform);
			GlobalVariables.mGameState = eGAME_STATE.LevelComplete;	
		} else if (_ExitState == eGAME_STATE.LevelFailed) {
			SetVisibleLayer (_LevelFailed.transform);
			GlobalVariables.mGameState = eGAME_STATE.LevelFailed;
		}
	}

	void OnSharePopUp ()
	{

		print ("share check----- " + PlayerPrefs.GetInt (GlobalVariables._sSharingString));

		if (PlayerPrefs.GetInt (GlobalVariables._sSharingString) == 1)
			return;
		
		SetVisibleLayer (_SharePopUp.transform);
		SetInviisbleLayer (mb_isLevelWin ? _LevelComplete.transform : _LevelFailed.transform);
		_SharePopUp.CallAllAnims ();
		GlobalVariables.mGameState = eGAME_STATE.Share;
	}

	public void OnShareButtonInPopUpClick ()
	{
		PlayerPrefs.SetInt (GlobalVariables._sSharingString, 1);

		print ("share clicked " + PlayerPrefs.GetInt (GlobalVariables._sSharingString));
		//if (AdsManager.Instance) {
		//	AdsManager.Instance.RequestToFBShare (_shareText.text);
		//}
		OnShareCancelClick ();
	}

	public void OnShareCancelClick ()
	{
		SetVisibleLayer (mb_isLevelWin ? _LevelComplete.transform : _LevelFailed.transform);
		SetInviisbleLayer (_SharePopUp.transform);
		_SharePopUp.ReverseAll ();

		if (_ExitState == eGAME_STATE.LevelComplete) {
			GlobalVariables.mGameState = eGAME_STATE.LevelComplete;
		} else {
			GlobalVariables.mGameState = eGAME_STATE.LevelFailed;


		}
	}

	void ShowAdsOnLevelFailed ()
	{
		//if (AdsManager.Instance) {

		//	if (GlobalVariables.isMultiPlayerMode) {
		//		AdsManager.Instance.RequestToShowAds (ADS_STATE.LEVEL_FAIL, gameConfigs.LfAdDelay);//1.0
		//	} else {
		//		AdsManager.Instance.RequestToShowAds (ADS_STATE.LEVEL_FAIL, gameConfigs.LfAdDelay);//1.0
		//	}
		//}
		GlobalVariables.mGameState	= _ExitState;
	}

	void ShowAdsOnGameStart ()
	{
		//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.ShowFullScreenAds, "", null, eACHIEVEMENTS_STATE.None, eADS_STATE.GamePlay);
	}

	#endregion

	#region Change Layer Region

	void SetVisibleLayer (Transform _ObjToChangeLayer)
	{
		_ObjToChangeLayer.gameObject.GetComponent<Canvas> ().enabled	= true;
	}

	void SetInviisbleLayer (Transform _ObjToChangeLayer)
	{
		_ObjToChangeLayer.gameObject.GetComponent<Canvas> ().enabled	= false;
	}

	#endregion

	#region Instruction Handler

	[HideInInspector] public bool IsInstructionAvlaiable	= false;

	public void StartGamePlayAfterInitialCameraPos ()
	{
		GlobalVariables.mGameState = eGAME_STATE.GamePlay;
		
		if (GlobalVariables.iCurrentLevel == 1) {
			ChangeInstructionState (e_INSTRUCTION_STATE.CameraButton);
			IsInstructionAvlaiable	= true;
		} else {
			ChangeInstructionState (e_INSTRUCTION_STATE.NoInstruction);
			IsInstructionAvlaiable	= false;
		}
	}

	public void ChangeInstructionState (e_INSTRUCTION_STATE mState)
	{
		GlobalVariables.mInsState	= mState;
		if (InstructionHandler.Instance)
			InstructionHandler.Instance.ChangeInstructionState ();
		
		if (mState == e_INSTRUCTION_STATE.NoInstruction)
			IsInstructionAvlaiable	= false;
		else
			IsInstructionAvlaiable	= true;
	}

	public void SetInstructionVisibleOnTrigger (e_INSTRUCTION_STATE _insState)
	{
		if (_insState == e_INSTRUCTION_STATE.ChangeDirection_Left || _insState == e_INSTRUCTION_STATE.ChangeDirection_Right) {
			GlobalVariables.mInsSubState	= _insState;
			_insState = e_INSTRUCTION_STATE.ChangeDirection;
		}

		_ExitState	= eGAME_STATE.Instruction;
		ChangeInstructionState (_insState);
		Invoke ("SetDelayOnPause", 0.2f);
	}

	void OnInstructionOkButtonClick (e_INSTRUCTION_STATE mNextState)
	{
		if (mNextState == e_INSTRUCTION_STATE.NoInstruction) {
			Time.timeScale = 1f;

		}
		GlobalVariables.mInsState	= mNextState;
		ChangeInstructionState (mNextState);
	}

	#endregion

	#region Station Characters Aniamtion

	void OnStationReacheStartAniamtion ()
	{

		GameObject[] _TrainDoorObjects	= null;
		CharacterInfo[] _CharactersInStation	= null;

		_CharactersInStation	= PatternManager.Instance.Patterns [PatternManager.Instance.currentPatternId]._characterInfo;

		if (mb_IsStationLeftSide) {
			_TrainDoorObjects	= _trainMovementScript._LeftSideDoorPoints;
		} else {
			_TrainDoorObjects	= _trainMovementScript._RightSideDoorPoints;
		}

		if (_TrainDoorObjects == null || _TrainDoorObjects.Length == 0 || PatternManager.Instance.Patterns [PatternManager.Instance.currentPatternId]._characterInfo.Length == 0)
			return;

		float distance	= 0;
		float calculatedDis	= 0;
		GameObject _doorObjToMove	= null;
		for (int i = 0; i < PatternManager.Instance.Patterns [PatternManager.Instance.currentPatternId]._characterInfo.Length; i++) {
			GameObject _character	= _CharactersInStation [i]._Character;
			distance	= 1000f;
			_doorObjToMove	= null;
			for (int j = 0; j < _TrainDoorObjects.Length; j++) {
				GameObject _doorTrigger	= _TrainDoorObjects [j] as GameObject;
				calculatedDis	= Vector3.Distance (_character.transform.position, _doorTrigger.transform.position);
				if (calculatedDis < distance) {
					distance	= calculatedDis;
					_doorObjToMove	= _doorTrigger;
				}
			}
			if (_doorObjToMove) {
				CharacterAnimSelector _scr	= _character.GetComponent<CharacterAnimSelector> ();
				_scr.SetAllStateFalse ();
				_scr.walk	= true;
				_scr.NormalWalk	= true;
				_scr._changeState	= true;

				//	Debug.Log ("overr b---");

				iTween.MoveTo (_character, iTween.Hash ("position", _doorObjToMove.transform.position, "time", 4f, "delay", 0.1f, "easetype", iTween.EaseType.linear, "orienttopath", true, "onComplete", "Completfun", "oncompletetarget", _character.gameObject, "oncompleteparams", _character.gameObject));

			}
		}
		Debug.Log ("###### Avatar ######");
		GameManager.Instance.isCharaterAnimationEnable = false;
		StartCoroutine (deletechars (_CharactersInStation));
	}

	IEnumerator deletechars (CharacterInfo[] aa)
	{
		yield return new WaitForSeconds (4);
		Debug.Log ("overr c");
		for (int i = 0; i < aa.Length; i++) {
			if (aa [i]._Character.GetComponent<iTween> ()) {
				Destroy (aa [i]._Character.GetComponent<iTween> ());
			}
			aa [i]._Character.SetActive (false);
		}
	}

	void Completfun (GameObject aa)
	{
		Debug.Log ("overr a");
		//Destroy (aa);
	}

	void DeleteCharactersAfterDelay ()
	{
//		if (_StationCharacters == null || _StationCharacters.Length <= 0)
//			return;

		int stationNo = _iStationNo - 1;
		if (stationNo < 0)
			stationNo	= 0;


			
		GameObject[] _CharactersInStation	= null;
//		if (mb_IsStationLeftSide)
//			_CharactersInStation	= _StationCharacters [_iStationNo]._characterInStationLeftSide;
//		else
//			_CharactersInStation	= _StationCharacters [_iStationNo]._characterInStationRightSide;


		for (int i = 0; i < _CharactersInStation.Length; i++) {
			_CharactersInStation [i].SetActive (false);	
		}
	}

	#endregion

	#region New Methods for AI

	public void ChangeAIDirection(eDIRECTION_TO_CHANGE dir){
		Debug.Log ("--:ChangeAIDirection:=" + dir);
		CalculateDirectionChange (dir,true);
	}
	#endregion
}

[System.Serializable]
class StationCharacters
{
	public GameObject[] _characterInStationLeftSide;
	public GameObject[] _characterInStationRightSide;
}
