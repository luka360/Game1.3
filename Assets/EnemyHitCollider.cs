using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitCollider : MonoBehaviour {

    public float kickForce = 0.2f;
    GameObject player;
    EnemyMovement enemyMovement;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //enemyMovement = enemy.GetComponent<EnemyMovement>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Debug.Log("I punched player with " + this.gameObject.name);
        }
    }
}
