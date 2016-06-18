
using System.Collections;
using UnityEngine;

public class FadeText : MonoBehaviour
{
	public float fadeDuration = 3.0f;
	
	private void Start ()
	{
		StartCoroutine(StartFading());
	}

	void OnGUI() {
		GUI.depth = GameObject.Find ("_GM").GetComponent<MainComponent> ().drawDepth - 10;
	}
	
	private IEnumerator StartFading()
	{
		yield return StartCoroutine(Fade(0.0f, 1.0f, fadeDuration));
		yield return StartCoroutine(Fade(1.0f, 0.0f, fadeDuration));
		Destroy(gameObject);
	}
	
	private IEnumerator Fade (float startLevel, float endLevel, float time)
	{
		float speed = 1.0f/time;
		
		for (float t = 0.0f; t < 1.0; t += Time.deltaTime*speed)
		{
			float a = Mathf.Lerp(startLevel, endLevel, t);
			GetComponent<GUIText>().font.material.color = new Color(GetComponent<GUIText>().font.material.color.r,
			GetComponent<GUIText>().font.material.color.g,
			GetComponent<GUIText>().font.material.color.b, a);
			GUI.depth = -10;
			yield return 0;
		}
	}
}