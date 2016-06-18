using UnityEngine;
using System.Collections;

public class EntityBehaviour : MonoBehaviour {

	public bool isAlive;
	public float dialogueTimer = 5F;
	public string[] toSay = new string[20];
	private GUIText EntityTextObject;
	public string currentStr;
	Animator anim;


	// Use this for initialization
	void Start () {
		if (isAlive && GetComponent<Animator> () != null) {
			anim = GetComponent<Animator> ();	
		}
		else {
			anim = null;
		}
		EntityTextObject = GetComponentInChildren<GUIText> ();
	}

	public void Say(int index, float writeSpeed) {
		StartCoroutine (WriteText (index, writeSpeed));
	}

	void Update() {
		if(currentStr != "") EntityTextObject.text = currentStr;
	}

	private IEnumerator WriteText(int index, float writeSpeed) {
		if (true) {
			StartCoroutine(AnimateText(toSay[index], writeSpeed));
			yield return new WaitForSeconds ((writeSpeed * toSay[index].Length) + dialogueTimer);
			currentStr = "";
			EntityTextObject.text = "";
		}
	}

	public IEnumerator AnimateText(string strComplete, float writeSpeed) {
		int i = 0;
		float tmp = 0F;
		if(anim != null) tmp = anim.speed;
		currentStr = "";
		while (i < strComplete.Length) {
			currentStr += strComplete[i++];
			yield return new WaitForSeconds(writeSpeed);
		}
		if(anim != null) anim.speed = tmp;
	}

	private IEnumerator AnimateTalkMovement () {
		return null;
	}
}
