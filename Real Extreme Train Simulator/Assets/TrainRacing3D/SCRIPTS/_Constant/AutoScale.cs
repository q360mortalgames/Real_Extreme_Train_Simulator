using UnityEngine;
using System.Collections;

public class AutoScale : MonoBehaviour {
	
	[SerializeField] CURRENT_AXIS _axis;
	void Start () {
		transform.localScale = ScaleController.setGameObjectScale (transform, _axis);
	}

}
