using UnityEngine;
using System.Collections;

public class LetterBehaviour : MonoBehaviour {
	[SerializeField]
	private Vector3 posUp;
	[SerializeField]
	private Vector3 posDown;
	[SerializeField]
	private float value;
	[SerializeField]
	private float originalValue = 0.5F;
	[SerializeField]
	private float value2 = 0.3F;
	[SerializeField]
	private bool go = false;

	void Start() {
		value = originalValue;
		if (Random.Range (0, 2) == 1) {
			posUp = new Vector3 (transform.localPosition.x, transform.localPosition.y + value2, transform.localPosition.z);
			posDown = new Vector3 (transform.localPosition.x, transform.localPosition.y - value2, transform.localPosition.z);	
		}
		else {
			posUp = new Vector3 (transform.localPosition.x, transform.localPosition.y - value2, transform.localPosition.z);
			posDown = new Vector3 (transform.localPosition.x, transform.localPosition.y + value2, transform.localPosition.z);
		}
	}

	void Update () {
		value -= Time.deltaTime;

		if (value <= 0F) {
			go = !go;
			value = originalValue;
		}

		if(go) transform.localPosition = Vector3.Lerp (transform.localPosition, posUp, Time.deltaTime * 0.5F);
		if(!go) transform.localPosition = Vector3.Lerp (transform.localPosition, posDown, Time.deltaTime * 0.5F);
	}

	public void resetValues () {
		value = originalValue;
		if (Random.Range (0, 2) == 1) {
			posUp = new Vector3 (transform.localPosition.x, transform.localPosition.y + value2, transform.localPosition.z);
			posDown = new Vector3 (transform.localPosition.x, transform.localPosition.y - value2, transform.localPosition.z);	
		}
		else {
			posUp = new Vector3 (transform.localPosition.x, transform.localPosition.y - value2, transform.localPosition.z);
			posDown = new Vector3 (transform.localPosition.x, transform.localPosition.y + value2, transform.localPosition.z);
		}
	}
}
