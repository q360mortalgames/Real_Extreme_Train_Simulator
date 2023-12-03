using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PrizeHandler : MonoBehaviour {

	[SerializeField] Image player;
	[SerializeField] Sprite[] trainsTexture;

	void Start () {
		
		player.sprite = trainsTexture [GlobalVariables.i_CurrentTrainSelected];

		transform.gameObject.GetComponent<Text> ().text = "" + GameManager.Instance.Prize [GameManager.Instance.pathIndex];
	}
}
