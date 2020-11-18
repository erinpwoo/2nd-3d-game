using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Gameplay")]
    public int initialHealth = 100;
    public int health;

    private bool isHurt;
    public float knockBackForce = 10f;

    public float hurtDuration = .5f;
    public bool isGameOver;
    public bool wonGame;

    public Text gameOverText;
    public Text youSurvivedText;

    public GameObject safeZone;
    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
        isGameOver = false;
        wonGame = false;
        gameOverText.gameObject.SetActive(false);
        youSurvivedText.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver || wonGame) {
            if (Input.anyKey) {
                SceneManager.LoadScene("MainScene");
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Enemy>() != null) {
            if (isHurt == false) {
                Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                health -= enemy.damage;
                isHurt = true;
                StartCoroutine(HurtTime());
            }
            if (health <= 0) {
                CancelInvoke();
                GameOver();
            }
        }
    }

    IEnumerator HurtTime() {
        yield return new WaitForSeconds(hurtDuration);
        isHurt = false;
    }

    public void GameOver() {
        gameOverText.gameObject.SetActive(true);
        gameObject.GetComponent<GunInventory>().currentGun.SetActive(false);
        TextMesh[] t =  gameObject.transform.GetComponentsInChildren<TextMesh>();
        foreach (TextMesh i in t) {
            i.gameObject.SetActive(false);
        }
        isGameOver = true;
    }

    public void OnTriggerEnter(Collider collider) {
        if (collider.gameObject == safeZone) {
            WinGame();
        }
    }

    public void WinGame() {
        youSurvivedText.gameObject.SetActive(true);
        gameObject.GetComponent<GunInventory>().currentGun.SetActive(false);
        TextMesh[] t =  gameObject.transform.GetComponentsInChildren<TextMesh>();
        foreach (TextMesh i in t) {
            i.gameObject.SetActive(false);
        }
        wonGame = true;
    }
}