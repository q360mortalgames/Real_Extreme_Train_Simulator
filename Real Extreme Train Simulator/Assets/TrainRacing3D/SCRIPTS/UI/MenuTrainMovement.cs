

using UnityEngine;
using System.Collections;

public class MenuTrainMovement : MonoBehaviour
{

	[SerializeField] Rigidbody[] _WheelRigidBodys;
	public int _iConstantForMoveVelocity;
	public float _fTrainMoveVelocity = 0;
	public float currentMaxSpeed, m_fTrainSpeed;


	// Use this for initialization
	void Start ()
	{
	
	}
	bool goingSlow = false;
	bool goingSpeed = false;
	bool crowdSnd = false;

	// Update is called once per frame
	void Update ()
	{
		UpdateWheelRigidBodies ();

		/*
		if (m_fTrainSpeed >= 45 && goingSpeed == false) {
			
			SoundController.Instance.TrainFastSound ();
			goingSpeed = true;
			goingSlow = false;
		} else if (m_fTrainSpeed >= 1 && m_fTrainSpeed <= 40 && goingSlow == false) {

			SoundController.Instance.TrainSlowSound ();
			goingSlow = true;
			goingSpeed = false;
			crowdSnd = true;
		} else if (m_fTrainSpeed <= 0 && crowdSnd == true) {

			crowdSnd = false;
			SoundController.Instance.playTrainidle ();
			goingSlow = false;
			goingSpeed = false;
		}
		*/

	}

	public void UpdateWheelRigidBodies ()
	{

		if (_WheelRigidBodys.Length <= 0)
			return;
		m_fTrainSpeed	= (int)(_WheelRigidBodys [0].velocity.sqrMagnitude) / 5;
		for (int i = 0; i < _WheelRigidBodys.Length; i++) {
			{
				_fTrainMoveVelocity = Mathf.Lerp (_fTrainMoveVelocity, _iConstantForMoveVelocity, 0.01f * Time.deltaTime);
								
				if (m_fTrainSpeed < currentMaxSpeed)
					_WheelRigidBodys [i].AddRelativeTorque (_fTrainMoveVelocity, 0, 0);
			}
		}
	}
}
