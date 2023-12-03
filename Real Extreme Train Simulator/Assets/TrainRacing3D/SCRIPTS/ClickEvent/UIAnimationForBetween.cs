using UnityEngine;
using System.Collections;

public class UIAnimationForBetween : MonoBehaviour {

	public bool xDir,yDir,left,up,scale,directPlay;
	public float distance,time,delay;
	public float _val;
	public iTween.EaseType effectType;
	public iTween.LoopType loopType;
	public int addValue;
	public bool alphaFade;
	
	void Start () 
	{
//		if(directPlay)
//			CallStart ();
		CallStart ();
	}
	public void CallStart()
	{
		if (addValue == 0)
			addValue = 1;
		if (time == 0)
			time = 1;
		if(xDir)
		{
			Vector3 temp = transform.position;
			_val = temp.x;
			if(left)
				temp.x -=addValue;
			else
				temp.x += addValue; 
			
			
			transform.position = temp;
		}
		if (yDir) 
		{
			Vector3 temp = transform.position;
			_val = temp.y;
			if(up)
				temp.y +=addValue;
			else
				temp.y -= addValue; 
			
			transform.position = temp;
		}
		

		//		Debug.Log (this.gameObject.name);
		////		StartCoroutine("Call");
		//		Invoke ("Call", delay);
		Animate ();
	}
	public	void Animate()
	{
		//		yield return new WaitForSeconds (delay);
		if (xDir) 
		{
			
			iTween.MoveTo (this.gameObject, iTween.Hash ("x",  _val,"delay",delay, "looptype", loopType, "time", time, "easeType", effectType,"ignoretimescale",true));
		} 
		if(yDir)
		{
			iTween.MoveTo (this.gameObject, iTween.Hash ("y",  _val, "delay",delay,"looptype", loopType, "time", time, "easeType", effectType,"ignoretimescale",true));
		}

		if (scale)
			iTween.ScaleAdd (this.gameObject, iTween.Hash ("x", _val, "y", _val, "time", time, "delay", delay, "looptype", loopType, "easetype", effectType));
		if (alphaFade) 
		{
			iTween.FadeFrom(this.gameObject,iTween.Hash("a",0,"time",0.5f));
		}
	}
	
	public	void ReverseAnimate()
	{
		if (xDir) 
		{
			//				_val = transform.position.x;
			if(left)
				iTween.MoveTo (this.gameObject, iTween.Hash ("x",  _val-1, "looptype", loopType, "time", time, "easeType", effectType,"ignoretimescale",true));
			else
				iTween.MoveTo (this.gameObject, iTween.Hash ("x",  _val+1, "looptype", loopType, "time", time, "easeType", effectType,"ignoretimescale",true));		
		} 
		if(yDir)
		{
			//				_val = transform.position.y;
			if(up)
				iTween.MoveTo (this.gameObject, iTween.Hash ("y",  _val+1, "looptype", loopType, "time", time, "easeType", effectType,"ignoretimescale",true));
			else
				iTween.MoveTo (this.gameObject, iTween.Hash ("y",  _val-1, "looptype", loopType, "time", time, "easeType", effectType,"ignoretimescale",true));		
		}
	}
}
