using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f; // amount of time between each attack
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    bool baseInRange;
    float timer;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            baseInRange = true;
            if(playerHealth.currentHealth > 0)
            {
                anim.SetTrigger("Attack");
            }
           
        }
       
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            baseInRange = false;
            anim.SetTrigger("Move");
        }
    }


    void Update ()
    {
        timer += Time.deltaTime;


        if(timer >= timeBetweenAttacks && baseInRange && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("Idle");
        }
    }


    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
