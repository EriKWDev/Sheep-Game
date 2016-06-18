using UnityEngine;
using System.Collections;

public class CursorBehaviour : MonoBehaviour {
	public bool showCursor = false;

	public GameObject current;

	void Update () {
		if (showCursor) {
			GetComponent<SpriteRenderer> ().enabled = true;
			GetComponent<CircleCollider2D> ().enabled = true;
			transform.position = new Vector3 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0F);

			if(Input.GetKeyUp(KeyCode.C) && current != null) {
				current.SendMessage ("Click", null, SendMessageOptions.DontRequireReceiver);
			}
		}
		else{
			GetComponent<SpriteRenderer> ().enabled = false;
			GetComponent<CircleCollider2D> ().enabled = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		current = other.gameObject;
		Debug.Log (other.gameObject.name);
	}

	void OnTriggeExit2D(Collider2D other) {
		Debug.Log (other.gameObject.name);
		current = null;
	}

}
