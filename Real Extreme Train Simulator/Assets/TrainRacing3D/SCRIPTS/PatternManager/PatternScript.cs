using UnityEngine;
using System.Collections;

public class PatternScript : MonoBehaviour 
{
	public Transform head , tail, nextPatternGenerate;
	[Space(10)]
	public TrackInfo[] _scrTrackChange;
	public AIWayPointMovement[] _AIWayPointMovement;
	public TrainSignalHandler[] _AISignalHandler;
	public CrowdCrossingInfo[] _CrowdCrossingInfo;
	public RoadCrossingInfo[] _RoadCrossingInfo;
	public StationInfo _stationInfo;
	public CharacterInfo[] _characterInfo;



	void Start ()
	{
		for (int i = 0; i < this._characterInfo.Length; i++) {
			this._characterInfo [i]._InitialPosition = this._characterInfo [i]._Character.transform.localPosition;
		}
	}

}


