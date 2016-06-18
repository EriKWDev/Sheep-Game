using UnityEngine;
using System.Collections;
using System.Linq;

public class FitToScreen : MonoBehaviour {

	public GameObject object1;
	public GameObject object2;
	public GameObject[] toBeSeen;
	public Vector3[] toBeSeenPositions;
	public float orthographicSize = 10f;
	public float defaultOrthographicSize;
	public float extra = 1.6F;


	// Use this for initialization
	void Start () {
		defaultOrthographicSize = 18F * Screen.height / Screen.width * 0.5F;
	}
	
	// Update is called once per frame
	void Update () {
		defaultOrthographicSize = 18F * Screen.height / Screen.width * 0.5F;
		if (object1 != null) {

			toBeSeen = GameObject.FindGameObjectsWithTag ("ToBeSeen");
			toBeSeenPositions = new Vector3[toBeSeen.Length];

			for (int i = 0; i< toBeSeen.Length; i++) {
				toBeSeenPositions[i] = toBeSeen[i].transform.position;
			}

			SortByDistances (toBeSeenPositions, object1.transform.position);

			orthographicSize = CalculateHyp3 (toBeSeenPositions[0], toBeSeenPositions[toBeSeenPositions.Length-1]);

			Debug.DrawLine(object1.transform.position, toBeSeenPositions[toBeSeenPositions.Length-1], Color.yellow);
			
			if ((defaultOrthographicSize+(orthographicSize/2)) > defaultOrthographicSize) Camera.main.orthographicSize = Mathf.Lerp (Camera.main.orthographicSize, defaultOrthographicSize+(orthographicSize/2), Time.deltaTime * 3F);		
			else Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, defaultOrthographicSize, Time.deltaTime * 3F);
		}
		else {
			Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, defaultOrthographicSize, Time.deltaTime * 3F);
		}
	}

	private float CalculateHyp (GameObject o1, GameObject o2) {
		if(o1.transform.position.x > o2.transform.position.x && o1.transform.position.y > o2.transform.position.y) return Mathf.Sqrt (((o1.transform.position.x-o2.transform.position.x)*(o1.transform.position.x-o2.transform.position.x))+((o1.transform.position.y-o2.transform.position.y)*(o1.transform.position.y-o2.transform.position.y)));
		if(o2.transform.position.x > o1.transform.position.x && o1.transform.position.y > o2.transform.position.y) return Mathf.Sqrt (((o2.transform.position.x-o1.transform.position.x)*(o2.transform.position.x-o1.transform.position.x))+((o1.transform.position.y-o2.transform.position.y)*(o1.transform.position.y-o2.transform.position.y)));
		if(o1.transform.position.x > o2.transform.position.x && o2.transform.position.y > o1.transform.position.y) return Mathf.Sqrt (((o1.transform.position.x-o2.transform.position.x)*(o1.transform.position.x-o2.transform.position.x))+((o2.transform.position.y-o1.transform.position.y)*(o2.transform.position.y-o1.transform.position.y)));
		if(o2.transform.position.x > o1.transform.position.x && o2.transform.position.y > o1.transform.position.y) return Mathf.Sqrt (((o2.transform.position.x-o1.transform.position.x)*(o2.transform.position.x-o1.transform.position.x))+((o2.transform.position.y-o1.transform.position.y)*(o2.transform.position.y-o1.transform.position.y)));
		return 0.0F;
	}

	private float CalculateHyp2 (GameObject o1, Vector3 o2) {
		if(o1.transform.position.x > o2.x && o1.transform.position.y > o2.y) return Mathf.Sqrt (((o1.transform.position.x-o2.x)*(o1.transform.position.x-o2.x))+((o1.transform.position.y-o2.y)*(o1.transform.position.y-o2.y)));
		if(o2.x > o1.transform.position.x && o1.transform.position.y > o2.y) return Mathf.Sqrt (((o2.x-o1.transform.position.x)*(o2.x-o1.transform.position.x))+((o1.transform.position.y-o2.y)*(o1.transform.position.y-o2.y)));
		if(o1.transform.position.x > o2.x && o2.y > o1.transform.position.y) return Mathf.Sqrt (((o1.transform.position.x-o2.x)*(o1.transform.position.x-o2.x))+((o2.y-o1.transform.position.y)*(o2.y-o1.transform.position.y)));
		if(o2.x > o1.transform.position.x && o2.y > o1.transform.position.y) return Mathf.Sqrt (((o2.x-o1.transform.position.x)*(o2.x-o1.transform.position.x))+((o2.y-o1.transform.position.y)*(o2.y-o1.transform.position.y)));
		return 0.0F;
	}

	private float CalculateHyp3 (Vector3 o1, Vector3 o2) {
		if(o1.x > o2.x && o1.y > o2.y) return Mathf.Sqrt (((o1.x-o2.x)*(o1.x-o2.x))+((o1.y-o2.y)*(o1.y-o2.y)));
		if(o2.x > o1.x && o1.y > o2.y) return Mathf.Sqrt (((o2.x-o1.x)*(o2.x-o1.x))+((o1.y-o2.y)*(o1.y-o2.y)));
		if(o1.x > o2.x && o2.y > o1.y) return Mathf.Sqrt (((o1.x-o2.x)*(o1.x-o2.x))+((o2.y-o1.y)*(o2.y-o1.y)));
		if(o2.x > o1.x && o2.y > o1.y) return Mathf.Sqrt (((o2.x-o1.x)*(o2.x-o1.x))+((o2.y-o1.y)*(o2.y-o1.y)));
		return 0.0F;
	}

	public static void SortByDistances (Vector3[] objects, Vector3 origin) {
		float[] distances = new float[objects.Length];
		for (int i = 0; i< objects.Length; i++) {
			distances[i] = (objects[i] - origin).sqrMagnitude;		
		}
		System.Array.Sort (distances, objects);
	}
}
