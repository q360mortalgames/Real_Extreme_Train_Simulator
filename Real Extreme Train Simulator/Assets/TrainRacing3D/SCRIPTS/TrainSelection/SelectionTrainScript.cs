using UnityEngine;
using System.Collections;

public class SelectionTrainScript : MonoBehaviour 
{

	Quaternion _startRotation;
	[HideInInspector] 
	public Vector3 _startPos;
	public Transform TrainFrontViewCameraPos; 
	public Transform TrainEngineTarget; 

	// Use this for initialization
	void Awake () 
	{
		_startPos	= TrainFrontViewCameraPos.localPosition;
		_startRotation	= transform.rotation;
	}
	public void setParent(Transform _parent)
	{
		transform.parent	= _parent;
	}
	public void ResetToOriginalPos()
	{
		TrainFrontViewCameraPos.localPosition	= _startPos;
	}
}
