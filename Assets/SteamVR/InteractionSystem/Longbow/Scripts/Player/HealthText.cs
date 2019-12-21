using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthText: MonoBehaviour
{
    public Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        int current = PlayerHealth.currHealth;
        int starting = PlayerHealth.startHealth;
        text.text = "Health (" + current + " / " + starting + ")";
    }
}
