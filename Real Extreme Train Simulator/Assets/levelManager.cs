using UnityEngine;
using System.Collections;

public class levelManager : MonoBehaviour {

	public PatternScript[] LevelsData;
	private string[] textSplit;
	//tn1,ts1
	public string[] levelString;
	private int tracknum,totaltracks = 0;
	public static bool LevelMode;
	public static levelManager mee;

	public int NumofStations = 0;

	public static int stationsreached = 0;

	// Use this for initialization
	void Start () {
		mee = this;
		LevelMode = true;
		preId=previousPatternId = 0;
		for (int i = 0; i < LevelsData.Length; i++) {
			LevelsData [i].gameObject.SetActive (false);
						
		}

		int lvlnum=levelselectionNew.OpenLevelNUm;
	//	lvlnum = 1;
		stationsreached = 0;
		NumofStations = 0;
		textSplit = levelString[lvlnum].Split(","[0]);

		for (int i = 0; i < textSplit.Length; i++) {
			print(textSplit[i]);
			if(textSplit[i].Contains ("ts")){

				NumofStations++;
			}
		}
		totaltracks = textSplit.Length;

		print(NumofStations+" : NumofStations");
		//createtrainTracks ();
		NextTrackCreate ();
	}


	public void createtrainTracks(){


		int mydataNum =int.Parse( textSplit [tracknum].Substring (2, textSplit [tracknum].Length - 2));
		string trackType = textSplit [tracknum].Substring (0, 2);






		LevelsData[mydataNum].gameObject.SetActive (true);
		LevelsData [mydataNum].nextPatternGenerate.gameObject.GetComponent<BoxCollider> ().enabled = true;

		if (trackType == "tn") {
			LevelsData [mydataNum]._stationInfo._StationTrigger.gameObject.transform.parent.parent.gameObject.SetActive (false);
		} else {
			LevelsData [mydataNum]._stationInfo._StationTrigger.gameObject.transform.parent.parent.gameObject.SetActive (true);

		}


		tracknum++;

		previousPatternId = mydataNum;

	}


	public void NextTrackCreate(){



		print(tracknum+" : trackn");
		if(tracknum>=textSplit.Length){

			print(" no more tracks ");
			return;
		}
		preId = previousPatternId;

		int mydataNum =int.Parse( textSplit [tracknum].Substring (2, textSplit [tracknum].Length - 2));
		string trackType = textSplit [tracknum].Substring (0, 2);

		print(tracknum+" : "+textSplit[tracknum]+" : "+ textSplit[tracknum].Substring (0, 2));
		print(mydataNum+" ## "+ trackType);

		LevelsData[mydataNum].gameObject.SetActive (true);

		if (trackType == "tn") {
			LevelsData [mydataNum]._stationInfo._StationTrigger.gameObject.transform.parent.parent.gameObject.SetActive (false);
		} else {
			LevelsData [mydataNum]._stationInfo._StationTrigger.gameObject.transform.parent.parent.gameObject.SetActive (true);

		}
		LevelsData [mydataNum].nextPatternGenerate.gameObject.GetComponent<BoxCollider> ().enabled = true;
		if (tracknum != 0) {
			
			LevelsData [mydataNum].transform.position = LevelsData [previousPatternId].head.position;
		}

		tracknum++;
		previousPatternId = mydataNum;


		Debug.Log (previousPatternId +" : ------ : "+preId);

	}

	public int previousPatternId,preId = 0;
	// Update is called once per frame
	void Update () {
	
	}
}
