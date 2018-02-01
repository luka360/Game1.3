using UnityEngine;
using System.Collections;

// maintains position offset while orbiting around target

public class OrbitCamera : MonoBehaviour {
	[SerializeField] private Transform target;

	public float rotSpeed = 1.5f;
	public float minVert = 1f;
	public float maxVert = 75f;
	public float zoom = 1.5f;

	private float _rotY;
	private float _rotX;
	private Vector3 _offset;

	// Use this for initialization
	void Start() {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		_rotY = transform.eulerAngles.y;
		_rotX = transform.eulerAngles.x;
		_offset = target.position - transform.position;
	}

	// Update is called once per frame
	void LateUpdate() {
		float horInput = Input.GetAxis("Horizontal");
		if (horInput != 0) {
			_rotY += horInput * rotSpeed;
		} else {
			_rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
			_rotX -= Input.GetAxis ("Mouse Y") * rotSpeed * 3;
			_rotX = Mathf.Clamp (_rotX, minVert, maxVert);


		}

		Quaternion rotation = Quaternion.Euler(_rotX, _rotY, 0);
		transform.position = target.position - (rotation * _offset);
		transform.LookAt(target);
	}
}
