using UnityEngine;
using System.Collections;

public class Emission : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Horizontal") != 0.0f && GameObject.Find("Character").GetComponent<SheepControllerScript> ().grounded == true) {
			GetComponent<ParticleSystem>().emissionRate = 10;
		}else{
			GetComponent<ParticleSystem>().emissionRate = 0;
		}
	}
}
