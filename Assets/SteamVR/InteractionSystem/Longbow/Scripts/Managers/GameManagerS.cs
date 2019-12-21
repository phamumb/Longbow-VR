using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.IO;

public class GameManagerS : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public float restartDelay = 5f;
    public float nextLevelCounter = 180f;
    public AudioSource nextLevel;
    public AudioSource gameover;
    public static int coin = 0;
    public static int damage = 30;
    public Text coinText;
    public Text timerText;
    public static int ScreenIndex = 0;
    public static float timeAccum = 180f;

    Animator anim;
    float restartTimer;
    private bool count;
    private bool play;
    private bool done = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        nextLevelCounter = timeAccum;
        count = true;
        play = true;
    }


    void Update()
    {
        if (coinText)
        {
            coinText.text = "" + coin;
        }

        if (count)
        {
            nextLevelCounter -= Time.deltaTime;
            timerText.text = "Time: " + nextLevelCounter.ToString("F");
        }

        // If timer reaches 0, player has survived and next level.
        if (nextLevelCounter <= 0)
        {
            if (ScreenIndex < 3)
            {
                anim.SetTrigger("NextLevel");
                if (play)
                {
                    nextLevel.Play();
                    play = false;
                }
                count = false;
                restartTimer += Time.deltaTime;
                if (restartTimer >= restartDelay)
                {
                    ScreenIndex++; // Move to next screen index;
                    SceneManager.LoadScene(ScreenIndex);
                    timeAccum += 60f;
                }
            }
            else
            {
                done = true;
            }
        }

        // If it is not the last level, Game Over if playerhealth reaches 0.
        if (playerHealth.currentHealth <= 0 || done)
        {
            count = false;
            if (!done)
            {
                anim.SetTrigger("GameOver");
                if (play)
                {
                    gameover.Play();
                    play = false;
                }
                restartTimer += Time.deltaTime;
            }
            ScreenIndex = 0;  // Reset the screen to be 0 which is back to Menu Screen
            if (restartTimer >= restartDelay)
            {
                ExpManager.Reset();
                StatManager.Reset();
                done = false;
                WriteString();
                SceneManager.LoadScene(ScreenIndex);
                timeAccum = 180f;
                ExpManager.Reset();
            }
        }
    }

    public static void WriteString()
    {
        string path = Application.dataPath + "/test.txt";
        if (!File.Exists(path))
        {
            File.WriteAllText(path, "Date\t\t\tScore\n");

        }
        string content = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") 
            + "    " +  coin + "\n";
        File.AppendAllText(path, content);

    }

    public static void AddCoin(int amount)
    {
        coin += amount;
    }

   
}
