using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour 
{
	public	bool walk,idle,run,sit,drive,changeState;
	Animator _self;
	public static CharacterAnimator _characterAnim;
	void OnEnable()
	{
		if (_characterAnim == null)
			_characterAnim = this;
	}

	void OnDisable()
	{
		if (_characterAnim != null)
			_characterAnim = null;
	}

	void Update()
	{
		if (changeState) 
		{
			ChangeState ();

		}
	}

 	void Start () 
	{
		_self = GetComponent<Animator> ();
 	}
 	public void ChangeState()
	{
		if(walk)
		{
			idle = false;
			run = false;
			sit = false;
			drive = false;
 		}
		if (run) 
		{
			walk = false;
			idle = false;
 			sit = false;
			drive = false;
 		}
		if (sit) 
		{
			walk = false;
			idle = false;
			run = false;
 			drive = false;
 		}
		if (drive) 
		{
			walk = false;
			idle = false;
			run = false;
			sit = false;
  		}
		if (idle) 
		{
			walk = false;
			run = false;
			sit = false;
			drive = false;
		}

		_self.SetBool("Idle",idle);
		_self.SetBool("Walk",walk);
		_self.SetBool("Run",run);
		_self.SetBool("Sit",sit);
		_self.SetBool("Drive",drive);
		changeState = false;
		idle = false;
		run = false;
		sit = false;
		drive = false;
		walk = false;
	}
}
