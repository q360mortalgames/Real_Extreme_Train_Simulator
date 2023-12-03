using UnityEngine;
using System.Collections;

public class TimerForLevels : MonoBehaviour {

	public TextMesh _text;
	float mf_TimerVal	= 0f;

	int sec	= 0;
	int min	= 0;

	void Update()
	{

		if(GlobalVariables.mGameState != eGAME_STATE.GamePlay)
			return;

		mf_TimerVal+=Time.deltaTime;

		min	= Mathf.FloorToInt(mf_TimerVal/60);
		sec	= Mathf.CeilToInt(mf_TimerVal%60);
		_text.text	= "Time - "+min.ToString("00")+" : "+sec.ToString("00");
	}

}
