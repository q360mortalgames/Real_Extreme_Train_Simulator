using UnityEngine;
using System.Collections;

public class UIAnimation : MonoBehaviour 
{
	public GameObject _target;
	public Sprite[] _loading;
	public float delay =0.1f;
	public bool sprites,loop,fromStart;
	SpriteRenderer _spriteTarget;
	//	int i;
	void Start () 
	{
//		if (sprites)
//			_spriteTarget = _target.GetComponent<SpriteRenderer> ();
		if(fromStart)
		StartCoroutine ("ChangeTexture");

		if (_target == null)
						_target = this.gameObject;
	}

	public void AnimateTexture()
	{
		StartCoroutine ("ChangeTexture");
	}

	IEnumerator ChangeTexture()
	{
		if (sprites == false) 
		{
			for (int i = 0; i < _loading.Length; i++) 
			{
			//	_target.GetComponent<Texture>().texture = _loading [i].texture;	
				yield return new WaitForSeconds (delay);
			}
		} 
		else 
		{
			for (int i = 0; i < _loading.Length; i++) 
			{
				_spriteTarget.sprite = _loading[i];
				yield return new WaitForSeconds (delay);
			}
		}
		if(loop)
		StartCoroutine ("ChangeTexture");
	}

	public void ChangeTexture(int number)
	{
		//_target.GetComponent<GUITexture>().texture = _loading [number].texture;
	}
}
