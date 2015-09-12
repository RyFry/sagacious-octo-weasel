using UnityEngine;
using System.Collections;

public class AudioState : MonoBehaviour {

	public AudioState nextAudioState;

	public AudioSource audioSource;
	public AudioClip yes;
	public AudioClip no;
	public AudioClip spit;

	// Use this for initialization
	public virtual void StartIntro () {
		this.audioSource = GetComponent<AudioSource> ();
		this.audioSource.Play();
	}
	
	// Update is called once per frame
	public virtual void StartYes () {
		if (! this.audioSource.isPlaying) {
			this.audioSource.clip = yes;
			this.audioSource.Play();
		}
	}

	// Update is called once per frame
	public virtual void StartNo () {
		if (! this.audioSource.isPlaying) {
			this.audioSource.clip = no;
			this.audioSource.Play();
		}
	}

	// Update is called once per frame
	public virtual void StartSpit () {
		print ("start spit");
			this.audioSource.clip = spit;
			this.audioSource.Play();
	}
}
