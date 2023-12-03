using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class glowingA : MonoBehaviour
{
	int colorTag	= 0;
	float f_timer	= 0, f_Targettimer	= 0.5f;

	Material _MaterialForEnabled;

	[SerializeField] Color _color1;
	[SerializeField] Color _color2;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		

		f_timer += Time.deltaTime;

		if (f_timer >= f_Targettimer) {
			colorTag++;
			if (colorTag	> 1)
				colorTag	= 0;

			if (colorTag == 0)
				GetComponent<Image> ().color	= _color1;
			else
				GetComponent<Image> ().color	= _color2;

			f_timer	= 0;

		}

	}
}
