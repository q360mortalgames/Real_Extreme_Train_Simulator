using UnityEngine;
  
public class ClickEventManager : MonoBehaviour 
{
	 
	public static void CheckGUIClick(string clickedGUI)
	{

		if (clickedGUI == null)
				return;
	}

	public static void CheckGUIClick(string clickedGUI,GameObject _obj)
	{

		//Debug.Log(clickedGUI+" current  "+GlobalVariables.mCurrentScene);
		if (clickedGUI == null)
			return;



//		if(clickedGUI == "Buy1" || clickedGUI == "Buy2" || clickedGUI == "Buy3" || clickedGUI == "Buy4" || clickedGUI == "Buy5" )
//		{
//			OnShopButtonClick(clickedGUI);
//			return;
//		}	

		if(clickedGUI.Contains("MoreGames"))
		{
			MoreGamesClick();
			return;
		}

		if(GlobalVariables.mCurrentScene == eCURRENT_SCENE.GamePlay)
		{
			
			GamePlayManager.Instance.OnButtonClick(clickedGUI);

		}
		else if(GlobalVariables.mCurrentScene ==eCURRENT_SCENE.Menu)
		{
			//MenuPageHandler.Instance.OnButtonClick(clickedGUI);
		}
		else if(GlobalVariables.mCurrentScene == eCURRENT_SCENE.TrainSelection)
		{
			//TrainSelectionHandler.Instance.OnButtonClick(clickedGUI);
		}
		else if(GlobalVariables.mCurrentScene == eCURRENT_SCENE.LevelSelection)
		{
			LevelSelectionHandler.Instance.OnButtonClick(clickedGUI);
		}
	}
	static void OnShopButtonClick(string _shop)
	{
//		int avaliableCoin	= PlayerPrefs.GetInt(GlobalVariables.sCoinsAvaliable);
//
//		switch(_shop)
//		{
//		case "Buy1":
//			avaliableCoin	+= GlobalVariables.iBuy1Cost;
//			break;
//		case "Buy2":
//			avaliableCoin	+= GlobalVariables.iBuy2Cost;
//			break;
//		case "Buy3":
//			avaliableCoin	+= GlobalVariables.iBuy3Cost;
//			break;
//		case "Buy4":
//			avaliableCoin	+= GlobalVariables.iBuy4Cost;
//			break;
//		case "Buy5":
//			avaliableCoin	+= GlobalVariables.iBuy5Cost;
//			break;
//		}
//
//		PlayerPrefs.SetInt(GlobalVariables.sCoinsAvaliable,avaliableCoin);
//		if(GlobalVariables.mGameScene == eCURRENTSCENE.GamePlay)
//		{
//			GamePlayManager.Instance.setCoinsData();
//		}
	}
	static void MoreGamesClick()
	{

	}

}

