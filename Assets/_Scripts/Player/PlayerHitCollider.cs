using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitCollider : MonoBehaviour {

    public float hitForce = 0.2f;

	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody body = other.attachedRigidbody;
        if (body != null && !body.isKinematic)
        {
            body.velocity = other.transform.position * hitForce;
            Debug.Log("I hit " + other.gameObject.name + " with " + this.gameObject.name);
        }

        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(10);
            Debug.Log("I hit " + other.gameObject.name + " with " + this.gameObject.name);
        }
    }
}
