
using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class FPSHUD : MonoBehaviour 
{
 
	public  float updateInterval = 0.5F;
 
	private float accum   = 0; 	// FPS accumulated over the interval
	private int   frames  = 0; 	// Frames drawn over the interval
	private float timeleft; 	// Left time for current interval


	void Awake()
	{
	   useGUILayout = false;
	}


	void Start()
	{
	    if( !GetComponent<TextMesh>() )
	    {
			#if DEBUG
	        Debug.Log("UtilityFramesPerSecond needs a GUIText component!");
			#endif
	        enabled = false;
	        return;
	    }
	    timeleft = updateInterval;  
	}


	void Update()
	{
	    timeleft -= Time.deltaTime;
	    accum += Time.timeScale/Time.deltaTime;
	    ++frames;
    
	    // Interval ended - update GUI text and start new interval
	    if( timeleft <= 0.0 )
	    {
	        // display two fractional digits (f2 format)
		    float fps = accum/frames;
		    string format = System.String.Format("{0:F2} FPS",fps);
		    GetComponent<TextMesh>().text = format;

		    if( fps < 30 )
			{
		      //  GetComponent<TextMesh>().material.color = Color.yellow;
			}
		 //   else 
			{
		      //  if( fps < 10 )
		          //  GetComponent<GUIText>().material.color = Color.red;
		       // else
		          //  GetComponent<GUIText>().material.color = Color.green;
	
		        timeleft = updateInterval;
		        accum = 0.0F;
		        frames = 0;
		    }
		}
	}

}