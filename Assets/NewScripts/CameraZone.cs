using UnityEngine;
using System.Collections;

[AddComponentMenu("ErikWDev/2D Camera Zone")]
[ExecuteInEditMode]
public class CameraZone : MonoBehaviour {

	[Header("Debugging")]
	public bool debug = false;
	[SerializeField]
	private Color zoneColor = Color.red;

	[Header("Camera Zone Settings")]
	public CameraController.CameraStates zoneEffect;
	public bool requireKeyToActivate = false;
	public KeyCode keyToActivate = KeyCode.C;
	[SerializeField]
	private GameObject letter;

	private bool isSquare = false;
	private bool isCircle = false;
	private Collider2D currentTriggerCollider = null;
	private bool playerIsInside = false;

	[Header("Camera Zone Settings : StickToNewTarget")]
	public GameObject newTargetToStickTo;

	void Start () {
		isSquare = (GetComponent<BoxCollider2D> () != null && GetComponent<BoxCollider2D> ().isActiveAndEnabled ? true : false);
		isCircle = (GetComponent<CircleCollider2D> () != null && GetComponent<CircleCollider2D> ().isActiveAndEnabled ? true : false);
		if (requireKeyToActivate) {
			letter.SetActive (false);
		}
	}

	void Update() {
		setValues (currentTriggerCollider, playerIsInside);
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.tag == "Player") {
			playerIsInside = true;
			currentTriggerCollider = other;
		}
	}

	void OnTriggerStay2D (Collider2D other) {
		if (other.tag == "Player") {
			playerIsInside = true;
			currentTriggerCollider = other;
		}
	}

	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Player") {
			playerIsInside = false;
			currentTriggerCollider = other;
		}
	}
		
	private void setValues(Collider2D playerCollider, bool isInside) {
		if (playerCollider == null) {
			return;
		}
		if (requireKeyToActivate) {
			if (isInside) {
				letter.transform.position = playerCollider.gameObject.transform.position + Vector3.up * 1.2f;
				letter.SetActive (true);
			}
			if (isInside && Input.GetKey (keyToActivate) || !isInside) {
				letter.SetActive (false);
			}
		}


		switch (zoneEffect) {
		case CameraController.CameraStates.None:
		default:
			break;

		case CameraController.CameraStates.StickToNewTarget:
			if (debug) {
				Debug.DrawLine (transform.position, newTargetToStickTo.transform.position, zoneColor);
			}
			if (requireKeyToActivate) {
				playerCollider.GetComponent<SheepControllerScript> ().myCamera.stickToNewTarget = (isInside && Input.GetKey (keyToActivate) ? true : true);
				playerCollider.GetComponent<SheepControllerScript> ().myCamera.newTargetToStickTo = (isInside && Input.GetKey (keyToActivate) ? newTargetToStickTo : null);
			} else {
				playerCollider.GetComponent<SheepControllerScript> ().myCamera.stickToNewTarget = (isInside ? true : true);
				playerCollider.GetComponent<SheepControllerScript> ().myCamera.newTargetToStickTo = (isInside ? newTargetToStickTo : null);
			}
			break;
		}
	}

	void OnDrawGizmos () {
		if (debug) {
			Gizmos.color = zoneColor;
			isSquare = (GetComponent<BoxCollider2D> () != null && GetComponent<BoxCollider2D> ().isActiveAndEnabled ? true : false);
			isCircle = (GetComponent<CircleCollider2D> () != null && GetComponent<CircleCollider2D> ().isActiveAndEnabled ? true : false);
			if (isSquare) {
				Gizmos.DrawWireCube (transform.position, new Vector3(GetComponent<BoxCollider2D> ().size.x, GetComponent<BoxCollider2D> ().size.y, 1f));
			} else if (isCircle) {
				Gizmos.DrawWireSphere (transform.position, GetComponent<CircleCollider2D> ().radius);
			}

			if (newTargetToStickTo != null) {
				Gizmos.DrawWireSphere (newTargetToStickTo.transform.position, 1f);
			}
		}
	}
}
