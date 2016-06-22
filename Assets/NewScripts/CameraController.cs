using UnityEngine;
using System.Collections;

[AddComponentMenu("ErikWDev/2D Camera Follow Script")]
[DisallowMultipleComponent]
public class CameraController : MonoBehaviour {

	[Header("Debugging")]
	public bool debug = false;

	[Header("Public values")]
	public Transform followTransform;

	public enum CameraStates {
		None,
		Follow,
		StickToNewTarget,
		FitToScreen
	}

	public CameraStates currentCameraState = CameraStates.Follow;

	[Header("Camera State Settings : StickToNewTarget")]
	public GameObject newTargetToStickTo;

	[Header("Camera State Settings : FitToScreen")]

	[Header("Smoothing and Lerping")]
	[SerializeField]
	private float lerpValue;

	void Start () {
	
	}

	void Update () {

		switch (currentCameraState) {
		case CameraStates.Follow:
		default:

			break;

		case CameraStates.StickToNewTarget:

			break;

		case CameraStates.FitToScreen:

			break;

		case CameraStates.None:
			break;
		}
	}

	private void smoothPosition(Vector3 fromPos, Vector3 toPos) {
		
	}
}
