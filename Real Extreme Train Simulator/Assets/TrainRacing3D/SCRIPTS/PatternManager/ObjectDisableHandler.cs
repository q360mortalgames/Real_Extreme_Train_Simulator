using UnityEngine;
using System.Collections;

public class ObjectDisableHandler : MonoBehaviour {

	[SerializeField] GameObject disableObject;

	void Start () {
	
	}
	
	void OnTriggerEnter (Collider other)
	{	
		switch (other.tag) {
			
		case "TrainDriver":
			disableObject.SetActive(false);
			transform.gameObject.GetComponent<BoxCollider> ().enabled = false;
			break;
		}
	}
}
