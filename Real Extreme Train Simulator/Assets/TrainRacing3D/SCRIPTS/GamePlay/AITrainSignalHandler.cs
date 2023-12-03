using UnityEngine;
using System.Collections;

public class  AITrainSignalHandler: MonoBehaviour
{
	[SerializeField] TrainSignalHandler _trainSignalHandler;

	void Start ()
	{
		
	}

	void OnTriggerEnter (Collider other)
	{	
		switch (other.tag) {

		case "Obstacle":

			_trainSignalHandler.SetSignalToGreen ();
			transform.GetComponent<BoxCollider> ().enabled = false;
			break;
		}
	}

}
