using UnityEngine;
using System.Collections;

public class ThemesManager : MonoBehaviour {

	private static ThemesManager _instance = null;
	public static ThemesManager Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(ThemesManager)) as ThemesManager; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	public TrackInfo[] trackChangePlayer1;
	public TrackInfo[] trackChangePlayer2;

	void Start () {
		
	}
}
