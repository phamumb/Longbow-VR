using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExpManager : MonoBehaviour
{
    //Variables for player exp system
    public static int exp = 0;              // Current exp accumulator
    public static int expKill;              // Exp given per kill
    public static int expHit;               // Exp given per hit
    public static int levelUp = 100;        // Exp needed to increase playerLevel
    public static int playerLevel = 1;      // current player's level (affects normal arrow dmg)
    public static int maxLevel = 10;        // Maximum level to upgrade arrow
    public static bool isMaxLevel;          // True if player has reached max level

    public Text text;

    //initializes ExpManager for the player
    void Awake()
    {

        // init player level
        //playerLevel = 1;
        isMaxLevel = false;
        // exp = 0;
        //levelUp = 100;
        UpdateExp();
    }

    //function that processes leveling up player
    public static void UpdatePlayerLevel()
    {
        if (!(isMaxLevel))                                    // Don't level up if player is max level
        {
            int expTemp = exp;
            exp = 0;
            while ((expTemp >= levelUp) && (!(isMaxLevel)))   // Check if there is leftover exp and make sure the
            {                                                    // player does not overlevel
                
                playerLevel += 1;                             // Increase player level by 1
                if (playerLevel >= maxLevel)
                {
                    isMaxLevel = true;
                }
                else
                {
                    expTemp = expTemp - levelUp;              // Remove difference
                    UpdateExp();                              // Update levelUp, expKill, and expHit
                }
            }
        }
    }

    //method that updates level up threshold and exp per hit/kill
    static void UpdateExp()
    {
        levelUp = levelUp + (50 * playerLevel);           // Increase exp needed to level up
        expKill = 15 + (5 * playerLevel);                 // Increase exp per kill
        expHit = 5 + (1 * playerLevel);                   // Increase exp per hit
    }

    public static void Reset()
    {
        exp = 0;
        levelUp = 100;              // Exp needed to increase playerLevel
        playerLevel = 1;
        maxLevel = 10;
    }

    //function that outputs the current level, total exp, and exp needed for next level
    void Update()
    {
        text.text = "Level: " + playerLevel + "\nEXP: " + exp + " / " + levelUp;
    }
}