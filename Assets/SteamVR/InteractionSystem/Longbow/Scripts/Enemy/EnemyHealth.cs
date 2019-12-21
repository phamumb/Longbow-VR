using UnityEngine;
using UnityEngine.UI;
using System;
public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 50;
    public float currentHealth;
    public float sinkSpeed = 2.5f;
    public Slider healthSlider;
    public int scoreValue = 10;
    public AudioSource deathSound;
    public AudioSource hurtSound;
    public GameObject[] items;
    public GameObject FloatingText;

    //Arrow base damage values
    private int normalScale = 5;
    private static int buffDamage;

    //Fire Arrow base damage values
    private double fireTick = 5;
    private int fireScale = 1;

    //Ice Arrow base slow values
    private int slowAmount;
    private int iceScale;

    //Streak damage
    private float streakMult = 0.05f;

    Animator anim;
    ParticleSystem hitParticles;
    MeshCollider meshCollider;
    bool isDead;
    bool isSinking;



    void Awake()
    {
        anim = GetComponent<Animator>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        meshCollider = GetComponentInChildren<MeshCollider>();

        // Level 1 = 100, Level 2 = 150, Level 3 = 200
        currentHealth = startingHealth;
    }


    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    private void ShowFloatingText(int amount)
    {
        var go = Instantiate(FloatingText, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = "- " + amount.ToString();
    }

    public void TakeDamage()
    {
        if (isDead)
            return;
        hurtSound.Play();
        //enemyAudio.Play ();
        //Calculate damage done to health
        float dmg = CalcDamage();
        Debug.Log("b4 Curr: " + currentHealth);
        currentHealth -= dmg;
        Debug.Log("aft Curr: " + currentHealth);
        Debug.Log("Dmg: " + dmg);
        healthSlider.value = currentHealth;

        // Update stats
        StatManager playerStats = GetComponent<StatManager>();
        StatManager.damageAccum += dmg;
        StatManager.currStreak += 1;
        if (StatManager.currStreak > StatManager.highStreak) StatManager.UpdateStreak();
        if (FloatingText)
        {
            ShowFloatingText((int)dmg);
        }
        
        // Hits reward exp. If enough exp is accum, level up.
        ExpManager playerExp = GetComponent<ExpManager>();
        ExpManager.exp += ExpManager.expHit;
        StatManager.expAccum += ExpManager.expHit;
        if (ExpManager.exp >= ExpManager.levelUp) ExpManager.UpdatePlayerLevel();

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    // This function calculates how much damage for a normal arrow
    public float CalcDamage()
    {
        float damage;
        float multiplier = StatManager.currStreak * streakMult;
        damage = GameManagerS.damage + (normalScale * ExpManager.playerLevel);
		damage += damage * multiplier;
        return (damage + PlayerHealth.buffDamage);
    }

    int tickSeconds;

    public void StartFireDamage ()
    {
        tickSeconds = 10;
        InvokeRepeating("TakeFireDamage", 1f, 1f);
    }

    //Fire arrow damage
    void TakeFireDamage()
    {
        //if the enemy is already dead from normal arrow, return
        if (isDead)
            return;

        // While the fire burns, deal damage and give exp
        if (--tickSeconds == 0)
        {
            CancelInvoke("TakeFireDamage");
        } else {
            // Calculate fire damage done to health
            float dmg = CalcFireDamage();
            currentHealth -= dmg;
            healthSlider.value = currentHealth;

            // Update stats
            StatManager.damageAccum += dmg;
            if (FloatingText && currentHealth > 0)
            {
                ShowFloatingText((int)dmg);
            }
            // If health is depleted, kill.
            if (currentHealth <= 0)
            {
                Death();
                CancelInvoke("TakeFireDamage");
            }
        }
    }

    //Explosive Damage results from fire level exp
    public void TakeExplosiveDamage()
    {
        //if the enemy is already dead from normal arrow, return
        if (isDead)
            return;

        // Calculate explosion damage done to health
        float explosionDamage = CalcFireDamage() * 10f;
        currentHealth -= explosionDamage;
        healthSlider.value = currentHealth;

        // Update stats
        StatManager.damageAccum += explosionDamage;

        hurtSound.Play();

        // If health is depleted, kill.
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeIceDamage()
    {
        // If the enemy is already dead from normal arrow, return
        if (isDead)
            return;

        // While enemy is affected by slow
        while (true)
        {

        }
    }

    // This function calculates how much damage per fire arrow tick
    private float CalcFireDamage()
    {
        float fireDamage;
        fireDamage = (float)(fireTick + (fireScale * ExpManager.playerLevel));
        return fireDamage;
    }


    void Death()
    {
        isDead = true;
        meshCollider.isTrigger = true;
        anim.SetTrigger("Dead");
        deathSound.Play();
        StartSinking();

    }


    public void StartSinking()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        StatManager.killAccum += 1;
        ExpManager.exp += ExpManager.expKill;
        StatManager.expAccum += ExpManager.expKill;
        if (ExpManager.exp >= ExpManager.levelUp) ExpManager.UpdatePlayerLevel();
        Destroy(gameObject, 2f);

        if (items.Length > 0)
        {
            int itemIndex = UnityEngine.Random.Range(0, items.Length);
            Instantiate(items[itemIndex], transform.position, Quaternion.identity);
        }

    }

}
