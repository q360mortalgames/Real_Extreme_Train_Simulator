using UnityEngine;
using System.Collections;

public class UVMapping : MonoBehaviour {

	public Material _Material;
	public float mf_XSpeed;
	public float mf_YSpeed;
	public bool mb_IsXMovement;
	public bool mb_IsYMovement;

	Vector2 Val;

	// Use this for initialization
	void Start () 
	{
		if(_Material == null )
			_Material	= GetComponent<Renderer>().material;

		Val	= _Material.GetTextureOffset("_MainTex");

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(mb_IsXMovement)
		{
			Val.x += mf_XSpeed*Time.deltaTime;
		}
		if(mb_IsYMovement)
		{
			Val.y += mf_YSpeed*Time.deltaTime;
		}

		if(mb_IsXMovement || mb_IsYMovement)
		{
			_Material.SetTextureOffset("_MainTex",Val);
		}

	}
}
