using UnityEngine;
using System.Collections;

public class SheepControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	bool facingRight = true;

	Animator anim;

	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	public float jumpForce = 700f;

	public Transform edgeCheckFront;
	public Transform edgeCheckBack;
	public Vector3 spawnPoint;
	bool onEdge = false;

	public Transform door;
	public int cameraManipulators = 0;

	void Start () {
		anim = GetComponent<Animator> ();
		spawnPoint = transform.position;
	}

	void FixedUpdate () {
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);
		if(!grounded && GetComponent<Rigidbody2D>().velocity.y <= 0) {
			onEdge = true;
			anim.SetBool ("onEdge", onEdge);
		}else if(grounded || GetComponent<Rigidbody2D>().velocity.y >= 0 || GetComponent<Rigidbody2D>().velocity.y <= -10){
			onEdge = false;
			anim.SetBool ("onEdge", onEdge);
		}
		anim.SetBool ("Ground", grounded);

		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);


		float move = Input.GetAxis ("Horizontal");

		GetComponent<Rigidbody2D>().velocity = new Vector2 (move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		anim.SetFloat ("Speed", Mathf.Abs(move));

		if (move < 0 && facingRight) flip ();
		else if (move > 0 && !facingRight) flip ();
	}

	void Update () {
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("Ground", false);
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (0, jumpForce));
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			GameObject.Find("_GM").GetComponent<MainComponent> ().toggleEffects();		
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			Camera.main.GetComponent<GlowEffect> ().newColor = new Color(Random.Range(0.0F,1.0F), Random.Range(0.0F,1.0F), Random.Range(0.0F,1.0F));		
		}
	}

	void flip() {
			facingRight = !facingRight;
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
	}

	public void turn(bool dir) {							//true -> turn right
		if(!facingRight && dir || facingRight && !dir) {
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;
		}
		facingRight = dir;
	}

	public IEnumerator Die() {
		float fadeTime = GameObject.Find ("_GM").GetComponent<MainComponent>().BeginFade(1);
		yield return new WaitForSeconds (fadeTime);
	}
}













