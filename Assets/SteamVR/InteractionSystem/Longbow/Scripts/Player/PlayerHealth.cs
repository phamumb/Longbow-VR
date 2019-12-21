using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth: MonoBehaviour
{
    public int startingHealth = 100;                     // The amount of health the player starts the game with.
    public int currentHealth;                            // The current health the player has.
    public static int startHealth = 100;
    public static int currHealth;
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
    public Slider baseHealthSlider;
    public static int buffDamage = 0;

    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.

    void Awake()
    {
        // Set the initial health of the player.
        SetHealth();
    }

    void SetHealth()
    {
        startingHealth = 100 * (GameManagerS.ScreenIndex);
        currentHealth = startingHealth;
        startHealth = startingHealth;
        currHealth = currentHealth;
        baseHealthSlider.value = startingHealth;
    }

    void Update()
    { 
        
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }

    public void IncreaseHealth(int amount)
    {
        if (currentHealth < startingHealth)
        {
            int difference = startingHealth - currentHealth;
            if(difference < 20)
            {
                currentHealth += difference;
            }
            else
            {
                currentHealth += amount;
            }
            baseHealthSlider.value = currentHealth;
            currHealth = currentHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;
        baseHealthSlider.value = currentHealth;
        currHealth = currentHealth;

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            DestroyBase();
        }
    }

    void DestroyBase()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
    }

    public void RestartLevel()
    {
        // Reload the level that is currently loaded.
        SceneManager.LoadScene(0);
    }

    // These methods increase damage when the buff is hit.
    int buffSeconds;

    public void StartIncreaseDamage()
    {
        buffSeconds += 5;
        InvokeRepeating("IncreaseDamage", 1f, 1f);
    }

    void IncreaseDamage()
    {

        // While the fire burns, deal damage and give exp
        if (--buffSeconds == 0)
        {
            buffDamage = 0;
            CancelInvoke("IncreaseDamage");
        }
        else
        {
            buffDamage = 50;
        }
    }
}
