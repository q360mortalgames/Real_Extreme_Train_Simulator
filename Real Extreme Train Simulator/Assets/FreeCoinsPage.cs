using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FreeCoinsPage : MonoBehaviour
{
	public Image FB_Btn, Subscibe_Btn, Follow_Btn, DownLoad_Btn;
	public static int FreeCoins = 500;
	private Color cc;
	// Use this for initialization
	void Start ()
	{

        print(PlayerPrefs.GetString (GameEnums.FB_Like));

        if (PlayerPrefs.HasKey(GameEnums.DownloadGame) == false)
        {
           // PlayerPrefs.SetString(GameEnums.DownloadGame, "Newgame");
		}

//        if (PlayerPrefs.GetString(GameEnums.DownloadGame) != gameConfigs.MoreAdClickLink)
//        {
//			cc = DownLoad_Btn.color;
//			cc.a = 1;
////			iTween.FadeTo (DownLoad_Btn, iTween.Hash ("alpha", 1, "time", 0));
//			DownLoad_Btn.color = cc;
//		}
	}

	public void Fb_Like ()
	{
        if (PlayerPrefs.HasKey(GameEnums.FB_Like) == false)
        {
            PlayerPrefs.SetString(GameEnums.FB_Like, "Done");
			Application.OpenURL ("https://www.facebook.com/timuzsolutions");
			//StoreManager.AddCash (FreeCoins);
            print("yes");
			GameManager.Instance.totalCoins = GameManager.Instance.totalCoins + CoinsToAdd;
			
			cc = FB_Btn.color;
			cc.a = 0.25f;
			FB_Btn.color = cc;

//			iTween.FadeTo (FB_Btn.gameObject, iTween.Hash ("alpha", 0, "time", 0.3));

		} else {
			return;
		}
	}

	public void Subscribe ()
	{
        if (PlayerPrefs.HasKey(GameEnums.Subscribe) == false)
        {
			Application.OpenURL ("https://www.youtube.com/c/Timuz");
			//StoreManager.AddCash (FreeCoins);
			GameManager.Instance.totalCoins = GameManager.Instance.totalCoins + CoinsToAdd;
            PlayerPrefs.SetString(GameEnums.Subscribe, "Done");
//			iTween.FadeTo (Subscibe_Btn, iTween.Hash ("alpha", 0, "time", 0.3));
			cc = Subscibe_Btn.color;
			cc.a = 0.25f;
			Subscibe_Btn.color = cc;
		} else {
			return;
		}
	}

	public void Follow ()
	{
        if (PlayerPrefs.HasKey(GameEnums.Follow) == false)
        {
			Application.OpenURL ("https://www.instagram.com/timuzgamesofficial/");
			//StoreManager.AddCash (FreeCoins);
			GameManager.Instance.totalCoins = GameManager.Instance.totalCoins + CoinsToAdd;
            PlayerPrefs.SetString(GameEnums.Follow, "Done");
//			iTween.FadeTo (Follow_Btn.gameObject, iTween.Hash ("alpha", 0, "time", 0.3));
			cc = Follow_Btn.color;
			cc.a = 0.25f;
			Follow_Btn.color = cc;

		} else {
			return;
		}
	}

	private int CoinsToAdd = 2000;
	public void DownLoadFreeGame ()
	{
//        if (PlayerPrefs.GetString(GameEnums.DownloadGame) != gameConfigs.MoreAdClickLink)
//        {
//			//Application.OpenURL (gameConfigs.MoreAdClickLink);
//			//StoreManager.AddCash (FreeCoins);
//			GameManager.Instance.totalCoins = GameManager.Instance.totalCoins + CoinsToAdd;
//           // PlayerPrefs.SetString(GameEnums.DownloadGame, gameConfigs.MoreAdClickLink);
////			iTween.FadeTo (DownLoad_Btn, iTween.Hash ("alpha", 0.3, "time", 0.3));
//			cc = DownLoad_Btn.color;
//			cc.a = 0.25f;
//			DownLoad_Btn.color = cc;
//		} else {
//			return;
//		}
	}

	public static int atPage =0;
	public void HidePage ()
	{
		//gameObject.SetActive (false);
		UIHandler.Instance.RequestToEnableObject(atPage);
	}
}
