using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TrainBreakScript : MonoBehaviour,IPointerUpHandler,IPointerDownHandler
{
	public static TrainBreakScript _instance = null;
	public bool isReverse = false;
	public Sprite[] ReverseButtons;
	public GameObject reverseIndication;

	public static bool waitForbreak = false;

	public static TrainBreakScript Instance{
		get{
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<TrainBreakScript>() as TrainBreakScript;
			}
			return _instance;
		}
		set{
			_instance = value;
		}

	}

	void Start ()
	{
		waitForbreak = false;
		LevelEnabler.TrainDirection = 1;
		if (isReverse) {
			GetComponent<Image> ().sprite = ReverseButtons [1];
		}

	}

	public void OnPointerUp (PointerEventData eventData)
	{
		PostBreak ();
	}

	public void PostBreak(){
		if (!waitForbreak) {
			GamePlayManager.Instance.mb_IsBreak	= false;
		}
		SoundController.Instance.StopBreakSound ();
		;

		//Debug.Log ("--PostBreak..");
	}

	public void PostAIBreak(){
		if (!waitForbreak) {
			GamePlayManager.Instance.mb_IsAIBreak = false;
		}
		SoundController.Instance.StopBreakSound ();
		;

		Debug.Log ("--PostAIBreak..");
	}

	public void OnPointerDown (PointerEventData eventData)
	{		
		UseBreak ();
	}

	public void UseAIBreak(){
		if (GlobalVariables.mGameState != eGAME_STATE.GamePlay)
			return;

		if (!isReverse) {
			if (GlobalVariables.iCurrentLevel == 3) {
				GamePlayManager.Instance.IsInstructionAvlaiable = false;
			}

			if (GamePlayManager.Instance.IsInstructionAvlaiable && GlobalVariables.mInsState == e_INSTRUCTION_STATE.Break) {
				GamePlayManager.Instance.ChangeInstructionState (e_INSTRUCTION_STATE.Horn);
				GamePlayManager.Instance.f_DiffInSpeed_AI	= 0;
				GamePlayManager.Instance.mb_IsAIBreak	= true;
				SoundController.Instance.PlayBreakSound ();


			} else if (!GamePlayManager.Instance.IsInstructionAvlaiable) {
				GamePlayManager.Instance.f_DiffInSpeed_AI	= -1;
				GamePlayManager.Instance.mb_IsAIBreak	= true;
				SoundController.Instance.PlayBreakSound ();
			}
			Debug.Log ("--UseAIBreak::"+GamePlayManager.Instance.mb_IsAIBreak);
		}
	}

	public void UseBreak(){
		if (GlobalVariables.mGameState != eGAME_STATE.GamePlay || GamePlayManager.Instance.levelCrashed)
			return;

		if (!isReverse) {
			if (GlobalVariables.iCurrentLevel == 3) {
				GamePlayManager.Instance.IsInstructionAvlaiable = false;
			}
			//	Debug.Log(GamePlayManager.Instance.IsInstructionAvlaiable +" :  : "+GlobalVariables.mInsState);


			if (GamePlayManager.Instance.IsInstructionAvlaiable && GlobalVariables.mInsState == e_INSTRUCTION_STATE.Break) {

				//GameTimerHandler.Instance.isStartUpdate	= true;
				//GamePlayManager.Instance.ChangeInstructionState (e_INSTRUCTION_STATE.NoInstruction);
				GamePlayManager.Instance.ChangeInstructionState (e_INSTRUCTION_STATE.Horn);

				GamePlayManager.Instance.f_DiffInSpeed	= 0;
				GamePlayManager.Instance._gAccelSlider.value	= 0;
				GamePlayManager.Instance.mb_IsBreak	= true;
				SoundController.Instance.PlayBreakSound ();


			} else if (!GamePlayManager.Instance.IsInstructionAvlaiable) {
				GamePlayManager.Instance.f_DiffInSpeed	= -1;
				GamePlayManager.Instance._gAccelSlider.value	= 0;
				GamePlayManager.Instance.mb_IsBreak	= true;
				SoundController.Instance.PlayBreakSound ();	


			}
		}

		if (isReverse) {

			if (!GameTimerHandler.Instance.isTimmerUpdate) {
				Debug.Log ("reverse dont work");
				return;
			}
			LevelEnabler.ReverseDone = false;
			LevelEnabler.TrainDirection = -1 * (LevelEnabler.TrainDirection);

			if (LevelEnabler.TrainDirection == -1) {
				//GamePlayManager.Instance.f_DiffInSpeed	= LevelEnabler.TrainDirection;
				GamePlayManager.Instance._gAccelSlider.value	= 3;
				GamePlayManager.Instance.mb_IsBreak	= true;
				SoundController.Instance.PlayBreakSound ();

				GetComponent<Image> ().sprite = ReverseButtons [0];
				reverseIndication.SetActive (true);
			} else {
				reverseIndication.SetActive (false);

				waitForbreak = true;
				GamePlayManager.Instance.mb_IsBreak	= true;
				GamePlayManager.Instance._gAccelSlider.value	= 0.1f;
				GetComponent<Image> ().sprite = ReverseButtons [1];
				Invoke ("pickup", 0.5f);

			}
			Debug.Log ("train direction : " + LevelEnabler.TrainDirection);
		}

		//Debug.Log ("--Used break..");
	}

	void pickup ()
	{
		Debug.Log ("train direction a ");
		//if (LevelEnabler.TrainDirection == -1){
		GamePlayManager.Instance.mb_IsBreak	= false;
		GamePlayManager.Instance._gAccelSlider.value	= 3f;

		waitForbreak = false;
		//}
	}
}
