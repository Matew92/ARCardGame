using UnityEngine;
using System.Collections;

public class AniBehaviour : MonoBehaviour {

	public Animator monster;
	public int nuno = 1;
	// Use this for initialization
	void Start () {
		monster = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AniSelectAttack(){
		monster.Play ("flight", -1, 0f);
	}
	public void AniAttack(){
		monster.Play ("get a gun", -1, 0f);
	}
	public void AniDefance(){
		monster.Play ("dead", -1, 0f);
	}
}
