using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheelS : MonoBehaviour
{
  
    void Start()
    {
        
    }

    
    void Update()
    {
        gameObject.transform.Rotate(2.5f, 0f, 0f);
    }
}
