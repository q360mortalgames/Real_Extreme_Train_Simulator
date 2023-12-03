using UnityEngine;
using System.Collections;

public class CenterBaseRotation : MonoBehaviour {

	public float f_RotationSpeed = 10; 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.localEulerAngles	= new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y+(Time.deltaTime*f_RotationSpeed),transform.localEulerAngles.z);	
	}
}
