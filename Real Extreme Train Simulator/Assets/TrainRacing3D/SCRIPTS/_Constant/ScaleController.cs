using UnityEngine;
using System.Collections;

public enum CURRENT_AXIS
{
	X_AXIS,
	Y_AXIS,
	Z_AXIS,
}

public class ScaleController {
	
	public const float customWidth  = 1280f; //Set this value to the Width of your Game Tab in the Editor
	public const float customHeight  = 800f; //Set this value to the Height of your Game Tab in the Editor
	public static  Rect returnRect;
	
	public static float yValue;
	public static float yDivValue;
	
	public static Rect scaledRect (float x , float y,  float width , float height ) {		
		x = (x*Screen.width) / customWidth;
		y = (y*Screen.height) / customHeight;
		width = (width*Screen.width) / customWidth;
		height = (height*Screen.height) / customHeight;
		
		returnRect = new Rect(x,y,width,height);
		return returnRect;
	} 
	
	public static Rect scaledRect (Rect rect ) {		
		rect.x = (rect.x*Screen.width) / customWidth;
		rect.y = (rect.y*Screen.height) / customHeight;
		rect.width = (rect.width*Screen.width) / customWidth;
		rect.height = (rect.height*Screen.height) / customHeight;
		
		returnRect = new Rect(rect.x,rect.y,rect.width,rect.height);
		return returnRect;
	} 
	
	public static float scaleWidth(float width){
		width = (width*Screen.width) / customWidth;
		return width;
	}	
	
	public static float scaleHeight(float height){
		height = (height*Screen.height) / customHeight;
		return height;
	}
	
	static Vector3 Vec3;	
	public static void setGUIScale(){		
		Vec3 = new Vector3(Screen.width /customWidth, Screen.height /customHeight, 1.0f);
		GUI.matrix = Matrix4x4.Scale(Vec3);
	}	
	
	public static Vector3 setGameObjectScale(Transform trans, CURRENT_AXIS _cAxis){

		switch (_cAxis) {

		case CURRENT_AXIS.X_AXIS:
			trans.localScale = new Vector3(((float)Screen.width/(float)Screen.height),trans.localScale.y,trans.localScale.z);
			break;

		case CURRENT_AXIS.Y_AXIS:
			trans.localScale = new Vector3(trans.localScale.x,((float)Screen.width/(float)Screen.height),trans.localScale.z);
			break;

		case CURRENT_AXIS.Z_AXIS:
			trans.localScale = new Vector3(trans.localScale.x,trans.localScale.y,((float)Screen.width/(float)Screen.height));
			break;
		}

		return trans.localScale;
	} 

	public static Vector3 setGameObjectScale(Transform trans, CURRENT_AXIS _cAxis, float multiplier){

		switch (_cAxis) {

		case CURRENT_AXIS.X_AXIS:
			trans.localScale = new Vector3((((float)Screen.width/(float)Screen.height)*multiplier),trans.localScale.y,trans.localScale.z);
			break;

		case CURRENT_AXIS.Y_AXIS:
			trans.localScale = new Vector3(trans.localScale.x,(((float)Screen.width/(float)Screen.height)*multiplier),trans.localScale.z);
			break;

		case CURRENT_AXIS.Z_AXIS:
			trans.localScale = new Vector3(trans.localScale.x,trans.localScale.y,(((float)Screen.width/(float)Screen.height)*multiplier));
			break;
		}
		return trans.localScale;
	} 


	static float xValue;
	static void xValueControl(float btnWidth, float btnHeight){
		xValue = btnWidth / btnHeight;
	}
	
	static float xDivValue;
	static void xDivValueControl(float btnWidth, float maxScreen, float minScreen){
		xDivValue=(btnWidth * minScreen)/maxScreen;		
	}
	
	static void yDivValueControl(float minScreen,float xDivVal){
		yDivValue = minScreen / xDivVal;		
	}
	
	static void yValueControl(){
		yValue = xValue * yDivValue;
	}
	
	public static void scaledRected(float btnWidth, float btnHeight, float maxScreen, float minScreen){
		xValueControl(btnWidth, btnHeight);
		xDivValueControl(btnWidth, maxScreen, minScreen);
		yDivValueControl(minScreen, xDivValue);
		yValueControl();
	}
}
