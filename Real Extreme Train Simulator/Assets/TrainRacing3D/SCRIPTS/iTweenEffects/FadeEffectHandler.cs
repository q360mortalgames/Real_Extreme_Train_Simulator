using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeEffectHandler : MonoBehaviour {

	[SerializeField] eFADE_MODE _fademode;

	[SerializeField] float fadeAmount;
	[SerializeField] float fadeTime;
	[SerializeField] float fadeDelay;


	void OnEnable () {
		
		if (gameObject.GetComponent<iTween> ())
			Destroy (gameObject.GetComponent<iTween> ());
		
		SetToDefaultValue ();
		RequestToFadeFrom (fadeTime, fadeDelay, fadeAmount);
	}

	void SetToDefaultValue(){
		
		if (_fademode == eFADE_MODE.FadeIn) {
			OnUpdateProcess (0);
		} else {
			OnUpdateProcess (1);
		}
	}

	void RequestToFadeFrom(float _fd_time, float _fd_delay, float _fd_Amount){
		if (_fademode == eFADE_MODE.FadeIn) {
			iTween.ValueTo (gameObject, iTween.Hash ("from", 0, "to", _fd_Amount, "time", _fd_time, "delay", _fd_delay, "onupdate", "OnUpdateProcess", "easetype", iTween.EaseType.linear, "onupdatetarget", gameObject));

		} else {
			iTween.ValueTo(gameObject, iTween.Hash("from", 1, "to", _fd_Amount, "time", _fd_time, "delay", _fd_delay, "onupdate", "OnUpdateProcess", "easetype",  iTween.EaseType.linear, "onupdatetarget", gameObject));

		}
	}

	void OnUpdateProcess(float _value)
	{
		gameObject.GetComponent<Image> ().color = new Color (gameObject.GetComponent<Image> ().color.r,gameObject.GetComponent<Image> ().color.g,gameObject.GetComponent<Image> ().color.b,_value);
	}
}
