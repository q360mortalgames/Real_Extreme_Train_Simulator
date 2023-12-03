using UnityEngine;
using System.Collections;

public class LevelDetailsInLevelSelection : MonoBehaviour
{

		public int mi_StopsCount;
		public int mi_TimerCountinSec;
		[SerializeField] int mi_XPTotal;
		[HideInInspector] public int mi_XP1, mi_XP2, mi_XP3;

		void Awake ()
		{
				mi_XP1	= Mathf.CeilToInt (mi_XPTotal * 0.5f);
				mi_XP2	= Mathf.CeilToInt (mi_XPTotal * 0.85f);
				mi_XP3	= mi_XPTotal;
		}
}
