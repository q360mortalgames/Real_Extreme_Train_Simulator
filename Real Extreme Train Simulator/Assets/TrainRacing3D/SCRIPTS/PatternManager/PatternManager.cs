using UnityEngine;
using System.Collections;

public enum ePATTERN_GENERATION
{
	Random,
	Queue,
	levels
}

public class PatternManager : MonoBehaviour
{
		private static PatternManager _instance = null;	
		public static PatternManager Instance {
				get {
						// if the instance hasn't been assigned then search for it
						if (_instance == null) {
								_instance = GameObject.FindObjectOfType (typeof(PatternManager)) as PatternManager; 	
						}
			
						return _instance; 
				}
		}

		public ePATTERN_GENERATION PatternsGeneration;
        
		public PatternScript[]Patterns;
		public Material[] skyboxes;
		public GameObject[] lights;
			
		[HideInInspector]
		public int nextPatternId = 0,currentPatternId = 0;
		int previousPatternId = 0;	 
		int startPatternID;
        
		void Start ()
		{

		    levelManager.LevelMode = false;
			startPatternID = (LevelData.Instance.mi_Level != 1?Random.Range (0,Patterns.Length):0);
            
		    print ("start id"+startPatternID);
			RenderSettings.skybox = skyboxes[startPatternID];
			lights [startPatternID].SetActive (true);
            
            


			for (int i = 0; i < Patterns.Length; i++) {
				if (startPatternID != i) {
					Patterns [i].transform.position = new Vector3 (5000, 0, 0);
				}				
			}
			
			nextPatternId = startPatternID;
			previousPatternId = startPatternID;	
            
			if (LevelData.Instance.mi_Level != 1) {
				Patterns [nextPatternId].nextPatternGenerate.gameObject.GetComponent<BoxCollider> ().enabled = true;
			}						
		}

	int randonNum;
	int RequestToGenerateRandomNumber(int _previousPatternId, int _currentPatternId, int size)
	{
		do {
			randonNum = Random.Range (0, size);
		} while(randonNum == _previousPatternId || randonNum == _currentPatternId);

		return randonNum;
	}

	[SerializeField] GameObject[] particles;
	public void RequestToEnableEffect(int _currentPattern, bool isTunnelEnter = false){
		
		foreach (GameObject particle in particles) {
			particle.SetActive (false);		
		}

		int randomNumber = Random.Range (0, 100);
		bool isEffectDisable = (randomNumber > 50) ? false : true;

		if (isEffectDisable || isTunnelEnter) {
			return;
		}

		switch (_currentPattern) {
		case 2:
			particles [0].SetActive (true);
			break;

		case 0:
		case 1:
		case 3:
			particles [1].SetActive (true);
			break;
		}
	}

	int LapCount;
	public void SetToNextPattern ()
	{		
		print ("next : "+PatternsGeneration);
		switch (PatternsGeneration) 
		{
		case ePATTERN_GENERATION.Random:

			previousPatternId = currentPatternId;
			currentPatternId = nextPatternId;		
			nextPatternId = RequestToGenerateRandomNumber (previousPatternId, currentPatternId, Patterns.Length);

			Patterns [nextPatternId].transform.position = Patterns [currentPatternId].head.position;	
			Patterns [nextPatternId].nextPatternGenerate.gameObject.GetComponent<BoxCollider> ().enabled = true;
			OnRequestToResetTrack ();

			RequestToEnableEffect (currentPatternId);
			//trackBlockers.position = Patterns [previousPatternId].transform.position + new Vector3 (0, 0.35f, 50);
			break;

		case ePATTERN_GENERATION.Queue:

			previousPatternId = nextPatternId;
			nextPatternId++;

			if (nextPatternId >= Patterns.Length) {	
				nextPatternId = 0;	
				LapCount++;
			}

			Patterns [nextPatternId].transform.position = Patterns [previousPatternId].head.position;	
			Patterns [nextPatternId].nextPatternGenerate.gameObject.GetComponent<BoxCollider> ().enabled = true;
			OnRequestToResetTrack ();
			break;
		}
	}

	void OnRequestToResetTrack()
	{
		for (int i = 0; i < Patterns [nextPatternId]._scrTrackChange.Length; i++) {
			Patterns [nextPatternId]._scrTrackChange [i].trackChanger.OnResetChangeTrack ();
			Patterns [nextPatternId]._scrTrackChange [i].startCollider.SetActive (true);
		}

		for (int cd = 0; cd < Patterns [nextPatternId]._CrowdCrossingInfo.Length; cd++) {
			Patterns [nextPatternId]._CrowdCrossingInfo [cd].hornCollider.SetActive (true);
		}

		for (int t = 0; t < Patterns [nextPatternId]._RoadCrossingInfo.Length; t++) {
			Patterns [nextPatternId]._RoadCrossingInfo [t].hornCollider.SetActive (true);
			Patterns [nextPatternId]._RoadCrossingInfo [t].onTrigger.OnHornReset();
		}

		for (int m = 0; m < Patterns [nextPatternId]._AIWayPointMovement.Length; m++) {
			Patterns [nextPatternId]._AIWayPointMovement [m].OnRequestToSetInitialPosition();
		}

		for (int a = 0; a < Patterns [nextPatternId]._AISignalHandler.Length; a++) {
			Patterns [nextPatternId]._AISignalHandler [a]._AITrainSignalReset();
		}

		if (Patterns [nextPatternId]._stationInfo._StationTrigger != null) {
			Patterns [nextPatternId]._stationInfo._StationTrigger.enabled = true;
		}
		if (Patterns [nextPatternId]._stationInfo._StationStopPoint != null) {
			Patterns [nextPatternId]._stationInfo._StationStopPoint.SetActive (true);
		}
		if (Patterns [nextPatternId]._stationInfo._StationRoadBlocker != null) {
			Patterns [nextPatternId]._stationInfo._StationRoadBlocker.SetActive (true);
		}
		for (int c = 0; c < Patterns [nextPatternId]._characterInfo.Length; c++) {
			Patterns [nextPatternId]._characterInfo [c]._Character.transform.localPosition = Patterns [nextPatternId]._characterInfo [c]._InitialPosition;
			Patterns [nextPatternId]._characterInfo [c]._Character.SetActive(true);
		}
	}
}
