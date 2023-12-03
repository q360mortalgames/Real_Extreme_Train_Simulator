using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimerHandler : MonoBehaviour
{

	private static GameTimerHandler _instance = null;
	public static GameTimerHandler Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(GameTimerHandler)) as GameTimerHandler; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	[SerializeField]
	float mf_LevelCompleteTimer;
	public bool isTimmerUpdate	= false;


	bool isColorChange	= false;
	int colorVal = 0;
	float _fColorChnageTime	= 1f;
	float _fColorChnageTimeCu	= 1f;
	[SerializeField]
	Color BaseColor, AlertColor;


	int min = 0;
	int sec = 0;

	[SerializeField] GameObject g_TimerBase;

	void Start ()
	{
		if (mf_LevelCompleteTimer == 0) {
			mf_LevelCompleteTimer	= 0;
		}

		gameObject.GetComponent<Text> ().text	= min.ToString ("00") + ":" + sec.ToString ("00");


	//	Invoke ("makefail",10);


	}


	void makefail(){
		Debug.Log ("make fail....");


		GamePlayManager.Instance.OnLevelFailed ();

	}
	public void UpdateTime (int timeToChange)
	{
		mf_LevelCompleteTimer	= timeToChange;
		min	= Mathf.CeilToInt (mf_LevelCompleteTimer) / 60;
		sec	= Mathf.CeilToInt (mf_LevelCompleteTimer) % 60;
		gameObject.GetComponent<Text> ().text	= min.ToString ("00") + ":" + sec.ToString ("00");

	}
	// Update is called once per frame
	void Update ()
	{
		if (isTimmerUpdate && GlobalVariables.mGameState == eGAME_STATE.GamePlay) 
		{
			mf_LevelCompleteTimer	-= Time.deltaTime;

			if (mf_LevelCompleteTimer < 0) {

				//GlobalVariables.mGameState	= eGAME_STATE.LevelFailed;
				//GamePlayManager.Instance.mb_isCrashed	= false;
				//GamePlayManager.Instance.OnLevelFailed ();
				if (levelManager.LevelMode || GlobalVariables.isMultiPlayerMode) {
					GamePlayManager.Instance.mb_isTimeUp = true;
					makefail ();
					isTimmerUpdate = false;
				} else {

					mf_LevelCompleteTimer	= 1600;
				}


			}

			if (mf_LevelCompleteTimer <= 5 && !isColorChange)
				isColorChange	= true;
			else if(isColorChange )
			{
				isColorChange	= false;
				GetComponent<Text> ().color	= BaseColor;
			}

			if (isColorChange) {
				_fColorChnageTimeCu	+= Time.deltaTime;

				if (_fColorChnageTimeCu >= _fColorChnageTime) {
					if (colorVal == 0)
						GetComponent<Text> ().color	= AlertColor;
					else
						GetComponent<Text> ().color	= BaseColor;

					colorVal++;
					if (colorVal > 1)
						colorVal	= 0;

					_fColorChnageTimeCu	= 0f;
				}
			}

			min	= Mathf.CeilToInt (mf_LevelCompleteTimer) / 60;
			sec	= Mathf.CeilToInt (mf_LevelCompleteTimer) % 60;
			gameObject.GetComponent<Text> ().text	= min.ToString ("00") + ":" + sec.ToString ("00");
		}
	
	}
}
