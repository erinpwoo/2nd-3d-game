using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Gameplay")]
    public int initialHealth = 100;
    private int health;
    public int Health { get { return health; }}
    // Start is called before the first frame update
    void Start()
    {
        health = initialHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
