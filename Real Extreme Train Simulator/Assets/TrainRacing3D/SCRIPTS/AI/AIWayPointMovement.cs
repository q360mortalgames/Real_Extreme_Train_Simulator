using UnityEngine;
using System.Collections;

public class AIWayPointMovement : MonoBehaviour 
{
	public bool mb_IsStart = false;
	public bool mb_IsOrientToPath = false;
	public Transform[] _WayPoints;
	public float mf_Speed	= 2f;

	public float _fWayPointRadius = 1f;
	int waypointID	= 0;
	Transform _transform;

	void Start()
	{
		waypointID	= 0;
		_transform	= transform;
	}

	void Update()
	{
		if(GlobalVariables.mGameState != eGAME_STATE.GamePlay || !mb_IsStart)
			return;

		if(Vector3.Distance(_transform.position,_WayPoints[waypointID].position) <= _fWayPointRadius)
		{
			waypointID++;	
			if(waypointID >= _WayPoints.Length)
				waypointID	= _WayPoints.Length-1;
		}
		transform.position	= Vector3.MoveTowards(transform.position,_WayPoints[waypointID].position,Time.deltaTime*mf_Speed);

		if(mb_IsOrientToPath)
			transform.LookAt(_WayPoints[waypointID]);
	}

	public void OnRequestToSetInitialPosition(){
		waypointID = 0;
		transform.position = _WayPoints [waypointID].position;
		this.mb_IsStart = false;
	}

	public void OnDrawGizmos() {
		if(_WayPoints == null)
			return;
		
		for(int i=0;i< _WayPoints.Length;i++) {
			Gizmos.color = new Color(0.9f, 0, 0, 0.3f);
			Gizmos.DrawSphere(_WayPoints[i].position, _fWayPointRadius);
			Gizmos.color = Color.red;
			Vector3 pos = _WayPoints[i].position;
			if(i>0) {
				Vector3 prev = _WayPoints[i-1].position;
				Gizmos.DrawLine(prev,pos);
			}
		}
		
		
	}
}
