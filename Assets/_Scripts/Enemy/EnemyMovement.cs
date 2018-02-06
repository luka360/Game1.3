using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	public Transform player;
    public int startingHealth = 100;
    public float strikeRange = 1.0f;

    private NavMeshAgent navAgent;
	private Animator _animator;
    private int currentHealth;
    private float timer;
    private float timeBetweenAttacks = 5.0f;
    private ParticleSystem hitParticles;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		navAgent = GetComponent<NavMeshAgent> ();
        _animator = GetComponent<Animator> ();
        hitParticles = GetComponentInChildren<ParticleSystem> ();
        currentHealth = startingHealth;
	}
		
	// Update is called once per frame
	void Update () {

        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= 10f)
        {
            navAgent.SetDestination(player.position);
            _animator.SetBool("Move", true);

            if (distance < navAgent.stoppingDistance)
            {
                _animator.SetBool("Move", false);

                //Attack Stance
                _animator.SetBool("Fighting", true);
                _animator.SetBool("MoveToward", true);


                if (distance <= strikeRange)
                {
                    _animator.SetBool("MoveToward", false);

                    Attack();
                }

                //Face the player
                FaceThePlayer();

                if (distance < 0.7f)
                {
                    MoveAway();
                }
                else
                {
                    _animator.SetBool("MoveAway", false);
                }
            }
            else {
                _animator.SetBool("Fighting", false);
                _animator.SetBool("MoveToward", false);
            }
        }
    }

    void FaceThePlayer() {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 5f);
    }

    void MoveAway() {
        _animator.SetBool("MoveAway", true);
    }

    public void TakeDamage(int amount, string hitType) {
        currentHealth -= amount;

        Debug.Log("Current health: " + currentHealth);

        if (hitType.Equals("head"))
        {
            _animator.SetTrigger("HeadPunch");
            hitParticles.Play();
        }
        else
        {
            _animator.SetTrigger("TorsoPunch");
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy dead.");
            _animator.SetBool("Dead", true);
        }
    }

    void  Attack()
    {
        float hitType = Random.Range(0.0f, 4.0f);
        //Debug.Log("Hit Type: " + hitType);
       /* if (hitType <= 1.0f)
        {
            _animator.SetTrigger("LeftPunch");
        }
        else if (hitType > 1.0f && hitType <= 2.0f)
        {
            _animator.SetTrigger("LeftKick");
        }
        else if (hitType > 2.0f && hitType <= 3.0f)
        {
            _animator.SetTrigger("RightPunch");
        }
        else
        {
            _animator.SetTrigger("RightKick");
        }*/
    }
}
