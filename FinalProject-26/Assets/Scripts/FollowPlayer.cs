using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

	// The target object
	private Transform targetObject;

	// Default distance between the target and the player
	public Vector3 cameraOffset;

	// Smooth the camera
	[Range(0.01f, 1.0f)]
	public float SmoothFactor;
	private Vector3 velocity;

	void LateUpdate()
	{
		//This is for if instantiated Player object
		//Then the script can target the object such as when rocedurally generate a level(include player)
		if (targetObject == null)
		{
			targetObject = GameObject.Find("Character [connId=0]").transform;
		}
		Vector3 newPos = targetObject.position + cameraOffset;
		Vector3 smoothPos = Vector3.SmoothDamp(transform.position, newPos, ref velocity, SmoothFactor);
		transform.position = smoothPos;
	}
}
