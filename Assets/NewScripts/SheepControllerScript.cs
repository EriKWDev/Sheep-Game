using UnityEngine;
using System.Collections;

[AddComponentMenu("ErikWDev/Sheep Controller Script")]
[DisallowMultipleComponent]
public class SheepControllerScript : MonoBehaviour {

	[SerializeField]
	private float maxSpeed = 10f;
	[SerializeField]
	private bool facingRight = true;

	private Animator anim;

	public bool grounded = false;
	[SerializeField]
	private Transform groundCheck;
	[SerializeField]
	private float groundRadius = 0.2f;
	public LayerMask whatIsGround;
	[SerializeField]
	private float jumpForce = 700f;

	[SerializeField]
	private Transform edgeCheckFront;
	[SerializeField]
	private Transform edgeCheckBack;
	public Vector3 spawnPoint;
	[SerializeField]
	private bool onEdge = false;
	public CameraController myCamera;

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













