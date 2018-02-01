using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

	public float kickForce = 0.2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		Debug.Log ("I hit " + other.gameObject.name + " with " + this.gameObject.name);
		Rigidbody body = other.attachedRigidbody;
		if (body != null && !body.isKinematic) {
			body.velocity = other.transform.position * kickForce;
		}
	}
}
