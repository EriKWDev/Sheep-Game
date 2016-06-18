using UnityEngine;
using System.Collections;

public class NPCBehaviour : MonoBehaviour {

	public float timeUntilNextMove = 0F;
	public float maxTimeUntilAction;
	public float minTimeUntilAction;

	public bool turnAtAction = true;
	public bool walkRandom = false;
	public bool specialAnimation = true;
	public int extraAnimationActionProbability = 5;
	public bool facingRight = true;

	private bool currentAnimActionBool = false;
	private bool currentAnimSpecialBool = false;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.SetBool("Action", currentAnimActionBool);
		timeUntilNextMove = Random.Range (minTimeUntilAction, maxTimeUntilAction);
	}
	
	// Update is called once per frame
	void Update () {
		timeUntilNextMove -= Time.deltaTime;
		if (timeUntilNextMove < 0) {
			Action();
			timeUntilNextMove = Random.Range (minTimeUntilAction, maxTimeUntilAction + 1F);
		}
	}

	public void Action() {
		if (turnAtAction) {
			if(Random.Range(0, 2) == 1) {
				flip ();
			}
			if(Random.Range(0, extraAnimationActionProbability) == 1) {
				currentAnimActionBool = !currentAnimActionBool;
				anim.SetBool("Action", currentAnimActionBool);
			}
		}
	}

	public void Special() {
		if (specialAnimation) {
			currentAnimSpecialBool = !currentAnimSpecialBool;
			anim.SetBool("Special", currentAnimSpecialBool);		
		}
	}

	public void flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	public void turn(bool dir) {				//true -> turn right
		if(!facingRight && dir || facingRight && !dir) {
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
		facingRight = dir;
	}
}
