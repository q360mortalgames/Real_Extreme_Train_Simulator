using UnityEngine;

public class PointHandler : MonoBehaviour {

	void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position,0.25f);	
	}
}
