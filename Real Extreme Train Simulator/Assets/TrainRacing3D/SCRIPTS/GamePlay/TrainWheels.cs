using UnityEngine;
using System.Collections;

public class TrainWheels : MonoBehaviour 
{
	[SerializeField] Transform[] _WheelColliders;
	[SerializeField] Transform[] _Wheels;

	void Start () 
	{
	
	}
	
	void FixedUpdate()
	{
		Transform _wheelColTrans;
		Transform _wheelTrans;

		for(int i = 0 ; i < _WheelColliders.Length ; i++)
		{
			_wheelTrans	= _Wheels[i];
			_wheelColTrans	= _WheelColliders[i];
			_wheelTrans.localEulerAngles = new Vector3(_wheelColTrans.localEulerAngles.x,_wheelTrans.localEulerAngles.y,_wheelTrans.localEulerAngles.z);
		}
	}
}
