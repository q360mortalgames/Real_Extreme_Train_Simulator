using UnityEngine;
using System.Collections;

[System.Serializable]
public class Doors
{
	public GameObject _DoorLF;
	public GameObject _DoorLB;
	public GameObject _DoorRF;
	public GameObject _DoorRB;
}

public class TrainMovementScript : MonoBehaviour
{
	private static TrainMovementScript _instance = null;
	public static TrainMovementScript Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(TrainMovementScript)) as TrainMovementScript; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	[SerializeField] Rigidbody _EngineRigidBody;
	[HideInInspector] public int m_iSpeedLimiterValue	= 25;



	[HideInInspector] public float m_fTrainSpeed = 0;
	float _fTrainMoveVelocity = 0;
	public int _iConstantForMoveVelocity = 0;


	[SerializeField] Rigidbody[] _WheelRigidBodys;
	[SerializeField] float _fWheelMassOnMovement;
	[SerializeField] float _fWheelDragOnMovement;
	[SerializeField] float _fMaxSpeedOfTrain;

	[SerializeField] Light[] _TrainLights;

	public GameObject[] _LeftSideDoorPoints;
	public GameObject[] _RightSideDoorPoints;

	public Transform[] cameraPositions;


	void Start ()
	{
		selectedTrainID	= GlobalVariables.i_CurrentTrainSelected;
		SetLights (false);
		OpenDoor ();
		SetDataInStart ();
	}

	void SetDataInStart ()
	{
		ResetSpeedLimiterValues (60);
	}

	bool ISKinematic	= false;

	void LateUpdate ()
	{
		
		if (!GlobalVariables.isMultiPlayerMode && _EngineRigidBody.gameObject.transform.localPosition.z > 0) {
			GamePlayManager.Instance.InGameDistance = _EngineRigidBody.gameObject.transform.localPosition.z / 500;
			GamePlayManager.Instance._KMText.text = (GamePlayManager.Instance.InGameDistance).ToString ("0.00");
		}

		if (!ISKinematic && (GlobalVariables.mGameState == eGAME_STATE.InitialDelay || GlobalVariables.mGameState == eGAME_STATE.Station || GlobalVariables.mGameState == eGAME_STATE.LevelComplete || GlobalVariables.mGameState == eGAME_STATE.LevelFailed || GlobalVariables.mGameState == eGAME_STATE.Rate || GlobalVariables.mGameState == eGAME_STATE.ExitPopUp)) {
			ISKinematic	= true;
//			_EngineRigidBody.isKinematic	= ISKinematic;
			_EngineRigidBody.constraints	= RigidbodyConstraints.None;
		} else if (ISKinematic && (GlobalVariables.mGameState == eGAME_STATE.GamePlay || GlobalVariables.mGameState == eGAME_STATE.Instruction || GlobalVariables.mGameState == eGAME_STATE.Pause)) {
			ISKinematic	= false;
			_EngineRigidBody.constraints	= RigidbodyConstraints.FreezePositionY;
//			_EngineRigidBody.isKinematic	= ISKinematic;
		}

		if (GlobalVariables.mGameState != eGAME_STATE.GamePlay && GlobalVariables.mGameState == eGAME_STATE.InitialDelay) {
			for (int i = 0; i < _WheelRigidBodys.Length; i++) {
				_fTrainMoveVelocity	= 0f;
				_WheelRigidBodys [i].AddRelativeTorque (_fTrainMoveVelocity, 0, 0);
			}
			return;
		}

		if (_WheelRigidBodys.Length <= 0)
			return;
		
		m_fTrainSpeed	= (int)(_WheelRigidBodys [0].velocity.sqrMagnitude) / 5;

		UpdateWheelRigidBodies ();
		CheckForStationStop ();

	}

	float fUpdatedMass	= 0;
	float fTrainMaximumTorque = 0;
	bool ISBreak;

	float dragSpeed	= 0.001f;

	public void UpdateWheelRigidBodies ()
	{
		if (_WheelRigidBodys.Length <= 0)
			return;

		if (GlobalVariables.mGameState != eGAME_STATE.GamePlay)
			return;

		RedefineMaximumLimitOfTrain ();
		ISBreak	= getBreak;//GamePlayManager.Instance.mb_IsBreak;//getBreak;

		for (int i = 0; i < _WheelRigidBodys.Length; i++) {

			if (ISBreak) {
				_fTrainMoveVelocity	= -5f;
				_WheelRigidBodys [i].AddRelativeTorque (_fTrainMoveVelocity, 0, 0);
				//Debug.Log ("--ISBreak"+GetComponent<TrainCollisionMultiPlayerHandler> ().isAI);
			} else if (LevelEnabler.TrainDirection == 1 || LevelEnabler.TrainDirection==-1) {
				fTrainMaximumTorque	= _iConstantForMoveVelocity * getSpeed;

				if (m_fTrainSpeed > 40) {
					dragSpeed	= 0.0001f;
				} else if (m_fTrainSpeed < 5) {
					dragSpeed	= 0.009f;
				} else {
					dragSpeed	= 0.001f;
				}

				_fTrainMoveVelocity = Mathf.Lerp (_fTrainMoveVelocity, fTrainMaximumTorque, dragSpeed * Time.deltaTime);

				if(_fTrainMoveVelocity<=0){
					_fTrainMoveVelocity = 0.1f;
				}

				if (m_fTrainSpeed < currentMaxSpeed)
					_WheelRigidBodys [i].AddRelativeTorque ((LevelEnabler.TrainDirection * (_fTrainMoveVelocity)), 0, 0);
			} else if (LevelEnabler.TrainDirection == -1) {
				fTrainMaximumTorque	= _iConstantForMoveVelocity * 0.5f * getSpeed;


				if (m_fTrainSpeed > 1 && !LevelEnabler.ReverseDone) {
					dragSpeed	= 5f;
				} else if (LevelEnabler.ReverseDone) {
					dragSpeed	= 10f;

				}

				_fTrainMoveVelocity = Mathf.Lerp (_fTrainMoveVelocity, fTrainMaximumTorque, dragSpeed * Time.deltaTime);

				if (m_fTrainSpeed < currentMaxSpeed && !LevelEnabler.ReverseDone) {
					
					_WheelRigidBodys [i].AddRelativeTorque ((LevelEnabler.TrainDirection * (_fTrainMoveVelocity)), 0, 0);
				} else if (m_fTrainSpeed < 5 && LevelEnabler.ReverseDone) {
					//Debug.Log ("torque : " + GamePlayManager.Instance.f_DiffInSpeed);
					_WheelRigidBodys [i].AddRelativeTorque ((LevelEnabler.TrainDirection * (_fTrainMoveVelocity)), 0, 0);
				} 
			
			}
		}
		CheckForTrainDerailDueToOverSpeed ();
	}

	public float currentMaxSpeed	= 0f;
	[SerializeField] float diffInLimitValues;

	void RedefineMaximumLimitOfTrain ()
	{
		currentMaxSpeed	= _fMaxSpeedOfTrain * getSpeed;
		currentMaxSpeed	= Mathf.Clamp (currentMaxSpeed, 0, _fMaxSpeedOfTrain);
	}

	bool isWaringIssued	= false;

	void CheckForTrainDerailDueToOverSpeed ()
	{
		if (isWaringIssued) {
			float waringingResetSpeed	= m_iSpeedLimiterValue - ((float)m_iSpeedLimiterValue) / 5f;
			if (m_fTrainSpeed < waringingResetSpeed)
				isWaringIssued	= false;
			return;
		}
		float maximumObtainableSpeed	= m_iSpeedLimiterValue + ((float)m_iSpeedLimiterValue) / 2f;

	}
	[SerializeField] TrainCollisionHandler _scrEngineColliderScript;

	void CheckForStationStop ()
	{
		if (GlobalVariables.mGameState != eGAME_STATE.GamePlay)
			return;
		
		if (!GlobalVariables.isMultiPlayerMode) {			

			if (m_fTrainSpeed <= 0f && _scrEngineColliderScript != null && _scrEngineColliderScript.mb_IsStationTriggered && _WheelRigidBodys [0].velocity.magnitude <= 1f) {
				_scrEngineColliderScript.mb_IsStationTriggered	= false;

				Debug.Log ("--OnStationStop.."+" isReverse"+GamePlayManager.Instance.isReverse);
				if (!GamePlayManager.Instance.isReverse && !GamePlayManager.Instance.levelCrashed) {
					GamePlayManager.Instance.OnStationStop ();
					_scrEngineColliderScript.DisableCollider ();

				}

			}
		}
	}

	#region SETTER AND GETTER
	public void ResetSpeedLimiterValues (int SpeedLimit)
	{
		m_iSpeedLimiterValue	= SpeedLimit;
		GamePlayManager.Instance.SetSpeedLimit (m_iSpeedLimiterValue);

	}

	public void SetRigidBodyKinematic (bool isKinematic)
	{

	}
	#endregion

	#region DOOR

	//***** DOORS

	[Space (10)]
	public Doors[] _CompartmentDoors;
	public float _fDoorOpenXVal;
	public float _fDoorOpenZVal;

	int selectedTrainID;
	bool isLeft = true;
	public float doorOpenTimer	= 0.5f;

	public void OpenDoor ()
	{
		if (GlobalVariables.isMultiPlayerMode) {
			return;
		}
		Vector3 pos;
		for (int i = 0; i < _CompartmentDoors.Length; i++) {
			if (GamePlayManager.Instance.mb_IsStationLeftSide) {
				pos	= _CompartmentDoors [i]._DoorLF.transform.localPosition;
				pos	= pos + new Vector3 (-_fDoorOpenXVal, 0, _fDoorOpenZVal);
				iTween.MoveTo (_CompartmentDoors [i]._DoorLF, iTween.Hash ("position", pos, "time", doorOpenTimer, "easetype", iTween.EaseType.linear, "islocal", true));

				pos	= _CompartmentDoors [i]._DoorLB.transform.localPosition;
				pos	= pos + new Vector3 (-_fDoorOpenXVal, 0, -_fDoorOpenZVal);
				iTween.MoveTo (_CompartmentDoors [i]._DoorLB, iTween.Hash ("position", pos, "time", doorOpenTimer, "easetype", iTween.EaseType.linear, "islocal", true));
			} else {
				pos	= _CompartmentDoors [i]._DoorRF.transform.localPosition;
				pos	= pos + new Vector3 (_fDoorOpenXVal, 0, _fDoorOpenZVal);
				iTween.MoveTo (_CompartmentDoors [i]._DoorRF, iTween.Hash ("position", pos, "time", doorOpenTimer, "easetype", iTween.EaseType.linear, "islocal", true));
				
				pos	= _CompartmentDoors [i]._DoorRB.transform.localPosition;
				pos	= pos + new Vector3 (_fDoorOpenXVal, 0, -_fDoorOpenZVal);
				iTween.MoveTo (_CompartmentDoors [i]._DoorRB, iTween.Hash ("position", pos, "time", doorOpenTimer, "easetype", iTween.EaseType.linear, "islocal", true));

			}
		}
	}

	public void CloseDoor ()
	{
		if (GlobalVariables.isMultiPlayerMode) {
			return;
		}
		Vector3 pos;
		for (int i = 0; i < _CompartmentDoors.Length; i++) {
			if (GamePlayManager.Instance.mb_IsStationLeftSide) {
				pos	= _CompartmentDoors [i]._DoorLF.transform.localPosition;
				pos	= pos + new Vector3 (_fDoorOpenXVal, 0, -_fDoorOpenZVal);
				iTween.MoveTo (_CompartmentDoors [i]._DoorLF, iTween.Hash ("position", pos, "time", doorOpenTimer, "easetype", iTween.EaseType.linear, "islocal", true));
				
				pos	= _CompartmentDoors [i]._DoorLB.transform.localPosition;
				pos	= pos + new Vector3 (_fDoorOpenXVal, 0, _fDoorOpenZVal);
				iTween.MoveTo (_CompartmentDoors [i]._DoorLB, iTween.Hash ("position", pos, "time", doorOpenTimer, "easetype", iTween.EaseType.linear, "islocal", true));
			} else {
				pos	= _CompartmentDoors [i]._DoorRF.transform.localPosition;
				pos	= pos + new Vector3 (-_fDoorOpenXVal, 0, -_fDoorOpenZVal);
				iTween.MoveTo (_CompartmentDoors [i]._DoorRF, iTween.Hash ("position", pos, "time", doorOpenTimer, "easetype", iTween.EaseType.linear, "islocal", true));
				
				pos	= _CompartmentDoors [i]._DoorRB.transform.localPosition;
				pos	= pos + new Vector3 (-_fDoorOpenXVal, 0, _fDoorOpenZVal);
				iTween.MoveTo (_CompartmentDoors [i]._DoorRB, iTween.Hash ("position", pos, "time", doorOpenTimer, "easetype", iTween.EaseType.linear, "islocal", true));

			}
		}
	}

	public void SetLights (bool isVisible)
	{

		for (int i = 0; i < _TrainLights.Length; i++) {
			_TrainLights [i].enabled	= isVisible;
		}
	}

	#endregion

	#region TrainCollision

	[SerializeField] Rigidbody[] _rigidBodiesToRemoveConsrtrain;

	public void OnCollisionDisableLimits ()
	{
		//return;//testarj
		if (_rigidBodiesToRemoveConsrtrain == null || _rigidBodiesToRemoveConsrtrain.Length	== 0)
			return;
		for (int i = 0; i < _rigidBodiesToRemoveConsrtrain.Length; i++)
			_rigidBodiesToRemoveConsrtrain [i].constraints = RigidbodyConstraints.None;
	}

	#endregion

	float getSpeed{
		get{
			return (GetComponent<TrainCollisionHandler>() && GetComponent<TrainCollisionHandler>().isAI)? GamePlayManager.Instance.f_DiffInSpeed_AI :GamePlayManager.Instance.f_DiffInSpeed;
		}
		set{
			getSpeed = value;

		}
	}

	bool getBreak{
		get{
			return (GetComponent<TrainCollisionHandler> () && GetComponent<TrainCollisionHandler> ().isAI) ? GamePlayManager.Instance.mb_IsAIBreak : GamePlayManager.Instance.mb_IsBreak;
		}
		set{
			getBreak = value;
		}
	}

}
