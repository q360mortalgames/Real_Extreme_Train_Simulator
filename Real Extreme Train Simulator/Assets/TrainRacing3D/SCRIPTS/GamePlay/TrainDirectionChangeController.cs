using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TrainDirectionChangeController : MonoBehaviour,IPointerUpHandler {


	public void OnPointerUp (PointerEventData eventData)
	{
		gameObject.GetComponent<Slider>().value	= 0;
	}
}
