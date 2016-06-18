using UnityEngine;
using System.Collections;

public class CandleLightBehaviour : MonoBehaviour {

	public float maxTimeUntilAction = 10F;
	public float minTimeUntilAction = 5F;
	public float timeUntilNextMove = 0F;
	Animator anim;
	
	void Start () {
		anim = GetComponent<Animator> ();
	}

	void Update () {
		timeUntilNextMove -= Time.deltaTime;
		if (timeUntilNextMove < 0) {

			anim.SetInteger ("State", Random.Range(0, 3));
			timeUntilNextMove = Random.Range (minTimeUntilAction, maxTimeUntilAction + 1F);
		}
	}
}
