using UnityEngine;
using System.Collections;

public class ballDestroy : MonoBehaviour {


	// Use this for initialization
	//
	int level;
	int giveScore;
	int score;
	void Start () {
	if (level == 2) {
						giveScore = 20;
				} else {
			giveScore=10;
				}

	}
	void ScoreOnDestroy()
	{
		ScoreManager.score = ScoreManager.score + giveScore;



		PlayerPrefs.SetInt ("Score", PlayerPrefs.GetInt ("Score") + giveScore);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
