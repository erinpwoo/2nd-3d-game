using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Gameplay")]
    public int initialHealth = 100;
    public int health;

    private bool isHurt;
    public float knockBackForce = 10f;

    public float hurtDuration = .5f;
    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Enemy>() != null) {
            if (isHurt == false) {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                health -= enemy.damage;
                isHurt = true;

                Vector3 hurtDirection = (transform.position - enemy.transform.position).normalized;
                Vector3 knockBackDirection = (hurtDirection + hurtDirection + Vector3.up).normalized;
                GetComponent<Rigidbody>().AddForce(knockBackDirection * knockBackForce);

                StartCoroutine(HurtTime());
            }
        }
    }

    IEnumerator HurtTime() {
        yield return new WaitForSeconds(hurtDuration);
        isHurt = false;
    }
}