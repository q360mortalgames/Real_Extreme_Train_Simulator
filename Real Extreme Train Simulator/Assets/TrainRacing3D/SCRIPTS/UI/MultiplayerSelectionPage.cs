using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MultiplayerSelectionPage : MonoBehaviour
{
    private static MultiplayerSelectionPage _instance = null;

    public Text FreeCashText;

    public static MultiplayerSelectionPage Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType(typeof(MultiplayerSelectionPage)) as MultiplayerSelectionPage;
            }
            return _instance;
        }
        set {
            _instance = value;
        }
    }

    [Space(10)]
    [SerializeField] Text _totalCoins;

    int tempTotalCoins;

    void TotalCoinInfo()
    {

        if (GameManager.Instance && GameManager.Instance.totalCoins != tempTotalCoins) {
            tempTotalCoins = GameManager.Instance.totalCoins;
            _totalCoins.text = tempTotalCoins.ToString();
            PlayerPrefs.SetInt("totalCoins", GameManager.Instance.totalCoins);
        }
    }

    public void SetFreeCashProperties()
    {
        //if (GameConfigs2015.FreeCashGames.Count < PlayerPrefs.GetInt ("CurrentFreeCashGame", 0)) {
        //	FreeCashText.transform.parent.gameObject.SetActive (false);
        //}

        if (PlayerPrefs.HasKey("IsGameInstalling")) {

            FreeCashText.text = "claim";

        } else {

            FreeCashText.text = "Free Cash";
        }

        if (PlayerPrefs.HasKey("FreeCashAddedTime") && System.DateTime.Now.Subtract(System.DateTime.Parse(PlayerPrefs.GetString("FreeCashAddedTime"))).Hours < 24) {
            FreeCashText.transform.parent.gameObject.SetActive(false);
        } else {
            FreeCashText.transform.parent.gameObject.SetActive(true);
        }
    }



    public void GetFreeCash()
    {
        int coinval;
        if (PlayerPrefs.HasKey("IsGameInstalling")) {

            Debug.Log("FreeCash b: " + PlayerPrefs.GetString("IsGameInstalling"));

            //if (gameConfigs.isGameInstalled (PlayerPrefs.GetString ("IsGameInstalling").Split (new char[]{ '&' }) [0])) {


            //	coinval = GameConfigs2015.FreeCashCoins [PlayerPrefs.GetInt ("CurrentFreeCashGame", 0)];
            //	#if !UNITY_EDITOR
            //	gameConfigs.mee.jarToast (coinval+" coins added successfully");
            //	#endif
          //  GameManager.Instance.totalCoins += coinval;
            PlayerPrefs.SetInt("totalCoins", GameManager.Instance.totalCoins);

            //	GameManager.Instance.totalCoins = GameManager.Instance.totalCoins + _amount;
            //	PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);


            int index = PlayerPrefs.GetInt("CurrentFreeCashGame", 0);
            index++;
            PlayerPrefs.SetInt("CurrentFreeCashGame", index);
            PlayerPrefs.DeleteKey("IsGameInstalling");
            PlayerPrefs.SetString("FreeCashAddedTime", System.DateTime.Now.ToString());


            //TotalCoinInfo ();
            //
            FreeCashText.transform.parent.gameObject.SetActive(false);

            tempTotalCoins = PlayerPrefs.GetInt("totalCoins");
            _totalCoins.text = tempTotalCoins.ToString();
        }
        
            //GameConfigs2015.IsFreeCashPopupOpened = true;

            //Debug.Log("Game not installed,Please try again "+GameConfigs2015.getLinkToUrl (PlayerPrefs.GetString ("IsGameInstalling")));
            //#if !UNITY_EDITOR
            //	gameConfigs.mee.showCustomPopup ("Game not installed,Please try again ", "Yes", "No", "Claim");
            //gameConfigs.mee.showCustomPopup ("Game not installed,Please try again", "Yes",noo[languageSetup.CurrentlangIndex], claimmm[languageSetup.CurrentlangIndex]);
            //Debug.Log (gameNtinstalled[languageSetup.CurrentlangIndex]+"" + GameConfigs2015.getLinkToUrl (PlayerPrefs.GetString ("IsGameInstalling")));
            //#endif


        }
    
 //       else {

	//		Debug.Log ("FreeCash a ");
	//		GameConfigs2015.IsFreeCashPopupOpened = true;

	//		string coinss = GameConfigs2015.FreeCashCoins [PlayerPrefs.GetInt ("CurrentFreeCashGame", 0)].ToString ();
	//		//string DownloadGameText = "Download  & get Game";
	//		string DownloadGameText = "Download & get *** coins free";



	//		gameConfigs.mee.showCustomPopup (DownloadGameText.Replace ("***", coinss), "Yes", "No", "Download Game");
	//		//gameConfigs.mee.showCustomPopup (DownloadGameText.Replace ("***", coinss), yess[languageSetup.CurrentlangIndex],noo[languageSetup.CurrentlangIndex], dnlgame[languageSetup.CurrentlangIndex]);

	//	}
	//}


	void OnEnable ()
	{
		if (GameManager.Instance) {
			_totalCoins.text = GameManager.Instance.totalCoins.ToString ();
		}

	//	GameConfigs2015.DownloadFreeCashGame += SetFreeCashProperties;
		SetFreeCashProperties ();
	}

	void OnDisable ()
	{
		//GameConfigs2015.DownloadFreeCashGame -= SetFreeCashProperties;
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

	IEnumerator RequestToLoadNextScene (float lTime)
	{
		yield return new WaitForSeconds (lTime);
		Application.LoadLevel ("TrainSelectionPage");
	}

	void OnClickComplete (string _click)
	{
		switch (_click) {	

		case "BTN_BACK":
			UIHandler.Instance.RequestToEnableObject (3);
			break;


		case "freecash":
			GetFreeCash ();

			break;

		case "TEX_COINS_PLUS":
			UIHandler.Instance.RequestToEnableObject (2);
			break;

		case "BTN_500":
			Debug.Log ("BTN_500");	
			#if CUSTOM_CLIENT
			GameManager.Instance.RequestTopayEntryFee (2500, 10, 0);

			#else
			GameManager.Instance.RequestTopayEntryFee (2500,201,0);
			#endif
			break;

		case "BTN_750":
			Debug.Log ("BTN_750");	
			#if CUSTOM_CLIENT
			GameManager.Instance.RequestTopayEntryFee (5000, 20, 1);
			#else
			GameManager.Instance.RequestTopayEntryFee (5000, 202, 1);
			#endif
			break;

		case "BTN_1000":
			Debug.Log ("BTN_1000");	
			#if CUSTOM_CLIENT
			GameManager.Instance.RequestTopayEntryFee (10000, 30, 2);
			#else
			GameManager.Instance.RequestTopayEntryFee (10000, 203, 2);
			#endif
			break;
		}
		#endregion
	}
}