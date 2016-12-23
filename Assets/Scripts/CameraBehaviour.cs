using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour {

	// Use this for initialization
	public bool follow;
	public Transform target;
	public float damping;
	public float dx, dy;

	private Vector3 velocity = Vector3.zero;

	public PlayerController player;

	void Update() 
	{
		if ((player.GetFace () && dx > 0) || (!player.GetFace () && dx < 0)) dx *= -1;

		Vector3 targetPosition = target.TransformPoint (new Vector3 (dx, dy, -10));
		transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, damping);
	

	}

	public void SetTarget(Transform trans){}

}
