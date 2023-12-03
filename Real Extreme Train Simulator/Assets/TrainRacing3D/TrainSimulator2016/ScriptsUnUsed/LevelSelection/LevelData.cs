using UnityEngine;
using System.Collections;

public class LevelData : MonoBehaviour 
{


	private static LevelData _instance = null;
	public static LevelData Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(LevelData)) as LevelData; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	public int[] mf_TimeInSec;

	public int mi_Level;
	public int mi_NoOfStops;

	public int _iMaximumRewards;
	public int _iMaximumXPToGainForRewards;

	[HideInInspector] public int mi_CoinReward1,mi_CoinReward2,mi_CoinReward3;
	[HideInInspector] public int mi_XPRequired1,mi_XPRequired2,mi_XPRequired3;

	void Awake()
	{
		CalculateRewardsForLevel();
	}
	void Start()
	{
		
	}
	void CalculateRewardsForLevel()
	{
		mi_CoinReward1	= Mathf.CeilToInt(_iMaximumRewards*0.5f);
		mi_CoinReward2	= Mathf.CeilToInt(_iMaximumRewards*0.85f);
		mi_CoinReward3	= _iMaximumRewards;

		mi_XPRequired1	= Mathf.CeilToInt(_iMaximumXPToGainForRewards*0.5f);
		mi_XPRequired2	= Mathf.CeilToInt(_iMaximumXPToGainForRewards*0.85f);
		mi_XPRequired3	= _iMaximumXPToGainForRewards;
	}
}
