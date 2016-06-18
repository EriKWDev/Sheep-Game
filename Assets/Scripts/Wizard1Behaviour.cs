using UnityEngine;
using System.Collections;

public class Wizard1Behaviour : MonoBehaviour {

	Animator anim;
	public int timesActivated = 0;

	void Start() {
		anim = GetComponent<Animator> ();
		anim.SetBool ("BeSad", true);
		GetComponent<NPCBehaviour> ().turnAtAction = false;
		GetComponent<NPCBehaviour> ().turn (true);
	}

	public IEnumerator Activate() {
		Destroy (GetComponent<Interactable> ().letter);
		GetComponent<Interactable> ().summonFloatingSprite = false;
		EntityBehaviour eb = GetComponent<EntityBehaviour> ();
		GetComponent<NPCBehaviour> ().turnAtAction = false;
		gameObject.tag = "ToBeSeen";
		timesActivated++;

		if (timesActivated == 1) {
			anim.SetBool ("BeSad", false);
			yield return new WaitForSeconds (0.8F);
			GetComponent<NPCBehaviour> ().turn (false);
			yield return new WaitForSeconds (0.2F);

			eb.Say (1, 0.02F);
			yield return new WaitForSeconds ((0.02F * eb.toSay[1].Length) + eb.dialogueTimer);
			
			eb.Say (2, 0.02F);
			yield return new WaitForSeconds ((0.02F * eb.toSay[2].Length) + eb.dialogueTimer);
			
			eb.Say (3, 0.02F);
			yield return new WaitForSeconds ((0.02F * eb.toSay[3].Length) + eb.dialogueTimer);
			
			eb.Say (4, 0.1F);
			yield return new WaitForSeconds ((0.1F * eb.toSay[4].Length) + eb.dialogueTimer);
			
			eb.Say (5, 0.1F);
			yield return new WaitForSeconds ((0.1F * eb.toSay[5].Length) + eb.dialogueTimer);
			
			eb.Say (6, 0.1F);
			yield return new WaitForSeconds ((0.1F * eb.toSay[6].Length) + eb.dialogueTimer);
		}

		if (timesActivated > 1) {
			eb.Say (7, 0.02F);
			yield return new WaitForSeconds ((0.02F * eb.toSay[7].Length) + eb.dialogueTimer);

			eb.Say (8, 0.02F);
			yield return new WaitForSeconds ((0.02F * eb.toSay[8].Length) + eb.dialogueTimer);

			eb.Say (9, 0.02F);
			yield return new WaitForSeconds ((0.02F * eb.toSay[9].Length) + eb.dialogueTimer);

			GetComponent<NPCBehaviour> ().turn (true);
			yield return new WaitForSeconds (0.5F);

			GetComponent<Animator> ().SetBool ("Special", true);
			eb.Say (10, 0.03F);
			yield return new WaitForSeconds ((0.03F * eb.toSay[10].Length) + eb.dialogueTimer);

			eb.Say (11, 0.02F);
			yield return new WaitForSeconds ((0.02F * eb.toSay[11].Length) + eb.dialogueTimer);

			eb.Say (12, 0.02F);
			yield return new WaitForSeconds ((0.02F * eb.toSay[12].Length) + eb.dialogueTimer);
		}




		gameObject.tag = "Default";
		GetComponent<Animator> ().SetBool ("Special", false);
		GetComponent<Interactable> ().summonFloatingSprite = true;
		GetComponent<NPCBehaviour> ().turnAtAction = true;
	}
}
