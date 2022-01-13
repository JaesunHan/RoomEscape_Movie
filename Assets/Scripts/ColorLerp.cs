using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{

    public Material mat1;
    public Material mat2;
    float duration = 2.0f;
    MeshRenderer rend;

    void Start()
    {

        rend = GetComponent<MeshRenderer>();

    }

    void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        rend.material.Lerp(mat1, mat2, lerp);
    }
}
