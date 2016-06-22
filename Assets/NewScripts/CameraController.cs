using UnityEngine;
using System.Collections;

[AddComponentMenu("ErikWDev/2D Camera Follow Script")]
[DisallowMultipleComponent]
public class CameraController : MonoBehaviour {

	[Header("Debugging")]
	public bool debug = false;

	[Header("Public values")]
	public Transform followTransform;
	public GameObject[] toBeSeen;

	public enum CameraStates {
		None,
		Follow,
		StickToNewTarget,
		FitToScreen
	}

	public CameraStates defaultCameraState = CameraStates.Follow;
	public CameraStates currentCameraState = CameraStates.Follow;

	//[Header("Camera State Settings : FollowMultiple")]
	//public bool followMultiple = false;

	[Header("Camera State Settings : StickToNewTarget")]
	public bool stickToNewTarget = false;
	public GameObject newTargetToStickTo;

	[Header("Camera State Settings : FitToScreen")]

	[Header("Offsets, Smoothing and Lerping")]
	[SerializeField]
	private float offsetUp;
	[SerializeField]
	private float distanceAway = -40f;
	[SerializeField]
	private float lerpValue;
	[SerializeField]
	private float camSmoothDampTime;
	[SerializeField]
	private Vector3 velocityCamSmooth;

	private Vector3 targetPosition;

	private Vector3 characterOffset;

	void Start () {
		transform.position = followTransform.position + new Vector3 (0f, 0f, distanceAway);
		characterOffset = followTransform.position + new Vector3 (0f, offsetUp, 0f);
	}

	void LateUpdate () {

		{
			currentCameraState = defaultCameraState;
			
			if (stickToNewTarget && newTargetToStickTo != null)
				currentCameraState = CameraStates.StickToNewTarget;
		}

		switch (currentCameraState) {
		case CameraStates.Follow:
		default:
			characterOffset = Vector3.Lerp (characterOffset, followTransform.position + new Vector3 (0f, offsetUp, 0f), lerpValue * Time.deltaTime);
			targetPosition = characterOffset;
			targetPosition.z = distanceAway;

			smoothPosition (transform.position, targetPosition);
			break;

		case CameraStates.StickToNewTarget:
			characterOffset = Vector3.Lerp (characterOffset, newTargetToStickTo.transform.position + new Vector3 (0f, 0f, 0f), lerpValue * Time.deltaTime);
			targetPosition = characterOffset;
			targetPosition.z = distanceAway;

			smoothPosition (transform.position, targetPosition);
			break;

		case CameraStates.FitToScreen:

			break;

		case CameraStates.None:
			break;
		}
	}

	private void smoothPosition(Vector3 fromPos, Vector3 toPos) {
		transform.position = Vector3.SmoothDamp (fromPos, toPos, ref velocityCamSmooth, camSmoothDampTime);
		//transform.position = Vector3.Lerp (fromPos, toPos, Time.deltaTime * lerpValue);
	}
}
