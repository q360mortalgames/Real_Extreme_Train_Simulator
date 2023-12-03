using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingPage : MonoBehaviour
{
	AsyncOperation _async;
	[SerializeField] Image _ProgressBarSlider;

	void Start (){

		//if (AdsManager.Instance) {
		//	AdsManager.Instance.RequestToShowAds(ADS_STATE.OTHER_PAGE,0);
		//}
		Invoke ("RequestToLoadNextScene", 2f);
		_ProgressBarSlider.fillAmount = 0;
	}

	void RequestToLoadNextScene (){
		
		StartCoroutine (ieRequestToLoadNextScene ());
	}

	IEnumerator ieRequestToLoadNextScene (){
		
		if (GlobalVariables.sSceneToBeLoaded == "")
			yield break;
		
		//Debug.Log ("load scene : " + GlobalVariables.sSceneToBeLoaded);
		_async	= Application.LoadLevelAsync (GlobalVariables.sSceneToBeLoaded);

		yield return _async; 
	}

	void Update (){
		
		if (_async != null) {
			_ProgressBarSlider.fillAmount = _async.progress+0.1f;
		}
	}
}
