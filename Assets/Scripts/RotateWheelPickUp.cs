using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWheelPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(5f, 0f, 0f);
    }
}
