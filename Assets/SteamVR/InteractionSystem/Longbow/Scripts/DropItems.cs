using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource pickSound;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private GameObject player;
    public ParticleSystem ps;
    public GameObject FloatingText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime);
        //Destroy(gameObject, 5.0f);
    }



    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.name == "Bow Arrow")
        {
            Debug.Log(gameObject.name);
            pickSound.Play();
            if (gameObject.name == "Coin(Clone)")
            {
                ShowFloatingText("+ 10 Coins");
                GameManagerS.AddCoin(10);
            }
            else if (gameObject.name == "EggLife(Clone)")
            {
                ShowFloatingText("+ 20 Health");
                playerHealth.IncreaseHealth(20);
            }
            else if (gameObject.name == "EggInvincible(Clone)")
            {
                ShowFloatingText("+ 50 Strength");
                playerHealth.StartIncreaseDamage();
            }else if(gameObject.name == "Explosion Barrel(Clone)")
            {
                ps.Play();
            }
            Destroy(gameObject,0.5f);
        }
    }

    private void ShowFloatingText(string text)
    {
        var go = Instantiate(FloatingText, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = text;
    }

    private void OnParticleCollision(GameObject other)
    {
        EnemyHealth eh = other.GetComponent<EnemyHealth>();
        Rigidbody rgb = other.GetComponent<Rigidbody>();
        float thrust = 1;
        if (eh != null)
        {
            rgb.AddForce(0, 0, thrust, ForceMode.Impulse);
            eh.TakeDamage();
        }
    }
}
