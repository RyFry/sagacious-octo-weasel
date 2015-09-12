using UnityEngine;
using System.Collections;

public class CircularAudioState : MonoBehaviour {
	public AudioSource audio;
	public AudioClip yes;
	public AudioClip no;
	public AudioClip spit;
	bool SpitOn = false;
	
	// Use this for initialization
	void Start () {
		this.audio = GetComponent<AudioSource> ();
		this.audio.Play();
	}

	void Update() {
		if(!this.audio.isPlaying && !SpitOn) {
			this.audio.clip = yes;
			this.audio.Play ();
		}
	}
	
	void StartYes() {
	}
	
	void StartNo() {
	}
	void StartSpit() {
			this.audio.clip = spit;
			this.audio.Play ();
	}
}
