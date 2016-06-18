using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour {

	public Sprite floatingSprite;
	public GameObject floatingSpriteObject;
	public float offset = 1.5F;
	public Object letter;
	public GameObject objectToActivate;
	public KeyCode letterToInteract = KeyCode.E;
	private bool isInRange = false;
	public bool summonFloatingSprite = true;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject == GameObject.Find ("Character")) {
			isInRange = true;
			if(summonFloatingSprite){
				floatingSpriteObject.transform.position = new Vector3 (transform.position.x, transform.position.y + offset, transform.position.z);
				floatingSpriteObject.GetComponent<SpriteRenderer> ().sprite = floatingSprite;
				letter = Instantiate (floatingSpriteObject);
			}
		}
	}

	void Update() {
		if (Input.GetKeyDown (letterToInteract) && isInRange) {
			objectToActivate.SendMessage("Activate", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject == GameObject.Find ("Character")) {
			GameObject.FindGameObjectWithTag ("Cursor").GetComponent<CursorBehaviour> ().showCursor = false;
			isInRange = false;
			if(summonFloatingSprite) Destroy (letter);
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere (transform.position, 1F);
		if (objectToActivate != this.gameObject && objectToActivate != null) {
			Gizmos.DrawLine(transform.position, objectToActivate.transform.position);
		}
	}
}
