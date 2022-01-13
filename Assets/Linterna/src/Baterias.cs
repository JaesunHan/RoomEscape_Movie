using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baterias : MonoBehaviour
{
	public Flashlight flashlight;
	public AudioSource audioSource;

	public AudioClip audioClip;


	
	public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{

			flashlight.bateries += 2;

			AudioSource.PlayClipAtPoint(audioClip, transform.position);

			Destroy(this.gameObject);
		}
	}
}
