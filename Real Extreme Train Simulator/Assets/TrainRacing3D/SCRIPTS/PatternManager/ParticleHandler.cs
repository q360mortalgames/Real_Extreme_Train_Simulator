using UnityEngine;
using System.Collections;

public class ParticleHandler : MonoBehaviour {

	[SerializeField] GameObject[] particleObject;

	void OnTriggerEnter (Collider other)
	{	
		switch (other.tag) {
			
		case "TrainDriver":
			switch (GameManager.Instance.themesIndex) {

			case 0:
			case 1:
			case 3:
				particleObject [0].SetActive (Random.Range (0, 100) < 70 ? true : false);
				break;

			case 2:
				particleObject [1].SetActive (Random.Range (0, 100) < 70 ? true : false);
				break;
			}						
			break;
		}
		transform.gameObject.GetComponent<BoxCollider> ().enabled = false;
	}
}
