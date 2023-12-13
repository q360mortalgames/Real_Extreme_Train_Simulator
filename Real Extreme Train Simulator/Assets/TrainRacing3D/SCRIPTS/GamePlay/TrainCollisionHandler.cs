using UnityEngine;
using System.Collections;

public class TrainCollisionHandler : MonoBehaviour 
{
	public static TrainCollisionHandler mee;
	[SerializeField] bool IsEngine	= false;
	[HideInInspector] public bool mb_IsStationTriggered = false;
	TriggerPropertiesForTrain _TriggerScript = null;
	Collider _cCollider;
	[HideInInspector] public bool isAI = false;


	void Awake(){
		//isAI = true;
	}

	void Start ()
	{
		mee = this;
	}

	void OnTriggerEnter (Collider _col)
	{
		if (IsEngine == false)
			return;

		_TriggerScript	= _col.gameObject.GetComponent<TriggerPropertiesForTrain> ();
		if (_TriggerScript == null) {
			InstructionTriggerScript _insTrigger	= _col.gameObject.GetComponent<InstructionTriggerScript> ();
			if (_insTrigger == null)
				return;

			switch (_insTrigger.mTriggerState) {
			case e_INSTRUCTION_TRIGGER.SpeedTrigger:
				GlobalVariables.mInsTrigger	= e_INSTRUCTION_TRIGGER.SpeedTrigger;
				_col.enabled	= false;
				GamePlayManager.Instance.SetInstructionVisibleOnTrigger (e_INSTRUCTION_STATE.SpeedLimit);
				break;
			case e_INSTRUCTION_TRIGGER.HornTrigger:
				_col.enabled	= false;
				GamePlayManager.Instance.SetInstructionVisibleOnTrigger (e_INSTRUCTION_STATE.Horn);
				break;
			case e_INSTRUCTION_TRIGGER.ChangeDirectionLeft_Trigger:
				_col.enabled	= false;
				GamePlayManager.Instance.SetInstructionVisibleOnTrigger (e_INSTRUCTION_STATE.ChangeDirection_Left);
				break;
			case e_INSTRUCTION_TRIGGER.ChangeDirectionRight_Trigger:
				_col.enabled	= false;
				GamePlayManager.Instance.SetInstructionVisibleOnTrigger (e_INSTRUCTION_STATE.ChangeDirection_Right);
				break;
			case e_INSTRUCTION_TRIGGER.StationStopTrigger:
				_col.enabled	= false;
				GamePlayManager.Instance.SetInstructionVisibleOnTrigger (e_INSTRUCTION_STATE.Break);
				break;
			case e_INSTRUCTION_TRIGGER.AITrainSignalTrigger:
				_col.enabled	= false;
				GamePlayManager.Instance.SetInstructionVisibleOnTrigger (e_INSTRUCTION_STATE.Signals);
				break;
			}

			return;
		}


		switch (_TriggerScript.mTriggerState) {
		case eTRIGGER_STATE.SpeedLimiter:
			TrainMovementScript.Instance.ResetSpeedLimiterValues (_TriggerScript.SpeedLimit);
			break;
		case eTRIGGER_STATE.HornGateClose:
			_col.gameObject.GetComponent<OnTriggerFunctionalities> ().OnTriggered ();
			break;
		case eTRIGGER_STATE.Horn:
			GamePlayManager.Instance.OnResetHornBool (true);
			break;
		case eTRIGGER_STATE.StationStop:
			_cCollider	= _col; 
			mb_IsStationTriggered	= true;

			break;
		case eTRIGGER_STATE.DirectionChangeEnable:
			Debug.Log ("--Triggers.. DirectionChangeEnable:: " + this.gameObject.name + " : this");

			GamePlayManager.Instance.PathChanger_obj = _col.gameObject.transform.parent.gameObject;
			if (!_TriggerScript.mb_IsFakeDirectionAvalaiable) {
				GamePlayManager.Instance.ResetDirectionChangeValue (_TriggerScript.mDirection);
				AiCheck (_TriggerScript);
			}
			else
				GamePlayManager.Instance.ResetDirectionChangeValue (_TriggerScript.mFakeDirection);
			break;
		case eTRIGGER_STATE.DirectionChangeDisable:
			GamePlayManager.Instance.PathChanger_obj = null;
			if (LevelEnabler.TrainDirection == 1) {
				GamePlayManager.Instance.ResetDirectionChangeValue (eDIRECTION_TO_CHANGE.None);
			} else if (LevelEnabler.TrainDirection == -1) {
				GamePlayManager.Instance.ResetDirectionChangeValue (_TriggerScript.mDirection);
			}

			break;
		case eTRIGGER_STATE.AITrainStart:
			_col.gameObject.GetComponent<TrainAIEnableOnTrigger> ().OnTriggerToEnableAIMovement ();
			break;
		case eTRIGGER_STATE.AICarStart:
			_col.gameObject.GetComponent<TrainAIEnableOnTrigger> ().OnTriggerToEnableAIMovement ();
			break;
		case eTRIGGER_STATE.TrainSignalAlert:
			_col.gameObject.GetComponent<TrainSignalHandler> ().SetSignalToRed ();
			_col.GetComponent<BoxCollider> ().enabled = false;
			AiCheck (_TriggerScript);
			break;
		case eTRIGGER_STATE.TrainSignalAlertExit:
			GamePlayManager.Instance.OnSignalBroke ();
			break;
		case eTRIGGER_STATE.LeftTrainStationTrigger:
			GamePlayManager.Instance.mb_IsStationLeftSide	= true;
			AiCheck (_TriggerScript);
			break;
		case eTRIGGER_STATE.RightTrainStationTrigger:
			GamePlayManager.Instance.mb_IsStationLeftSide	= false;
			AiCheck (_TriggerScript);
			break;

		case eTRIGGER_STATE.StaticCameraEntry:
			
			TrainStaticCamera _CameraObj	= _col.gameObject.GetComponent<TrainStaticCamera> ();
			if (_CameraObj) {
				Transform _transform	= _CameraObj._staticCameraTransform;
				GamePlayManager.Instance.OnStaticCameraEntry (_transform);
			}
			break;

		case eTRIGGER_STATE.StaticCameraExit:
			GamePlayManager.Instance.OnStaticCameraExit ();
			break;

		case eTRIGGER_STATE.TunnelStaticCameraEntry:
			GamePlayManager.Instance.OnTunnelEnterTrain (true);	
			break;

		case eTRIGGER_STATE.TunnelStaticCameraExit:
			GamePlayManager.Instance.OnTunnelEnterTrain (false);	
			break;
		}

	}

	public void DisableCollider ()
	{
		if (_cCollider != null)
			_cCollider.enabled	= false;
	}

	static  Collider finalStopCol;
	public void EnableCollider(){
		if (_cCollider != null) {			
			_cCollider.enabled	= true;
		}
		Debug.Log ("++EnableCollider: ");
		if (finalStopCol) {
			Debug.Log ("++_cColliderToEnable: " + finalStopCol.name);
			finalStopCol.enabled = true;
		}
	}

	void OnTriggerExit (Collider _col)
	{
		if (IsEngine == false)
			return;
		
		_TriggerScript	= _col.gameObject.GetComponent<TriggerPropertiesForTrain> ();
		if (_TriggerScript == null)
			return;

		switch (_TriggerScript.mTriggerState) {
		case eTRIGGER_STATE.Horn:
			GamePlayManager.Instance.OnResetHornBool (false);
			_col.gameObject.SetActive (false);
			break;
		case eTRIGGER_STATE.StationStop:
			if (_cCollider == _col) {
				_cCollider.enabled	= false;
				finalStopCol = _cCollider;
				Debug.Log ("++_cColliderToEnable:OnTriggerExit: " + finalStopCol.name);
				_cCollider	= null;
			}

			mb_IsStationTriggered	= false;
			break;
		}
	}

	string _sObstacleTag	= "Obstacle";
	public static Collider disabledCollider,AiMovingTrainCollider;
	void OnCollisionEnter (Collision _col)
	{
		if (GlobalVariables.mGameState != eGAME_STATE.GamePlay)
			return;

		GameObject _obj = _col.gameObject;
		string gameobjectNAme = _obj.name;
		string _ObjTag = _obj.tag;

		if (_ObjTag == _sObstacleTag) {

            //TrainMovementScript.Instance.OnCollisionDisableLimits ();

            if (SoundController.Instance)
            {
                SoundController.Instance.StopTrainSource();
                SoundController.Instance.TrainCrash();
            }

            CreateParticleOnCollision (_col.contacts [0].point);

			/*_col.gameObject.AddComponent<Rigidbody> ();
			_col.gameObject.GetComponent<Rigidbody> ().mass	= 500;
			_col.gameObject.GetComponent<Rigidbody> ().AddExplosionForce (0f, Vector3.zero, 0.1f);

			GameObject _newObj	= new GameObject ();
			_newObj.transform.position	= _col.contacts [0].point;
			_newObj.transform.parent	= transform;
			_newObj.name = "LevelFailedCameraView";*/

		
			_col.collider.enabled = false;
			disabledCollider = _col.collider;
			Debug.Log ("--ObstacleName: " + _col.collider.name);
			GamePlayManager.normalDirectionDuration = 8;
			if (_col.transform.parent.GetComponent<AIWayPointMovement> ()) {
				Debug.Log ("--Hits with Ai Train--");
				AiMovingTrainCollider = _col.collider;
				_col.transform.parent.GetComponent<AIWayPointMovement> ().mb_IsStart = false;
				GamePlayManager.normalDirectionDuration = 10;
			}
			lFailCall ();
		}
	}

	public void lFailCall ()
	{
		GamePlayManager.Instance.OnLevelFailed ();
	}

	public GameObject _Particle;

	void CreateParticleOnCollision (Vector3 pos)
	{
		GameObject _newParticle	= Instantiate (_Particle) as GameObject;
		_newParticle.transform.position	= pos;
		_newParticle.transform.parent	= transform;
	}

	#region New Methods for AI
	void AiCheck(TriggerPropertiesForTrain _TriggerScript){
		//if (!isAI)
			return; //// NOT USING THIS........................

		Debug.Log ("--Ai check.." + _TriggerScript.mTriggerState);
		if (_TriggerScript.mTriggerState == eTRIGGER_STATE.DirectionChangeEnable) {
			GamePlayManager.Instance.ChangeAIDirection (_TriggerScript.mDirection);
		}
		if (_TriggerScript.mTriggerState == eTRIGGER_STATE.TrainSignalAlert) {
			TrainBreakScript.Instance.Invoke("UseBreak",3f);
			//TrainBreakScript.Instance.Invoke ("PostBreak", 6f);
			Invoke ("ApplySpeed", 5);
		}

		if (_TriggerScript.mTriggerState == eTRIGGER_STATE.RightTrainStationTrigger || _TriggerScript.mTriggerState == eTRIGGER_STATE.LeftTrainStationTrigger) {
			TrainBreakScript.Instance.Invoke("UseBreak",1);
			//TrainBreakScript.Instance.Invoke ("PostBreak", 4f);
			Invoke ("ApplySpeed", 10);
		}
	}

	void ApplySpeed(){
		TrainBreakScript.Instance.PostBreak ();
		GamePlayManager.Instance.f_DiffInSpeed = 0.5f;
		Debug.Log ("-- Apply speed now::");
	}
	#endregion
}
