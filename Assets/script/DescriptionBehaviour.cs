using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DescriptionBehaviour : MonoBehaviour {
	bool hidden = true;
	Text turn; 

	// Use this for initialization
	void Start () {
		
		transform.Translate (new Vector2 (0, -Screen.height / 2 - 120));
		transform.Find("NameImage").Translate (new Vector2 (-Screen.width / 2 + 500, 0));
		transform.Find("TextTurn").Translate (new Vector2 (Screen.width / 2 - 500, 240));
		turn = GameObject.Find ("TextTurn").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Show() {
		if (hidden) {
			transform.Find("NameImage").Translate (new Vector2 (0, 240));
			hidden = false;
		}
	}

	public void Hide() {
		if (!hidden) {
			transform.Find("NameImage").Translate  (new Vector2 (0, - 240));
			hidden = true;
		}
	}

	public void UpdateTurn(int turnNow){
		turn.text = "Turn: "+ turnNow;

	
	}

}
