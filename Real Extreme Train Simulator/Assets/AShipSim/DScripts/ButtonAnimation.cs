using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable]
public enum eMovementDirection
{
	Up,
	Down,
	Left,
	Right
}




public class ButtonAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public iTween.EaseType effectType;
	public eMovementDirection Direction;
	
	bool _pressed = false;
	private bool mb_btnDown;
	private Vector3 initScaleVal;
	public Vector3 Scalval = new Vector3 (1, 1, 1);

	void Start ()
	{
		initScaleVal = gameObject.transform.localScale;
	}

	public void OnPointerDown (PointerEventData eventData)
	{
		_pressed = true;
		//		print ("Button Down");
	}

	public void OnPointerUp (PointerEventData eventData)
	{
		_pressed = false;
		//		print ("Button Up");
	}

	void Update ()
	{
		if (_pressed) {
			if (mb_btnDown)
				return;
			ButtonDownAnim ();
		} else {
			if (!mb_btnDown)
				return;
			ButtonUpAnim ();  
				
		}
		
		switch (Direction) {
		case eMovementDirection.Up:
			break;
			
		// (etc...)
			
		}
	}

	public void ButtonDownAnim ()
	{
		mb_btnDown = true;

		iTween.ScaleTo (this.gameObject, iTween.Hash ("x", 0.9, "y", 0.9, "time", 0f, "IgnoreTimeScale", true));  //0.95
	}

	public void ButtonUpAnim ()
	{
		mb_btnDown = false;

		iTween.ScaleTo (this.gameObject, iTween.Hash ("x", Scalval.x, "y", Scalval.y, "time", 0f, "IgnoreTimeScale", true));        //1
	}
}
