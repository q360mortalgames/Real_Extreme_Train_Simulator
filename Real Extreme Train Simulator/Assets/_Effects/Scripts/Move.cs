using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

/*	// Use this for initialization
	public bool start  ;
	private float speed,Vehiclespeed;

	public static bool isfrontcheck,isbackcheck,iscrashed,changecamera;
	private string frontobj,backobj,crashobj=""; 
	public static int finishcount;



	void Start () {
		finishcount=0;

		frontcheck.Frontname="";
		Backcheck.Backname="";
		frontcheck.crashname="";

		changecamera=false;
		isfrontcheck=false;
		isbackcheck=false;
		iscrashed=false;


		rigidbody.maxAngularVelocity = 50.0f ;
		Physics.IgnoreLayerCollision ( 0 , 8 , true ) ;
		Physics.IgnoreLayerCollision ( 8 , 8 , false ) ;
	}

	public static int Speed;

	private bool Ishelp;

	void Update () {







		Speed=(int)rigidbody.velocity.sqrMagnitude/2;

	
		if(!crashcheck.iscrashed && start)
		{
			if(Drag.Isbrake || Leveltarget.islevelcomplete)
			{
				Vehiclespeed=0;
					rigidbody.AddRelativeTorque(0,0,0);
					if(rigidbody.drag<10)
					{
					rigidbody.mass=10;
					if(Drag.Issteambrake)
					{
					rigidbody.drag+=2f*Time.deltaTime;

					}
					else
					{
						rigidbody.drag+=0.75f*Time.deltaTime;

					}


					}
			}
			else
			{
				Vehiclespeed=20000*Drag.diffvalue;
				rigidbody.AddRelativeTorque ( Vehiclespeed, 0 , 0 ) ;
				rigidbody.drag=0;
				rigidbody.mass=0.5f;
			}
		}
		else
		{
			Vehiclespeed=0;
			rigidbody.mass=10;
			rigidbody.drag+=1.5f*Time.deltaTime;
			rigidbody.AddRelativeTorque(0,0,0);

		}


		if((frontcheck.Frontname=="Frontcheck1" && Backcheck.Backname=="Backcheck1") ||
		   (frontcheck.Frontname=="Frontcheck2" && Backcheck.Backname=="Backcheck2") ||
		   (frontcheck.Frontname=="Frontcheck3" && Backcheck.Backname=="Backcheck3"))
		{
			rigidbody.maxAngularVelocity=0;
			rigidbody.AddRelativeTorque(0,0,0);
			if(Menu.Trainnumber==1 || Menu.Trainnumber==4)
			{
				StartCoroutine(waitforcrashcheck(6));
			}
			else
			{
				StartCoroutine(waitforcrashcheck(3));
			}

			frontobj=frontcheck.Frontname;
			backobj=Backcheck.Backname;
			crashobj=frontcheck.crashname;
			print("Front obj"+frontobj);

		}


	}

	IEnumerator waitforcrashcheck(float sec)
	{
		yield return new WaitForSeconds(3);
		if(rigidbody.maxAngularVelocity==0 && !iscrashed)
		{
		
			changecamera=true;
			if(frontobj=="Frontcheck1")
			{
				finishcount=1;
			}
			if(frontobj=="Frontcheck2")
			{
				finishcount=2;
			}
			if(frontobj=="Frontcheck3")
			{
				finishcount=3;
			}
			rigidbody.drag=50;	
			if(this.tag=="Maintyre")
			{
				GameObject.Find("Finish"+frontobj.Substring(10)).transform.position=new Vector3(0,100,0);
			}

			yield return new WaitForSeconds(sec);

			frontobj="";
			backobj="";
			changecamera=false;
			camfollow.camview=camfollow.previouscam;
			rigidbody.maxAngularVelocity=50;
			rigidbody.drag=0;	
			Camcheck.isnearfinish=false;
			camfollow.camview=camfollow.previouscam;

		
		

		
		}
		else
		{
			rigidbody.maxAngularVelocity=50;
		
			crashcheck.iscrashed=true;
		
		

		}


			 
	} */
}
