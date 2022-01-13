using UnityEngine;
using System.Collections;

public class FootStepSound : MonoBehaviour {
	public RaycastHit hit;
	public GameObject RayGo;
	public AudioClip[] clipsMetal, clipsTree, clipsGrass, clipsDirt, clipsWater;
	public float dist = 2;
	private string tagProv;
	private int j, go = 0, go1 = 0, go2 = 0, go3 = 0, go4 = 0;
	public AudioSource AS;
	public float PitchWalk, PitchRun;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) 
			AS.pitch = PitchRun;
		else
			AS.pitch = PitchWalk;
		
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
		{
			if(Physics.Raycast(RayGo.transform.position, Vector3.down, out hit, dist))
			{
				if(hit.collider)
				{
					
					if (hit.collider.tag == "Metal")
					{
						if(go == 0)
						{
							AS.clip = null;
							go = 1;
						}
						if(!AS.isPlaying)
						{
							AS.clip = clipsMetal[Random.Range(0,clipsMetal.Length)];
							AS.Play();
						}
						go1 = 0;
						go2 = 0;
						go3 = 0;
						go4 = 0;
					}
					else if (hit.collider.tag == "Tree")
					{
						if(go1 == 0)
						{
							AS.clip = null;
							go1 = 1;
						}
						if(!AS.isPlaying)
						{
							AS.clip = clipsTree[Random.Range(0,clipsTree.Length)];
							AS.Play();
						}
						go = 0;
						go2 = 0;
						go3 = 0;
						go4 = 0;
					} 
					else if (hit.collider.tag == "Grass")
					{
						if(go2 == 0)
						{
							AS.clip = null;
							go2 = 1;
						}
						if(!AS.isPlaying)
						{
							AS.clip = clipsGrass[Random.Range(0,clipsGrass.Length)];
							AS.Play();
						}
						go = 0;
						go1 = 0;
						go3 = 0;
						go4 = 0;
					}
					else if (hit.collider.tag == "Dirt")
					{
						if(go3 == 0)
						{
							AS.clip = null;
							go3 = 1;
						}
						if(!AS.isPlaying)
						{
							AS.clip = clipsDirt[Random.Range(0,clipsDirt.Length)];
							AS.Play();
						}
						go = 0;
						go1 = 0;
						go2 = 0;
						go4 = 0;
					}
					else if (hit.collider.tag == "Water")
					{
						if(go4 == 0)
						{
							AS.clip = null;
							go4 = 1;
						}
						if(!AS.isPlaying)
						{
							AS.clip = clipsWater[Random.Range(0,clipsWater.Length)];
							AS.Play();
						}
						go = 0;
						go1 = 0;
						go2 = 0;
						go3 = 0;
					}
					else
					{
						AS.Stop();
						go = 0;
						go1 = 0;
						go2 = 0;
						go3 = 0;
						go4 = 0;
					}
					
					
					
				}
				
				
			}
			
		}
		else
		{
			AS.clip = null;
			AS.Stop();
			go = 0;
		}
		
		
	}
}

