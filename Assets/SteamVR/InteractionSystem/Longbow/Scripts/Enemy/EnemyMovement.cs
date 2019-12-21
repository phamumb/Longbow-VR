using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;
    Animator anim;
    public float distance = 10;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
        anim = GetComponent<Animator>();
    }


    void Update ()
    {

        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            if (GameManagerS.ScreenIndex == 0) nav.speed = 1;
            else
            {
                nav.speed = GameManagerS.ScreenIndex;
            }
            nav.SetDestination(player.position);
        }
        else
        {
            if (playerHealth.currentHealth <= 0)
            {
                anim.SetTrigger("Idle");
            }
            nav.Stop(true);
        }
    }

}
