using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class levelselectionNew : MonoBehaviour {

	[SerializeField] Text _totalCoins;

	public Image[] AllLevels;
	public GameObject[] levelInfo;
	public GameObject UnlockAlllevelsBtn;
	public int[] Xpdata,stopCount,Timeval;

	public static levelselectionNew mee;
	// Use this for initialization
	void Start () {
		mee = this;

		if (!PlayerPrefs.HasKey (GlobalVariables.sTotalUnlockedLevels)){
			PlayerPrefs.SetInt (GlobalVariables.sTotalUnlockedLevels, 1);
			PlayerPrefs.SetInt ("TotalLevelsToUnlock", 1);
		}

		//		PlayerPrefs.SetInt ("TotalLevelsToUnlock", 10);
		//		PlayerPrefs.SetInt (GlobalVariables.sTotalTrainsUnlocked, GlobalVariables.iTotalTrainsAvalaiable);
	//	PlayerPrefs.DeleteAll();
		GlobalVariables.iNextLevelToUnlock	= PlayerPrefs.GetInt ("TotalLevelsToUnlock");
		PlayerPrefs.SetInt (GlobalVariables.sTotalUnlockedLevels, PlayerPrefs.GetInt ("TotalLevelsToUnlock"));
		print ("stars :" + PlayerPrefs.GetInt (GlobalVariables.sTotalStarsGained));

		if(PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels, 1)<=1){
			PlayerPrefs.SetInt (GlobalVariables.sTotalUnlockedLevels, 1);
		}
		print (GlobalVariables.iNextLevelToUnlock+ "levels unlocked : "+PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels, 1));

		CheckLevelLocks ();

		if (GameManager.Instance && GameManager.Instance.totalCoins != tempTotalCoins) {
			tempTotalCoins = GameManager.Instance.totalCoins;
			_totalCoins.text = tempTotalCoins.ToString();
			PlayerPrefs.SetInt ("totalCoins", GameManager.Instance.totalCoins);
		}
	}

	public void unlockAllLevels(){
		PlayerPrefs.SetInt (GlobalVariables.sTotalUnlockedLevels, GlobalVariables.iTotalLevels);
		PlayerPrefs.SetInt ("TotalLevelsToUnlock", GlobalVariables.iTotalLevels);
		CheckLevelLocks ();
	}
	public void CheckLevelLocks(){
		int lnum;
		UnlockAlllevelsBtn.SetActive(PlayerPrefs.GetInt(GlobalVariables.sTotalUnlockedLevels) < GlobalVariables.iTotalLevels);
		for (int i=0;i<AllLevels.Length;i++){
			if ((i + 1) <= PlayerPrefs.GetInt (GlobalVariables.sTotalUnlockedLevels, 1)) {
				//AllLevels[i].GetComponent<Button> ().interactable	= true;
				AllLevels[i].gameObject.transform.GetChild(0).gameObject.SetActive (true);
				AllLevels [i].gameObject.transform.GetChild (5).gameObject.SetActive (false);
				//print ("lockds :  "+AllLevels[i].gameObject.transform.GetChild(5).gameObject.name);
				print("level data krishna..............");

				lnum = i + 1;


				//Debug.Log ("star in level "+i+" : "+PlayerPrefs.GetInt ((GlobalVariables.sStarsGainedInEachLevel + (i))));
				//Debug.Log ("star in level-- "+lnum+" : "+PlayerPrefs.GetInt ((GlobalVariables.sStarsGainedInEachLevel + (lnum))));

				// 234 stars
				switch(PlayerPrefs.GetInt ((GlobalVariables.sStarsGainedInEachLevel + (lnum)))){

				case 1:
					AllLevels[i].gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().enabled=true;


					break;

				case 2:
					AllLevels[i].gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().enabled=true;
					AllLevels[i].gameObject.transform.GetChild(3).gameObject.GetComponent<Image>().enabled=true;

					break;
				case 3:
					AllLevels[i].gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().enabled=true;
					AllLevels[i].gameObject.transform.GetChild(3).gameObject.GetComponent<Image>().enabled=true;
					AllLevels[i].gameObject.transform.GetChild(4).gameObject.GetComponent<Image>().enabled=true;

					break;

				}

				Color colorToSet = AllLevels [i].gameObject.GetComponent<Image> ().color;
				colorToSet.a = 1;
				AllLevels [i].gameObject.GetComponent<Image> ().color = colorToSet;

			} else {
				//lock state
				//AllLevels[i].GetComponent<Button> ().interactable	= false;
				AllLevels[i].gameObject.transform.GetChild(0).gameObject.SetActive (false);
				AllLevels [i].gameObject.transform.GetChild (5).gameObject.SetActive (true);


				AllLevels [i].gameObject.GetComponent<Image> ().color = AllLevels [i].gameObject.transform.parent.gameObject.GetComponent<Image> ().color;
			}


			//Debug.Log ("star in level "+i+" : "+PlayerPrefs.GetInt ((GlobalVariables.sStarsGainedInEachLevel + (i))));

		}

	}

	int tempTotalCoins;

	[HideInInspector] public int mi_XP1, mi_XP2, mi_XP3;

	public static int OpenLevelNUm;
	IEnumerator RequestToLoadNextScene (float lTime)
	{
		yield return new WaitForSeconds (lTime);
		Application.LoadLevel ("TrainSelectionPage");
	}
	public void OnClickComplete (GameObject _click)
	{

		if(_click.name=="play"){

			Debug.Log ("BTN_LevelsStart");

			GlobalVariables.loadScene = "Game_levels";
			GlobalVariables.isMultiPlayerMode = false;
			levelManager.LevelMode = true;

			StartCoroutine (RequestToLoadNextScene (0.3f));

			return;
		}

	
		if(_click.name=="TEX_COINS_PLUS"){
			GlobalVariables.CoinsClicked = true;
			UIHandler.Instance.RequestToEnableObject (2);

		}

		if(_click.name=="BTN_BACK"){
			UIHandler.Instance.RequestToEnableObject (0);
			return;
		}

		if(_click.name =="BTN_Gift"){
			Debug.Log("BTN_GIFT");
			UIHandler.Instance.RequestToEnableObject(6);
			FreeCoinsPage.atPage = 5;
			return;
		}
		if (_click.name == "UnlockAllLevels") {
			//if (AdsManager.Instance) {
			//	AdsManager.Instance.RequestToInApp (7);
			//}
			#if UNITY_EDITOR
			unlockAllLevels();
			#endif
			return;
		}


		if(_click.name == "CloseBtn"){

			print ("CloseBtn....... krishna");
			levelInfo [0].SetActive (false);
		}

		if (_click.name.Contains("level"))
		{
			print(_click.name);
			int levelNum = int.Parse(_click.name.Substring(5, _click.name.Length - 5)) - 1;
			print(levelNum + " : " + _click.name.Substring(5, _click.name.Length - 5));

			for (int i = 0; i < AllLevels.Length; i++)
			{
				//print (i+"--");
				AllLevels[i].transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

			}

			AllLevels[levelNum].transform.localScale = new Vector3(1, 1, 1);

			if (!_click.gameObject.transform.GetChild(5).gameObject.activeSelf)
			{


				mi_XP1 = Mathf.CeilToInt(Xpdata[levelNum] * 0.5f);
				mi_XP2 = Mathf.CeilToInt(Xpdata[levelNum] * 0.85f);
				mi_XP3 = Xpdata[levelNum];

				print("level Info krishna... level number...................");
				levelInfo[0].SetActive(true);

				levelInfo[0].gameObject.transform.GetChild(2).gameObject.GetComponent<Text>().text = "" + mi_XP1;
				levelInfo[0].gameObject.transform.GetChild(3).gameObject.GetComponent<Text>().text = "" + mi_XP2;
				levelInfo[0].gameObject.transform.GetChild(4).gameObject.GetComponent<Text>().text = "" + mi_XP3;
				levelInfo[0].gameObject.transform.GetChild(5).gameObject.GetComponent<Text>().text = "" + stopCount[levelNum];
				levelInfo[0].gameObject.transform.GetChild(7).gameObject.GetComponent<Text>().text = "LEVEL  " + (levelNum + 1);


				int timerVal = Timeval[levelNum];
				int min = Mathf.FloorToInt(timerVal / 60f);
				int sec = timerVal % 60;

				GlobalVariables.iCurrentLevel = levelNum + 1;
				OpenLevelNUm = levelNum;
				levelInfo[0].gameObject.transform.GetChild(6).gameObject.GetComponent<Text>().text = "" + min.ToString("00") + ":" + sec.ToString("00");
				//iTween.MoveTo (scrollRect.gameObject,iTween.Hash("y",levelInfo [2].transform.position.y, "time", 0.2f, "easetype", iTween.EaseType.linear));

			}
			else
			{

				print("els condition ...................");

				levelInfo[0].SetActive(false);
				//			levelInfo [0].SetActive (true);
				//iTween.MoveTo (scrollRect.gameObject,iTween.Hash("y",levelInfo [1].transform.position.y, "time", 0.2f, "easetype", iTween.EaseType.linear));
				//scrollRect.transform.position = levelInfo [1].transform.position; 
			}
		}
	}
	public void OpenDetails(){


	}
	// Update is called once per frame
	void Update () {
	
	}
}
