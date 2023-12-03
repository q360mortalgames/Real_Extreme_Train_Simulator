using UnityEngine;
using System.Collections;

public class ThemesPathHandler : MonoBehaviour
{

	private static ThemesPathHandler _instance = null;

	public static ThemesPathHandler Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(ThemesPathHandler)) as ThemesPathHandler; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	public ThemesInfo[] _themesInfo;

	public void RequestToSetTheme (int tIndex)
	{		
		GameManager.Instance.themesIndex = tIndex;

		foreach (ThemesInfo themeInfo in _themesInfo) {
			themeInfo.theme.SetActive (false);
		}

		_themesInfo [tIndex].theme.SetActive (true);
		RenderSettings.skybox = _themesInfo [tIndex].skyBox;
		PointerHandler.Instance.finalPosition = _themesInfo [tIndex].finalPosition;	

	//	TrainMultiPlayerManager.Instance.RequestToStartRace ();
	}
}
