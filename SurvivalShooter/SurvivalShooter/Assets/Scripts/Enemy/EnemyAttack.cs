using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    GameObject player2;
    PlayerHealth playerHealth;
    PlayerHealth playerHealth2;
    EnemyHealth enemyHealth;
    SplitScreen splitScreen;
    bool playerInRange;
    bool player2InRange;
    float timer;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        playerHealth = player.GetComponent <PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        if(splitScreen.multiPlayer == true)
        {
            if (other.gameObject == player2)
            {
                player2InRange = true;
            }
            if (other.gameObject == player)
            {
                playerInRange = true;
            }
        }
        if(splitScreen.multiPlayer == false)
        {
            if (other.gameObject == player)
            {
                playerInRange = true;
            }
        }
    }


    void OnTriggerExit (Collider other)
    {
        if (splitScreen.multiPlayer == true)
        {
            if (other.gameObject == player)
            {
                playerInRange = false;
            }
            if (other.gameObject == player2)
            {
                player2InRange = false;
            }
        }
        if (splitScreen.multiPlayer == false)
        {
            if (other.gameObject == player)
            {
                playerInRange = false;
            }
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if (splitScreen.multiPlayer == true)
        {
            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                Attack();
            }
            if (timer >= timeBetweenAttacks && player2InRange && enemyHealth.currentHealth > 0)
            {
                Attack();
            }

            if (playerHealth.currentHealth1 <= 0 && playerHealth.currentHealth2 <= 0)
            {
                anim.SetTrigger("PlayerDead");
            }
        }
        if(splitScreen.multiPlayer == false)
        {
            if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
            {
                Attack();
            }
            if (playerHealth.currentHealth1 <= 0)
            {
                anim.SetTrigger("PlayerDead");
            }
        }
    }


    void Attack()
    {
        timer = 0f;

        if (splitScreen.multiPlayer == true)
        {
            if (playerHealth.currentHealth1 > 0 || playerHealth.currentHealth2 > 0)
            {
                if (playerInRange == true)
                {
                    playerHealth.TakeDamage(attackDamage);
                }
                if (player2InRange == true)
                {
                    playerHealth.TakeDamage2(attackDamage);
                }
            }
        }
        if (splitScreen.multiPlayer == false)
        {
            if (playerHealth.currentHealth1 > 0)
            {
                if (playerInRange == true)
                {
                    playerHealth.TakeDamage(attackDamage);
                }
            }
        }
    }
}
