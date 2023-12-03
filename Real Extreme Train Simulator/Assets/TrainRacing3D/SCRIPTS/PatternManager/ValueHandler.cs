using UnityEngine;
using System.Collections;

public class ValueHandler : MonoBehaviour {
	
	void Start () {
	
	}
	
	void OnTriggerEnter (Collider other)
	{	
		switch (other.tag) {
			
		case "TrainDriver":
			GamePlayManager.Instance.RequestToResetDirectionChangeValue ();
			break;
		}
	}
}
