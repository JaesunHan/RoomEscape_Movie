using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Destroyonpieces>();
        StartCoroutine(gameObject.GetComponent<Destroyonpieces>().SplitMesh(true));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
