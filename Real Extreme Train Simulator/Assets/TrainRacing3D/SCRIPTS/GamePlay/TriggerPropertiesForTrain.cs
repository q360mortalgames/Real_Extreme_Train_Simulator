using UnityEngine;
using System.Collections;

public class TriggerPropertiesForTrain : MonoBehaviour
{
	public eTRIGGER_STATE mTriggerState	= eTRIGGER_STATE.None;
	[HideInInspector]
	public int SpeedLimit	= 0;
	public eDIRECTION_TO_CHANGE mDirection	= eDIRECTION_TO_CHANGE.None;
	[HideInInspector]
	public bool mb_IsFakeDirectionAvalaiable = false;
	[HideInInspector]
	public eDIRECTION_TO_CHANGE mFakeDirection = eDIRECTION_TO_CHANGE.None;

	void Start ()
	{
		if (!GlobalVariables.isMultiPlayerMode && mTriggerState == eTRIGGER_STATE.DirectionChangeEnable && this.GetComponent<BoxCollider> ()) {

			this.GetComponent<BoxCollider> ().enabled = true;
		//	this.gameObject.AddComponent<SphereCollider> ();
		//	this.gameObject.GetComponent<SphereCollider> ().isTrigger = true;
		//	this.gameObject.GetComponent<SphereCollider> ().radius = 1.5f;
		}
	}
}
