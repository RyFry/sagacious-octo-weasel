using UnityEngine;
using System.Collections;

public class AudioState : MonoBehaviour {
	public AudioSource audio;
	public AudioClip yes;
	public AudioClip no;
	public AudioClip spit;

	// Use this for initialization
	void Start () {
		this.audio = GetComponent<AudioSource> ();
		this.audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (! this.audio.isPlaying) {
			this.audio.clip = yes;
			this.audio.Play();
		}
	}
}
