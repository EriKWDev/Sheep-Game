using UnityEngine;
using System.Collections;

public class LetterBehaviour : MonoBehaviour {
	private Vector3 posUp;
	private Vector3 posDown;
	private float value;
	private float originalValue = 0.5F;
	public float value2 = 0.3F;
	private bool go = false;

	void Start() {
		value = originalValue;
		if (Random.Range (0, 2) == 1) {
			posUp = new Vector3 (transform.position.x, transform.position.y + value2, transform.position.z);
			posDown = new Vector3 (transform.position.x, transform.position.y - value2, transform.position.z);	
		}
		else {
			posUp = new Vector3 (transform.position.x, transform.position.y - value2, transform.position.z);
			posDown = new Vector3 (transform.position.x, transform.position.y + value2, transform.position.z);
		}
	}

	void Update () {
		value -= 1F * Time.deltaTime;
		if (value <= 0F) {
			go = !go;
			value = originalValue;
		}
		if(go) transform.position = Vector3.Lerp (transform.position, posUp, Time.deltaTime * 0.5F);
		if(!go) transform.position = Vector3.Lerp (transform.position, posDown, Time.deltaTime * 0.5F);
	}
}
