using UnityEngine;
using System.Collections;

public class BtnEffect : MonoBehaviour {
	 
	public float mf_size = 0.0025f;
	public bool mb_colorEffect;
	public bool mb_IsStartOnAwake;
	public float mf_SpeedOfAnimation = 1;
	public Color _color	= Color.cyan;
	public float mf_delay = 0;

	void Start()
	{
		if(mb_IsStartOnAwake)
			BtnEffectOn();
	}

	public void BtnEffectOn()
	{
		if(!mb_colorEffect)
		{
			iTween.ScaleTo (gameObject, iTween.Hash ("Name","BtnEffect","x", mf_size, "y", mf_size, "easetype", iTween.EaseType.easeInOutSine, "time", mf_SpeedOfAnimation, "delay", 0,"looptype",iTween.LoopType.pingPong));

		}
		else
		{ 
			iTween.ColorTo(gameObject,iTween.Hash("Color",_color,"easetype", iTween.EaseType.easeInOutSine, "time", mf_SpeedOfAnimation, "delay", 0,"looptype",iTween.LoopType.pingPong));
		}
	}
	
	public void BtnEffectOff()
	{
		iTween.Stop (this.gameObject);	
	}
	

}
