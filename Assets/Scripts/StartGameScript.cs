using UnityEngine;
using System.Collections;

public class StartGameScript : MonoBehaviour {
	private Vector3 newScale;

	public float extraScale = 0.1F;

	public bool shouldContinue = false;

	void Start() {
		newScale = transform.localScale;
	}

	void Update() {
		transform.localScale = Vector3.Lerp (transform.localScale, newScale, Time.deltaTime * 3F);
	}

	void OnMouseEnter () {
		newScale.x += extraScale;
		newScale.y += extraScale;
	}

	void OnMouseExit () {
		newScale.x -= extraScale;
		newScale.y -= extraScale;
	}

	void OnMouseDown () {
		newScale.x += extraScale;
		newScale.y += extraScale;

		if(!shouldContinue) {
			Application.LoadLevel (Application.loadedLevel + 1);
		}else {

		}
	}

	void OnMouseUp () {
		newScale.x -= extraScale;
		newScale.y -= extraScale;
	}
}
