using UnityEngine;
using System.Collections;

public class Cinematic : MonoBehaviour {

	public GameObject[] Points;
	public float secondsPerPoint;
	public float secondsForLastPoint = 4F;
	public bool onlyOnce = true;
	public bool hasAlreadyDone = false;
	public bool isInCinematic = false;

	void Update () {
	
	}

	void OnDrawGizmos() {
		if (gameObject.GetComponent<ManipulateCamera>().isInside) Gizmos.color = Color.magenta;
		if (!gameObject.GetComponent<ManipulateCamera>().isInside) Gizmos.color = Color.red;
		if(GetComponent<CircleCollider2D> () != null) Gizmos.DrawWireSphere (transform.position, GetComponent<CircleCollider2D> ().radius);

		if (gameObject.GetComponent<ManipulateCamera>().isInside) {
			Gizmos.color = Color.green;
			for(int i = 0; i < Points.Length; i++) {
				if(Points[i] != null) Gizmos.DrawWireSphere (Points[i].transform.position, 1F);	
				if(i == 0) Gizmos.DrawLine(transform.position, Points[i].transform.position);
				else Gizmos.DrawLine(Points[i-1].transform.position, Points[i].transform.position);
			}
		}
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.green;
		for(int i = 0; i < Points.Length; i++) {
			if(i == 0) Gizmos.DrawLine(transform.position, Points[i].transform.position);
			else Gizmos.DrawLine(Points[i-1].transform.position, Points[i].transform.position);
			if(Points[i] != null) Gizmos.DrawWireSphere (Points[i].transform.position, 1F);	
		}
		Gizmos.color = Color.red;
		if(GetComponent<CircleCollider2D> () != null) Gizmos.DrawWireSphere (transform.position, GetComponent<CircleCollider2D> ().radius);
	}

	public IEnumerator BeginCinematic() {
		if (!hasAlreadyDone && !isInCinematic) {
			GameObject.FindGameObjectWithTag ("Cursor").GetComponent<CursorBehaviour> ().showCursor = false;
			GameObject character = GameObject.Find("Character");
			isInCinematic = true;

			Camera.main.GetComponent<SmoothFollow> ().addMousePos = false;
			gameObject.GetComponent<ManipulateCamera>().originalCameraLerp = Camera.main.GetComponent<SmoothFollow> ().lerpSpeed;
			Camera.main.GetComponent<SmoothFollow> ().lerpSpeed = 1F;
			Camera.main.GetComponent<SmoothFollow> ().gap += gameObject.GetComponent<ManipulateCamera>().offset;
			character.GetComponent<SheepControllerScript> ().tag = "Default";
			foreach (GameObject o in Points) {
				o.tag = "ToBeSeen";
				if(System.Array.IndexOf(Points, o) == Points.Length-1) yield return new WaitForSeconds (secondsForLastPoint);
				else yield return new WaitForSeconds (secondsPerPoint);
				o.tag = "Default";
			}	
			character.GetComponent<SheepControllerScript> ().tag = "ToBeSeen";
			Camera.main.GetComponent<SmoothFollow> ().addMousePos = true;
			Camera.main.GetComponent<SmoothFollow> ().lerpSpeed = gameObject.GetComponent<ManipulateCamera>().originalCameraLerp ;
			Camera.main.GetComponent<SmoothFollow> ().gap -= gameObject.GetComponent<ManipulateCamera>().offset;
			isInCinematic = false;
		}

		if (onlyOnce && !hasAlreadyDone) {
			hasAlreadyDone = true;
		}
	}

	public void Activate() {
		StartCoroutine(GetComponent<Cinematic> ().BeginCinematic());
	}
}
