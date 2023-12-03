using UnityEngine;
using System.Collections;

public class Loopmove : MonoBehaviour {
	
	public Transform[] waypoints;
	public float waypointRadius = 1.0f;
	public bool loop ;
	public float speed = 2.0f;
	
	private Vector3 currentHeading,targetHeading;
	private  int targetwaypoint;
	private Transform _transform;
	public static bool isend;
	// Use this for initialization
	void Start () {
		//StartCoroutine(wait());

		isend=false;
		
		_transform = transform;
		targetwaypoint = 0;
		count=0;
		
		//iTween.MoveTo(this.gameObject, iTween.Hash ("y",waypoints[1].transform.position.y,"time",speed,"easeType","easeInOutQuad"));
	}
	
	private int count;
	// protected 
	void Update() {

					if(Vector3.Distance(_transform.position, waypoints[targetwaypoint].position) <= waypointRadius) 
					{
						targetwaypoint++;
						if(targetwaypoint >= waypoints.Length)
						{
							targetwaypoint = 0;
							if(!loop)
							enabled = false;
						}
					}
					
					_transform.position = Vector3.MoveTowards(_transform.position, waypoints[targetwaypoint].position, Time.deltaTime * speed);
					
		if(targetwaypoint==2)
		{
			isend=true;
		}
	//	print("isend"+isend);
	}
	


	void OnCollisionStay(Collision col)
	{
		if(col.gameObject.tag=="Player")
		{

			col.gameObject.transform.parent=this.transform.parent;

		}
	}

	void OnCollisionExit(Collision col)
	{
		if(col.gameObject.tag=="Player")
		{

			col.gameObject.transform.parent=null;
		
		}
		
	}
	
	
	public void OnDrawGizmos() {
		if(waypoints == null)
			return;
		
		for(int i=0;i< waypoints.Length;i++) {
			Gizmos.color = new Color(0.9f, 0, 0, 0.3f);
			Gizmos.DrawSphere(waypoints[i].position, waypointRadius);
			Gizmos.color = Color.red;
			Vector3 pos = waypoints[i].position;
			if(i>0) {
				Vector3 prev = waypoints[i-1].position;
				Gizmos.DrawLine(prev,pos);
			}
		}
		
		
	}
	
}
