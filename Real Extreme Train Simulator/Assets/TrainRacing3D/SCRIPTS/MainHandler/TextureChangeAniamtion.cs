using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextureChangeAniamtion : MonoBehaviour {

	Texture _textureObj;
	public Texture[] _textures;
	public float mf_TargetTime = 1f;
	float mf_Timer;
	int currentIndex	= 0;

	// Use this for initialization
	void Start () 
	{
		_textureObj	= gameObject.GetComponent<Texture>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_textureObj == null)
			return;

		mf_Timer += Time.deltaTime;

		if(mf_Timer >= mf_TargetTime)
		{
			mf_Timer= 0;
			currentIndex++;
			if(currentIndex >=_textures.Length)
				currentIndex	= 0;

		//	Texture	= _textures[currentIndex];
		}
	}
}
