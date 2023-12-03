using UnityEngine;
using System.Collections;

public class ThemesHandler : MonoBehaviour {

	[SerializeField] GameObject[] Themes;

	void Start () {
		Themes [GameManager.Instance.pathIndex].SetActive (true);
	}
}
