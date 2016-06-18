using UnityEngine;
using System.Collections;

public class InformativeSignBehaviour : MonoBehaviour {
	public IEnumerator Activate() {
		Destroy (GetComponent<Interactable> ().letter);
		GetComponent<Interactable> ().summonFloatingSprite = false;
		EntityBehaviour eb = GetComponent<EntityBehaviour> ();
		gameObject.tag = "ToBeSeen";
		
		for (int i = 0; i < eb.toSay.Length; i++) {
			eb.Say (i, 0.02F);		
			yield return new WaitForSeconds ((0.02F * eb.toSay[i].Length) + eb.dialogueTimer);
		}

		
		gameObject.tag = "Default";
		GetComponent<Interactable> ().summonFloatingSprite = true;
	}
}
