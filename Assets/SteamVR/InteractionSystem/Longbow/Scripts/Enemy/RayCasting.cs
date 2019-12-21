using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCasting : MonoBehaviour
{
    public float TargetDistance;
    UnityEngine.AI.NavMeshAgent nav;
    Animator anim;

    // Update is called once per frame
    private void Awake()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        RaycastHit theHit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out theHit))
        {
            if (theHit.transform.name == "MainBase")
            {
                TargetDistance = theHit.distance;
                if (theHit.distance <10)
                {
                    nav.speed = 3;      
                    anim.SetTrigger("Run");
                }
            }
        }

    }
}
