using UnityEngine;
using System.Collections;

public class CircularAudioState : MonoBehaviour {
	bool SpitOn = false;
	public AudioState nextAudioState;
	
	public AudioSource audioSource;
	public AudioClip yes;
	public AudioClip no;
	public AudioClip spit;

	/// <summary>
	/// ///////////////////////t<MEP
	///
	/// </summary>
	/// 
	public void Start() {
		StartIntro();
	}

	// Use this for initialization
	public void StartIntro () {
		this.audioSource = GetComponent<AudioSource> ();
		audioSource.enabled = true;
		this.audioSource.Play();
	}

	public void Update() {
		if(!this.audioSource.isPlaying && !SpitOn) {
			this.audioSource.clip = yes;
			this.audioSource.Play ();
		}
	}
	
	public void StartYes() {
	}
	
	public void StartNo() {
	}
	public void StartSpit() {
		this.audioSource.clip = spit;
		SpitOn = true;
		this.audioSource.Play ();
	}
}
