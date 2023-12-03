using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointerHandler : MonoBehaviour {

	private static PointerHandler _instance = null;
	public static PointerHandler Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(PointerHandler)) as PointerHandler; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	float totalDistance;
	[HideInInspector]
	public Transform finalPosition;

	public Sprite[] pointerTextures;
	[Space(5)]
	public Image pointerPlayer;
	public Image pointerOpponent;

	[HideInInspector]
	public GameObject _player;
	[HideInInspector]
	public GameObject _opponent;

	Vector3 startPlayerPosition;
	Vector3 _pointerInitialPos;


	float pointerToTravel = 1000;
	Vector3 _currentPointerPosition;

	public void StartToProcess () {
		
		if (pointerPlayer)
			_pointerInitialPos = pointerPlayer.transform.localPosition;

		startPlayerPosition = _player.transform.position;
		totalDistance = Mathf.Abs (startPlayerPosition.z - finalPosition.position.z);
	}

	public void StartToProcessOpponent () {

		if (_opponent)
			_pointerInitialPos = pointerOpponent.transform.localPosition;

		startPlayerPosition = _opponent.transform.position;
	}

	public void UpdateToProcess ()
	{
		if (_player == null || _opponent == null) {
			return;
		}
		//Debug.Log ("--UpdateToProcess Points Poses::");
		float distanceTravelled = Mathf.Abs (_player.transform.position.z - startPlayerPosition.z);

		float playerProgress = (distanceTravelled / totalDistance);
		playerProgress = Mathf.Clamp (playerProgress, 0, 1);
		distanceTravelled = Mathf.Abs (_opponent.transform.position.z - startPlayerPosition.z);

		float opponentProgress = (distanceTravelled / totalDistance);
		opponentProgress = Mathf.Clamp (opponentProgress, 0, 1);

		_currentPointerPosition = _pointerInitialPos;
		_currentPointerPosition.x += (playerProgress * pointerToTravel);
		pointerPlayer.rectTransform.anchoredPosition3D = new Vector3(_currentPointerPosition.x , pointerPlayer.rectTransform.anchoredPosition3D.y,pointerPlayer.rectTransform.anchoredPosition3D.z);

		_currentPointerPosition = _pointerInitialPos;
		_currentPointerPosition.x += (opponentProgress * pointerToTravel);
		pointerOpponent.rectTransform.anchoredPosition3D = new Vector3(_currentPointerPosition.x , pointerOpponent.rectTransform.anchoredPosition3D.y,pointerOpponent.rectTransform.anchoredPosition3D.z);

	}
}
