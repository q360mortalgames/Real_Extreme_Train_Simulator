using UnityEngine;
using System.Collections;

public class StationPointAnimation : MonoBehaviour {


	public Color _ToColor;
	public float _Time;
	// Use this for initialization
	void Start () 
	{	
		iTween.ColorTo(gameObject,iTween.Hash("color",_ToColor,"time",_Time,"easetype",iTween.EaseType.linear,"looptype",iTween.LoopType.pingPong));
	}

}
