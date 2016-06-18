using UnityEngine;
using System.Collections;

public class MainComponent : MonoBehaviour {

	public bool debug = false;

	public bool shouldFade = false;

	public Texture2D fadeOutTexture;	// the texture that will overlay the screen. This can be a black image or a loading graphic
	public float fadeSpeed = 0.05f;		// the fading speed
	public int fadeDelay = 2; 

	public int drawDepth = -1000;			// the texture's order in the draw hierarchy: a low number means it renders on top
	public float alpha = 1.0f;			// the texture's alpha value between 0 and 1
	public int fadeDir = -1;			// the direction to fade: in = -1 or out = 1

	public int playerHealth;
	public GameObject cursorObject;

	void Start(){
		Cursor.visible = !debug;
		cursorObject.GetComponent<CursorBehaviour> ().showCursor = debug;
		Instantiate (cursorObject);
	}

	void OnGUI()
	{
		if (!shouldFade)	GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);

		if (!shouldFade) wait ();
		// fade out/in the alpha value using a direction, a speed and Time.deltaTime to convert the operation to seconds
		if(shouldFade){
			alpha += fadeDir * fadeSpeed * Time.deltaTime;
			// force (clamp) the number to be between 0 and 1 because GUI.color uses Alpha values between 0 and 1
			alpha = Mathf.Clamp01(alpha);

			// set color of our GUI (in this case our texture). All color values remain the same & the Alpha is set to the alpha variable
			GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
			GUI.depth = drawDepth;																// make the black texture render on top (drawn last)
			GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);		// draw the texture to fit the entire screen area
		}
	}

	// sets fadeDir to the direction parameter making the scene fade in if -1 and out if 1
	public float BeginFade (int direction)
	{
		if (direction == 1) {
			fadeSpeed *= 10;
		}
		fadeDir = direction;
		return (fadeSpeed);
	}
	
	void OnLevelWasLoaded()
	{
		// alpha = 1;		// use this if the alpha is not set to 1 by default
		playerHealth = 120;	//Player Health is 120 by default (Maybe boosts can add some health later?)

		BeginFade(-1);		// call the fade in function
	}

	public void toggleEffects() {
		Camera.main.GetComponent<GlowEffect> ().enabled = !Camera.main.GetComponent<GlowEffect> ().enabled;
	}

	public void wait(){
		if(Input.anyKey) shouldFade = true;
	}

	public int GetPlayerHealth(){
		return playerHealth;
	}

	public void SetPlayerHealth(int newPlayerHealth){
		playerHealth = newPlayerHealth;
	}
}
