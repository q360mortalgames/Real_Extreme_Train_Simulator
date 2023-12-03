
using UnityEngine;
using System.Collections;

public class Drag2 : MonoBehaviour {
//	
//	// Use this for initialization
//	private float startvalue,movedvalue,Xval;
//	public static float diffvalue;
//	
//	private GUITexture Accelator;
//
//	void Start () {
//
//		
//
//		diffvalue=0;
//		startvalue=0.2f;
//		movedvalue=startvalue;
//
//
//		Accelator=GameObject.Find("Controller").guiTexture;
//		Invoke("Getposition",4);
//
//
//		Input.multiTouchEnabled=true;
//
//	}
//
//	void Getposition()
//	{
//		Startpos=new Vector3(Accelator.transform.position.x,Accelator.transform.position.y,Accelator.transform.position.z);
//		Reposition=true;
//	}
//
//	private Vector3 Startpos;
//	private bool ishold,Reposition;
//
//	Vector3 Mousepoint;		
//	// Update is called once per frame
//	void Update () 
//	{
//
//	#if UNITY_ANDROID
//
//
//
//		if(!ishold)
//		{
//			if(Reposition)
//			Accelator.transform.position=Vector3.Lerp(Accelator.transform.position, Startpos,5*Time.deltaTime);
//		}
//
//
//		if(Input.touches.Length<0)
//		{
//
//		}
//		else
//		{
//			foreach(Touch t in Input.touches)
//			{
//			
//				if(this.guiTexture.HitTest(t.position))
//				{
//
//					if(t.phase==TouchPhase.Canceled)
//					{
//						str="Cancelled";
//					}
//					if(t.phase==TouchPhase.Began)
//					{
//						ishold=true;
//						str="Began";
//					}
//					if(t.phase==TouchPhase.Ended)
//					{
//						ishold=false;
//						Accelator.guiTexture.texture=Resources.Load("Previoustexture") as Texture2D;
//
//						str="Ended";
//
//					}
//					if(t.phase==TouchPhase.Moved)
//					{
//						if(ishold)
//						{
//
//							float Xval=0;//Input.mousePosition.x/600;
//							Mousepoint=Camera.main.ScreenToViewportPoint(t.position);
//							
//							Xval=Mathf.Clamp(Mousepoint.x,0.07f,0.29f);
//							Accelator.transform.position=new Vector3(Xval,Accelator.transform.position.y ,0);
//							
//
//							str="Moved";
//
//
//							if(Mousepoint.x>0.4f || Mousepoint.y>0.15f || Mousepoint.y<0.05f  )
//							{
//								ishold=false;
//							}
//							if(Xval <0.1f && Triggercheck.Mee.isleft)
//							{
//							//	Accelator.guiTexture.texture=Resources.Load("Changedtexture") as Texture2D;
//								Triggercheck.Mee.Lefttrigger();
//							}
//							
//							else if(Xval>0.26f && Triggercheck.Mee.isright)
//							{
//							//	Accelator.guiTexture.texture=Resources.Load("Changedtexture") as Texture2D;
//								Triggercheck.Mee.Righttrigger();
//							}
//							else
//							{
//								Accelator.guiTexture.texture=Resources.Load("Previoustexture") as Texture2D;
//							}
//
//						}
//					}
//				}
//				
//				
//			}
//		}
//
//
//
//	#endif
//
//
//		#if UNITY_EDITOR
//		
//		if(ishold)
//		{
//			float Xval=0;//Input.mousePosition.x/600;
//			Vector3 Mousepoint1=Camera.main.ScreenToViewportPoint(Input.mousePosition);
//
//			Xval=Mathf.Clamp(Mousepoint1.x,0.07f,0.29f);
//			Accelator.transform.position=new Vector3(Xval,Accelator.transform.position.y ,0);
//
//
//
//			if(Xval <0.1f && Triggercheck.Mee.isleft)
//			{
//				Triggercheck.Mee.Lefttrigger();
//			}
//
//			else if(Xval>0.26f && Triggercheck.Mee.isright)
//			{
//				Triggercheck.Mee.Righttrigger();
//			}
//			else
//			{
//				Accelator.guiTexture.texture=Resources.Load("Previoustexture") as Texture2D;
//			}
//
//
//	
//		}
//		else
//		{
//			if(Reposition)
//			Accelator.transform.position=Vector3.Lerp(Accelator.transform.position, Startpos,5*Time.deltaTime);
//		}
//
//
//		#endif
//	}
//	
//	void OnMouseDown()
//	{
//		#if UNITY_EDITOR
//		ishold=true;
//		print("Down"+diffvalue);
//		#endif
//	}
//	
//	void OnMouseUp()
//	{
//
//		#if UNITY_EDITOR
//		ishold=false;
//		Accelator.guiTexture.texture=Resources.Load("Previoustexture") as Texture2D;
//		#endif
//	
//	}
//	
//	
//	void OnGUI()
//	{
//	}
//
//	string str;


}
