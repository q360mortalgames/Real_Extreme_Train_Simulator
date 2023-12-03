using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstructionHandler : MonoBehaviour
{

	public static InstructionHandler Instance;
	public Vector3 AcclerationPos;
	public Vector3 BreakPos;
	public Vector3 CameraPos;
	public Vector3 HornPos;
	public Vector3 NoInstructionPos;
	public Vector3 SpeedIndicatorPos;
	public Vector3 KMPos;
	public Vector3 TimePos;
	public Vector3 SpeedLimitPos;
	public Vector3 ChangeDirectionPos;
	public Vector3 ReversePos;
	public float fDiffInMovementYDirection	= 100;

	public Text _insText;
	public Image _insTexture;
	public Text _tutorialTitle;

	public Image _insOkButton;
	public Image	_insHandTexture;

	public Image _transpTexture;

	RectTransform _rectTransform;

	public Sprite[] _handTexture;

	string AccelrationText = "Drag this slider up to accelerate the train.";

	string BreakText = "Touch and hold this button to stop the train.";

	string CameraText_freeHandView = "Tap this icon to change the Camera View.\n Touch and drag to change the angle.";

	string CameraText_DriverView	= "Tap again to change the camera view.";
	string HornText = "Use this to blow the Horn at\nthe crossing and  earn 250 Coins.";

	string NoIns = "";
	string SpeedLimiterText = "Watch out for the speed limit and drive \n responsibly.";

	string SpeedLimiterTriggerText	= "Keep an eye on the speed always!";

	string SpeedText = "Monitor the speed of the train and stay\nwithin the speed limit.";

	string XPText = "You can check distance Traveled here.";

	string ReverseText = "Use this button to Forward/Reverse";

	string TimerText = "Reach the Station in time and get 500 Coins.";

	string DirectionLeftText = "Swipe Left to direct train to left direction";
	string DirectionRightText = "Swipe Right to direct train to Right direction";
	string SignalBreakText = "Watch out signals carefully and apply break to \n avoid crashing with other trains";

	Vector3 movePos;

	void Awake ()
	{
		Instance	= this;
		if (_insText == null)
			_insText	= GameObject.Find ("InstructionText").GetComponent<Text> ();

		if (_insTexture == null)
			_insTexture	= GameObject.Find ("InsBG").GetComponent<Image> ();
//		_tutorialTitle.gameObject.SetActive (false);

		if (_insOkButton == null)
			_insOkButton	= GameObject.Find ("InsOkButton").GetComponent<Image> ();

		if (_insHandTexture == null)
			_insHandTexture	= gameObject.GetComponent<Image> ();

		_rectTransform	= GetComponent<RectTransform> ();
	}


	public void ChangeInstructionState ()
	{

		DeleteAllItweenScript ();
		SetBGVisibility (true);
		switch (GlobalVariables.mInsState) {

		case e_INSTRUCTION_STATE.NoInstruction:
			_insHandTexture.sprite	= _handTexture [0];
			_insText.text = NoIns;
			_rectTransform.anchoredPosition3D	= NoInstructionPos;
			SetBGVisibility (false);
			break;

		case e_INSTRUCTION_STATE.CameraButton:
			
			_insHandTexture.sprite	= _handTexture [0];
			_rectTransform.anchorMin = new Vector2 (1, 1);
			_rectTransform.anchorMax = new Vector2 (1, 1);
			_rectTransform.anchoredPosition3D	= CameraPos;


			if (GlobalVariables.mInsSubState == e_INSTRUCTION_STATE.CameraFreeHandMode)
				_insText.text = CameraText_freeHandView;
			else
				_insText.text = CameraText_DriverView;

			iTween.ScaleTo (gameObject, iTween.Hash ("scale", new Vector3 (0.8f, 0.8f, 0.8f), "time", 0.25f, "delay", 0.25f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong, "ignoretimescale", true));
			break;

		case e_INSTRUCTION_STATE.TimeForLevels:
			_insHandTexture.sprite	= _handTexture [0];
			_insText.text = TimerText;
			_rectTransform.anchorMin = new Vector2 (0, 1);
			_rectTransform.anchorMax = new Vector2 (0, 1);
			_rectTransform.anchoredPosition3D	= TimePos;
			movePos	= new Vector3 (_rectTransform.anchoredPosition3D.x, _rectTransform.anchoredPosition3D.y, _rectTransform.anchoredPosition3D.z);
			//iTween.MoveTo (gameObject, iTween.Hash ("y", (_insHandTexture.rectTransform.rect.height * 5f), "time", 0.5f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
			break;

		case e_INSTRUCTION_STATE.Speed:

			_insHandTexture.sprite	= _handTexture [0];
			_insText.text = SpeedText;
			_rectTransform.anchorMin = new Vector2 (0, 1);
			_rectTransform.anchorMax = new Vector2 (0, 1);
			_rectTransform.anchoredPosition3D	= SpeedIndicatorPos;
			movePos	= new Vector3 (_rectTransform.anchoredPosition3D.x, _rectTransform.anchoredPosition3D.y + fDiffInMovementYDirection, _rectTransform.anchoredPosition3D.z);
//			iTween.MoveTo (gameObject, iTween.Hash ("y", _insHandTexture.rectTransform.rect.height * 3.5f, "time", 0.5f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
			break;

		case e_INSTRUCTION_STATE.SpeedLimit:
			 
			_insHandTexture.sprite	= _handTexture [0];

			_rectTransform.anchorMin = new Vector2 (1, 0);
			_rectTransform.anchorMax = new Vector2 (1, 0);

			if (GlobalVariables.mInsTrigger == e_INSTRUCTION_TRIGGER.SpeedTrigger)
				_insText.text = SpeedLimiterTriggerText;
			else
				_insText.text = SpeedLimiterText;
			_rectTransform.anchoredPosition3D	= SpeedLimitPos;
			movePos	= new Vector3 (_rectTransform.anchoredPosition3D.x, _rectTransform.anchoredPosition3D.y, _rectTransform.anchoredPosition3D.z);
//			iTween.MoveTo (gameObject, iTween.Hash ("y", _insHandTexture.rectTransform.rect.height *1.5f, "time", 1f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
			break;

		case e_INSTRUCTION_STATE.Accelration:
			_insHandTexture.sprite	= _handTexture [0];

			_rectTransform.anchorMin = new Vector2 (1, 0);
			_rectTransform.anchorMax = new Vector2 (1, 0);
			_rectTransform.anchoredPosition3D	= AcclerationPos;
			_insText.text = AccelrationText;	
			iTween.ScaleTo (gameObject, iTween.Hash ("scale", new Vector3 (0.8f, 0.8f, 0.8f), "time", 0.25f, "delay", 0.15f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
			iTween.ScaleTo (gameObject, iTween.Hash ("scale", new Vector3 (1, 1, 1), "time", 0.15f, "delay", 1.4f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
			iTween.MoveTo (gameObject, iTween.Hash ("y", 100f, "time", 1f, "delay", 0.4f, "looptype", iTween.LoopType.pingPong, "easetype", iTween.EaseType.linear));
			break;

		case e_INSTRUCTION_STATE.Break:
			_insHandTexture.sprite	= _handTexture [1];
			_insText.text = BreakText;
			_rectTransform.anchorMin = new Vector2 (1, 0);
			_rectTransform.anchorMax = new Vector2 (1, 0);
			_rectTransform.anchoredPosition3D	= BreakPos;
			iTween.ScaleTo (gameObject, iTween.Hash ("scale", new Vector3 (0.8f, 0.8f, 0.8f), "time", 0.25f, "delay", 0.25f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong, "ignoretimescale", true));
			break;
		
		case e_INSTRUCTION_STATE.Horn:
			_insHandTexture.sprite	= _handTexture [0];
			_rectTransform.anchorMin = new Vector2 (0, 0);
			_rectTransform.anchorMax = new Vector2 (0, 0);
			_rectTransform.anchoredPosition3D	= HornPos;
			_insText.text = HornText;
			iTween.ScaleTo (gameObject, iTween.Hash ("scale", new Vector3 (0.8f, 0.8f, 0.8f), "time", 0.25f, "delay", 0.25f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong, "ignoretimescale", true));
			break;
					
		case e_INSTRUCTION_STATE.XpPoints:
			
			_insHandTexture.sprite	= _handTexture [0];
			_insText.text = XPText;
			_rectTransform.anchorMin = new Vector2 (0, 1);
			_rectTransform.anchorMax = new Vector2 (0, 1);
			_rectTransform.anchoredPosition3D	= KMPos;
			movePos	= new Vector3 (_rectTransform.anchoredPosition3D.x, _rectTransform.anchoredPosition3D.y + fDiffInMovementYDirection, _rectTransform.anchoredPosition3D.z);
//			iTween.MoveTo (gameObject, iTween.Hash ("y", _insHandTexture.rectTransform.rect.height * 3.5f, "time", 1f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong));
			break;


		case e_INSTRUCTION_STATE.Reverse:

			_insHandTexture.sprite	= _handTexture [1];
			_insText.text = ReverseText;
			_rectTransform.anchoredPosition3D	= ReversePos;
			//iTween.ScaleTo (gameObject, iTween.Hash ("scale", new Vector3 (0.8f, 0.8f, 0.8f), "time", 0.25f, "delay", 0.25f, "easetype", iTween.EaseType.linear, "looptype", iTween.LoopType.pingPong, "ignoretimescale", true));
			break;

		case e_INSTRUCTION_STATE.ChangeDirection:
			
			_insHandTexture.sprite	= _handTexture [1];
			_rectTransform.anchoredPosition3D	= ChangeDirectionPos;

			if (GlobalVariables.mInsSubState == e_INSTRUCTION_STATE.ChangeDirection_Left)
				_insText.text	= DirectionLeftText;
			else if (GlobalVariables.mInsSubState == e_INSTRUCTION_STATE.ChangeDirection_Right)
				_insText.text	= DirectionRightText;

			if (GlobalVariables.mInsSubState == e_INSTRUCTION_STATE.ChangeDirection_Right)
				movePos	= new Vector3 (_insHandTexture.rectTransform.rect.width * 2, _rectTransform.anchoredPosition3D.y, _rectTransform.anchoredPosition3D.z);
			else if (GlobalVariables.mInsSubState == e_INSTRUCTION_STATE.ChangeDirection_Left)
				movePos	= new Vector3 (0, _rectTransform.anchoredPosition3D.y, _rectTransform.anchoredPosition3D.z);

			iTween.MoveTo (gameObject, iTween.Hash ("x", movePos.x, "time", 1f, "looptype", iTween.LoopType.loop, "easetype", iTween.EaseType.linear, "ignoretimescale", true));
			break;

		case e_INSTRUCTION_STATE.Signals:
			_rectTransform.anchoredPosition3D	= NoInstructionPos;
			_insText.text = SignalBreakText;
			break;
		}
	}

	void DeleteAllItweenScript ()
	{
		iTween[] _itweeenScript	= gameObject.GetComponents<iTween> ();
		if (_itweeenScript == null || _itweeenScript.Length == 0)
			return;

		for (int i = _itweeenScript.Length - 1; i >= 0; i--) {
			Destroy (_itweeenScript [i]);
		}

		transform.localScale	= new Vector3 (1, 1, 1);
	}

	void SetBGVisibility (bool isVisible)
	{
		if (isVisible) {
			_insOkButton.enabled	= true;
//			_insOkButton.gameObject.layer	= GlobalVariables.iVisibleLayer;

			_insTexture.enabled	= true;
			_transpTexture.enabled = true;
//			_tutorialTitle.gameObject.SetActive (true);
//			_insTexture.gameObject.layer	= GlobalVariables.iVisibleLayer;

			if (GlobalVariables.mInsState == e_INSTRUCTION_STATE.CameraButton || GlobalVariables.mInsState == e_INSTRUCTION_STATE.Accelration || (GlobalVariables.mInsState == e_INSTRUCTION_STATE.Break && GlobalVariables.mGameState == eGAME_STATE.GamePlay)) {
				_insOkButton.enabled	= false;
//				_insOkButton.gameObject.layer	= GlobalVariables.iInvisibleLayer;
			}

		} else {
			
			_insOkButton.enabled	= false;
//			_insOkButton.gameObject.layer	= GlobalVariables.iInvisibleLayer;
			
			_insTexture.enabled	= false;
			_transpTexture.enabled = false;
//			_tutorialTitle.gameObject.SetActive (false);
//			_insTexture.gameObject.layer	= GlobalVariables.iInvisibleLayer;
		}
	}
}
