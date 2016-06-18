using UnityEngine;
using System.Collections;

public class OnCollision : MonoBehaviour {

	void Start () {
	}
	
	IEnumerator OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject == GameObject.Find ("Character")) {

			Camera.main.GetComponent<GlowEffect> ().realIntensity = 10F;

			yield return new WaitForSeconds (0.5F);

			other.gameObject.transform.position = other.gameObject.GetComponent<SheepControllerScript> ().spawnPoint;
			Camera.main.GetComponent<GlowEffect> ().realIntensity = 1.2F;
		}
	}
}
