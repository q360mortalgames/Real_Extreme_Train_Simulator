using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonClickAnimation : MonoBehaviour,IPointerUpHandler,IPointerDownHandler {

	Vector3 mv_ButtonScale;	
	Vector3 mv_ButtonScaleHAlt;	
	Vector3 mv_ButtonScaleHorn;
	Vector3 MainVal;

	bool isButtonPressed;
	void Start () 
	{
		MainVal = mv_ButtonScale		= new Vector3(1,1,1);
		mv_ButtonScaleHAlt= new Vector3(1.2f,1.2f,1.2f);
		mv_ButtonScaleHorn= new Vector3(1.5f,1.5f,1.5f);
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		if(name=="Halt"){

			mv_ButtonScale		= mv_ButtonScaleHAlt;
		}else if(name=="Horn"){
			mv_ButtonScale=mv_ButtonScaleHorn;
		}

		else{

			mv_ButtonScale=MainVal;
		}
		iTween.ScaleTo(gameObject,iTween.Hash("scale",mv_ButtonScale,"time",0.25f,"easetype",iTween.EaseType.easeOutBack));


	}
	public void OnPointerDown (PointerEventData eventData)
	{
		Vector3 newScale	= mv_ButtonScale + new Vector3(-0.2f,-0.2f,-0.2f);
		gameObject.GetComponent<RectTransform>().localScale	= newScale;
//		iTween.ScaleTo(gameObject,iTween.Hash("scale",newScale,"time",0.25f,"easetype",iTween.EaseType.easeOutBounce));
	}
}
