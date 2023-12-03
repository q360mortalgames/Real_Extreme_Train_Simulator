using UnityEngine;
using System.Collections;

public class TrackChangeHandler : MonoBehaviour
{
	[SerializeField] Transform _CurrentTrack;
	[SerializeField] Transform _NewTrack;

	void Start ()
	{
		OnResetChangeTrack ();
	}

	public void OnResetChangeTrack()
	{
		if (_CurrentTrack == null || _NewTrack == null)
			return;

		Collider _collider;

		for (int i = 0; i < _CurrentTrack.childCount; i++) {
			_collider = _NewTrack.GetChild (i).GetComponent<Collider> ();
			if (_collider)
				_collider.enabled	= false;
			_collider = _CurrentTrack.GetChild (i).GetComponent<Collider> ();
			if (_collider)
				_collider.enabled	= true;
		}

		for (int i = 0; i < _NewTrack.childCount; i++) {
			_collider = _NewTrack.GetChild (i).GetComponent<Collider> ();
			if (_collider)
				_collider.enabled	= false;

		}
	}

	public void OnChangeTrackClick ()
	{	
		if (_CurrentTrack == null || _NewTrack == null)
			return;
		Collider _collider;
		//print (this.gameObject.name+" : OnChangeTrackClick");
		GamePlayManager.Instance.PathChanger_obj = this.gameObject;


		for (int i = 0; i < _CurrentTrack.childCount; i++) {
			_collider = _CurrentTrack.GetChild (i).GetComponent<Collider> ();
			if (_collider)
				_collider.enabled	= false;

		}
		for (int i = 0; i < _NewTrack.childCount; i++) {
			_collider = _NewTrack.GetChild (i).GetComponent<Collider> ();
			if (_collider)
				_collider.enabled	= true;
		}

	}
}
