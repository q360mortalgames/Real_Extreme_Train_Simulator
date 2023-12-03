using UnityEngine;
using System.Collections;

public class PatternController : MonoBehaviour {
	
	void Start () {
	
	}
	
	void OnTriggerEnter (Collider other)
	{	

		print ("check next track....");
		switch (other.tag) {
			
		case "TrainDriver":
			transform.gameObject.GetComponent<BoxCollider> ().enabled = false;
			if(levelManager.LevelMode){
				levelManager.mee.NextTrackCreate ();
			}else{
				PatternManager.Instance.SetToNextPattern ();

			}


			break;
		}
	}
}
