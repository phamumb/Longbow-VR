using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndStatManager : MonoBehaviour
{
    
    public Text text;

    void Awake ()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        int kills = StatManager.killAccum;
        int bestStreak = StatManager.highStreak;
        float hits = StatManager.hitAccum;
        float shots = StatManager.shotAccum;
        float accuracy;
        if (shots == 0)
        {
            accuracy = hits;
        } else { accuracy = (hits / shots); }
        int exp = StatManager.expAccum;
        float damage = StatManager.damageAccum;
        text.text = "Kills: " + kills + "   Streak: " + bestStreak + "\nDamage: " 
            + damage + "\nExp: " + exp + "\nAccuracy: " + accuracy*100 + "%";
    }
}
