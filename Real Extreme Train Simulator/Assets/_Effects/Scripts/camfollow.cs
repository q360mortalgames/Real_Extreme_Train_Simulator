using UnityEngine;
using System.Collections;

public class camfollow : MonoBehaviour {

	private Transform Player1;

	private float distance = 10;//32
	// the height we want the camera to be above the target
	public static   float height = 6.0f;
	// How much we 
	public float heightDamping = 2.0f;
	public float rotationDamping = 3.0f;
	public static int camview;
	private bool isshown;
	// Use this for initialization
	void Start () {
		angle=0;
		cam=Resources.Load("Camera") as Texture2D;	
		Rightrot=Resources.Load("Rightrot") as Texture2D;
		Leftrot=Resources.Load("Leftrot") as Texture2D;
		camview=1;
		//Player1=GameObject.FindWithTag("Player").transform;
		Player1=GameObject.FindWithTag("Playerbody").transform;
		Driver=GameObject.Find("Driverposition");
		Camchange=GameObject.Find("Camerachange");
		Camview4=GameObject.Find("Camview4");
		Body=GameObject.Find("Body_Main02");
	}

	private float change;
	public static int previouscam;

	private GameObject Driver,Camchange,Camview4,Body;
	// Update is called once per frame
	void Update () {

//		Vector3 newpos=new Vector3(Player1.transform.position.x+change,Player1.transform.position.y+5,Player1.transform.position.z+8);
//
//	
//		if(Camcheck.isnearfinish && (!Move.changecamera && !crashcheck.iscrashed) )
//			{
//				previouscam=camview;
//
//				angle=-30;				
//				camview=3;
//				Camcheck.issubway=false;
//			}
//
//
//		if(crashcheck.iscrashed  )
//		{
//
//			distance=Mathf.Lerp(distance,10,5*Time.deltaTime);
//			height=Mathf.Lerp(height,3,5*Time.deltaTime);
//		
//
//			angle+=0.5f;
//			if(!isshown)
//			{
//			StartCoroutine(waitforcrashshow());
//
//			}
//
//			Cam();
//		}
//		else
//		{
//
//
//			if(camview==1 )
//			{
//
//				previouscam=camview;
//				if(Move.changecamera)
//				{
//					camview=6;
//				}
//				if(Camcheck.issubway)
//				{
//					distance=Mathf.Lerp(distance,10,5*Time.deltaTime);
//					height=Mathf.Lerp(height,4,5*Time.deltaTime);
//					angle=Mathf.Lerp(angle,10,5*Time.deltaTime);
//					Cam();
//				}
//
//				else
//				{
//
//					distance=Mathf.Lerp(distance,20,5*Time.deltaTime);
//					height=Mathf.Lerp(height,12,5*Time.deltaTime);
//					angle=Mathf.Lerp(angle,10,5*Time.deltaTime);
//					Cam();
//				}
//			}
//
//
//			if(camview==2 )
//			{
//				previouscam=camview;
//				if(Move.changecamera)
//				{
//					camview=6;
//				}
//			
//
//				else
//				{
//				angle=0;
//					this.transform.position=Driver.transform.position;
//					this.transform.rotation=Driver.transform.rotation;
//				}
//			}
//			if(camview==3)
//			{
//				previouscam=camview;
//				if(Move.changecamera)
//				{
//					camview=6;
//				}
//				if(Camcheck.issubway)
//				{
//
//					distance=Mathf.Lerp(distance,10,5*Time.deltaTime);
//					height=Mathf.Lerp(height,4,5*Time.deltaTime);
//					angle=Mathf.Lerp(angle,-20,5*Time.deltaTime);
//
//					Cam();
//				}
//
//				else
//				{
//					distance=Mathf.Lerp(distance,20,5*Time.deltaTime);
//					height=Mathf.Lerp(height,5,5*Time.deltaTime);
//
//				
//					Cam();
//	
//				}
//			}
//			if(camview==6)
//			{
//				this.transform.position=Vector3.Lerp(this.transform.position,Camchange.transform.position,3*Time.deltaTime);
//				this.transform.LookAt(Body.transform);
//			}
//			if(camview==4)
//			{
//				previouscam=camview;
//				if(Move.changecamera)
//				{
//					camview=6;
//				}
//				else
//				{
//
//				this.transform.position=Vector3.Lerp(this.transform.position,Camview4.transform.position,3*Time.deltaTime);
//					this.transform.LookAt(Body.transform);
//				}
//			}
//			if(camview==5)
//			{
//				previouscam=camview;
//				if(Move.changecamera)
//				{
//					camview=5;
//				}
//				if(Camcheck.issubway)
//				{
//					distance=Mathf.Lerp(distance,10,5*Time.deltaTime);
//					height=Mathf.Lerp(height,4,5*Time.deltaTime);
//					angle=Mathf.Lerp(angle,0,5*Time.deltaTime);
//
//
//					Cam();
//				}
//
//				else
//				{
//					distance=Mathf.Lerp(distance,30,5*Time.deltaTime);
//					height=Mathf.Lerp(height,5,5*Time.deltaTime);
//
//
//					angle+=0.5f;
//					Cam();
//	}
//
//			if(camview==7)
//				{
//
//					distance=Mathf.Lerp(distance,5,5*Time.deltaTime);
//					height=Mathf.Lerp(height,4,5*Time.deltaTime);
//				
//						Cam();
//
//				}
//			
//			}
//
//
//			if(Move.changecamera)
//			{
//				camview=6;
//			}
//			if(!Move.changecamera)
//			{
//				if(camview!=6 )
//				camview=previouscam;
//
//			}
//
//		}

	}

	IEnumerator waitforcrashshow()
	{
		isshown=true;
	
		Time.timeScale=0.3f;
		yield return new WaitForSeconds(1);
		Time.timeScale=1f;


		yield return new WaitForSeconds(1);
	//	Time.timeScale=0.1f;


		yield return new WaitForSeconds(0.5f);
		Time.timeScale=1;

	
		yield return new WaitForSeconds(1);
	//	Time.timeScale=0.1f;

	
		yield return new WaitForSeconds(1);
		Time.timeScale=1;
 		print("no of times");

	}
	public static float angle=0;
	
	void Cam () {
		// Early out if we don't have a target
		if (!Player1)
			return;
		
	
		
		float  wantedRotationAngle = Player1.eulerAngles.y-angle;// for monster truck
		//	print("target.eulerAngles"+target.eulerAngles);
		float wantedHeight = Player1.position.y + height;
		
		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;
		
		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
		
		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
		
		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
		
		
		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		
		transform.position = Player1.position;
		//transform.position.z = 0;
		//transform.rotation=Quaternion.Euler(0,0,-0.6f);
		transform.position -= currentRotation * Vector3.forward * distance;
		
		// Set the height of the camera
		
		
		//	transform.position.y=currentHeight;
		transform.position=new Vector3(transform.position.x,currentHeight,transform.position.z);
		// Always look at the target
		transform.LookAt (Player1);
	}
	GUIStyle myStyle = new GUIStyle();
	private Texture2D cam,Rightrot,Leftrot;
//	void OnGUI()
//	{
//		if(camview==3 && !crashcheck.iscrashed && !Leveltarget.islevelcomplete && !Leveltarget.ispaused && !Camcheck.issubway && !Camcheck.isnearfinish)
//		{
//		
//			if(GUI.RepeatButton(new Rect(Screen.width/7f,Screen.width/45,Screen.width/25,Screen.width/25),Rightrot,myStyle))
//			{
//				angle+=0.5f;
//			}
//
//			if(GUI.RepeatButton(new Rect(Screen.width/11.5f,Screen.width/45,Screen.width/25,Screen.width/25),Leftrot,myStyle))
//			{
//				angle-=0.5f;
//			}
//		}
//
//		if(!crashcheck.iscrashed &&  !Leveltarget.islevelcomplete  && !Leveltarget.ispaused && !Move.changecamera)
//		{
//			if(GUI.Button(new Rect(Screen.width/48,Screen.width/60,Screen.width/20,Screen.width/20),cam,myStyle)  )
//			{
//
//
//				if(camview<3)
//				{
//				camview++;
//				angle=0;
//				
//				}
//				else
//				{
//					angle=0;
//					camview=1;
//				}
//
//				if(camview==3)
//				{
//					angle=-30;
//				}
//			}
//		}
//
//	}
}
