using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float cameraSpeed = 120.0f;
	public GameObject cameraFollowObj;
	public GameObject camera;
	public GameObject player;
	public float clampAngle = 80.0f;
	public float inputSensitivity = 150.0f;
	public float offsetX;
	public float offsetY;
	public float offsetZ;
	public float mouseX;
	public float mouseY;
	public float finalInputX;
	public float finalInputZ;
	Vector3 targetPosition;
	public float smoothX;
	public float smoothY;

	private float _rotY = 0.0f;
	private float _rotX = 0.0f;

	// Use this for initialization
	void Start () {
		Vector3 rot = transform.localRotation.eulerAngles;
		_rotY = rot.y;
		_rotX = rot.x;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		// Setup the rotation of teh sticks for xbox controller
		float inputX = Input.GetAxis("RightStickHorizontal");
		float inputZ = Input.GetAxis ("RightStickVertical");
		mouseX = Input.GetAxis ("Mouse X");
		mouseY = Input.GetAxis ("Mouse Y");
		finalInputX = inputX + mouseX;
		finalInputZ = inputZ + mouseY;

		_rotY += finalInputX * inputSensitivity * Time.deltaTime;
		_rotX -= finalInputZ * inputSensitivity * Time.deltaTime;

		_rotX = Mathf.Clamp (_rotX, -clampAngle, clampAngle);

		Quaternion localRotation = Quaternion.Euler (_rotX, _rotY, 0.0f);
		transform.rotation = localRotation;
	}

	void LateUpdate(){
		CameraUpdater ();
	}

	void CameraUpdater(){
		//set the target object to follow
		Transform target = cameraFollowObj.transform;

		//move towards the target
		float step = cameraSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, target.position, step);
	}
}
