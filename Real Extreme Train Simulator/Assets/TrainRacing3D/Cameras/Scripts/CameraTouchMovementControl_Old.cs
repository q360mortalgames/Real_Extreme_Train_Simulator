using UnityEngine;
using System.Collections;

public enum eCAMERA_MODE
{
	FreeHand,
	Static
}

public class CameraTouchMovementControl_Old : MonoBehaviour {

	public static CameraTouchMovementControl_Old Instance;


	bool isSingleTouchEnabled	= false;
	bool isMultiTouchEnabled	= false;


	// Single Touch 
	Vector3 initialPos			= Vector3.zero;
	float xDiff					= 0;
	float yDiff					= 0;
	float xOffset				= 0f;
	float yOffset				= 0f;



	Vector3 eulerAngle			= Vector3.zero;

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
	

	private float touchRightLimit;
	private float touchLeftLimit;

	private float FreeHandCameraZoomValue;
	public eCAMERA_MODE mCameraMode	= eCAMERA_MODE.FreeHand;

	GameObject _cameraObj;

	void Awake()
	{
		Instance	= this;
		_cameraObj	= transform.GetChild(0).gameObject;


//		float[32] cullMaskArray ;

		float [] cullMaskArray;
		cullMaskArray	= new float[32];
		cullMaskArray[13]	= 50;
		cullMaskArray[19]	= 50;
		cullMaskArray[28]	= 50;	
		Camera.main.layerCullDistances	= cullMaskArray;

//		cullMaskArray[14]	= 50;
//		Camera.main.layerCullDistances	= cullMaskArray;
	}

	// Use this for initialization
	void Start () 
	{


		diffInMag = 0f;
		fieldOfView = Camera.main.fieldOfView;
		eulerAngle = transform.eulerAngles;

		touchLeftLimit = Screen.width / 4;
		touchRightLimit =  Screen.width - Screen.width / 4;
	}
	
	// Update is called once per frame
	void Update () {
	
//		if(GlobalVariables.mGameState	!= eGAME_STATE.GamePlay && GlobalVariables.mGameState != eGAME_STATE.Unloading)
//			return;

		#if UNITY_EDITOR || UNITY_WEBPLAYER
		if(Input.GetMouseButton(0))
		{
			//			Touch _touch	= _touches[0];
			
			if(Input.mousePosition.x > touchLeftLimit && Input.mousePosition.x < touchRightLimit)
			{
				//				if (_touch.phase	== TouchPhase.Began)
				if(Input.GetMouseButtonDown(0))
				{
					initialPos				= Input.mousePosition;
					isSingleTouchEnabled	= true;
				}
				if (Input.GetMouseButtonUp(0) && isSingleTouchEnabled)
				{
					isSingleTouchEnabled	= false;
				}
			}
			else
				isSingleTouchEnabled	= false;
		}
		else
		{
			isSingleTouchEnabled	= false;
			isMultiTouchEnabled		= false;
		}
	#elif UNITY_ANDROID
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

#endif

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
		transform.eulerAngles = new Vector3 (yEulerVal, eulerAngle.y, 0);
	
		#endregion

//#if UNITY_ANDROID
//
//		#region MULTITOUCH ZOOM
//		if(isMultiTouchEnabled)
//		{
//			Touch _touchZero	= _touches[0];
//			Touch _touchOne		= _touches[1];
//
//			Vector3 _previousTouchZero	= _touchZero.position - _touchZero.deltaPosition;
//			Vector3 _previousTouchOne	= _touchOne.position - _touchOne.deltaPosition;
//
//			Vector3 _currentTouchZero	= _touchZero.position;
//			Vector3 _curremtTouchOne	= _touchOne.position;
//
//			float previousTouchMag		= (_previousTouchZero - _previousTouchOne).magnitude;
//			float currentTouchMag		= (_currentTouchZero - _curremtTouchOne).magnitude;
//
//			diffInMag				= previousTouchMag - currentTouchMag;
//
//			if(Mathf.Abs(diffInMag) > 0.001f)
//			diffInMag	= diffInMag* zoomResponse;
////			if(Mathf.Abs(diffInMag) > 0.01f)
////				zoomOffset	= 1;
////			else 
////				zoomOffset	*= 0.9f;
//		}
////		else
//		{
//			diffInMag	*= 0.9f;
//		}
//
//
//		fieldOfView += diffInMag; //* zoomResponse * zoomOffset;
//		fieldOfView	= Mathf.Clamp(fieldOfView,zoomMinimum,zoomMaximum);
//		camera.fieldOfView	= fieldOfView;
//
//		#endregion
//#endif



		CheckForLerpingPosition();
	}

	Vector3 targetPos;
	float f_PosLerpSpeed = 2f;
	void CheckForLerpingPosition()
	{
		if(targetPos != transform.localPosition)
		{
			transform.localPosition	= Vector3.Lerp(transform.localPosition,targetPos,Time.deltaTime*f_PosLerpSpeed);
		}
	}


	float ClampAngle(float angle,float min,float max)
	{
		if (angle > 360)
						angle -= 360;
		if (angle < -360)
						angle += 360;


		return Mathf.Clamp (angle, min, max);
	}

	#region STATIC CAMERA HANDLER 

	Vector3 initialCameraPos;

	[SerializeField] Vector3 v_posTractorInitialPos ;
	[SerializeField] Vector3 v_posTruckInitialPos ;
	[SerializeField] Vector3 v_posCombinerInitialPos ;

	public void OnChangeCameraPosition(bool isAddTool)
	{
//		if(!isAddTool)
//		{
//			switch(GlobalVariables.mVehicleType)
//			{
//			case eVEHICLETYPES.Tractor:
//				targetPos	= v_posTractorInitialPos;
//				_cameraObj.transform.localPosition	= new Vector3(0,0,-10);
//				break;
//			case eVEHICLETYPES.Truck:
//				targetPos	= v_posTruckInitialPos;
//				_cameraObj.transform.localPosition	= new Vector3(0,0,-10);
//				break;
//			case eVEHICLETYPES.HarvestCombiner:
//				targetPos	= v_posCombinerInitialPos;
//				_cameraObj.transform.localPosition	= new Vector3(0,0,-12);
//				break;
//			}
//		}
//		else
//		{
//			switch(GlobalVariables.mVehicleType)
//			{
//			case eVEHICLETYPES.Tractor:
//			{
//				_cameraObj.transform.localPosition	= new Vector3(0,0,-10);
//
//				switch(GlobalVariables.mToolType)
//				{
//				case eTOOLTYPE.Seeding:
//					break;
//				case eTOOLTYPE.TractorTrailor_1:
//					targetPos	= targetPos + new Vector3(0,0,-2f);
//					break;
//				case eTOOLTYPE.TractorTrailor_2:
//					targetPos	= targetPos + new Vector3(0,0,-2f);
//					break;
//				case eTOOLTYPE.TractorTrailor_3:
//					targetPos	= targetPos + new Vector3(0,0,-2f);
//					break;
//				case eTOOLTYPE.Plow:
//					break;
//				}
//			}
//				break;
//			case eVEHICLETYPES.HarvestCombiner:
//				_cameraObj.transform.localPosition	= new Vector3(0,0,-15f);
//				break;
//			}
//		}
	}
	#endregion

}
