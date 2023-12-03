using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class SpriteScaleController : MonoBehaviour {

	public enum Stretch{Horizontal, Vertical, Both, SpriteObject};
	public Stretch stretchDirection = Stretch.Both;
	public Camera _mCamera;
	public float ratio;

	Vector2 offset = new Vector2(0f,0f);	
	SpriteRenderer sprite;
	Transform _thisTransform;
		
	void Start () 
	{
		_thisTransform = transform;
		sprite = GetComponent<SpriteRenderer>();
		StartCoroutine(stretch());
	}

	IEnumerator stretch()
	{
		yield return new WaitForEndOfFrame();
		AutoSetScale();
	}

//	void Update(){
//		AutoSetScale ();
//	}
	void AutoSetScale()
	{
		float worldScreenHeight = _mCamera.orthographicSize*ratio;
		float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
		float ratioScale = worldScreenWidth / sprite.sprite.bounds.size.x;
		ratioScale += offset.x;
		float h = worldScreenHeight /  sprite.sprite.bounds.size.y;
		h += offset.y;
		switch(stretchDirection)
		{
		case Stretch.SpriteObject:			
			_thisTransform.localScale = new Vector3(ratioScale,ratioScale,_thisTransform.localScale.z);
			break;
		case Stretch.Horizontal:
			_thisTransform.localScale = new Vector3(ratioScale,_thisTransform.localScale.y,_thisTransform.localScale.z);
			break;
		case Stretch.Vertical:
			_thisTransform.localScale = new Vector3(_thisTransform.localScale.x, h,_thisTransform.localScale.z);
			break;
		case Stretch.Both:
			_thisTransform.localScale = new Vector3(ratioScale, h,_thisTransform.localScale.z);
			break;		
		default:break;
		}
	}
}