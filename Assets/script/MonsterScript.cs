using UnityEngine;
using System.Collections;

public class MonsterScript : MonoBehaviour
{

	private bool selected = false;
	private bool attacking = false;
	private bool defencing = false;

	Color select = new Color (255, 255, 255, 255);
	Color hidden = new Color (255, 255, 255, 0);
	Color attack = new Color(255,0,0,255);
	Color defence = new Color(34,139,34,255);




	[SerializeField]
	public int idMonster;

	[SerializeField]
	public Texture Name;

	[SerializeField]
	public Texture HealthPoints;

	//[SerializeField]
	//public Texture AttackPoints;

	//[SerializeField]
	//public Texture DefencePoints;


	void Start () {
		transform.Find ("Selection").GetComponent<Renderer> ().material.color = hidden;
	}

	// Update is called once per frame
	void Update () {

	}

	public void Select() {
		print (idMonster);
		if (!selected && !attacking && !defencing) {
			selected = true;
			transform.Find ("Selection").GetComponent<Renderer> ().material.color = select;
		} else if (!selected && attacking) {
			selected = true;
			transform.Find ("Selection").GetComponent<Renderer> ().material.color = attack;
		}else if (!selected && defencing) {
			selected = true;
			transform.Find ("Selection").GetComponent<Renderer> ().material.color = defence;
		}
	}

	public void Attack() {
		if (!defencing) {
			attacking = true;
			transform.Find ("Selection").GetComponent<Renderer> ().material.color = attack;
			GetComponentInChildren<AniBehaviour> ().AniSelectAttack ();
		}
	}

	public void StartAttackAni(){
		GetComponentInChildren<AniBehaviour> ().AniAttack ();
	}
	public void StartDefenceAni(){
		GetComponentInChildren<AniBehaviour> ().AniDefance ();
	}

	public void Defence() {
		if (!attacking) {
			defencing = true;
			transform.Find ("Selection").GetComponent<Renderer> ().material.color = defence;

		}
	}

	public void Deselect() {
		if (selected && !attacking && !defencing) {
			selected = false;
			transform.Find ("Selection").GetComponent<Renderer> ().material.color = hidden;
		} else if (selected && defencing) {
			//selected = false;
			//defencing = true;
		} else if (selected && attacking) {
			//selected = false;
			//attacking = true;
		} else if (selected) {
			//selected = false;
			//transform.Find ("Selection").GetComponent<Renderer> ().material.color = hidden;
		}
	}
	public void EndTurn(){
		if (attacking) {
			selected = false;
			attacking = false;
			transform.Find ("Selection").GetComponent<Renderer> ().material.color = hidden;
		}
		else if (defencing){
			selected = false;
			defencing = false;
			transform.Find ("Selection").GetComponent<Renderer> ().material.color = hidden;
		}
	}

	public bool IsSelected() {
		return selected;
	}

	public bool IsAttaking(){
		return attacking;
	}
	public bool IsDefencing(){
		return defencing;
	}

	public void ChangeColor(Color c) {
		if (transform.childCount == 1) {
			GetComponent<Renderer> ().material.color = c;
		} else {
			for (int i = 0; i < transform.childCount; i++) 
				if (transform.GetChild(i).name != "Selection") {
					transform.GetChild(i).GetComponent<Renderer> ().material.color = c;
				}
		}
	}
}

