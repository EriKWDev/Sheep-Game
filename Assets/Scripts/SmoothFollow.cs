using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SmoothFollow : MonoBehaviour {

	public GameObject[] toBeSeen;
	private Vector3 Center;

	public float gap;
	public float lerpSpeed = 1F;

	public bool addMousePos = true;
	public bool follow = true;

	void OnDrawGizmos() {
		Gizmos.color = Color.gray;
		Gizmos.DrawWireSphere (new Vector3(transform.position.x, transform.position.y, toBeSeen[0].transform.position.z), 1F);
		Gizmos.DrawWireSphere (new Vector3(Center.x, Center.y, toBeSeen[0].transform.position.z), 0.6F);
		Gizmos.DrawLine (new Vector3(transform.position.x, transform.position.y, toBeSeen[0].transform.position.z), new Vector3(Center.x, Center.y, toBeSeen[0].transform.position.z));

		Gizmos.color = Color.cyan;
		foreach (GameObject o in toBeSeen) {
			Gizmos.DrawWireSphere (o.transform.position, 0.6F);
			Gizmos.DrawLine (new Vector3(transform.position.x, transform.position.y, toBeSeen[0].transform.position.z), o.transform.position);
		}
	}
	
	void Update () {
		if (follow) {
			toBeSeen = GameObject.FindGameObjectsWithTag ("ToBeSeen");
			
			Vector3 tmpPos = Vector3.zero;
			Center = Vector3.zero;

			foreach (GameObject o in toBeSeen) {
				tmpPos += o.transform.position;		
			}

			if (addMousePos == true) {
				GameObject character = GameObject.Find ("Character");
				Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				Vector3 characterPos = character.transform.position;
				mousePos = new Vector3(Mathf.Clamp(mousePos.x, characterPos.x-3F, characterPos.x+3F), Mathf.Clamp(mousePos.y, characterPos.y-3F, characterPos.y+3F), mousePos.z);
				tmpPos += mousePos;
			}
			
			Center.x = tmpPos.x / (toBeSeen.Length + ((addMousePos) ? 1 : 0)); 			//+1 If the mouse also counts
			Center.y = tmpPos.y / (toBeSeen.Length + ((addMousePos) ? 1 : 0)) + 2F; 	// -||-
			Center.y = Center.y + gap;
			Center.z = Camera.main.transform.position.z;
			
	 		Camera.main.transform.position = Vector3.Lerp (Camera.main.transform.position, Center, Time.deltaTime * lerpSpeed);		
		}
	}
}
