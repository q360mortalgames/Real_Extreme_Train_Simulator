using UnityEngine;
using System.Collections;

public class TrackChangerIndicator : MonoBehaviour {

	TriggerPropertiesForTrain _script;
	[SerializeField] Transform[] _PathIndicator;
	[SerializeField] Material[] _RedMaterial;
	[SerializeField] Material[] _GreenMaterial;

	Material _MaterialForEnabled;

	[SerializeField] Color _color1;
	[SerializeField] Color _color2;
	int colorTag	= 0;
	float f_timer	= 0, f_Targettimer	=0.5f;
	// Use this for initialization
	void Start () 
	{
		_script	= GetComponent<TriggerPropertiesForTrain>();
		SetDataOnStart();
	}
	void SetDataOnStart()
	{
		_PathIndicator[0].GetComponent<Renderer>().sharedMaterial	= _RedMaterial[0]; // 0 - Straight
		_PathIndicator[1].GetComponent<Renderer>().sharedMaterial	= _RedMaterial[1]; // 1 - Left
		_PathIndicator[2].GetComponent<Renderer>().sharedMaterial	= _RedMaterial[2]; // 2 - Right

		if(_script.mTriggerState == eTRIGGER_STATE.DirectionChangeEnable)
		{
			if(_script.mDirection == eDIRECTION_TO_CHANGE.Right)
			{
				_PathIndicator [2].gameObject.GetComponent<MeshRenderer> ().enabled = true;
				_PathIndicator[2].GetComponent<Renderer>().sharedMaterial	= _GreenMaterial[2];
				_MaterialForEnabled	= _PathIndicator[2].GetComponent<Renderer>().material;
				_PathIndicator[2].localPosition = new Vector3(-0.2f,_PathIndicator[2].localPosition.y,_PathIndicator[2].localPosition.z);
			}
			else if(_script.mDirection == eDIRECTION_TO_CHANGE.Left)
			{
				_PathIndicator [1].gameObject.GetComponent<MeshRenderer> ().enabled = true;


				_PathIndicator[1].localPosition = new Vector3(-0.2f,_PathIndicator[1].localPosition.y,_PathIndicator[1].localPosition.z);
				_PathIndicator[1].GetComponent<Renderer>().sharedMaterial	= _GreenMaterial[1];
				_MaterialForEnabled	= _PathIndicator[1].GetComponent<Renderer>().material;
			}
			else if(_script.mDirection == eDIRECTION_TO_CHANGE.None)
			{
				_PathIndicator [0].gameObject.GetComponent<MeshRenderer> ().enabled = true;


				_PathIndicator[0].localPosition = new Vector3(-0.2f,_PathIndicator[0].localPosition.y,_PathIndicator[0].localPosition.z);
				_PathIndicator[0].GetComponent<Renderer>().sharedMaterial	= _GreenMaterial[0];
				_MaterialForEnabled	= _PathIndicator[0].GetComponent<Renderer>().material;
			}

		}
	}
	// Update is called once per frame
	void Update () 
	{
		if(_script == null)
			return;

		f_timer += Time.deltaTime;

		if(f_timer >= f_Targettimer)
		{
			colorTag++;
			if(colorTag	> 1)
				colorTag	= 0;

			if(colorTag == 0)
				_MaterialForEnabled.color	= _color1;
			else
				_MaterialForEnabled.color	= _color2;

			f_timer	= 0;

		}

	}

	void OnDisable(){
		_MaterialForEnabled.color	= _color1;
	}
}
