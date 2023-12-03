using UnityEngine;
using System.Collections;

public enum CAMERA_MODE
{
	FreeHand,
	DriverView,
	StationView,
	InitialView,
	CrashView,
	LevelCompleteView
}

public class CameraTouchMovementControl : MonoBehaviour {

	public static CameraTouchMovementControl Instance;


	bool isSingleTouchEnabled	= false;
	bool isMultiTouchEnabled	= false;


	// Single Touch 
	Vector3 initialPos			= Vector3.zero;
	float xDiff					= 0;
	float yDiff					= 0;
	float xOffset				= 0f;
	float yOffset				= 0f;



	Vector3 eulerAngle			= Vector3.zero;
	Vector3 CameraStartPos		= Vector3.zero;
	Quaternion CameraStartAngle	;
	Transform parent;

	public float response		= 0.1f;
	public float yMinLimit		= 10;
	public float yMaxLimit		= 80;

	// Multi Touch
	float fieldOfView			= 0;
	public float zoomMinimum	= 25f;
	public float zoomMaximum	= 75f;
	public float zoomResponse	= 0.1f;
	float zoomOffset;
	float diffInMag = 0;

	public float distance		= 10;

	public Transform _tFreeHandPos;
	public Transform _tDriverView;
	public Transform _tStationView;


	Transform target;


	private float touchRightLimit;
	private float touchLeftLimit;

	private float FreeHandCameraZoomValue;
	public CAMERA_MODE eCAMERAMODE	= CAMERA_MODE.FreeHand;

	// Use this for initialization
	void Start () 
	{
		Instance	= this;

		diffInMag = 0f;
		fieldOfView = GetComponent<Camera>().fieldOfView;
		eulerAngle = transform.eulerAngles;
		CameraStartPos	= transform.position;
		CameraStartAngle = transform.rotation;
		parent	= transform.parent;

		touchLeftLimit = Screen.width / 4;
		touchRightLimit = Screen.width - Screen.width / 4;

		target	= _tFreeHandPos;
	}
	
	// Update is called once per frame
	void Update () {
	
//		if(GlobalVariables.mGameState != eGAME_STATE.GamePlay)
//			return;


		if(eCAMERAMODE == CAMERA_MODE.DriverView)
		{
//			Vector3.Lerp(transform.position,target.position,100f);
//			Quaternion.Slerp(transform.rotation,target.rotation,100f);
//
//			camera.fieldOfView	= Mathf.Lerp(camera.fieldOfView,60f,100f);

			transform.position	= target.position;
			transform.rotation	= target.rotation;
			GetComponent<Camera>().fieldOfView	= 60f;

			return;
		}
		else if(eCAMERAMODE == CAMERA_MODE.StationView)
		{
			transform.position	= target.position;
			transform.rotation	= target.rotation;
			GetComponent<Camera>().fieldOfView	= 60f;
			return;
		}

		Touch[] _touches = Input.touches;

		if(_touches.Length == 1)
		{
			Touch _touch	= _touches[0];

			if(_touch.position.x > touchLeftLimit && _touch.position.x < touchRightLimit)
			{
				if (_touch.phase	== TouchPhase.Began)
				{
					initialPos				= Input.mousePosition;
					isSingleTouchEnabled	= true;
				}
				if ((_touch.phase	== TouchPhase.Ended || _touch.phase	== TouchPhase.Canceled) && isSingleTouchEnabled)
				{
					isSingleTouchEnabled	= false;
				}
			}
			else
				isSingleTouchEnabled	= false;
		}
		else if(_touches.Length == 2)
		{
			isSingleTouchEnabled	= false;

			Touch _touchZero	= _touches[0];
			Touch _touchOne		= _touches[1];

			if(_touchZero.position.x > touchLeftLimit && _touchZero.position.x < touchRightLimit && _touchOne.position.x > touchLeftLimit && _touchOne.position.x < touchRightLimit)
				isMultiTouchEnabled		= true;
			else
				isMultiTouchEnabled		= false;
		}
		else
		{
			isSingleTouchEnabled	= false;
			isMultiTouchEnabled		= false;
		}


		#region SINGLE TOUCH 
		if(isSingleTouchEnabled)
		{
			xDiff	= Input.mousePosition.x - initialPos.x;
			yDiff	= Input.mousePosition.y - initialPos.y;

			initialPos	= Input.mousePosition;

			if(Mathf.Abs(xDiff)> 0.01f)
				xOffset	= xDiff*response;

			if(Mathf.Abs(yDiff) > 0.01f)
			yOffset	= yDiff*response;
		}
//		else
		{
			xOffset	*= 0.95f;
			yOffset *= 0.9f;
		}



		eulerAngle = transform.eulerAngles;
		float yEulerVal = transform.eulerAngles.x - yOffset;
		yEulerVal = Mathf.Clamp (yEulerVal, yMinLimit, yMaxLimit);

		eulerAngle = eulerAngle + new Vector3 (0, xOffset, 0);
		transform.eulerAngles = new Vector3 (yEulerVal, eulerAngle.y, eulerAngle.z);

		Quaternion transformRotation	= transform.rotation;
		Vector3 transformPosition	= transformRotation * new Vector3(0,0,-distance) + target.position;
		transform.position	= transformPosition;

		transform.LookAt (_tDriverView);
		#endregion


		#region MULTITOUCH ZOOM
		if(isMultiTouchEnabled)
		{
			Touch _touchZero	= _touches[0];
			Touch _touchOne		= _touches[1];

			Vector3 _previousTouchZero	= _touchZero.position - _touchZero.deltaPosition;
			Vector3 _previousTouchOne	= _touchOne.position - _touchOne.deltaPosition;

			Vector3 _currentTouchZero	= _touchZero.position;
			Vector3 _curremtTouchOne	= _touchOne.position;

			float previousTouchMag		= (_previousTouchZero - _previousTouchOne).magnitude;
			float currentTouchMag		= (_currentTouchZero - _curremtTouchOne).magnitude;

			diffInMag				= previousTouchMag - currentTouchMag;

			if(Mathf.Abs(diffInMag) > 0.001f)
			diffInMag	= diffInMag* zoomResponse;
//			if(Mathf.Abs(diffInMag) > 0.01f)
//				zoomOffset	= 1;
//			else 
//				zoomOffset	*= 0.9f;
		}
//		else
		{
			diffInMag	*= 0.9f;
		}


		fieldOfView += diffInMag; //* zoomResponse * zoomOffset;
		fieldOfView	= Mathf.Clamp(fieldOfView,zoomMinimum,zoomMaximum);
		GetComponent<Camera>().fieldOfView	= fieldOfView;

		#endregion


		if (Input.GetKey (KeyCode.W))
			target.transform.position = target.transform.position + Vector3.forward*0.1f;
		else if(Input.GetKey(KeyCode.S))
			target.transform.position = target.transform.position - Vector3.forward*0.1f;
	}

	Vector3 targetPos;
	float f_PosLerpSpeed = 2f;
	void CheckForLerpingPosition()
	{
		Quaternion transformRotation	= transform.rotation;
		Vector3 transformPosition	= transformRotation * new Vector3(0,0,-distance) + target.position;
		transform.position	= transformPosition;
//		if(targetPos != transform.localPosition)
//		{
//			transform.localPosition	= Vector3.Lerp(transform.localPosition,targetPos,Time.deltaTime*f_PosLerpSpeed);
//		}
	}

	float ClampAngle(float angle,float min,float max)
	{
		if (angle > 360)
			angle -= 360;
		if (angle < -360)
			angle += 360;

		return Mathf.Clamp (angle, min, max);
	}

	public void OnChangeCameraButtonClick()
	{
		switch(eCAMERAMODE)
		{
		case CAMERA_MODE.FreeHand:
			target	= _tFreeHandPos;

			transform.position	= target.position;
			transform.rotation	= target.rotation;
			GetComponent<Camera>().fieldOfView	= 60f;
			break;
		case CAMERA_MODE.DriverView:
			target	= _tDriverView;
			break;
		case CAMERA_MODE.StationView:
			target	= _tStationView;
			break;
		}
	}
}
