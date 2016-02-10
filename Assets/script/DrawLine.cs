using UnityEngine;
using System.Collections;

public class DrawLine : MonoBehaviour {

	private LineRenderer lineRender;
	private float counter;
	private float dist;

	public Transform origin;
	public Transform destination;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartLine(){
	}

	public void SetLine(){

		//lineRender = GetComponent<LineRenderer> ();
		//lineRender.SetPosition (0,0);
		//lineRender.SetWidth (.45f, .45f);
	}
}
