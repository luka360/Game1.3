using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour {

	public float kickForce = 0.2f;
    GameObject enemy;
    EnemyMovement enemyMovement;
	// Use this for initialization
	void Awake () {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyMovement = enemy.GetComponent<EnemyMovement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		Rigidbody body = other.attachedRigidbody;
		if (body != null && !body.isKinematic) {
			body.velocity = other.transform.position * kickForce;
            Debug.Log("I hit " + other.gameObject.name + " with " + this.gameObject.name);
        }

        if (other.gameObject == enemy)
        {
            Debug.Log("I punched enemy! " + enemy.gameObject.name + " with " + this.gameObject.name);
            if (this.gameObject.name.Equals("RightLegHitCollider"))
            {
                Debug.Log("TORSO");
                enemyMovement.TakeDamage(10, "torso");
            }
            else
            {
                enemyMovement.TakeDamage(10, "head");
            }
            
        }
	}
}
