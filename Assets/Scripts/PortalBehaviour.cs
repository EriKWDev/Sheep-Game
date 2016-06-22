using UnityEngine;
using System.Collections;

public class PortalBehaviour : MonoBehaviour {

	public GameObject linkedPortal;
	public bool onlyReceive;
	Animator anim;

	void Start () {
		anim = GetComponentInChildren<Animator> ();
	}

	void Update () {
		if(onlyReceive) {
			GetComponent<Interactable> ().summonFloatingSprite = false;
		}
	}

	void Activate() {
		if(!onlyReceive) {
			StartCoroutine("Teleport");
		}
		else if(onlyReceive) {
			
		}
	}

	private IEnumerator Teleport() {
		GameObject character = GameObject.Find ("Character");
		Camera.main.GetComponent<SmoothFollow> ().addMousePos = false;
		character.tag = "Default";
		gameObject.tag = "ToBeSeen";
		anim.SetBool ("Teleporting", true);

		yield return new WaitForSeconds (0.3F);

		Camera.main.GetComponent<SmoothFollow> ().follow = false;
		character.GetComponent<SpriteRenderer> ().enabled = false;

		yield return new WaitForSeconds (0.6F);

		character.transform.position = linkedPortal.transform.position + new Vector3 (0f, 0.5f, 0f);
		gameObject.tag = "Default";
		character.tag = "ToBeSeen";
		anim.SetBool ("Teleporting", false);
		character.GetComponent<SpriteRenderer> ().enabled = true;
		Camera.main.GetComponent<SmoothFollow> ().addMousePos = true;
		Camera.main.GetComponent<SmoothFollow> ().follow = true;
	}
}
