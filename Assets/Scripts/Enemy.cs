using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float shootingInterval = 4f;

    private float shootingTimer;
    public int health = 5;
    public int damage = 5;

    public float shootingDistance = 100f;

    public GameObject player;

    public GameObject bullet;

    public Transform spawn;

    public AudioSource shotSound;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootingTimer = Random.Range(0, shootingInterval);
        spawn = gameObject.GetComponent<Transform>().GetChild(0);
        shotSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0 && (Vector3.Distance(transform.position, player.transform.position) <= shootingDistance)) {
            shootingTimer = shootingInterval;
            GameObject currentBullet = Instantiate (bullet, spawn.position, spawn.rotation);
            currentBullet.transform.forward = (player.transform.position - spawn.position).normalized;
            shotSound.Play();
            print("shooting");
        }
    }

    public void gotShot() {
        health--;
    }
}
