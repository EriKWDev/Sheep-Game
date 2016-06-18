using UnityEngine;
using System.Collections;

public class ChestBehaviour : MonoBehaviour {
	
	public Sprite floatingSprite;
	public GameObject floatingSpriteObject;
	public float offset = 1.5F;
	private Object letter;
	private bool isInRange = false;
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject == GameObject.Find ("Character")) {
			GameObject.FindGameObjectWithTag ("Cursor").GetComponent<CursorBehaviour> ().showCursor = true;
			isInRange = true;
			floatingSpriteObject.transform.position = new Vector3 (transform.position.x, transform.position.y + offset, transform.position.z);
			floatingSpriteObject.GetComponent<SpriteRenderer> ().sprite = floatingSprite;
			letter = Instantiate (floatingSpriteObject);
		}
	}
	
	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject == GameObject.Find ("Character")) {
			GameObject.FindGameObjectWithTag ("Cursor").GetComponent<CursorBehaviour> ().showCursor = false;
			isInRange = false;
			Destroy (letter);
		}
	}
	
	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, 1F);
		if (isInRange) {
				
		}
	}
}
