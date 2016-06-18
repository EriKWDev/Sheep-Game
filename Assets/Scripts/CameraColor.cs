using UnityEngine;
using System.Collections;

public class CameraColor : MonoBehaviour {
	public Color color1 = Color.red;
	public Color color2 = Color.blue;
	public float duration = 3.0F;
	public bool colorSwitch = false;

	public Transform player;

	void Start() {
		Example ();
	}

	void Update() {
		if(colorSwitch) {
			float t = Mathf.PingPong(Time.time, duration) / duration;
			GetComponent<Camera>().backgroundColor = Color.Lerp(color1, color2, t);
		}
	}

	void Example() {
		GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
	}
}
