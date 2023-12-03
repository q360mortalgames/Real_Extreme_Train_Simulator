using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HornCheckScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler 
{
	bool isHornClick	= false;
	public void OnPointerDown(PointerEventData eventData)
	{
		isHornClick	= true;
		SoundController.Instance.StartHorn();
	}
	public void OnPointerUp(PointerEventData eventData)
	{
		isHornClick	= false;
		SoundController.Instance.StopHorn();
	}
	void Update()
	{
		if(isHornClick)
			GamePlayManager.Instance.OnHornClick();
	}
}
