using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float shootingInterval = 4f;
    private NavMeshAgent agent;

    private float shootingTimer;
    public int health = 5;
    public int damage = 5;

    public float shootingDistance = 100f;

    public GameObject player;

    public GameObject bullet;

    public Transform spawn;

    public AudioSource shotSound;
    public float chaseDistance = 12f;
    private float chaseTimer;
    public float chaseInterval = 2f;

    public Animator animator;
    bool justDied;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootingTimer = Random.Range(0, shootingInterval);
        spawn = gameObject.GetComponent<Transform>().GetChild(0);
        shotSound = gameObject.GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 2f;
        animator = GetComponent<Animator>();
        chaseTimer = chaseInterval;
        justDied = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!justDied) {
            // shooting logic
            shootingTimer -= Time.deltaTime;
            if (shootingTimer <= 0 && (Vector3.Distance(transform.position, player.transform.position) <= shootingDistance)) {
                transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
                shootingTimer = shootingInterval;
                animator.SetBool("isShooting", true);
                GameObject currentBullet = Instantiate (bullet, spawn.position, spawn.rotation);
                currentBullet.transform.forward = (player.transform.position - spawn.position).normalized;
                shotSound.Play();
            } else {
                animator.SetBool("isShooting", false);
            }
            // chasing logic
            chaseTimer -= Time.deltaTime;
            if (chaseTimer <= 0 && (Vector3.Distance(transform.position, player.transform.position) <= chaseDistance)) {
                chaseTimer = chaseInterval;
                agent.SetDestination(player.transform.position);
                animator.SetBool("isChasing", true);
                print("ischasing");
            }

            if (agent.velocity != Vector3.zero) {
                transform.rotation = Quaternion.LookRotation(player.transform.position - transform.position);
                animator.SetBool("isChasing", true);
            } else {
                animator.SetBool("isChasing", false);
            }
        }
        if (player.GetComponent<Player>().isGameOver || player.GetComponent<Player>().wonGame) {
            gameObject.SetActive(false);
        }
    }

    public void gotShot() {
        health--;
        if (health <= 0) {
            animator.SetTrigger("justDied");
            justDied = true;
            agent.isStopped = true;
        }
    }
}
