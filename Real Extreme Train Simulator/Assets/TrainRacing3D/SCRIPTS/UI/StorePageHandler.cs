using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StorePageHandler : MonoBehaviour
{

	private static StorePageHandler _instance = null;
	public static StorePageHandler Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(StorePageHandler)) as StorePageHandler; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	[Space (10)]
	[SerializeField] Text _totalCoins;

	[SerializeField] ScrollRect scrollRect;

	int tempTotalCoins;

	void TotalCoinInfo ()
	{

		if (GameManager.Instance && GameManager.Instance.totalCoins != tempTotalCoins) {
			tempTotalCoins = GameManager.Instance.totalCoins;
			_totalCoins.text = tempTotalCoins.ToString ();
			PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);
		}
		scrollRect.horizontalNormalizedPosition = 0f;
	}

//	void Update ()
//	{
//
//		if (GlobalVariables.isBackNavigation && Input.GetKeyUp (KeyCode.Escape)) {
//			UIHandler.Instance.RequestToEnableObject (UIHandler.Instance.previousPage);
//		}
//	}


	void OnEnable ()
	{
		TotalCoinInfo ();
	}


	#region OnButtonAction

	public void OnClick (string _click)
	{
		StartCoroutine (ieOnClick (_click));
	}

	IEnumerator	ieOnClick (string _click)
	{
		SoundController.Instance.OnButtonClick ();
		yield return new WaitForSeconds (UIHandler.Instance.navigationTime);
		OnClickComplete (_click);
	}

	void OnClickComplete (string _click)
	{
		switch (_click) {	
		case "BTN_MORE_GAMES":
			Debug.Log ("BTN_MORE_GAMES");
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToMoreGames ();
			//}
			break;

		case "BTN_BACK":
			Debug.Log ("BTN_BACK");	
			UIHandler.Instance.RequestToEnableObject (UIHandler.Instance.previousPage);
			break;	

		case "BTN_HOME":
			UIHandler.Instance.RequestToEnableObject (0);
			Debug.Log ("BTN_HOME");
			break;

		case "BTN_NO_ADS":
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToInApp (0);
			//}
			break;

		case "BTN_PACK_1":
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToInApp (1);
			//}
			break;

		case "BTN_PACK_2":
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToInApp (2);
			//}
			break;

		case "BTN_PACK_3":
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToInApp (3);
			//}
			break;

		case "BTN_PACK_4":
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToInApp (4);
			//}
			break;

		case "BTN_PACK_5":
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToInApp (5);
			//}
			break;
		
		}
		#endregion
	}
}