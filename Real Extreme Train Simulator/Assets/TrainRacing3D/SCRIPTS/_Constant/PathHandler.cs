using UnityEngine;
using System.Collections;

public class PathHandler : MonoBehaviour{
	
	[SerializeField] Transform transformObject;
	[SerializeField] float mSpeed;
	[SerializeField] Transform[] path;
	[SerializeField] ITWEEN_EVENT _itweenEvent;

	void Start(){
		
			SetToiTweenEffect (_itweenEvent);
	}

	void OnDrawGizmos(){
		
		iTween.DrawPath(path,Color.cyan);	
	}
	
	void SetToiTweenEffect(ITWEEN_EVENT itweenEvent){

		transformObject.position =  path[0].position;
		switch (itweenEvent) {
		case ITWEEN_EVENT.LOOP:
			iTween.MoveTo(transformObject.gameObject,iTween.Hash("path",path, "time",mSpeed, "orienttopath",true ,"looktime", 0.1, "easetype",iTween.EaseType.linear,"looptype", iTween.LoopType.loop));

			break;
		case ITWEEN_EVENT.ON_COMPLETE:
			iTween.MoveTo (transformObject.gameObject, iTween.Hash ("path", path, "time", mSpeed, "orienttopath",true, "looktime", 0.1, "easetype",iTween.EaseType.linear, "looptype", iTween.LoopType.none, "oncomplete", "OnPathComplete", "oncompletetarget", this.gameObject));

			break;
		}
	}

	void OnPathComplete(){
		Debug.Log("OnPathComplete");
	}

}

