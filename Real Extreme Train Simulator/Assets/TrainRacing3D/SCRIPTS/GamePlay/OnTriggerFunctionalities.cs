using UnityEngine;
using System.Collections;

public class OnTriggerFunctionalities : MonoBehaviour {

		[SerializeField] eTRIGGER_STATE mTriggerState = eTRIGGER_STATE.None;
		[SerializeField] GameObject[] _ArrayOfGameObjects = null;

	public void OnTriggered()
	{
		switch(mTriggerState)
		{
		case eTRIGGER_STATE.HornGateClose:
			OnHornTriggerEnter();
			break;
		}
		mTriggerState	= eTRIGGER_STATE.None;
	}
	void OnHornTriggerEnter()
	{
		iTween.RotateTo(_ArrayOfGameObjects[0],iTween.Hash("x",0,"time",1f,"easetype",iTween.EaseType.linear));
		iTween.RotateTo(_ArrayOfGameObjects[1],iTween.Hash("x",0,"time",1f,"easetype",iTween.EaseType.linear));
	}

	public void OnHornReset()
	{
		this.mTriggerState	= eTRIGGER_STATE.HornGateClose;

			iTween.RotateTo(_ArrayOfGameObjects[0],iTween.Hash("x",310,"time",0f,"easetype",iTween.EaseType.linear));
			iTween.RotateTo(_ArrayOfGameObjects[1],iTween.Hash("x",310,"time",0f,"easetype",iTween.EaseType.linear));
	}

	public void OnHornResetTrigger()
	{
		this.mTriggerState	= eTRIGGER_STATE.HornGateClose;
	}
}
