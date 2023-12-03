using UnityEngine;
using System.Collections;

public class TrainSignalController : MonoBehaviour
{
	public GameObject mg_signalIndicator;

	public e_TRAIN_SIGNAL mCurrentSignal = e_TRAIN_SIGNAL.Red;

	float mf_Timer	= 0;
	public float mf_TargetTime = 100f;
	// in sec
	public bool mb_IsEnableUpdate;

	public GameObject[] SignalEffect;

	[SerializeField] Material _Materials;

	float f_AngelRed	= 0;
	float f_AngleGreen	= 90f;
	float f_AngleYellow	= 45f;

	[SerializeField] Collider _trainAlertExitCollider;
	// Use this for initialization
	void Start ()
	{

		SignalEffect [0].SetActive (false);
		SignalEffect [1].SetActive (false);

		if (mCurrentSignal == e_TRAIN_SIGNAL.Red)
			mg_signalIndicator.transform.localEulerAngles	= new Vector3 (f_AngelRed, 0, 0);
		else if (mCurrentSignal == e_TRAIN_SIGNAL.Yellow)
			mg_signalIndicator.transform.localEulerAngles	= new Vector3 (f_AngleYellow, 0, 0);
		else if (mCurrentSignal == e_TRAIN_SIGNAL.Green)
			mg_signalIndicator.transform.localEulerAngles	= new Vector3 (f_AngleGreen, 0, 0);	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!mb_IsEnableUpdate)
			return;

		mf_Timer	+= Time.deltaTime;
		if (mf_Timer > mf_TargetTime) {
			mCurrentSignal	= e_TRAIN_SIGNAL.Green;
			mb_IsEnableUpdate	= false;
			mf_Timer	= 0;
			SetSignaltoGreen ();
//			this.enabled	= false;
		}
	}

	void SetSignaltoGreen()
	{
		if (_trainAlertExitCollider)
			_trainAlertExitCollider.enabled	= false;
		iTween.RotateTo (mg_signalIndicator, iTween.Hash ("rotation", new Vector3 (90f, 0, 0), "time", 0.5f, "easetype", iTween.EaseType.linear, "islocal", true));

		SignalEffect [0].SetActive (false);
		SignalEffect [1].SetActive (true);

		Invoke ("disableEffect", 5);
	}

	public void SetSignaltoRed ()
	{
		iTween.RotateTo (mg_signalIndicator, iTween.Hash ("rotation", new Vector3 (0, 0, 0), "time", 0.5f, "easetype", iTween.EaseType.linear, "islocal", true));
		SignalEffect [0].SetActive (true);
		SignalEffect [1].SetActive (false);
		mb_IsEnableUpdate = true; 

	}

	void disableEffect ()
	{
		SignalEffect [1].SetActive (false);
	}
}
