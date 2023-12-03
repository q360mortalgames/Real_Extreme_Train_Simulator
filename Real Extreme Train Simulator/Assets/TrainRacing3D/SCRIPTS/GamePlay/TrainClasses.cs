using System;
using UnityEngine;
using System.Collections;

#region TrackInfo
[Serializable]
public class TrackInfo
{
	public TrackChangeHandler trackChanger;
	public GameObject startCollider;
}
#endregion

#region RoadCrossingInfo
[Serializable]
public class RoadCrossingInfo
{
	public GameObject hornCollider;
	public OnTriggerFunctionalities onTrigger;
}
#endregion

#region ThemesInfo
[Serializable]
public class ThemesInfo
{
	public GameObject theme;
	public Transform finalPosition;
	public Material skyBox;
}
#endregion

#region CrowdCrossingInfo
[Serializable]
public class CrowdCrossingInfo
{
	public GameObject hornCollider;
}
#endregion

#region StationInfo
[Serializable]
public class StationInfo
{
	public Collider _StationTrigger;
	public GameObject _StationStopPoint;
	public GameObject _StationRoadBlocker;

}
#endregion

#region CharacterInfo
[Serializable]
public class CharacterInfo
{
	public GameObject _Character;
	[HideInInspector]
	public Vector3 _InitialPosition;
}
#endregion

#region UIObject
[Serializable]
public class UIObject
{
	public string objectName;
	public int ID;
	public GameObject _object;
}
#endregion

#region EnvironmentInfo
[Serializable]
public class EnvironmentInfo
{
	public GameObject environmentObject;
	public GameObject particleEffect;
	public Material skyBox;

}
#endregion

