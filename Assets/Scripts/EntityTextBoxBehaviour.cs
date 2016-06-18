using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class EntityTextBoxBehaviour : MonoBehaviour {

	public GameObject Entity;
	public float extraY;
	public float extraX;
	
	void Update () {
		transform.position = Camera.main.WorldToViewportPoint (new Vector3(Entity.transform.position.x + extraX, Entity.transform.position.y + extraY, Entity.transform.position.z));
	}
}
