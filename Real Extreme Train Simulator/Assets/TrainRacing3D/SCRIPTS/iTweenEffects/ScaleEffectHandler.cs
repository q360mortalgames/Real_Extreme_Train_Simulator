using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScaleEffectHandler : MonoBehaviour {
	
	[SerializeField] Animator myAnimator;
	[SerializeField] float scaleTime;
	[SerializeField] float scaleDelay;
	[SerializeField] Vector3 myVector;
	[SerializeField] bool NeedLoopAnim = false;

	void OnEnable () {
		
		if(myAnimator!=null)
			myAnimator.enabled = false;

		if (gameObject.GetComponent<iTween> ())
			Destroy (gameObject.GetComponent<iTween> ());

		gameObject.transform.localScale = new Vector3(1,1,1);

		iTween.ScaleFrom(gameObject, iTween.Hash("Scale", Vector3.zero, "time", scaleTime, "delay", scaleDelay, "oncomplete" , "iTweenComplete" , "oncompletetarget" , this.gameObject));

		if (NeedLoopAnim) {
			gameObject.transform.localEulerAngles = new Vector3 (0,0,10);
			//iTween.ScaleTo (gameObject, iTween.Hash ("scale", Vector3.one*1.5f, "time", 1, "loopType", iTween.LoopType.pingPong,"easeType",iTween.EaseType.linear,"islocal",true));
			iTween.RotateTo (gameObject, iTween.Hash ("z", -10f, "time", 1, "loopType", iTween.LoopType.pingPong,"easeType",iTween.EaseType.linear,"islocal",true));
		}
	}

	void iTweenComplete()
	{
		if(myAnimator!=null)
			myAnimator.enabled = true;
	}
}
