using UnityEngine;
using System.Collections;

public class LevelEnabler : MonoBehaviour
{
	[SerializeField] Transform _GamePlayUI;
	[Space(10)]
	[SerializeField] ButtonAnims _LevelComplete;
	[SerializeField] ButtonAnims _LevelFailed,_LevelCrashed;
	[SerializeField] ButtonAnims _Pause;
	[SerializeField] ButtonAnims _ExitPopUp;
	[SerializeField] ButtonAnims _RatePopUp;
	[SerializeField] ButtonAnims _SharePopUp;

	[HideInInspector]
	public GameObject Ui_manager;
	[HideInInspector]
	public GameObject Click_manager;
	[HideInInspector]
	public GameObject Sound_manager;
	[HideInInspector]
	public GameObject Camera_manager;

	private bool dollerbutonactive;


	private string LevelName;

	public static LevelEnabler mee;

	public static int TrainDirection = 1;
	public static bool ReverseDone = false;

	public void Awake ()
	{
		
		mee = this;
		ReverseDone = false;
		TrainDirection = 1;

		if (GlobalVariables.isMultiPlayerMode) {
			//Debug.Log ("Level :" + GlobalVariables.iCurrentLevel);
		} else {
			Debug.Log ("Free Mode");
		}

//		LevelName = "Level" + GlobalVariables.iCurrentLevel;

//		if (IsTestLevel) {
//			#if UNITY_EDITOR
//			LevelName = "Level" + LevelNum;
//			#endif
//		}
		//public bool IsTestLevel;
//		public int LevelNum;
//		Debug.Log (LevelName + " ---- to open");
//		for (int i = 0; i < levels_all.Length; i++) {
////			Debug.Log (levels_all [i].name);
//			levels_all [i].SetActive (false);
//			if (LevelName == levels_all [i].name) {
//				levels_all [i].SetActive (true);
//			}
//
//		}
	}


	void Start ()
	{
		dollerbutonactive = true;
		print (levelManager.LevelMode+" lm");
		SetDataObjects ();
      //  AdManager.instance.showInterstitial();
	}

	void SetDataObjects ()
	{
		GlobalVariables.mCurrentScene	= eCURRENT_SCENE.GamePlay;

		_LevelComplete = GameObject.Find ("LevelComplete").GetComponent<ButtonAnims> ();
		_LevelFailed = GameObject.Find ("LevelFailed").GetComponent<ButtonAnims> ();
		if(GameObject.Find ("LevelCrashed"))_LevelCrashed = GameObject.Find ("LevelCrashed").GetComponent<ButtonAnims> ();
		_Pause = GameObject.Find ("PausePopUp").GetComponent<ButtonAnims> ();
		_GamePlayUI = GameObject.Find ("GamePlayUICanvas").transform;
		_ExitPopUp = GameObject.Find ("ExitCanvas").GetComponent<ButtonAnims> ();
		_RatePopUp = GameObject.Find ("RateCanvas").GetComponent<ButtonAnims> ();
		_SharePopUp = GameObject.Find ("ShareCanvas").GetComponent<ButtonAnims> ();
	}

	public void OnButtonClick (string _str)
	{
		//Debug.Log(_str+" --In LevelEnabler-------------------- "+GlobalVariables.mGameState);
		switch (GlobalVariables.mGameState) {
		case  eGAME_STATE.InitialDelay:
			if (_str == "homegg") {

				//GamePlayManager.Instance.OnHomeworldClick ();

				//if (GlobalVariables.isMultiPlayerMode && PhotonNetwork.connected) {
				//	PhotonNetwork.Disconnect ();
				//}

				GlobalVariables.iMenuEnableIndex = 4;
				Application.LoadLevel ("GameUI");


				SoundController.Instance.OnButtonClick ();
			}

			break;
		case eGAME_STATE.GamePlay:
			if (!GamePlayManager.Instance.isTutorialCompletePopUpShow) {
				if (_str == "CameraButton" && (!GamePlayManager.Instance.IsInstructionAvlaiable || (GamePlayManager.Instance.IsInstructionAvlaiable && GlobalVariables.mInsState == e_INSTRUCTION_STATE.CameraButton))) {
					SoundController.Instance.OnButtonClick ();
					if (GamePlayManager.Instance.IsInstructionAvlaiable) {
						if (GlobalVariables.mInsSubState == e_INSTRUCTION_STATE.CameraFreeHandMode) {
							GlobalVariables.mInsSubState = e_INSTRUCTION_STATE.CameraDriverMode;
							GamePlayManager.Instance.ChangeInstructionState (e_INSTRUCTION_STATE.CameraButton);
						} else {
							GamePlayManager.Instance.iCameraCurrentView = 3;
							GlobalVariables.mInsSubState = e_INSTRUCTION_STATE.CameraFreeHandMode;
							GamePlayManager.Instance.ChangeInstructionState (e_INSTRUCTION_STATE.TimeForLevels);
						}
					}
					GamePlayManager.Instance.OnChangeCameraButtonClick ();


				} else if (_str == "Horn" && (!GamePlayManager.Instance.IsInstructionAvlaiable || (GamePlayManager.Instance.IsInstructionAvlaiable && GlobalVariables.mInsState == e_INSTRUCTION_STATE.Horn))) {
					GamePlayManager.Instance.OnHornClick ();
				} else if (_str == "InsOkButton") {
					Debug.Log ("mini state" + GlobalVariables.mInsState);
					SoundController.Instance.OnButtonClick ();
					if (GlobalVariables.mInsState == e_INSTRUCTION_STATE.TimeForLevels)
						GamePlayManager.Instance.ChangeInstructionState (e_INSTRUCTION_STATE.SpeedLimit);
					else if (GlobalVariables.mInsState == e_INSTRUCTION_STATE.SpeedLimit)
						GamePlayManager.Instance.ChangeInstructionState (e_INSTRUCTION_STATE.Speed);
					else if (GlobalVariables.mInsState == e_INSTRUCTION_STATE.Speed)
						GamePlayManager.Instance.ChangeInstructionState (e_INSTRUCTION_STATE.XpPoints);
					else if (GlobalVariables.mInsState == e_INSTRUCTION_STATE.XpPoints)
						GamePlayManager.Instance.ChangeInstructionState (e_INSTRUCTION_STATE.Accelration);
				} else if ((_str == "PauseButton") && !GamePlayManager.Instance.IsInstructionAvlaiable) {
					GamePlayManager.Instance.OnPauseClick ();
					SoundController.Instance.OnButtonClick ();
				} 
				//		else if (_str == "UnityBackButton") {
				//				SoundController.Instance.OnButtonClick();
				//				GamePlayManager.Instance.OnShowExitPopUp (eGAME_STATE.LevelComplete);
				//			}

				else if (_str == "UnityBackButton") {
					SoundController.Instance.OnButtonClick ();
					GamePlayManager.Instance.OnShowExitPopUp (eGAME_STATE.GamePlay);
				}
				else if (_str == "FreeCoins") {
					if (dollerbutonactive == true) {
						SoundController.Instance.OnButtonClick ();
						TrainSelectionHandler.ingameVideo = 1;
					//	GoogleMobileAdsDemoScript.mee.ShowRewardBasedVideo ();
						GamePlayManager.Instance.dollerbutton.SetActive (false);
						dollerbutonactive = false;
						Invoke ("dolleractive", 30);
					}
				}
			} else {
				if (_str == "tutorialContinue") {
					SoundController.Instance.OnButtonClick ();
					GamePlayManager.Instance.OnHomeClick ();
				}
			}


			break;

//------------In pause
		case eGAME_STATE.Pause:
			if (_str == "Resume") {
				SoundController.Instance.OnButtonClick ();
				GamePlayManager.Instance.ClosePausePopUp ();
			} else if (_str == "Retry") {
				SoundController.Instance.OnButtonClick ();
				GamePlayManager.Instance.OnRetryClick ();
			} else if (_str == "Sound") {
				SoundController.Instance.OnButtonClick ();
				SoundController.Instance.ChangeSoundState ();
				GamePlayManager.Instance.CheckSoundImage ();
			} else if (_str == "Home") {
				SoundController.Instance.OnButtonClick ();
				GamePlayManager.Instance.OnHomeClick ();
			} else if (_str == "MoreGames") {
				SoundController.Instance.OnButtonClick ();
			//	ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.MoreGames);
			}
			break;

// ----------- In Instruction

		case eGAME_STATE.Instruction:
			Debug.Log ("Instruction");
			if (_str == "InsOkButton") {
				SoundController.Instance.OnButtonClick ();
				Time.timeScale	= 1;
				GlobalVariables.mGameState	= eGAME_STATE.GamePlay;
				GamePlayManager.Instance.ChangeInstructionState (e_INSTRUCTION_STATE.NoInstruction);
			} else if (_str == "Halt") {
				SoundController.Instance.OnButtonClick ();
				Time.timeScale	= 1;
				Debug.Log ("Halt");
				GlobalVariables.mGameState	= eGAME_STATE.GamePlay;
				GamePlayManager.Instance.ChangeInstructionState (e_INSTRUCTION_STATE.NoInstruction);
			}

			break;

//---------- in level complete 

		case eGAME_STATE.LevelComplete:

			if (_str == "Retry_lc") {
				GamePlayManager.Instance.OnRetryClick ();
				SoundController.Instance.OnButtonClick ();
			}else if(_str=="NextLevel"){

				GamePlayManager.Instance.OnNextClick ();
				SoundController.Instance.OnButtonClick ();
					//   AdManager.instance.showInterstitial();  venkat
					AdsManager.Instance.ShowAd();
                }

			else if (_str == "Home") {
				GamePlayManager.Instance.OnHomeClick ();
				SoundController.Instance.OnButtonClick ();
					//   AdManager.instance.showInterstitial();  venkat
					AdsManager.Instance.ShowAd();
				} else if (_str == "UnityBackButton") {
				SoundController.Instance.OnButtonClick ();
				GamePlayManager.Instance.OnShowExitPopUp (eGAME_STATE.LevelComplete);
			} else if (_str == "MoreGames") {
				SoundController.Instance.OnButtonClick ();
				//ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.MoreGames);
			}else if (_str == "ShareFBBtn") {
				Debug.Log ("Share FB Btn");
			
				SoundController.Instance.OnButtonClick ();
//				if (AdsManager.Instance) {
//					AdsManager.Instance.RequestToFBShare ("Yes! Train Racing 3D Multiplayer is Awesome!!! Play for free and race like Champ!!");
//				}

				//GameConfigs2015.share ();
			}
			break;

/// exit pop
		case eGAME_STATE.ExitPopUp:

			print ("in play area "+GamePlayManager.InPlayArea);
			if (_str == "UnityBackButton") {
				SoundController.Instance.OnButtonClick ();
				GamePlayManager.Instance.OnPlayOnClick ();
			} else if (_str == "Exit") {
                   
				if (GamePlayManager.InPlayArea) {
					GlobalVariables.mGameState = eGAME_STATE.GamePlay;
					GamePlayManager.Instance.ShowFailRef ();

				} else {
					GamePlayManager.Instance.OnExitClick ();
				}


			} else if (_str == "PlayOn") {
				if (GamePlayManager.InPlayArea) {
					GamePlayManager.Instance.CloseExit ();
				} else {
					GamePlayManager.Instance.OnPlayOnClick ();
				}
                   
				SoundController.Instance.OnButtonClick ();
			}
			break;

		//------------ in share

		case eGAME_STATE.Share:
			if (_str == "UnityBackButton") {
				SoundController.Instance.OnButtonClick ();
				GamePlayManager.Instance.OnShareCancelClick ();
			} else if (_str == "Share") {
				Debug.Log ("in share button>>>>");
				SoundController.Instance.OnButtonClick ();
				//#if ADS_SETUP
				//GameConfigs2015.IsShareCoins = true;
				//#endif
				PlayerPrefs.SetInt (GlobalVariables._sSharingString, 1);



				//GameConfigs2015.share ();

				GamePlayManager.Instance.OnShareCancelClick ();


				//GameManager.Instance.totalCoins = GameManager.Instance.totalCoins+GameConfigs2015.Share_Coins;
				PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);

				GameManager.Instance.totalCoins = PlayerPrefs.GetInt ("totalCoins");
				//GamePlayManager.Instance.OnShareButtonInPopUpClick ();



			} else if (_str == "Cancel") {
				SoundController.Instance.OnButtonClick ();
				GamePlayManager.Instance.OnShareCancelClick ();
                
			}
			break;

		case eGAME_STATE.Rate:
			if (_str == "UnityBackButton") {
				SoundController.Instance.OnButtonClick ();
				GamePlayManager.Instance.CloseRatePopUp ();
			} else if (_str == "Rate") {
				SoundController.Instance.OnButtonClick ();
				GamePlayManager.Instance.OnRateClick ();
			} else if (_str == "Cancel") {
				SoundController.Instance.OnButtonClick ();
				GamePlayManager.Instance.OnCancelRateClick ();
			} else if (_str == "Later") {
				SoundController.Instance.OnButtonClick ();
				GamePlayManager.Instance.OnLaterClick ();
			}
			break;

		case eGAME_STATE.LevelFailed:
			if (_str == "Retry") {
				SoundController.Instance.OnButtonClick ();
				GamePlayManager.Instance.OnRetryClick ();
					//  AdManager.instance.showInterstitial();  venkat
					AdsManager.Instance.ShowAd();
				} else if (_str == "Home") {
				GamePlayManager.Instance.OnHomeClick ();
				SoundController.Instance.OnButtonClick ();
					//   AdManager.instance.showInterstitial();     venkat
					AdsManager.Instance.ShowAd();
				} else if (_str == "UnityBackButton") {
				SoundController.Instance.OnButtonClick ();
				GamePlayManager.Instance.OnShowExitPopUp (eGAME_STATE.LevelFailed);
			} else if (_str == "MoreGames") {
				SoundController.Instance.OnButtonClick ();
			//	ExternalInterfaceHandler.Instance.OnExternalCallFromGame (eBUTTON_CALL_STATE.MoreGames);
			}
			break;

		}
	
	}

	void dolleractive(){
		GamePlayManager.Instance.dollerbutton.SetActive (true);
		dollerbutonactive = true;
	}
}


