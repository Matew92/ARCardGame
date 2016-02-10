using UnityEngine;
using System.Collections;

public class FurnitureBehaviour : MonoBehaviour {
	private bool selected = false;

	[SerializeField]
	public Texture Name;

	[SerializeField]
	public Texture Price;

	Color vis = new Color (255, 255, 255, 255);
	Color transp = new Color (255, 255, 255, 0);

	void Start () {
		transform.Find ("Selection").GetComponent<Renderer> ().material.color = transp;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Select() {
		if (!selected) {
			selected = true;

			transform.Find ("Selection").GetComponent<Renderer> ().material.color = vis;
		}
	}

	public void Deselect() {
		if (selected) {
			selected = false;
			transform.Find ("Selection").GetComponent<Renderer> ().material.color = transp;
		}
	}

	public bool IsSelected() {
		return selected;
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
