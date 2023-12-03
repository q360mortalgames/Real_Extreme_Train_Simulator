
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TrainManager : MonoBehaviour {
	

	GameObject _CurrentTrainObject;

	[SerializeField] GameObject[] _DiffTrains;
	[SerializeField] Transform _trainPos;
	[Space(10)]
	[SerializeField] GamePlayManager _GamePlayManager;
	[SerializeField] CameraSmoothFollowWithRotation _smoothFollow;
	void Awake () {
		
		//RequestToInstantiateToFreeMode ();
	}


	void Start () {

		RequestToInstantiateToFreeMode ();
	}
	void RequestToInstantiateToFreeMode(){

		//if (AdsManager.Instance) {
		//	AdsManager.Instance.RequestToShowAds (ADS_STATE.INGAME_PAGE, 0);
		//}
		 
		_CurrentTrainObject = Instantiate (_DiffTrains [GlobalVariables.i_CurrentTrainSelected]) as GameObject;
		_CurrentTrainObject.name = "Player-"+GlobalVariables.i_CurrentTrainSelected;
		_CurrentTrainObject.tag = "TrainDriver";
		_CurrentTrainObject.transform.position	= _trainPos.position;
		_CurrentTrainObject.transform.rotation	= _trainPos.rotation;
		_GamePlayManager.gameObject.SetActive (true);
		_CurrentTrainObject.GetComponent<TrainMovementScript> ().enabled = true;
		_GamePlayManager._trainMovementScript = _CurrentTrainObject.GetComponent<TrainMovementScript> ();
		CameraSmoothFollowWithRotation.Instance.cameraPositions =  _CurrentTrainObject.GetComponent<TrainMovementScript> ().cameraPositions;
		_smoothFollow.enabled = true;
         
        

	}




}
