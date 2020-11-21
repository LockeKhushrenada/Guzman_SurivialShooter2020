using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    Transform player2;
    PlayerHealth playerHealth;
    PlayerHealth playerHealth2;
    EnemyHealth enemyHealth;
    SplitScreen splitScreen;
    UnityEngine.AI.NavMeshAgent nav;
    float nearestPos = 0;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        player2 = GameObject.FindGameObjectWithTag("Player2").transform;
        playerHealth = player.GetComponent <PlayerHealth>();
        playerHealth2 = player2.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }


    void Update ()
    {
        float p1Pos = Vector3.Distance(player.position, transform.position);
        float p2Pos = Vector3.Distance(player2.position, transform.position);
        if(p1Pos < p2Pos)
        {
            nearestPos = p1Pos;
        }
        else if (p1Pos > p2Pos)
        {
            nearestPos = p2Pos;
        }
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth1 > 0)
        {
            nav.SetDestination(player.position);
        }
        if(splitScreen.multiPlayer == true && enemyHealth.currentHealth > 0 && playerHealth.currentHealth1 > 0 || playerHealth2.currentHealth2 >0)
        {
            if (nearestPos == p1Pos)
            {
                nav.SetDestination(player.position);
            }
            if (nearestPos == p2Pos)
            {
                nav.SetDestination(player2.position);
            }
        }
        if(splitScreen.multiPlayer == false)
        {
            FollowSinglePlayer();
        }
        if (splitScreen.multiPlayer == true)
        {
            FollowMultiPlayer();
        }

        else
        {
            nav.enabled = false;
        }
    }
    void FollowSinglePlayer()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth1 > 0)
        {
            nav.SetDestination(player.position);
        }
    }
    void FollowMultiPlayer()
    {
        if (playerHealth.isDead == true ||enemyHealth.currentHealth > 0 && playerHealth.currentHealth1 > 0)
        {
            nav.SetDestination(player.position);
            if (playerHealth.isDead == true)
            {
                nav.SetDestination(player2.position);
            }
        }
        if (playerHealth2.isDead == true || enemyHealth.currentHealth > 0 && playerHealth2.currentHealth2 > 0)
        {
            nav.SetDestination(player.position);
            if (playerHealth2.isDead == true)
            {
                nav.SetDestination(player.position);
            }
        }
    }
}
