using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ManipulateCamera : MonoBehaviour {

	private string originalObjectTag;
	public float originalCameraLerp;
	private float offsetToCenter;
	public bool isInside = false;

	public GameObject newObject;

	public bool addObject;
	public bool cinematic;
	public float offset = -0.5F;

	public float zoomValue;

	void Start() {
		if (addObject) {
			originalObjectTag = newObject.tag;
		}
	}

	void OnDrawGizmos() {
		if (addObject) {
			if (isInside) Gizmos.color = Color.magenta;
			if (!isInside) Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (transform.position, GetComponent<CircleCollider2D> ().radius);
			if (isInside) {
				Gizmos.color = Color.green;
				Gizmos.DrawLine(transform.position, newObject.transform.position);
				if(newObject != null) Gizmos.DrawWireSphere (newObject.transform.position, 1F);		
			}		
		}
	}

	void OnDrawGizmosSelected() {
		if (addObject) {
			Gizmos.color = Color.green;
			if(newObject != null) Gizmos.DrawWireSphere (newObject.transform.position, 1F);
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere (transform.position, GetComponent<CircleCollider2D> ().radius);		
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject == GameObject.Find ("Character")) {
			isInside = true;
			if (addObject) {
				newObject.tag = "ToBeSeen";
				Camera.main.GetComponent<SmoothFollow> ().addMousePos = false;
				originalCameraLerp = Camera.main.GetComponent<SmoothFollow> ().lerpSpeed;
				Camera.main.GetComponent<SmoothFollow> ().lerpSpeed = 3F;
				Camera.main.GetComponent<SmoothFollow> ().gap += offset;
				other.GetComponent<SheepControllerScript> ().cameraManipulators += 1;
				Camera.main.GetComponent<FitToScreen> ().object1 = other.gameObject;
				Camera.main.GetComponent<FitToScreen> ().object2 = newObject;
				newObject.tag = "ToBeSeen";
			}
			else if(cinematic) {
				StartCoroutine(GetComponent<Cinematic> ().BeginCinematic());
			}
		}
	}	

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject == GameObject.Find ("Character")) {
			isInside = false;
			if (addObject) {
				newObject.tag = originalObjectTag;
				Camera.main.GetComponent<SmoothFollow> ().gap -= offset;
				other.GetComponent<SheepControllerScript> ().cameraManipulators -= 1;
				if(Camera.main.GetComponent<FitToScreen> ().object1 == other.gameObject && Camera.main.GetComponent<FitToScreen> ().object2 == newObject) {
					Camera.main.GetComponent<FitToScreen> ().object1 = null;
					Camera.main.GetComponent<FitToScreen> ().object2 = null;
				}
				if(other.GetComponent<SheepControllerScript> ().cameraManipulators <= 0) {
					Camera.main.GetComponent<SmoothFollow> ().addMousePos = true;
					Camera.main.GetComponent<SmoothFollow> ().lerpSpeed = originalCameraLerp;
					other.GetComponent<SheepControllerScript> ().cameraManipulators = 0;
				}
			}
		}
	}

	public void fakeEnter(Collider2D other) {
		if (other.gameObject == GameObject.Find ("Character")) {
			isInside = true;
			if (addObject) {
				newObject.tag = "ToBeSeen";
				Camera.main.GetComponent<SmoothFollow> ().addMousePos = false;
				originalCameraLerp = Camera.main.GetComponent<SmoothFollow> ().lerpSpeed;
				Camera.main.GetComponent<SmoothFollow> ().lerpSpeed = 3F;
				Camera.main.GetComponent<SmoothFollow> ().gap += offset;
				other.GetComponent<SheepControllerScript> ().cameraManipulators += 1;
				Camera.main.GetComponent<FitToScreen> ().object1 = other.gameObject;
				Camera.main.GetComponent<FitToScreen> ().object2 = newObject;
				newObject.tag = "ToBeSeen";
			}
			else if(cinematic) {
				StartCoroutine(GetComponent<Cinematic> ().BeginCinematic());
			}
		}
	}

	public void fakeExit(Collider2D other) {
		if (other.gameObject == GameObject.Find ("Character")) {
			isInside = false;
			if (addObject) {
				newObject.tag = originalObjectTag;
				Camera.main.GetComponent<SmoothFollow> ().gap -= offset;
				other.GetComponent<SheepControllerScript> ().cameraManipulators -= 1;
				if(Camera.main.GetComponent<FitToScreen> ().object1 == other.gameObject && Camera.main.GetComponent<FitToScreen> ().object2 == newObject) {
					Camera.main.GetComponent<FitToScreen> ().object1 = null;
					Camera.main.GetComponent<FitToScreen> ().object2 = null;
				}
				if(other.GetComponent<SheepControllerScript> ().cameraManipulators <= 0) {
					Camera.main.GetComponent<SmoothFollow> ().addMousePos = true;
					Camera.main.GetComponent<SmoothFollow> ().lerpSpeed = originalCameraLerp;
					other.GetComponent<SheepControllerScript> ().cameraManipulators = 0;
				}
			}
		}
	}
}



















