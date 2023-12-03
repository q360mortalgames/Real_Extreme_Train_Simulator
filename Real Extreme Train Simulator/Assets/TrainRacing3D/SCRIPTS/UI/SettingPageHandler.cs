using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingPageHandler : MonoBehaviour {
	
	private static SettingPageHandler _instance = null;
	public static SettingPageHandler Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(SettingPageHandler)) as SettingPageHandler; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	[SerializeField] Image BTN_SOUND;
	[Space(10)]
	[SerializeField] Sprite BTN_SOUND_ON;
	[SerializeField] Sprite BTN_SOUND_OFF;

	[Space(10)]
	[SerializeField] Text _totalCoins;

	int tempTotalCoins;
	void TotalCoinInfo(){

		if (GameManager.Instance && GameManager.Instance.totalCoins != tempTotalCoins) {
			tempTotalCoins = GameManager.Instance.totalCoins;
			_totalCoins.text = tempTotalCoins.ToString();
			PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);
		}
	}

//	void Update () {
//
//		if (GlobalVariables.isBackNavigation && Input.GetKeyUp (KeyCode.Escape)) {
//			UIHandler.Instance.RequestToEnableObject (0);
//		}
//	}
//
	void OnEnable()
	{
		if (GameManager.Instance) {
			BTN_SOUND.gameObject.GetComponent<Image>().sprite = GameManager.Instance.isMusicPause ?BTN_SOUND_OFF: BTN_SOUND_ON;
		}

		if (GameManager.Instance) {
			_totalCoins.text = GameManager.Instance.totalCoins.ToString ();
             
		}
	}




	#region OnButtonAction
	public void OnClick(string _click)
	{
		StartCoroutine (ieOnClick (_click));
	}
	
	IEnumerator	ieOnClick(string _click){
		SoundController.Instance.OnButtonClick ();
		yield return new WaitForSeconds (UIHandler.Instance.navigationTime);
		OnClickComplete (_click);
	}


	void OnClickComplete(string _click){
		switch(_click)
		{
		case "BTN_SHARE":			
			Debug.Log ("BTN_SHARE");
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToShare ();
			//}
			break;
			
		case "BTN_MORE_GAMES":
			Debug.Log ("BTN_MORE_GAMES");
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToMoreGames ();
			//}
			break;
			
		case "BTN_SOUND":
			Debug.Log ("BTN_SOUND");

                if (GameManager.Instance)
                {
                    GameManager.Instance.isSoundPause = !GameManager.Instance.isSoundPause;
                    GameManager.Instance.isMusicPause = GameManager.Instance.isSoundPause;

                }


                if (GameManager.Instance && GameManager.Instance.isMusicPause) {
				BTN_SOUND.gameObject.GetComponent<Image> ().sprite = BTN_SOUND_OFF;
			} else {
				BTN_SOUND.gameObject.GetComponent<Image> ().sprite = BTN_SOUND_ON;
			}
			if (SoundController.Instance) {
				SoundController.Instance.ChangeSoundState ();
			}


			break;

		case "TEX_COINS_PLUS":
			UIHandler.Instance.RequestToEnableObject (2);
			break;	

		case "BTN_BACK":
			UIHandler.Instance.RequestToEnableObject (0);
			break;

		}

	}
	#endregion
}
