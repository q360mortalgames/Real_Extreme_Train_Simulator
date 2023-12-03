using UnityEngine;
using System.Collections;
using System;


public enum eCURRENT_SCENE
{
	Menu,
	TrainSelection,
	LevelSelection,
	GamePlay
}

public enum eFADE_MODE
{
	FadeIn,
	fadeOut,
}

public enum eMOVE_MODE
{
	Up,
	Down,
	Left,
	Right,
}

public enum eGAME_STATE
{
	InitialDelay,
	GamePlay,
	LevelComplete,
	LevelFailed,
	Pause,
	Station,
	Crash,
	Instruction,
	ExitPopUp,
	Share,
	Rate,
	searchMultiplay,
	None
}

public enum eMENU_STATE
{
	Menu,
	Settings,
	Store,
	Shop,
	TrainSelection,
	LevelSelection,
	InititalAniamtion,
	ExitPopUp,
	Share,
	Rate,
	UnlockLevelsPopUp,
	UnlockTrainsPopUp,
	Sharepop
}

public enum eDIRECTION_TO_CHANGE
{
	None,
	Left,
	Right
}


public enum eTRIGGER_STATE
{
	None,
	SpeedLimiter,
	Horn,
	HornGateClose,
	DirectionChangeEnable,
	StationStop,
	DirectionChangeDisable,
	AITrainStart,
	AICarStart,
	TrainSignalAlert,
	TrainSignalAlertExit,
	LeftTrainStationTrigger,
	RightTrainStationTrigger,
	StaticCameraEntry,
	StaticCameraExit,
	TunnelStaticCameraEntry,
	TunnelStaticCameraExit,
	TrainSignalAlertMP,
	TrainSignalAlertExitMP,
}

public enum e_CAMERA_MODE
{
	InitialView,
	FreeHandView,
	DriverView,
	StationView,
	CrashView,
	LevelCompleteView,
	BridgeView_1,
	TrainFullView,
	FrontView,
	BridgeView_2,
}

public enum e_TRAIN_SIGNAL
{
	Red,
	Green,
	Yellow
}

public enum e_INSTRUCTION_STATE
{
	NoInstruction,
	Accelration,
	Break,
	CameraButton,
	CameraFreeHandMode,
	CameraDriverMode,
	Horn,
	ChangeDirection_Left,
	ChangeDirection_Right,
	Speed,
	XpPoints,
	TimeForLevels,
	SpeedLimit,
	ChangeDirection,
	Signals,
	Reverse
}

public enum e_INSTRUCTION_TRIGGER
{
	SpeedTrigger,
	ChangeDirectionLeft_Trigger,
	ChangeDirectionRight_Trigger,
	HornTrigger,
	StationStopTrigger,
	AITrainSignalTrigger,
	NoTrigger
}

public enum e_TRAINSELECTIONPAGE_sSTATE
{
	Animating,
	ButtonEnabled
}

public static class GlobalVariables
{
	public static eGAME_STATE mGameState = eGAME_STATE.InitialDelay;
	public static eCURRENT_SCENE mCurrentScene	= eCURRENT_SCENE.GamePlay;
	public static e_CAMERA_MODE mCameraMode = e_CAMERA_MODE.FreeHandView;
	public static eMENU_STATE mMenuState = eMENU_STATE.Menu;


	public static e_INSTRUCTION_TRIGGER mInsTrigger	= e_INSTRUCTION_TRIGGER.NoTrigger;
	public static e_INSTRUCTION_STATE mInsState = e_INSTRUCTION_STATE.NoInstruction;
	public static e_INSTRUCTION_STATE mInsSubState	= e_INSTRUCTION_STATE.CameraFreeHandMode;

	public static string sSceneToBeLoaded = "GameUI";

	public static string sSoundsString	=	"CheckForSounds";
	public static int iVisibleLayer = 0;
	public static int iInvisibleLayer = 31;


	// LEVEL
	public static string sTotalUnlockedLevels	= "UnloackedLevelsCount";
	public static int iNextLevelToUnlock = 0;
	public static int iCurrentLevel = 1;
	public static int iTotalLevels = 10;

	// TRAINS
	public static string sTotalTrainsUnlocked	= "UnlockedTrainCount";
	public static string sTrainsUnlockedString	= "UnlockedTrainString";
	public static int iTotalTrainsAvalaiable = 7;
	public static int i_CurrentTrainSelected = 0;
	public static int i_CurrentTrainOppSelected = 1;
	public static int iMenuEnableIndex = 0;

	public static bool isMultiPlayerMode = false;
	public static bool isBackNavigation = true;

	//Stars
	public static string sTotalCoinsAvaliable	= "CoinsAvalaibleInGame";
	public static string sTotalStarsGained = "TotalStarsGainedInAllLevels";
	public static string sStarsGainedInEachLevel = "StarsGainedInEachLevel";
	public static string loadScene = "FreeMode-Unlimited";

	public static int iRequiredCoinsForLevel6	= 10000;
	public static int iRequiredCoinsForLevel11	= 25000;
	public static int iRequiredCoinsForLevel16	= 50000;
	public static int iRequiredCoinsForLevel21	= 100000;
	public static int iRequiredCoinsForLevel26	= 150000;
	public static int iRequiredCoinsForLevel31	= 200000;

	public static int iRequiredCoinsForLevel36	= 250000;
	public static int iRequiredCoinsForLevel41	= 300000;
	public static int iRequiredCoinsForLevel46	= 350000;


	//Coin Purchase
	public static int iCoinsForMiniPack = 20000;
	public static int iCoinsForBoosterPack = 45000;
	public static int iCoinsForSuperPack = 100000;
	public static int iCoinsForProPack = 210000;
	public static int iCoinsForMegaPack = 325000;
	public static int iCoinsForUltraPack = 500000;

	// RATE
	public static string _sRatingString	= "DoneWithRating";
	public static string _sSharingString	= "DoneWithSharing";
	public static string _sRatingCancel	= "CanclRatingForCurrentGame";

	public static int iRateLevel1	= 3;
	public static int iRateLevel2	= 7;
	public static int iRateLevel3	= 13;
	public static int iRateLevel4	= 17;
	public static int iRateLevel5	= 22;
	public static int iRateLevel6	= 27;
	public static int iRateLevel7	= 30;
	public static int iRateLevel8	= 35;


	//SHARE
	public static int iShareLevel1	= 7;
	public static int iShareLevel2	= 9;
	public static int iShareLevel3	= 14;
	public static int iShareLevel4	= 19;
	public static int iShareLevel5	= 24;
	public static int iShareLevel6	= 29;
	public static int iShareLevel7	= 34;
	public static int iShareLevel8	= 35;




}
