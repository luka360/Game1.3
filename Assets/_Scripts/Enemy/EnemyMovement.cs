using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	public Transform player;

	NavMeshAgent navAgent;
	private Animator _animator;


	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		navAgent = GetComponent<NavMeshAgent> ();
		_animator = GetComponent<Animator> ();
	}
		
	// Update is called once per frame
	void Update () {

		if (Vector3.Distance(player.position, transform.position) > 4 && Vector3.Distance(player.position, transform.position) < 10) {
			navAgent.SetDestination (player.position);
			_animator.SetBool ("Move", true);
		} else {
			navAgent.isStopped = true;
			_animator.SetBool ("Move", false);
		}

	}
}
