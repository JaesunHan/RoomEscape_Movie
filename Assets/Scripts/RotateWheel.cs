using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheel : MonoBehaviour
{
  
    void Start()
    {
        
    }

    
    void Update()
    {
        gameObject.transform.Rotate(10f, 0f, 0f);
    }
}
