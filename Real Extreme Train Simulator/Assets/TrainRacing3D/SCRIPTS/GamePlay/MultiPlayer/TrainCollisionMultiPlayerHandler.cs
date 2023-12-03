using UnityEngine;
using System.Collections;

public class TrainCollisionMultiPlayerHandler : MonoBehaviour
{
//	private static TrainCollisionMultiPlayerHandler _instance = null;

//	public static TrainCollisionMultiPlayerHandler Instance {		
//		get {			
//			if (_instance == null) {
//				_instance = GameObject.FindObjectOfType (typeof(TrainCollisionMultiPlayerHandler)) as TrainCollisionMultiPlayerHandler; 	
//			}			
//			return _instance; 
//		}
//		set {			
//			_instance = value;			
//		}	
//	}

//	[SerializeField] bool IsEngine	= false;
//	[HideInInspector] public bool mb_IsStationTriggered = false;
//	TriggerPropertiesForTrain _TriggerScript = null;
//	Collider _cCollider;
//	//public PhotonView _photon;
//	public TrainMovementScript _trainMovementScript;
//	public bool isAI = false;

//	IEnumerator ieRequestToSignal (Collider _col, float signalDelay)
//	{
		
//		_col.gameObject.GetComponent<TrainSignalHandler> ().SetSignalToRed ();
//		_col.GetComponent<BoxCollider> ().enabled = false;
//		yield return new WaitForSeconds (signalDelay);
//		_col.gameObject.GetComponent<TrainSignalHandler> ().SetSignalToGreen ();

//	}

//	void OnTriggerEnter (Collider _col)
//	{
//		//Debug.Log ("--_photon.isMine "+_photon.isMine+" IsEngine: "+IsEngine);
//		//if ((!TrainMultiPlayerManager.Instance.isAIEnabled && !_photon.isMine) || !IsEngine) {
//		//	return;
//		//}
			

//		_TriggerScript	= _col.gameObject.GetComponent<TriggerPropertiesForTrain> ();
//		if (_TriggerScript == null) {
//			InstructionTriggerScript _insTrigger = _col.gameObject.GetComponent<InstructionTriggerScript> ();
//			if (_insTrigger == null)
//				return;

//			switch (_insTrigger.mTriggerState) {
//			case e_INSTRUCTION_TRIGGER.SpeedTrigger:
//				GlobalVariables.mInsTrigger	= e_INSTRUCTION_TRIGGER.SpeedTrigger;
//				_col.enabled	= false;
//				GamePlayManager.Instance.SetInstructionVisibleOnTrigger (e_INSTRUCTION_STATE.SpeedLimit);
//				break;
//			case e_INSTRUCTION_TRIGGER.HornTrigger:
//				_col.enabled	= false;
//				GamePlayManager.Instance.SetInstructionVisibleOnTrigger (e_INSTRUCTION_STATE.Horn);
//				break;
//			case e_INSTRUCTION_TRIGGER.ChangeDirectionLeft_Trigger:
//				_col.enabled	= false;
//				GamePlayManager.Instance.SetInstructionVisibleOnTrigger (e_INSTRUCTION_STATE.ChangeDirection_Left);
//				break;
//			case e_INSTRUCTION_TRIGGER.ChangeDirectionRight_Trigger:
//				_col.enabled	= false;
//				GamePlayManager.Instance.SetInstructionVisibleOnTrigger (e_INSTRUCTION_STATE.ChangeDirection_Right);
//				break;
//			case e_INSTRUCTION_TRIGGER.StationStopTrigger:
//				_col.enabled	= false;
//				GamePlayManager.Instance.SetInstructionVisibleOnTrigger (e_INSTRUCTION_STATE.Break);
//				break;
//			case e_INSTRUCTION_TRIGGER.AITrainSignalTrigger:
//				_col.enabled	= false;
//				GamePlayManager.Instance.SetInstructionVisibleOnTrigger (e_INSTRUCTION_STATE.Signals);
//				break;
//			}

//			return;
//		}


//		switch (_TriggerScript.mTriggerState) {
//		case eTRIGGER_STATE.SpeedLimiter:
//			_trainMovementScript.ResetSpeedLimiterValues (_TriggerScript.SpeedLimit);
//			break;
//		case eTRIGGER_STATE.HornGateClose:
//			_col.gameObject.GetComponent<OnTriggerFunctionalities> ().OnTriggered ();
//			break;
//		case eTRIGGER_STATE.Horn:
//			GamePlayManager.Instance.OnResetHornBool (true);
//			break;
//		case eTRIGGER_STATE.StationStop:
//			_cCollider	= _col; 
//			mb_IsStationTriggered	= true;
//			break;

//		case eTRIGGER_STATE.DirectionChangeEnable:
//			Debug.Log (this.gameObject.name + " : this");

//			if (!_TriggerScript.mb_IsFakeDirectionAvalaiable) {
//				GamePlayManager.Instance.ResetDirectionChangeValue (_TriggerScript.mDirection);
//				AiCheck (_TriggerScript);
//			}
//			else
//				GamePlayManager.Instance.ResetDirectionChangeValue (_TriggerScript.mFakeDirection);
//			break;
//		case eTRIGGER_STATE.DirectionChangeDisable:
			
//			if (LevelEnabler.TrainDirection == 1) {
//				GamePlayManager.Instance.ResetDirectionChangeValue (eDIRECTION_TO_CHANGE.None);
//			} else if (LevelEnabler.TrainDirection == -1) {
//				GamePlayManager.Instance.ResetDirectionChangeValue (_TriggerScript.mDirection);
//			}

//			break;
//		case eTRIGGER_STATE.AITrainStart:
//			_col.gameObject.GetComponent<TrainAIEnableOnTrigger> ().OnTriggerToEnableAIMovement ();
//			break;
//		case eTRIGGER_STATE.AICarStart:
//			_col.gameObject.GetComponent<TrainAIEnableOnTrigger> ().OnTriggerToEnableAIMovement ();
//			break;
//		case eTRIGGER_STATE.TrainSignalAlert:
//			_col.gameObject.GetComponent<TrainSignalHandler> ().SetSignalToRed ();
//			_col.GetComponent<BoxCollider> ().enabled = false;
//			break;
//		case eTRIGGER_STATE.TrainSignalAlertExit:
//			GamePlayManager.Instance.OnSignalBroke ();
//			break;

//		case eTRIGGER_STATE.TrainSignalAlertMP:
//			float signalTime = _col.gameObject.GetComponent<TrainSignalHandler> ().multiplayerSignalTime;
//			Debug.Log ("-- TrainSignalAlertMP"+gameObject.name);
//			StartCoroutine (ieRequestToSignal (_col, signalTime));
//			AiCheck (_TriggerScript);
//			break;

//		case eTRIGGER_STATE.TrainSignalAlertExitMP:
//			GamePlayManager.Instance.OnSignalBrokeMP ();
//			if(isAI){
//				GamePlayManager.Instance.OnLevelCompleted();
//			}else{
//				GamePlayManager.Instance.OnLevelFailed ();
//			}
//			break;

//		case eTRIGGER_STATE.LeftTrainStationTrigger:
//			GamePlayManager.Instance.mb_IsStationLeftSide	= true;
//			AiCheck (_TriggerScript);
//			break;

//		case eTRIGGER_STATE.RightTrainStationTrigger:
//			GamePlayManager.Instance.mb_IsStationLeftSide	= false;
//			AiCheck (_TriggerScript);
//			break;

//		case eTRIGGER_STATE.StaticCameraEntry:
			
//			TrainStaticCamera _CameraObj	= _col.gameObject.GetComponent<TrainStaticCamera> ();
//			if (_CameraObj) {
//				Transform _transform	= _CameraObj._staticCameraTransform;
//				GamePlayManager.Instance.OnStaticCameraEntry (_transform);
//			}
//			break;

//		case eTRIGGER_STATE.StaticCameraExit:
//			GamePlayManager.Instance.OnStaticCameraExit ();
//			break;

//		case eTRIGGER_STATE.TunnelStaticCameraEntry:
//			GamePlayManager.Instance.OnTunnelEnterTrain (true);	
//			break;

//		case eTRIGGER_STATE.TunnelStaticCameraExit:
//			GamePlayManager.Instance.OnTunnelEnterTrain (false);	
//			break;
//		}

//	}

//	public void DisableCollider ()
//	{
//		if (_cCollider != null)
//			_cCollider.enabled	= false;
//	}

//	void OnTriggerExit (Collider _col)
//	{
//		//if (!_photon.isMine || !IsEngine)
//		//	return;
		
//		//_TriggerScript	= _col.gameObject.GetComponent<TriggerPropertiesForTrain> ();
//		if (_TriggerScript == null)
//			return;

//		switch (_TriggerScript.mTriggerState) {
//		case eTRIGGER_STATE.Horn:
//			GamePlayManager.Instance.OnResetHornBool (false);
//			_col.gameObject.SetActive (false);
//			break;
//		case eTRIGGER_STATE.StationStop:
//			if (_cCollider == _col) {
//				_cCollider.enabled	= false;
//				_cCollider	= null;
//			}

//			mb_IsStationTriggered	= false;
//			break;
//		}
//	}

//	void OnCollisionEnter (Collision _col)
//	{
//		//if ((!TrainMultiPlayerManager.Instance.isAIEnabled &&!_photon.isMine) || GlobalVariables.mGameState != eGAME_STATE.GamePlay)
//		//	return;

//		switch (_col.gameObject.tag) {

//		case "Obstacle":
			
//			_trainMovementScript.OnCollisionDisableLimits ();
//			CreateParticleOnCollision (_col.contacts [0].point);

//			GameObject _newObj	= new GameObject ();
//			_newObj.transform.position	= _col.contacts [0].point;
//			_newObj.transform.parent = transform;
//			_newObj.name = "LevelFailedCameraView";

//			#if CUSTOM_CLIENT
//			if (TrainMultiPlayerManager.IsMasterClient) {
//				#else
//	//		if (PhotonNetwork.isMasterClient) {
//	//			#endif
//	//			//if(OnNetworkReceiver.Instance)OnNetworkReceiver.Instance.OnRequestSetToBoolean (mGameResult.PlayerLose, false);

//	//			if(isAI){
//	//				GamePlayManager.Instance.OnLevelCompleted();
//	//				//if(TrainMultiPlayerManager.Instance.infoTxt)TrainMultiPlayerManager.Instance.infoTxt.text = "Oppenent Crashed!";
//	//			//	if(gameConfigs.mee)gameConfigs.mee.jarToast ("Opponent crashed!");
//	//			}else{
//	//				GamePlayManager.Instance.OnLevelFailed();
//	//			}

//	//		} else {
//	//			//OnNetworkReceiver.Instance.OnRequestSetToBoolean (mGameResult.OppoentLose, false);
//	//			//if(TrainMultiPlayerManager.Instance.infoTxt)TrainMultiPlayerManager.Instance.infoTxt.text = "Oppenent Crashed!";
//	//		//	if(gameConfigs.mee)gameConfigs.mee.jarToast ("Opponent crashed!");
//	//		}
//	//		break;

//	//	case "RaceWin":

//	//		#if CUSTOM_CLIENT
//	//		if (TrainMultiPlayerManager.IsMasterClient) {
//	//			#else
//	//		if (PhotonNetwork.isMasterClient) {
//	//			#endif
//	//			//if(OnNetworkReceiver.Instance)OnNetworkReceiver.Instance.OnRequestSetToBoolean (mGameResult.PlayerWin, true);
//	//			if(TrainMultiPlayerManager.Instance.isAIEnabled){
//	//				GamePlayManager.Instance.IsGameResult = true;
//	//				if(isAI){
//	//					GamePlayManager.Instance.OnLevelFailed();
//	//				}else{
//	//					GamePlayManager.Instance.OnLevelCompleted();
//	//				}
//	//			}

//	//		} else {
//	//			//OnNetworkReceiver.Instance.OnRequestSetToBoolean (mGameResult.OppoentWin, true);
//	//		}
//	//		break;
//	//	}
//	//}

//	[SerializeField] GameObject _Particle;

//	void CreateParticleOnCollision (Vector3 pos)
//	{
//		GameObject _newParticle	= Instantiate (_Particle) as GameObject;
//		_newParticle.transform.position	= pos;
//		_newParticle.transform.parent	= transform;
//	}
//	#region MultiplayerAI

//	int chancesToFail = 0;
//	void AiCheck(TriggerPropertiesForTrain _TriggerScript){
//		if (!isAI)
//			return;

//		Debug.Log ("--Ai check.." + _TriggerScript.mTriggerState);
//		if (_TriggerScript.mTriggerState == eTRIGGER_STATE.DirectionChangeEnable) {
//			chancesToFail = Random.Range(0,100);
//			Debug.Log("--IntelegenceLvl: "+TrainMultiPlayerManager.Instance.inteligenceLevel+" chancesToFail: "+chancesToFail);
//			if (TrainMultiPlayerManager.Instance.inteligenceLevel < 3 && chancesToFail<25) {
//				Debug.Log("--So AI Direction Fials");
//				return;				
//			}
//			GamePlayManager.Instance.ChangeAIDirection (_TriggerScript.mDirection);
//		}
//		if (_TriggerScript.mTriggerState == eTRIGGER_STATE.TrainSignalAlertMP) {
//			TrainBreakScript.Instance.Invoke("UseAIBreak",2f);
//			float signalTime = _TriggerScript.gameObject.GetComponent<TrainSignalHandler> ().multiplayerSignalTime;
//			Debug.Log ("--SignalTime:: " + signalTime);
//			Invoke ("ApplySpeetoAI", signalTime);
//		}

//		if (_TriggerScript.mTriggerState == eTRIGGER_STATE.RightTrainStationTrigger || _TriggerScript.mTriggerState == eTRIGGER_STATE.LeftTrainStationTrigger) {
//			TrainBreakScript.Instance.Invoke("UseAIBreak",2f);
//		}
//	}

//	void ApplySpeetoAI(){
//		TrainBreakScript.Instance.PostAIBreak ();
//		GamePlayManager.Instance.f_DiffInSpeed_AI = TrainMultiPlayerManager.Instance.inteligenceSpeeds[TrainMultiPlayerManager.Instance.inteligenceLevel];
//		Debug.Log ("-- Apply speed now::");
//	}

//	#endregion
}
