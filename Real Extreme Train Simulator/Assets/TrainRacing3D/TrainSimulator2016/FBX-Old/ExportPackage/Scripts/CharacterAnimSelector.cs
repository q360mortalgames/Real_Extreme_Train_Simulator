using UnityEngine;
using System.Collections;
using System.Security.AccessControl;

public class CharacterAnimSelector : MonoBehaviour
{
		[SerializeField] bool isCharacterMoveOnStation;
		[SerializeField] bool isCharaterAnimationEnable;
		public bool run, walk, dead, idle, dancing, sitting, crouch, climbingRope, shoot, walkWithGun, psycoRun, slowRun, sitOnRoad, yelling, talkingOnPhone, girlTalking, tellingSecret, hanging, NormalWalk;
		bool trun, twalk, tdead, tidle, tdancing, tsitting, tcrouch, tclimbingRope, tshoot, twalkWithGun, tpsycoRun, tslowRun, tsitOnRoad, tyelling, ttalkingOnPhone, tgirlTalking, ttellingSecret, thanging, tNormalWalk;

		[HideInInspector]
		public bool _changeState;
		[HideInInspector]
		public Animator _animator;


		void Start ()
		{
				_animator = this.GetComponent<Animator> ();
                
		}

		void OnEnable()
		{
		
		if (isCharacterMoveOnStation || isCharaterAnimationEnable || GameManager.Instance.isCharaterAnimationEnable) {
				SetAllStateFinalCondition ();
			} else {
				SetAllStateFalse ();
				SetAllStateInitialCondition ();
			}
			_changeState = true;
		}
	 
		void Update ()
		{
				if (_changeState) {
						ChangeState ();
						_changeState = false;
                        
				}
		}

	   

		void ChangeState ()
		{
				_animator.SetBool ("WithGun", walkWithGun);
				_animator.SetBool ("ClimbingRope", climbingRope);
				_animator.SetBool ("Dancing", dancing);
				_animator.SetBool ("Sitting", sitting);
				_animator.SetBool ("PsycoRun", psycoRun);
				_animator.SetBool ("TalkingOnPhone", talkingOnPhone);
				_animator.SetBool ("GirlTalking", girlTalking);
				_animator.SetBool ("TellingSecret", tellingSecret);
				_animator.SetBool ("SittingOnRoad", sitOnRoad);
				_animator.SetBool ("Yelling", yelling);
				_animator.SetBool ("Hanging", hanging);
				_animator.SetBool ("Run", run);
				_animator.SetBool ("Walk", walk);
				_animator.SetBool ("Dead", dead);
				_animator.SetBool ("Idle", idle);
				_animator.SetBool ("Crouch", crouch);
				_animator.SetBool ("Shoot", shoot);
				_animator.SetBool ("SlowRun", slowRun);
				_animator.SetBool ("NormalWalk", NormalWalk);
		}

		public void SetAllStateFalse ()
		{
				run	= false;
				walk	= false;
				dead	= false;
				idle	= false;
				dancing	= false;
				sitting	= false;
				crouch	= false;
				walkWithGun	= false;
				climbingRope	= false;
				psycoRun	= false;
				talkingOnPhone	= false;
				girlTalking	= false;
				tellingSecret	= false;
				sitOnRoad	= false;
				yelling	= false;
				hanging	= false;
				shoot	= false;
				slowRun	= false;
				NormalWalk	= false;
                

		}

	void SetAllStateInitialCondition ()
	{
		run	= trun;
		walk	= twalk;
		dead	= tdead;
		idle	= tidle;
		dancing	= tdancing;
		sitting	= tsitting;
		crouch	= tcrouch;
		walkWithGun	= twalkWithGun;
		climbingRope	= tclimbingRope;
		psycoRun	= tpsycoRun;
		talkingOnPhone	= ttalkingOnPhone;
		girlTalking	= tgirlTalking;
		tellingSecret	= ttellingSecret;
		sitOnRoad	= tsitOnRoad;
		yelling	= tyelling;
		hanging	= thanging;
		shoot	= tshoot;
		slowRun	= tslowRun;
		NormalWalk	= tNormalWalk;

	}

	public void SetAllStateFinalCondition ()
	{
		trun	= run;
		twalk	= walk;
		tdead	= dead;
		tidle	= idle;
		tdancing	= dancing;
		tsitting	= sitting;
		tcrouch	= crouch;
		twalkWithGun	= walkWithGun;
		tclimbingRope	= climbingRope;
		tpsycoRun	= psycoRun;
		ttalkingOnPhone	= talkingOnPhone;
		tgirlTalking	= girlTalking;
		ttellingSecret	= tellingSecret;
		tsitOnRoad	= sitOnRoad;
		tyelling	= yelling;
		thanging	= hanging;
		tshoot	= shoot;
		tslowRun	= slowRun;
		tNormalWalk	= NormalWalk;
	}

}
