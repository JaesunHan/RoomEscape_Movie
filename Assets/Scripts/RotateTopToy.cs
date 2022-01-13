using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTopToy : MonoBehaviour
{
    public float maxRotationSpeed = 50f;
    public float maxFrequency = 3f;
    public float speed;
    public GameObject earth;
    float timeLeft, rotX, rotY;

    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.Rotate(0f, 15f, 0f);
        gameObject.transform.Rotate(Random.Range(0.25f,0.25f), 0f, 0f);

        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0)
        {
            rotX = Random.Range(-maxRotationSpeed, maxRotationSpeed);
            rotY = Random.Range(-maxRotationSpeed, maxRotationSpeed);
            timeLeft = Random.Range(0, maxFrequency);
        }
        speed = -90;
        transform.RotateAround(earth.transform.position, Vector3.down, Random.Range(0.1f, 1f) * speed * Time.deltaTime);
    }
}
