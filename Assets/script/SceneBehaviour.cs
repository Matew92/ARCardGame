using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SceneBehaviour : MonoBehaviour {
	public float minSwipeDistY;
	public float minSwipeDistX;
	private Vector2 startPos;
	private GameObject manipulatedObj;
	public int idMonsterAttack = 0;
	public int idMonsterDefence = 0;
	public int turn = 0;

	private List<ARMarker> visibleMarkers = new List<ARMarker> (); 

	int SWIPE_THRESHOLD = 50;
	int PALETTE_THRESHOLD = 200;
	float DIST_THRESHOLD = 0.25F;

	// Use this for initialization
	void Start () {
	}

	private GameObject DetectTarget() {
		GameObject target = null;

		RaycastHit hit;
		Camera camera = GameObject.Find ("Camera").GetComponent<Camera>();
		Ray ray = camera.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			Debug.Log ("Hit " + hit.transform.gameObject.name);
			target = hit.transform.gameObject;
		}

		return target;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			Touch touch = Input.touches [0];
			switch (touch.phase) {

			case TouchPhase.Began:
				manipulatedObj = DetectTarget ();
				startPos = touch.position;

				break;
			case TouchPhase.Ended:
				float swipeValue;
				float swipeDistVertical = (new Vector3 (0, touch.position.y, 0) - new Vector3 (0, startPos.y, 0)).magnitude;
				if (swipeDistVertical > minSwipeDistY) {
					swipeValue = Mathf.Sign (touch.position.y - startPos.y);

					if (swipeValue > 0) {
					} // up swipe
					else if (swipeValue < 0) {
					} //down swipe
				}

				float swipeDistHorizontal = (new Vector3 (touch.position.x, 0, 0) - new Vector3 (startPos.x, 0, 0)).magnitude;

				swipeValue = (touch.position.x - startPos.x);

				if (manipulatedObj) {
					if (swipeValue > SWIPE_THRESHOLD) { //right swipe
						//RotateObject(manipulatedObj, 270);
					} else if (swipeValue < -SWIPE_THRESHOLD) { //left swipe
						//RotateObject(manipulatedObj, 90);
					} else {
						ObjectTap (manipulatedObj);
					}
				}
				else
					if (touch.position.y < Screen.height - PALETTE_THRESHOLD) {
						TapOutside ();
					}

				break;
			}
		}
		else if (Input.GetMouseButtonDown(0)) {
			GameObject obj = DetectTarget();
			if(obj) {
				int objId = GetIdObj (obj);
				print (objId);
				ObjectTap (obj);
			}
			else if (Input.mousePosition.y < Screen.height - PALETTE_THRESHOLD) {
				TapOutside ();
			}
		}

		//DetectCloseObject ();
	}

	private void ObjectTap(GameObject obj) {
		//print ("Tap object!");
		if (!IsSelected (obj))
			SelectObject (obj);
		
		
	}

	private void TapOutside() {
		//print ("Tap outside");
		var prevSelectedObj = GetSelection ();
		if (prevSelectedObj)
			ClearSelection (prevSelectedObj);
	}


	private void SelectObject(GameObject obj) {
		var prevSelectedObj = GetSelection ();
		if (prevSelectedObj)
			ClearSelection (prevSelectedObj);
		print (GetIdObj (obj));


		if (idMonsterAttack == 0) {
			print (GetIdObj (obj));
			obj.GetComponent<MonsterScript> ().Select();
			GameObject.Find ("AttackButton").GetComponent<GameButtonBehaviour> ().Show ();
		} else if (idMonsterAttack!=0) {
			GameObject.Find ("DefanceButton").GetComponent<GameButtonBehaviour> ().Show ();
			obj.GetComponent<MonsterScript> ().Select();
			obj.GetComponent<MonsterScript> ().Defence();
		} else {
			obj.GetComponent<MonsterScript> ().Select();
		}

		Texture objName = obj.GetComponent<MonsterScript>().Name;
		Texture objPrice = obj.GetComponent<MonsterScript>().HealthPoints;
		GameObject.Find("NameImage").GetComponent<RawImage>().texture = objName;

		GameObject.Find ("DescObject").GetComponent<DescriptionBehaviour> ().Show();


	}

	private void ClearSelection(GameObject obj) {
		print ("Clear Selection");
		obj.GetComponent<MonsterScript> ().Deselect ();

		GameObject.Find ("DescObject").GetComponent<DescriptionBehaviour> ().Hide ();
		GameObject.Find ("AttackButton").GetComponent<GameButtonBehaviour> ().Hide ();
	}

	private int GetIdObj(GameObject obj){
		return obj.GetComponent<MonsterScript> ().idMonster;
	}
						

	private bool IsSelected(GameObject obj) {
		return obj.GetComponent<MonsterScript> ().IsSelected ();
	}

	private bool IsAttaking(GameObject obj) {
		return obj.GetComponent<MonsterScript> ().IsAttaking ();
	}

	private bool IsDefencing(GameObject obj) {
		return obj.GetComponent<MonsterScript> ().IsDefencing ();
	}

	private GameObject GetSelection() {
		var objects = GameObject.FindGameObjectsWithTag ("MonsterTag");
		foreach (var obj in objects) {
			if (IsSelected (obj))
				return obj;
		}
		return null;
	}


	public void OnMarkerLost(ARMarker marker) {
		visibleMarkers.Remove (marker);
	}

	public void OnMarkerFound(ARMarker marker) {
		visibleMarkers.Add (marker);
	}

	private GameObject FindIdDefence(){
		var objects = GameObject.FindGameObjectsWithTag ("MonsterTag");
		foreach (var obj in objects) {
			if (!IsAttaking(obj) && IsSelected(obj))
				return obj;
		}
		return null;
	}
	private GameObject FindIdAttack(){
		var objects = GameObject.FindGameObjectsWithTag ("MonsterTag");
		foreach (var obj in objects) {
			if (IsAttaking(obj))
				return obj;
		}
		return null;
	}


	public void startAttack(){
		
		GameObject obj = GetSelection ();
		print (GetIdObj(obj));
		if (!IsDefencing (obj)) {
			obj.GetComponent<MonsterScript> ().Attack ();
			idMonsterAttack = GetIdObj (obj);
			GameObject.Find ("AttackButton").GetComponent<GameButtonBehaviour> ().Hide ();


		}
		

	}
	public void startDefence(){
		GameObject objDeff = FindIdDefence ();
		GameObject objAtt = FindIdAttack ();
		print (GetIdObj(objDeff));
		if (!IsAttaking (objDeff)) {
			objDeff.GetComponent<MonsterScript> ().Defence ();
			idMonsterDefence = GetIdObj (objDeff);
			GameObject.Find ("DefanceButton").GetComponent<GameButtonBehaviour> ().Hide ();
			//print ("postionAt" + objAtt.transform.position);
			//print ("postionDef" + objDeff.transform.position);
			//GameObject.Find ("LineRender").GetComponent<DrawLine> ().origin = objAtt.transform;
			//GameObject.Find ("LineRender").GetComponent<DrawLine> ().destination = objDeff.transform ;
			objAtt.GetComponent<MonsterScript> ().StartAttackAni ();
			objDeff.GetComponent<MonsterScript> ().StartDefenceAni ();

			EndTurn ();
		}
	}

	public void EndTurn(){
		var prevSelectedObj = GetSelection ();
		if (prevSelectedObj)
			prevSelectedObj.GetComponent<MonsterScript> ().EndTurn ();
		if (idMonsterAttack != 0) {
			var objects = GameObject.FindGameObjectsWithTag ("MonsterTag");
			foreach (var obj in objects) {
				if (IsAttaking (obj)) {
					obj.GetComponent<MonsterScript> ().EndTurn ();
					idMonsterAttack = 0;
				}
			}
		}
		if (idMonsterAttack != 0) {
			var objects = GameObject.FindGameObjectsWithTag ("MonsterTag");
			foreach (var obj in objects) {
				if (IsDefencing (obj)) {
					obj.GetComponent<MonsterScript> ().EndTurn ();
					idMonsterDefence = 0;
				}
			}
		}

		Dismiss ();
		turn++;
		GameObject.Find ("DescObject").GetComponent<DescriptionBehaviour> ().UpdateTurn(turn);
		print ("turn: " + turn);
	}

	public void Dismiss(){
		var prevSelectedObj = GetSelection ();
		if (prevSelectedObj)
			ClearSelection (prevSelectedObj);
		idMonsterAttack = 0;
		idMonsterDefence = 0;
	}


	
}
