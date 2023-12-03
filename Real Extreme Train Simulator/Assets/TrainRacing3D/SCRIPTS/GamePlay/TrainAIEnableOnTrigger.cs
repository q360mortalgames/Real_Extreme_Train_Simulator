using UnityEngine;
using System.Collections;

public class TrainAIEnableOnTrigger : MonoBehaviour 
{
	public AIWayPointMovement _TrainAIScript = null;
	public hoMove[] _CarsAIScript;
	public void OnTriggerToEnableAIMovement()
	{
		Debug.Log ("--->> OnTriggerToEnableAIMovement");
		if(_TrainAIScript != null)
			_TrainAIScript.mb_IsStart	= true;
		else
		{
			for(int i = 0 ; i < _CarsAIScript.Length ; i++)
			{
				hoMove _scr	= _CarsAIScript[i] as hoMove;
				_scr.StartMove();
			}
		}
	}
}
