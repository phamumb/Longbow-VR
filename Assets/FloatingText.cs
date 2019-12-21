﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    // Start is called before the first frame update
    public float DestroyTime = 3f;
    public Vector3 offset = new Vector3(0, 2, 0);
    void Start()
    {
        Destroy(gameObject, DestroyTime);
        transform.localPosition += offset;
    }

}
