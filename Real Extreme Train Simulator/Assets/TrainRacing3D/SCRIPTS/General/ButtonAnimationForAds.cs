using UnityEngine;
using System.Collections;

public class ButtonAnimationForAds : MonoBehaviour {

	public float timeForAnim	= 0.2f;
	public float newChangeValue	= 0.1f;
	// Use this for initialization
	void Start () 
	{
		Vector3 newChangeVal	= new Vector3(1,1,1) + new Vector3(newChangeValue,newChangeValue,newChangeValue);
		iTween.ScaleTo(gameObject,iTween.Hash("scale",newChangeVal,"time",timeForAnim,"delay",0.0f,"easetype",iTween.EaseType.linear,"looptype",iTween.LoopType.pingPong));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
