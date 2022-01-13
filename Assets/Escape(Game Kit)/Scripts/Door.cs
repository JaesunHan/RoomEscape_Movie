using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public AudioSource _AudioSource;
	public AudioClip audioClipLock;
	public Animation _Animation;
	public AnimationClip animationOpenClip, animationCloseClip;
	public float RayDistance;
	public RaycastHit hit;
	public string NameTagLock;
	private int num;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Physics.Raycast(transform.position, transform.forward, out hit, RayDistance))
		{
			if(hit.collider)
			{
				if(hit.collider.gameObject.tag == NameTagLock)
				{
					if(Input.GetKeyDown(KeyCode.Q))
					{
						if(!_Animation.isPlaying)
						{
							if(num == 0)
								_Animation.clip = animationOpenClip;
							if(num == 1)
								_Animation.clip = animationCloseClip;
							_Animation.Play();
							_AudioSource.clip = audioClipLock;
							_AudioSource.Play();
							num ++;
							if(num > 1)
								num = 0;
						}
					}
				}
			}
		}
	}
}
