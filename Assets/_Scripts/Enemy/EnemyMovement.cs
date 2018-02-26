using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

	public Transform player;
    public GameObject leftLegHC;
    public GameObject rightLegHC;
    public GameObject leftHandHC;
    public GameObject rightHandHC;
    public float strikeRange = 1.0f;
    public float timeBetweenAttacks = 2.0f;

    private NavMeshAgent navAgent;
	private Animator _animator;
    private float timer;
    private ParticleSystem hitParticles;

	void Awake(){
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		navAgent = GetComponent<NavMeshAgent> ();
        _animator = GetComponent<Animator> ();
        hitParticles = GetComponentInChildren<ParticleSystem> ();
        leftLegHC.SetActive(false);
        rightLegHC.SetActive(false);
        leftHandHC.SetActive(false);
        rightHandHC.SetActive(false);
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
                    _animator.SetBool("MoveAway", true);
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


    void  Attack()
    {
        float hitType = Random.Range(0.0f, 4.0f);

        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks)
        {
            if (hitType <= 1.0f)
            {
                _animator.SetTrigger("LeftPunch");
            }
            else if (hitType > 1.0f && hitType <= 2.0f)
            {
                _animator.SetTrigger("RightPunch");
            }
            else if (hitType > 2.0f && hitType <= 3.0f)
            {
                _animator.SetTrigger("LeftKick");
            }
            else
            {
                _animator.SetTrigger("RightKick");
            }
            Debug.Log("HitTypeString: " + hitType);
            timer = 0;
        }
    }

    public void EnableHitCollider(int hitType)
    {
        if (hitType == 1 && leftLegHC != null)
        {
            leftLegHC.SetActive(true);
        }
        else if (hitType == 2 && rightLegHC != null)
        {
            rightLegHC.SetActive(true);
        }
        else if (hitType == 3 && leftHandHC != null)
        {
            leftHandHC.SetActive(true);
        }
        else if (hitType == 4 && rightHandHC != null)
        {
            rightHandHC.SetActive(true);
        }
    }

    public void DisableHitCollider(int hitType)
    {
        if (hitType == 1 && leftLegHC != null)
        {
            leftLegHC.SetActive(false);
        }
        else if (hitType == 2 && rightLegHC != null)
        {
            rightLegHC.SetActive(false);
        }
        else if (hitType == 3 && leftHandHC != null)
        {
            leftHandHC.SetActive(false);
        }
        else if (hitType == 4 && rightHandHC != null)
        {
            rightHandHC.SetActive(false);
        }
    }
}
