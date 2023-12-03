using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonVisibilityForShopAndChangeText : MonoBehaviour
{
		[SerializeField] Image _NoAdsBtn;
		[SerializeField] Image _UnlockTrains;
		[SerializeField] Image _UnlockLevels;
		[SerializeField] Image _UnlockTrainsAndLevels;
		[SerializeField] Image _MiniPack;
		[SerializeField] Image _BoosterPack;
		[SerializeField] Image _SuperPack;
		[SerializeField] Image _ProPack;
		[SerializeField] Image _MegaPack;
		[SerializeField] Image _UltraPack;



		// Use this for initialization
		void Start ()
		{
				SetTextForButtons ();
				CheckButtonVisibility ();
		}

    void SetTextForButtons()
    {
        //		_NoAdsBtn.transform.GetChild (0).gameObject.GetComponent<Text> ().text	= ExternalInterfaceHandler.Instance.getStringForBuyButtons (6);
        //		_UnlockLevels.transform.GetChild (0).gameObject.GetComponent<Text> ().text	= ExternalInterfaceHandler.Instance.getStringForBuyButtons (7);
        //		_UnlockTrains.transform.GetChild (0).gameObject.GetComponent<Text> ().text	= ExternalInterfaceHandler.Instance.getStringForBuyButtons (8);
        //		_UnlockTrainsAndLevels.transform.GetChild (0).gameObject.GetComponent<Text> ().text	= ExternalInterfaceHandler.Instance.getStringForBuyButtons (9);

        //		_MiniPack.transform.GetChild (0).gameObject.GetComponent<Text> ().text	= ExternalInterfaceHandler.Instance.getStringForBuyButtons (0);
        //		_BoosterPack.transform.GetChild (0).gameObject.GetComponent<Text> ().text	= ExternalInterfaceHandler.Instance.getStringForBuyButtons (1);
        //		_SuperPack.transform.GetChild (0).gameObject.GetComponent<Text> ().text	= ExternalInterfaceHandler.Instance.getStringForBuyButtons (2);
        //		_ProPack.transform.GetChild (0).gameObject.GetComponent<Text> ().text	= ExternalInterfaceHandler.Instance.getStringForBuyButtons (3);
        //		_MegaPack.transform.GetChild (0).gameObject.GetComponent<Text> ().text	= ExternalInterfaceHandler.Instance.getStringForBuyButtons (4);
        //		_UltraPack.transform.GetChild (0).gameObject.GetComponent<Text> ().text	= ExternalInterfaceHandler.Instance.getStringForBuyButtons (5);
        //}
    }

		public void CheckButtonVisibility ()
		{
				print ("check visibility");
				if (PlayerPrefs.GetString ("NoAdsPurchase") == "Success") {
						_NoAdsBtn.enabled	= false;
						_NoAdsBtn.gameObject.SetActive (false);
				}
				if (PlayerPrefs.GetInt (GlobalVariables.sTotalTrainsUnlocked) == GlobalVariables.iTotalTrainsAvalaiable) {
						_UnlockTrains.enabled	= false;
						_UnlockTrains.gameObject.SetActive (false);
				}
				if (PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == GlobalVariables.iTotalLevels) {
						_UnlockLevels.enabled	= false;
						_UnlockLevels.gameObject.SetActive (false);
				}
				if (PlayerPrefs.GetInt (GlobalVariables.sTotalTrainsUnlocked) == GlobalVariables.iTotalTrainsAvalaiable) {
						_UnlockTrains.enabled	= false;
						
						_UnlockTrains.gameObject.SetActive (false);
						
				}

		if ( PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == GlobalVariables.iTotalLevels) {
			
			_UnlockLevels.enabled	= false;


			_UnlockLevels.gameObject.SetActive (false);

		}
	
		if (PlayerPrefs.GetInt (GlobalVariables.sTotalTrainsUnlocked) == GlobalVariables.iTotalTrainsAvalaiable && PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels) == GlobalVariables.iTotalLevels) {
			
			_UnlockTrainsAndLevels.enabled	= false;

			_UnlockTrainsAndLevels.gameObject.SetActive (false);
		}


		}
}
