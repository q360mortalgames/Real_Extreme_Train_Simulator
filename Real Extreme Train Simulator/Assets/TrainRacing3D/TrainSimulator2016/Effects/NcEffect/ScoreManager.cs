using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	// Use this for initialization
 public  static	int score;
	void Start () {
		//score=PlayerPrefs.GetInt("Score"));
	}

	// Update is called once per frame
	void Update () {
	
	}
	void OnGui()
	{
		//GUI.Label (new Rect (0, 0, 120, 50), PlayerPrefs.GetInt("Score"));
	}

}
