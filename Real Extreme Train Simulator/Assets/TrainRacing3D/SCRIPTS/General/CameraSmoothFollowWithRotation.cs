using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class CameraSmoothFollowWithRotation : MonoBehaviour
{

	private static CameraSmoothFollowWithRotation _instance = null;
	public static CameraSmoothFollowWithRotation Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(CameraSmoothFollowWithRotation)) as CameraSmoothFollowWithRotation; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	public Transform target;
	[HideInInspector]
	public List<Transform> _arrInitialViewList;
	Transform _tDriverView;

	Transform _tStationView;
	Transform _tFreeHandView;
	Transform _tLevelCompleteView;
	Transform _tLevelFailedView;

	Transform _tBridgeView1;
	Transform _tTrainFullView;

	[HideInInspector]
	public float distance;
	[HideInInspector]
	public float height;
	[HideInInspector]
	public Vector3 rotationOffSet;
	[HideInInspector]
	public float rotationDamping;
	[HideInInspector]
	public float heightDamping;

	float XTouchSwipeLowerLimit;
	float XTouchSwipeHighrLimit;
	float YTouchSwipeLowerLimit;
	float YTouchSwipeHigherLimit;

	public Transform[] cameraPositions;


	void OnStartResetCameraValues ()
	{
		if (GlobalVariables.isMultiPlayerMode) {
			isShowCountDown = true;
		}

		GlobalVariables.mGameState = eGAME_STATE.InitialDelay;
		mf_TimerForCameraChange = 0f;
		_icurrentInitialCamera	= 0;
		_arrInitialViewList.Clear ();
		_arrInitialViewList	= null;
	}

	int totalInitalViews = 3;

	void SetLayerClippingDistanceOnStart ()
	{
		string _transString = "";
		if (_arrInitialViewList == null)
			_arrInitialViewList	= new List<Transform> ();
		for (int i = 1; i <= totalInitalViews; i++) {
			_transString	= "Pos" + i;
			Transform _tr	= GameObject.Find (_transString).transform;
			_arrInitialViewList.Add (_tr);
		}

		float[] distance = new float[32];
		distance [9]	= 200;    	// Layer 9 
		distance [8]	= 200;    	// Layer 10 
		distance [10]	= 200;   	// Layer 11
		distance [11]	= 200;  	// Layer 12
		Camera _Camera	= GetComponent<Camera> ();
		_Camera.layerCullDistances	= distance;
	}

	void Start ()
	{
		OnStartResetCameraValues ();
		SetLayerClippingDistanceOnStart ();

		XTouchSwipeLowerLimit	= Screen.width / 8f;
		XTouchSwipeHighrLimit	= Screen.width - Screen.width / 3.1f;

		YTouchSwipeLowerLimit	= Screen.height / 5f;
		YTouchSwipeHigherLimit	= Screen.height - Screen.height / 6f; 

		mf_TargetTimerForCameraChange = GlobalVariables.isMultiPlayerMode?1.5f:1.0f;

		GlobalVariables.mCameraMode	= e_CAMERA_MODE.InitialView;
		Invoke ("FindObjectsAtStartForPsotions", 0.1f);
		SetTargetForCameraToFollow ();
	}

	void FindObjectsAtStartForPsotions ()
	{
		_tDriverView	= cameraPositions [0];
		_tFreeHandView	= cameraPositions [1];
		_tBridgeView1	= cameraPositions [2];
		_tTrainFullView	= cameraPositions [3];
	}

	TouchPhase _touchPhaseInScreen;
	bool _bIsTouch	= false;
	Vector3 _touchPosInScreen;

	void CheckForTouch ()
	{
#if UNITY_EDITOR
		if (Input.GetMouseButtonDown (0) && _bIsTouch	== false) {
			_bIsTouch	= true;
			_touchPhaseInScreen	= TouchPhase.Began;
			_touchPosInScreen	= Input.mousePosition;
		} else if (Input.GetMouseButtonUp (0) && _bIsTouch	== true) {
			_bIsTouch	= false;
			_touchPhaseInScreen	= TouchPhase.Ended;
			_touchPosInScreen	= Input.mousePosition;
		} else if (Input.GetMouseButton (0) && _bIsTouch	== true) {
			_touchPhaseInScreen	= TouchPhase.Moved;
			_touchPosInScreen	= Input.mousePosition;
		}



#elif UNITY_ANDROID
		if(Input.touchCount <= 0)
			return;
		
		Touch[] _touches	= Input.touches;
		if(_touches[0].phase == TouchPhase.Began && _bIsTouch	== false)
		{
			_bIsTouch	= true;
			_touchPhaseInScreen	= TouchPhase.Began;
			_touchPosInScreen	= _touches[0].position;
		}
		else if((_touches[0].phase == TouchPhase.Ended || _touches[0].phase == TouchPhase.Canceled) && _bIsTouch	== true)
		{
			_bIsTouch	= false;
			_touchPhaseInScreen	= TouchPhase.Ended;
			_touchPosInScreen	= _touches[0].position;
		}
		else if((_touches[0].phase == TouchPhase.Moved || _touches[0].phase == TouchPhase.Stationary) && _bIsTouch	== true)
		{
			_touchPhaseInScreen	= TouchPhase.Moved;
			_touchPosInScreen	= _touches[0].position;
		}
#endif
	}

	float mf_TimerForCameraChange = 0;
	float mf_TargetTimerForCameraChange;
	int _icurrentInitialCamera = 0;
	int _CameraIndex = 2;

	[SerializeField] Sprite[] textureCountDown;
	[SerializeField] Image count;

	bool isShowCountDown;
	void RequestToHideCountDown(){
		isShowCountDown = false;
		count.gameObject.SetActive (false);
		RequestToDeductEntryFee ();
	}

	void RequestToDeductEntryFee(){
	
		if (GameManager.Instance.totalCoins >= GameManager.Instance.multiplayerEntryFee) {
			
			GameManager.Instance.isMultiPlayerRaceStart = true;
			GameManager.Instance.totalCoins = GameManager.Instance.totalCoins - GameManager.Instance.multiplayerEntryFee;
			PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);
		}
	}

	void LateUpdate ()
	{
		if (GlobalVariables.isMultiPlayerMode && isShowCountDown) {
			count.sprite = textureCountDown [_CameraIndex];
		}

		if (TrainMovementScript.Instance && GlobalVariables.mGameState == eGAME_STATE.InitialDelay) {

			mf_TimerForCameraChange	+= Time.deltaTime;

			transform.position	= _arrInitialViewList [_icurrentInitialCamera].position;
			transform.rotation	= _arrInitialViewList [_icurrentInitialCamera].rotation;

			if (mf_TimerForCameraChange >= mf_TargetTimerForCameraChange) {
				mf_TimerForCameraChange	= 0;
				_icurrentInitialCamera++;

				if (_CameraIndex>0 && GlobalVariables.isMultiPlayerMode) {
					_CameraIndex--;
					//iTween.ScaleFrom(count.gameObject, iTween.Hash("Scale", Vector3.zero, "time", 0.5f, "delay", 0));
				}

				if (_icurrentInitialCamera == _arrInitialViewList.Count - 1) {
					mf_TargetTimerForCameraChange *= 1.5f;
					TrainMovementScript.Instance.CloseDoor ();
				}

				if (_icurrentInitialCamera >= _arrInitialViewList.Count) {
					
					if (GlobalVariables.isMultiPlayerMode) {						
						Invoke ("RequestToHideCountDown", 1.0f);
					}

					GamePlayManager.Instance.StartGamePlayAfterInitialCameraPos ();
					GlobalVariables.mCameraMode	= e_CAMERA_MODE.FreeHandView;
					SetTargetForCameraToFollow ();
				}
			}
			return;
		}


		if (target == null)
			return;

		CheckForTouch ();

		switch (GlobalVariables.mCameraMode) {

		case e_CAMERA_MODE.FreeHandView:
			OnFreeHandModeTouch ();
			UpdateCameraPositionOnLateUpdateForDriverView ();
			break;

		case e_CAMERA_MODE.DriverView:
			
			if (_bIsTouch)
				OnDriverModeTouch (_touchPhaseInScreen, _touchPosInScreen);
			else
				rotationOffSet	= Vector3.zero;
			UpdateCameraPositionOnLateUpdateForDriverView ();
			break;

		case e_CAMERA_MODE.StationView:
			transform.position	= target.position;
			transform.rotation	= target.rotation;
			break;

		case e_CAMERA_MODE.CrashView:
			rotationOffSet.y += 0.25f;
			UpdateCameraPositionOnLateUpdateForDriverView ();
			break;

		case e_CAMERA_MODE.BridgeView_1:
			transform.position	= target.position;
			transform.rotation	= Quaternion.Slerp (transform.rotation, target.rotation, Time.deltaTime * rotationDamping);
			break;

		case e_CAMERA_MODE.TrainFullView:
			transform.position	= target.position;
			transform.rotation	= target.rotation;
			break;
		}


	}

	public void SetTargetForCameraToFollow ()
	{
		//Debug.Log ("Camera View :"+GlobalVariables.mCameraMode);

		switch (GlobalVariables.mCameraMode) {

		case e_CAMERA_MODE.FreeHandView:

			rotationOffSet	= new Vector3 (0, 10, 0);
			target = _tFreeHandView;
			distance	= 10f;
			height = 3.5f;

			heightDamping	= 2;
			rotationDamping	= 3;
			break;

		case e_CAMERA_MODE.DriverView:
			
			rotationOffSet	= Vector3.zero;
			distance	= 1f;
			height = 0f;

			heightDamping	= 100f;
			rotationDamping	= 100f;


			target = _tDriverView;
			transform.position	= target.position;
			transform.rotation	= target.rotation;
			break;

		case e_CAMERA_MODE.StationView:

			if (GamePlayManager.Instance.mb_IsStationLeftSide) {
				target = cameraPositions [4];
			} else {
				target = cameraPositions [5];
			}
			distance	= 10f;
			height = 5f;
			break;

		case e_CAMERA_MODE.CrashView:
			
			GameObject _obj = GameObject.Find ("LevelFailedCameraView");

			if (_obj) {
				_tLevelFailedView	= _obj.transform;
				target	= _tLevelFailedView;
			} else
				target	= _tFreeHandView;
			distance	= 15f;
			height = 15f;
			rotationOffSet	= new Vector3 (0, 0, 0);
			heightDamping	= 2;
			rotationDamping	= 1;
			break;

		case e_CAMERA_MODE.TrainFullView:
			
			rotationOffSet	= new Vector3 (0, 0, 0);
			target = _tTrainFullView;
			break;
		}
	}

	public void SetCameraToStatic (Transform _targetTransform, e_CAMERA_MODE _cameraMode)
	{
		GlobalVariables.mCameraMode	= _cameraMode;
		switch (_cameraMode) {
		case e_CAMERA_MODE.BridgeView_1:
			
			rotationOffSet	= new Vector3 (0, 0, 0);
			target	= _tBridgeView1;
			heightDamping	= 3f;
			rotationDamping	= 5f;
			break;

		case e_CAMERA_MODE.DriverView:
			
			rotationOffSet	= Vector3.zero;
			distance	= 1f;
			height = 0f;

			heightDamping	= 100f;
			rotationDamping	= 100f;


			target = _tDriverView;
			transform.position	= target.position;
			transform.rotation	= target.rotation;
			break;
		}
	}
	//******* VIEW FREE HAND MODE ***********//
	bool isSingleTouch = false;
	Vector3 mv_InitialPos = Vector3.zero;
	float xDiff, yDiff, xOffset, yOffset;
	[SerializeField] float mf_FreeHandResponse = 0.1f;

	void OnFreeHandModeTouch ()
	{
		if (GamePlayManager.Instance.isTrainOnTunnel) {
			return;
		}

		#region SINGLE TOUCH 
#if UNITY_EDITOR || UNITY_WEBPLAYER
		if (Input.mousePosition.x > XTouchSwipeLowerLimit && Input.mousePosition.x < XTouchSwipeHighrLimit && Input.mousePosition.y > YTouchSwipeLowerLimit && Input.mousePosition.y < YTouchSwipeHigherLimit) {
			if (Input.GetMouseButtonDown (0)) {
				isSingleTouch	= true;
				mv_InitialPos	= Input.mousePosition;
			} else if (Input.GetMouseButtonUp (0)) {
				isSingleTouch	= false;
			}
		} else
			isSingleTouch	= false;

#elif UNITY_ANDROID
		if(Input.touchCount == 1)
		{
			Touch _touch = Input.GetTouch(0);
			if(_touch.phase	== TouchPhase.Began && Input.mousePosition.x > XTouchSwipeLowerLimit && Input.mousePosition.x < XTouchSwipeHighrLimit && Input.mousePosition.y > YTouchSwipeLowerLimit && Input.mousePosition.y < YTouchSwipeHigherLimit)
			{
				mv_InitialPos	= _touch.position;
				isSingleTouch	= true;
			}
			else if(_touch.phase	== TouchPhase.Ended || _touch.phase == TouchPhase.Canceled)
			{
				isSingleTouch	= false;
			}

//			if(isSingleTouch && GamePlayManager.Instance.mb_IsTouchHasTexture)
//				isSingleTouch	= false;
		}
		else
			isSingleTouch	= false;
#endif
		

		if (isSingleTouch) {
			xDiff	= Input.mousePosition.x - mv_InitialPos.x;
			yDiff	= Input.mousePosition.y - mv_InitialPos.y;

			mv_InitialPos	= Input.mousePosition;

			if (Mathf.Abs (xDiff) > 0.001f) {
				xOffset	= xDiff * mf_FreeHandResponse;
			}

			if (Mathf.Abs (yDiff) > 0.001f) {
				yOffset	= yDiff * mf_FreeHandResponse;
			}
		}
//		else
		{
			xOffset	*= 0.9f;
			yOffset	*= 0.9f;
		}

		rotationOffSet.x	-= yOffset; 
		rotationOffSet.y	-= xOffset;
	
		rotationOffSet.x	= 0f;
		height	-= yOffset;
		height	= Mathf.Clamp (height, 3.5f, 10);

//		rotationOffSet.x	= clampAngles(rotationOffSet.x);
//		rotationOffSet.y	= clampAngles(rotationOffSet.y);

		#endregion
	}

	float clampAngles (float val)
	{
		if (val > 360)
			val	-= 360;
		if (val < -360)
			val	+= 360;

		return val;



	}


	//****** VIEW DRIVER MODE ****************//
	Vector3 _startTouchPoint = Vector3.zero;
	Vector3 _currentTouchPoint = Vector3.zero;

	void OnDriverModeTouch (TouchPhase _phase, Vector3 _touchPos)
	{
		if (_phase == TouchPhase.Began) {
			rotationOffSet	= Vector3.zero;
			_startTouchPoint	= _touchPos;
		} else if (_phase == TouchPhase.Ended || _phase == TouchPhase.Canceled) {
			rotationOffSet = Vector3.zero;
			_startTouchPoint	= Vector3.zero;
			_currentTouchPoint	= Vector3.zero;

		} else if (_phase == TouchPhase.Moved) {
			if (_touchPos.x < XTouchSwipeLowerLimit || _touchPos.x > XTouchSwipeHighrLimit || _touchPos.y < YTouchSwipeLowerLimit || _touchPos.y > YTouchSwipeHigherLimit)
				return;

			_currentTouchPoint	= _touchPos;
			rotationOffSet = _currentTouchPoint - _startTouchPoint;
			rotationOffSet.x	= rotationOffSet.x / 10f;
			rotationOffSet.y	= Mathf.Clamp (rotationOffSet.x, -limitOnDirection, limitOnDirection);
		}
	}

	float limitOnDirection	= 20f;
	Vector3 transformEulerAngles;

	void UpdateCameraPositionOnLateUpdateForDriverView ()
	{

		float wantedRotationInY	= target.eulerAngles.y - rotationOffSet.y;
		float wantedRotationInX	= target.eulerAngles.x + rotationOffSet.x;
		float wantedHeight = target.position.y + height;

		transformEulerAngles = transform.eulerAngles;
		float currentRotationYOffSet	= transformEulerAngles.y;
		float currentRotationXOffSet	= transformEulerAngles.x;
		float currentHeight	= transform.position.y;

		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		currentRotationYOffSet = Mathf.LerpAngle (currentRotationYOffSet, wantedRotationInY, rotationDamping * Time.deltaTime);
		currentRotationXOffSet = Mathf.LerpAngle (currentRotationXOffSet, wantedRotationInX, rotationDamping * Time.deltaTime);

		Quaternion rotationOfCamera	= Quaternion.Euler (0, currentRotationYOffSet, 0);

//		transformEulerAngles	= transformEulerAngles + new Vector3(0,currentRotationYOffSet,0);
//		transform.eulerAngles = new Vector3 (currentRotationYOffSet, transformEulerAngles.y, transformEulerAngles.z);
//		rotationOfCamera	= transform.rotation;

		transform.position	= target.position + (rotationOfCamera * new Vector3 (0, 0, -distance));
		transform.position	= new Vector3 (transform.position.x, currentHeight, transform.position.z);
		transform.LookAt (target);
	}
}
