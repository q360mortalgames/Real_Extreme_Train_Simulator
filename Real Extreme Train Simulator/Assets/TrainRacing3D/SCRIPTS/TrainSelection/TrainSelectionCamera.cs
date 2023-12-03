using UnityEngine;
using System.Collections;

public class TrainSelectionCamera : MonoBehaviour
{
	[SerializeField] GameObject trainSelectionUI;
	[Space(10)]
	[SerializeField] Transform _StaticPos;
	[SerializeField] Transform target;
	[Space(10)]
	[SerializeField] float fHeightDamping = 3;
	[SerializeField] float fRotationDamping = 3;
	[SerializeField] float rotationDamping;
	Transform _Target;


	void Start ()
	{


		trainSelectionUI.SetActive (true);
		xTouchLowerPos	= Screen.width / 8f;
		xTouchHigherPos	= Screen.width - Screen.width / 8f;
		yTouchLowerPos	= Screen.height / 10f;
		yTouchHigherPos	= Screen.height - Screen.height / 10f;
	}

	void Update ()
	{
		if (_Target == null)
			_Target	= _StaticPos;


		UpdateCameraPositionOnLateUpdateForDriverView ();
		transform.position	= Vector3.Lerp (transform.position, _Target.position, Time.deltaTime * fHeightDamping);

		if (TrainSelectionHandler.Instance.mb_IsCameraRotationEnable) {
			CheckForTouch ();

		}

	}

	public void ChangeTrainCamera (Transform _target, Transform _trainEngineTarget)
	{
		if (_target == null) {
			_Target	= _StaticPos;
			fHeightDamping	= 2f;
			fRotationDamping	= 1f;
		} else {
			_Target	= _target;
			target	= _trainEngineTarget;
			_targetStartPos	= _Target.localPosition;
			xLowerLimit = _targetStartPos.x - 2f;
			xHigherLimit	= _targetStartPos.x + 2f;

			yLowerLimit = 0;
			yHigherLimit	= _targetStartPos.y + 3f;
		}
	}



	Vector3 _startTouchPoint = Vector3.zero;
	Vector3 _currentTouchPoint = Vector3.zero;
	Vector3 _targetStartPos;
	float limitOnDirection	= 20f;
	Vector3 transformEulerAngles;

	void UpdateCameraPositionOnLateUpdateForDriverView ()
	{
		Quaternion rotation = Quaternion.LookRotation (target.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * rotationDamping);
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
			_startTouchPoint	= _touchPosInScreen;
		} else if (Input.GetMouseButtonUp (0) && _bIsTouch	== true) {
			_bIsTouch	= false;
			_touchPhaseInScreen	= TouchPhase.Ended;
			_touchPosInScreen	= Input.mousePosition;
			_currentTouchPoint	= _touchPosInScreen;
			CalculatePosChange ();
		} else if (Input.GetMouseButton (0) && _bIsTouch	== true) {
			_touchPhaseInScreen	= TouchPhase.Moved;
			_touchPosInScreen	= Input.mousePosition;
			_currentTouchPoint	= _touchPosInScreen;
			CalculatePosChange ();
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
			_startTouchPoint	= _touchPosInScreen;
		}
		else if((_touches[0].phase == TouchPhase.Ended || _touches[0].phase == TouchPhase.Canceled) && _bIsTouch	== true)
		{
			_bIsTouch	= false;
			_touchPhaseInScreen	= TouchPhase.Ended;
			_touchPosInScreen	= _touches[0].position;
			_currentTouchPoint	= _touchPosInScreen;
			CalculatePosChange();
		}
		else if((_touches[0].phase == TouchPhase.Moved || _touches[0].phase == TouchPhase.Stationary) && _bIsTouch	== true)
		{
			_touchPhaseInScreen	= TouchPhase.Moved;
			_touchPosInScreen	= _touches[0].position;
			_currentTouchPoint	= _touchPosInScreen;
			CalculatePosChange();
		}
		#endif
	}

	float xLowerLimit, xHigherLimit, yLowerLimit, yHigherLimit, zLowerLimit, zHigherLimit;
	float xTouchLowerPos, xTouchHigherPos, yTouchLowerPos, yTouchHigherPos;

	void CalculatePosChange ()
	{
		if (_startTouchPoint.x < xTouchLowerPos || _startTouchPoint.x > xTouchHigherPos || _startTouchPoint.y < yTouchLowerPos || _startTouchPoint.y > yTouchHigherPos)
			return;

		float xDiff	= _currentTouchPoint.x - _startTouchPoint.x;
		float yDiff = _currentTouchPoint.y - _startTouchPoint.y;

		_targetStartPos.x	+= xDiff / 50f;
		_targetStartPos.y	-= yDiff / 50f;

		_targetStartPos.x	= Mathf.Clamp (_targetStartPos.x, xLowerLimit, xHigherLimit);
		_targetStartPos.y	= Mathf.Clamp (_targetStartPos.y, yLowerLimit, yHigherLimit);
	
		_Target.localPosition	= _targetStartPos;
		_startTouchPoint	= _currentTouchPoint;
	}
}
