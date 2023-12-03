using UnityEngine;
using System.Collections;

public class TrainWheelsScript : MonoBehaviour
{

		public bool IsLeftWheel;
		public float f_DragSpeed = 0.5f;
		// Use this for initialization
		Vector3 transformPos;
		Transform _transform;

		void Start ()
		{
				_transform	= transform;
				if (IsLeftWheel)
			transformPos	= new Vector3 (0.784f, _transform.position.y, _transform.position.z);  
				else
			transformPos	= new Vector3 (-0.816f, _transform.position.y, _transform.position.z); 
				GetComponent<Rigidbody> ().maxAngularVelocity = 50;
		}

		void Update ()
		{
				if (GamePlayManager.Instance == null) {
						GetComponent<Rigidbody> ().drag	= 0.1f;
						return;
				}
				if (GlobalVariables.mGameState != eGAME_STATE.GamePlay && (GlobalVariables.mGameState == eGAME_STATE.InitialDelay || GlobalVariables.mGameState == eGAME_STATE.Station || GlobalVariables.mGameState == eGAME_STATE.LevelComplete || GlobalVariables.mGameState == eGAME_STATE.LevelFailed)) {
						GetComponent<Rigidbody> ().drag	= 50f;
						return;
				}

		if (transform.root.GetComponent<TrainCollisionMultiPlayerHandler> () == null && GamePlayManager.Instance.mb_IsBreak)
			GetComponent<Rigidbody> ().drag	= Mathf.Lerp (GetComponent<Rigidbody> ().drag, 3f, Time.deltaTime * f_DragSpeed);
		else if (GlobalVariables.mGameState == eGAME_STATE.InitialDelay)
			GetComponent<Rigidbody> ().drag	= 10f;
		else if (transform.root.GetComponent<TrainCollisionHandler> () && transform.root.GetComponent<TrainCollisionHandler> ().isAI && GamePlayManager.Instance.mb_IsAIBreak) {
			GetComponent<Rigidbody> ().drag	= Mathf.Lerp (GetComponent<Rigidbody> ().drag, 3f, Time.deltaTime * f_DragSpeed);
			//Debug.Log ("-- Apply drag to AI wheels..");
		}
		else if (transform.root.GetComponent<TrainCollisionHandler> () && !transform.root.GetComponent<TrainCollisionHandler> ().isAI && GamePlayManager.Instance.mb_IsBreak) {
			GetComponent<Rigidbody> ().drag	= Mathf.Lerp (GetComponent<Rigidbody> ().drag, 3f, Time.deltaTime * f_DragSpeed);
			//Debug.Log ("-- Apply drag to Player wheels..");
		}
				else
						GetComponent<Rigidbody> ().drag	= 0f;
		}
}
