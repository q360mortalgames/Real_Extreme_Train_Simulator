using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour {
    //public Material WaterMat;
    //[Range(-5,5)]
    //public float speed;
    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {
    //	WaterMat.SetTextureOffset ("_MainTex", new Vector2 (0, Time.time/speed));
    //}

    public float speed;
    public Renderer tex;

    private void Start()
    {
        tex = GetComponent<Renderer>();
    }
    private void Update()
    {
        float offset = Time.time * speed;
        tex.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
