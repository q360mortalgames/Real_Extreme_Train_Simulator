using UnityEngine;
using System.Collections;

public class TrainSignalHandler : MonoBehaviour
{
	[SerializeField] e_TRAIN_SIGNAL mCurrentSignal = e_TRAIN_SIGNAL.Red;

	[SerializeField] GameObject[] SignalEffect;
	[Space(10)]
	[SerializeField] GameObject mg_signalRed;
	[SerializeField] GameObject mg_signalGreen;
	[Space(10)]
	[SerializeField] Collider _trainAlertEntryCollider;
	[SerializeField] Collider _trainAlertExitCollider;
	[SerializeField] Collider AISignalCollider;
	[Space(10)]
	public float multiplayerSignalTime;

	void Start ()
	{
		SignalEffect [0].SetActive (false);
		SignalEffect [1].SetActive (false);

		if (mCurrentSignal == e_TRAIN_SIGNAL.Red) {

			mg_signalRed.SetActive (true);
			mg_signalGreen.SetActive (false);
		}
	}

	public void SetSignalToGreen()
	{
		if (_trainAlertExitCollider)
			_trainAlertExitCollider.enabled	= false;

		mCurrentSignal	= e_TRAIN_SIGNAL.Green;

		SignalEffect [0].SetActive (false);
		SignalEffect [1].SetActive (true);


		if(mg_signalRed.transform.childCount>=1){
			iTween.RotateTo (mg_signalRed.transform.GetChild(0).gameObject,iTween.Hash("z",180,"time",0.5f,"islocal",true));
			mg_signalRed.transform.GetChild (0).transform.parent = mg_signalRed.transform.GetChild (0).transform.parent.parent;


		}

		//mg_signalRed.SetActive (false);
		mg_signalGreen.SetActive (true);

		StartCoroutine( ieRequestToSignalEffectOff (5.0f));


	}

	IEnumerator ieRequestToSignalEffectOff(float offTime){
	
		yield return new WaitForSeconds (offTime);
		SignalEffect [0].SetActive (false);
		SignalEffect [1].SetActive (false);
	}

	public void SetSignalToRed ()
	{
		mCurrentSignal	= e_TRAIN_SIGNAL.Red;

		SignalEffect [0].SetActive (true);
		SignalEffect [1].SetActive (false);

		mg_signalRed.SetActive (true);
		mg_signalGreen.SetActive (false);

	}

	public void OnTrainPassedToGreenSignal()
	{
		mCurrentSignal	= e_TRAIN_SIGNAL.Red;

		SignalEffect [0].SetActive (true);
		SignalEffect [1].SetActive (false);

		mg_signalRed.SetActive (true);
		mg_signalGreen.SetActive (false);
	}

	public void _AITrainSignalReset ()
	{
		mCurrentSignal	= e_TRAIN_SIGNAL.Red;

		SignalEffect [0].SetActive (false);
		SignalEffect [1].SetActive (false);

		mg_signalRed.SetActive (false);
		mg_signalGreen.SetActive (false);

		if (_trainAlertExitCollider)
			_trainAlertExitCollider.enabled	= true;

		if (_trainAlertEntryCollider)
			_trainAlertEntryCollider.enabled = true;

		if (AISignalCollider)
			AISignalCollider.enabled = true;
		
		transform.GetComponent<BoxCollider> ().enabled = true;

	}
}
