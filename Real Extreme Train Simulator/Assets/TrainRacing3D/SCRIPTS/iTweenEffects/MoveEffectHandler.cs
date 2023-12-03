using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveEffectHandler : MonoBehaviour {

	[SerializeField] eMOVE_MODE _moveMode;

	[SerializeField] float moveTime;
	[SerializeField] float moveDelay;

	float defaultValue = 1000;
	Vector3 _InitialPosition;

	void OnEnable () {

		if (_InitialPosition == Vector3.zero) {
			_InitialPosition = gameObject.GetComponent<RectTransform> ().localPosition;
		}
		
		if (gameObject.GetComponent<iTween> ())
			Destroy (gameObject.GetComponent<iTween> ());

		gameObject.transform.localPosition = _InitialPosition;
		switch (_moveMode) {

		case eMOVE_MODE.Left:
			iTween.MoveFrom(gameObject, iTween.Hash("x", gameObject.transform.position.x - defaultValue, "time", moveTime, "delay", moveDelay));
			break;

		case eMOVE_MODE.Right:
			iTween.MoveFrom(gameObject, iTween.Hash("x", gameObject.transform.position.x + defaultValue, "time", moveTime, "delay", moveDelay));
			break;

		case eMOVE_MODE.Up:
			iTween.MoveFrom(gameObject, iTween.Hash("y", gameObject.transform.position.y + defaultValue, "time", moveTime, "delay", moveDelay));
			break;

		case eMOVE_MODE.Down:
			iTween.MoveFrom(gameObject, iTween.Hash("y", gameObject.transform.position.y - defaultValue, "time", moveTime, "delay", moveDelay));
			break;
		}
	}
}
