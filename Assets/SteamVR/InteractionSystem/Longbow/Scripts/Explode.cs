using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem ps;

    void OnParticleCollision(GameObject other)
    {
        EnemyHealth eh = other.GetComponent<EnemyHealth>();
        if(eh != null)
        {
            eh.TakeExplosiveDamage();
        }
        Debug.Log("Hit");
    }

    public void ExplodeBarrel(Collision collision)
    {
        gameObject.SetActive(false);
        ps.Play(true);
        Destroy(gameObject, 1.0f);
    }
}
