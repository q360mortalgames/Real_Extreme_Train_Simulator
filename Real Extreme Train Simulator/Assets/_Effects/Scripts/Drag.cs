
using UnityEngine;
using System.Collections;

public class Drag : MonoBehaviour {
	
//	// Use this for initialization
//	private float startvalue,movedvalue,yval;
//	public static float diffvalue;
//
//	private GameObject Accelator,Basebackground;
//
//
//	public static bool Isbrake,Issteambrake;
//
//	private GameObject Brake;
//
//	void Start () {
//	
//		Isbrake=true;
//		Issteambrake=false;
//
//
//		diffvalue=0;
//		startvalue=0.14f;
//		movedvalue=startvalue;
//		Accelator=GameObject.Find("Accel");
//
//		Basebackground=GameObject.Find("Basetexturebackground");
//
//		Brake=GameObject.Find("Brakes");
//		Input.multiTouchEnabled=true;
//	}
//	
//	private bool ishold;
//
//	void Update () 
//	{
//
//		#if UNITY_ANDROID
//
//
//				if(Input.touches.Length<0)
//				{
//				}
//				else
//				{
//					foreach(Touch t in Input.touches)
//					{
//
//						if(Brake.guiTexture.HitTest(t.position))
//						{
//							if(t.phase==TouchPhase.Began)
//							{
//							Isbrake=true;
//							Issteambrake=true;
//							Accelator.transform.position=new Vector3(Accelator.transform.position.x,0.14f ,0);
//							Basebackground.guiTexture.texture=Resources.Load("Brake") as Texture2D;
//
//							Audiomanager.mee.Brakesound();
//							}
//							if(t.phase==TouchPhase.Ended)
//							{
//								
//							}
//						}
//						
//						if(this.guiTexture.GetScreenRect().Contains(t.position))
//						{
//						if(t.phase==TouchPhase.Began)
//						{
//							Vector3 Mousepoint=Camera.main.ScreenToViewportPoint(t.position);
//							yval=Mousepoint.y;
//							ishold=true;
//							//yval=Input.GetTouch(i).position.y/800;
//
//
//							Issteambrake=false;
//						}
//						if(t.phase==TouchPhase.Ended)
//						{
//							Vector3 Mousepoint=Camera.main.ScreenToViewportPoint(t.position);
//							ishold=false;
//							movedvalue=Mousepoint.y;
//							//movedvalue=Input.GetTouch(i).position.y/800;
//						}
//						if(t.phase==TouchPhase.Moved)
//						{
//							if(ishold)
//							{
//
//								//	yval=Input.GetTouch(i).position.y/800;
//								Vector3 Mousepoint=Camera.main.ScreenToViewportPoint(t.position);
//								yval=Mousepoint.y;
//								yval=Mathf.Clamp(yval,0.14f,0.34f);
//								Accelator.transform.position=new Vector3(Accelator.transform.position.x,yval,0);
//								movedvalue=yval;
//								diffvalue=movedvalue-startvalue;
//
//								if(yval<=0.14f)
//								{
//									Basebackground.guiTexture.texture=Resources.Load("Brake") as Texture2D;
//									Isbrake=true;
//								}
//								else
//								{
//									Basebackground.guiTexture.texture=Resources.Load("Accelarate") as Texture2D;
//									Isbrake=false;
//								}
//							}
//						}
//						}
//		
//		
//					}
//				}
//
//
//		
//		#endif
//
//
//#if UNITY_EDITOR
//		
//		if(ishold)
//		{
//			Vector3 Mousepoint=Camera.main.ScreenToViewportPoint(Input.mousePosition);
//			float yval=0;//Input.mousePosition.y/600;
//			yval=Mousepoint.y;
//			yval=Mathf.Clamp(yval,0.14f,0.34f);
//			Accelator.transform.position=new Vector3(Accelator.transform.position.x,yval ,0);
//			//print(""+(Screen.width-Input.mousePosition.x)/1000);
//			
//			movedvalue=yval;
//			diffvalue=movedvalue-startvalue;
//			if(yval<=0.14f)
//			{
//				Basebackground.guiTexture.texture=Resources.Load("Brake") as Texture2D;
//				Isbrake=true;
//			}
//			else
//			{
//				Basebackground.guiTexture.texture=Resources.Load("Accelarate") as Texture2D;
//				Isbrake=false;
//			}
//		//	print("diff"+diffvalue);
//		}
//
//		if(Input.GetMouseButton(0))
//		{
//			if(GameObject.Find("Brakes").guiTexture.HitTest(Input.mousePosition))
//			{
//				Isbrake=true;
//				Issteambrake=true;
//				Accelator.transform.position=new Vector3(Accelator.transform.position.x,0.14f ,0);
//				Basebackground.guiTexture.texture=Resources.Load("Brake") as Texture2D;
//				Audiomanager.mee.Brakesound();
//
//			}
//			else
//			{
//			//	Drag.Isbrake=false;
//				Issteambrake=false;
//			}
//		}
//
//#endif
//	}
//	
//	void OnMouseDown()
//	{
//
//		#if UNITY_EDITOR
//
//		ishold=true;
//		//print("Down"+diffvalue);
//		#endif
//	}
//	
//	void OnMouseUp()
//	{
//		#if UNITY_EDITOR
//
//		ishold=false;
//
//		#endif
//	}
//	
//	
//	void OnGUI()
//	{
//		//GUI.Label(new Rect(50,50,100,100),"moved"+movedvalue+" yval"+yval+" scree"+Screen.width/600);
//	}
}
