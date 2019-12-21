using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatManager : MonoBehaviour
{
    //Base values for stats
    public static float damageAccum;
    public static int expAccum;
    public static int hitAccum;
    public static int shotAccum;
    public static int killAccum;
    public static int currStreak;
    public static int highStreak;
	
    public Text text;

    void Awake ()
    {
        text = GetComponent<Text>();
    }

    public static void Reset()
    {
        damageAccum = 0;
        hitAccum = 0;
        shotAccum = 0;
        killAccum = 0;
        currStreak = 0;
        highStreak = 0;
    }

    public static void UpdateStreak()
    {
        highStreak = currStreak;
    }


    void Update()
    {
        text.text = "Kills: " + killAccum + "\nStreak: " + currStreak;
    }
}
