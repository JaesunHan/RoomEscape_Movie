using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFORotation : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.Rotate(0f, -1f, 0f);
    }
}
