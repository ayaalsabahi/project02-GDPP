using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Events")]
    public GameEvent bigEnemy;
    public GameEvent smallEnemy;
    public GameEvent enemyDeath;
    
    public Transform playerTrans;
    public float lowSpeed;
    public float highSpeed;
    public float detectDistance; // the distance at which the


    private float currentSpeed;
    private float distanceToPlayer;
    private bool follow = false;
    private float timeAccumelated = 0;
    public float lifeSpan = 20f; //after 5 seconds, destroy self 

    private Rigidbody2D rb;

    


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = UnityEngine.Random.Range(lowSpeed, highSpeed); //decide a speed at random for the enemy

    }


    // Update is called once per frame
    void Update()
    {
        Vector2 directionToPlayer = (playerTrans.position - transform.position).normalized;
        timeAccumelated += timeAccumelated;

        distanceToPlayer = Vector2.Distance(transform.position, playerTrans.position);

        if (distanceToPlayer <= detectDistance)
        {
            follow = true;
        }

        if (lifeSpan <= 0)
        {
            Destroy(gameObject);
            enemyDeath.Raise();
        }


        if (follow)
        {
            lifeSpan -= Time.deltaTime;
            rb.MovePosition(rb.position + directionToPlayer * currentSpeed * Time.deltaTime);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the object that entered the trigger is tagged as "Player".
        if (other.gameObject.CompareTag("Player"))
        {
            
            // Check if the enemy object has the tag "bigEnemy".
            if (gameObject.CompareTag("BigEnemy"))
            {
                // This is where you handle the logic for when the bigEnemy touches the player.
                bigEnemy.Raise();
            }

            if (gameObject.CompareTag("SmallEnemy"))
            {
                smallEnemy.Raise();
            }
        }
    }


}
