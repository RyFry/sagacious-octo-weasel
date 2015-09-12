using UnityEngine;
using System.Collections;

public class AudioState : MonoBehaviour {

	public AudioState nextAudioState;

	public AudioSource audio;
	public AudioClip yes;
	public AudioClip no;
	public AudioClip spit;

	// Use this for initialization
	public void StartIntro () {
		this.audio = GetComponent<AudioSource> ();
		this.audio.Play();
	}
	
	// Update is called once per frame
	public void StartYes () {
		if (! this.audio.isPlaying) {
			this.audio.clip = yes;
			this.audio.Play();
		}
	}

	// Update is called once per frame
	public void StartNo () {
		if (! this.audio.isPlaying) {
			this.audio.clip = no;
			this.audio.Play();
		}
	}

	// Update is called once per frame
	public void StartSpit () {
		if (! this.audio.isPlaying) {
			this.audio.clip = spit;
			this.audio.Play();
		}
	}
}
