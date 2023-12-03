using UnityEngine;
using System.Collections;

public class TriggerTextureChanges : MonoBehaviour
{
	[SerializeField] TriggerPropertiesForTrain _scr;
	[SerializeField] Material[] _textueres;
	Material _tex;

	void Start ()
	{
		if (_scr == null)
			_scr = GetComponent<TriggerPropertiesForTrain> ();

		switch (_scr.mTriggerState) {
		case eTRIGGER_STATE.SpeedLimiter:
			switch (_scr.SpeedLimit) {
			case 25:
				_tex	= _textueres [0];
				break;
			case 30:
				_tex	= _textueres [1];
				break;
			case 50:
				_tex	= _textueres [2];
				break;
			case 60:
				_tex	= _textueres [3];
				break;
			case 80:
				_tex	= _textueres [4];
				break;
			case 100:
				_tex	= _textueres [5];
				break;
			case 130:
				_tex	= _textueres [6];
				break;
			case 150:
				_tex	= _textueres [7];
				break;
			case 180:
				_tex	= _textueres [8];
				break;
			}
			GetComponent<Renderer> ().sharedMaterial = _tex;	 
			break;
		}
	}
}
