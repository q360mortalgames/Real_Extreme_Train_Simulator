using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public AudioClip _ButtonClick;
	public AudioClip _ButtonClick2;
	public AudioClip _LFClip;
	public AudioClip _LCClip;

	public AudioClip TrainCrowd,TrainSlow,TrainSpeed,Trainidle;

	public AudioSource _ButtonSource;
	public AudioSource _ButtonSource2;
	public AudioSource _HornSource;
	public AudioSource _BreakSource;
	public AudioSource _BGSource;
	public AudioSource _TrainSource;
	public AudioSource _LevelFailed;
	public AudioSource _LevelCompleted;
	public AudioSource _TrainCrash;


	private static SoundController _instance = null;
	public static SoundController Instance {		
		get {			
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType (typeof(SoundController)) as SoundController; 	
			}			
			return _instance; 
		}
		set {			
			_instance = value;			
		}	
	}

	void Awake()
	{
		CheckForSoundEnable();
		PlayBG();

	}

	void Start () 
	{

	}

	void CheckForSoundEnable()
	{
		if(PlayerPrefs.GetInt(GlobalVariables.sSoundsString) == 1)
			AudioListener.volume	= 0;
		else
			AudioListener.volume	= 1;
	}
	public void ChangeSoundState()
	{
		if(PlayerPrefs.GetInt(GlobalVariables.sSoundsString) == 1)
			PlayerPrefs.SetInt(GlobalVariables.sSoundsString,0);
		else
			PlayerPrefs.SetInt(GlobalVariables.sSoundsString,1);

		CheckForSoundEnable();
	}
	public void OnButtonClick()
	{
		//Debug.Log("button sound");
		_ButtonSource.PlayOneShot(_ButtonClick);
	}
	public void OnButtonClick2()
	{
		_ButtonSource2.PlayOneShot(_ButtonClick2);
	}
	public void PlayBG()
	{
		
		_BGSource.Play();
	}

	public void StartHorn()
	{
		_HornSource.Play();
	}
	public void StopHorn()
	{
		_HornSource.Stop();
	}

	public void TrainCrash()
	{
		if(_TrainCrash)
			_TrainCrash.Play();
	}	

	public void PlayBreakSound()
	{
		_BreakSource.Play();

		GamePlayManager.goingSlow=false;
		GamePlayManager.goingSpeed=true;
		GamePlayManager.crowdSnd=true;
	}
	public void StopBreakSound()
	{
		_BreakSource.Stop();
	}

	public void playTrainidle()
	{
		_TrainSource.GetComponent<AudioSource>().clip=Trainidle;
		_TrainSource.GetComponent<AudioSource>().pitch=0.2f;
		_TrainSource.Play();
	}

	public void playTrainCrowd()
	{
		_TrainSource.GetComponent<AudioSource>().clip=TrainCrowd;
		_TrainSource.GetComponent<AudioSource>().pitch=1;

		_TrainSource.Play();
	}
	public void TrainSlowSound()
	{
		_TrainSource.GetComponent<AudioSource>().clip=TrainSlow;
		_TrainSource.GetComponent<AudioSource>().pitch=1;

		_TrainSource.Play();
	}
	public void TrainFastSound()
	{
		_TrainSource.GetComponent<AudioSource>().pitch=1;
		_TrainSource.GetComponent<AudioSource>().clip=TrainSpeed;
		_TrainSource.Play();
	}
	public void PlayLevelFailed()
	{
		_LevelFailed.PlayOneShot(_LFClip);
	}
	public void PlayLevelComplete()
	{
		_LevelCompleted.PlayOneShot(_LCClip);
	}
}
