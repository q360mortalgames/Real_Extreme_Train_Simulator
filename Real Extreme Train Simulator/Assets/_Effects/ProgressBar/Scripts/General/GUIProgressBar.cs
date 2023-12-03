using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent (typeof(Image))]

public class GUIProgressBar : MonoBehaviour 
{
	Rect _TextureCompleteRect,_TextureCurrentRect;
	public float mf_Percentage	= 0f;
	float f_PercentageVisible	= 1;

	void Start()
	{
		SetBasicRect();
	}
	void SetBasicRect()
	{
	//	_TextureCompleteRect	= GetComponent<Image>().pixelInset;
		_TextureCurrentRect		= _TextureCompleteRect;
	}

	void Update()
	{
//		if(GamePlayManager.Instance.mGameState == eGAME_STATE.Play)
		{
//			f_PercentageVisible	= Mathf.Lerp(f_PercentageVisible,mf_Percentage,Time.deltaTime);
			if(f_PercentageVisible != mf_Percentage)
			{
//				f_PercentageVisible	+= 0.01f*(f_PercentageVisible>mf_Percentage ? -1 : 1);
				f_PercentageVisible	= mf_Percentage;
				SetPercentage(f_PercentageVisible);
			}
		}
	}

	void SetPercentage(float percentageToShow)
	{
		float xOffSet	= _TextureCompleteRect.width*(percentageToShow/100);

		if(xOffSet < 0)
			xOffSet	= 0;
		else if(xOffSet > _TextureCompleteRect.width)
			xOffSet	= _TextureCompleteRect.width;

		_TextureCurrentRect	= new Rect(0,0,xOffSet,_TextureCompleteRect.height);
	//	GetComponent<GUITexture>().pixelInset	= _TextureCurrentRect;
	}
}
