using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class OnMouseDownEffect : MonoBehaviour {

	// Use this for initialization

	
	private bool tweenExist;
	public bool scale,fade,posY;
	void OnMouseDown () { 
		if ( GetComponent<iTween> ())
			return;
		
		if (scale) {
				iTween.ScaleFrom (gameObject, iTween.Hash ("x", gameObject.transform.localScale.x * 0.8f, "y", gameObject.transform.localScale.y * 0.8f, "z", 0.5f, "time", 0.5f));
			}
		if (fade) {
				iTween.FadeFrom (gameObject, iTween.Hash ("a", 0.5f, "time", 0.5f));
		}
		if (posY) {
			iTween.MoveFrom (gameObject, iTween.Hash ("y", gameObject.transform.localPosition.y-0.1f, "time", 0.5f));
		}
       
	}
}