using UnityEngine;
using System.Collections;

public class AudioStateMachine : MonoBehaviour {

	public enum AudioResponse {
		Yes,
		No,
		Spit
	};

	private AudioState currentAudioState;

	// Use this for initialization
	void Start () {
		this.currentAudioState = GameObject.Find ("Main Camera").GetComponent<AudioState>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ChangeAudioState (AudioResponse response) {
		// Don't accept input if audio is already playing
		if (this.currentAudioState.audio.isPlaying)
			return;

		switch (response) {
		case AudioResponse.Yes:
			this.currentAudioState.audio.clip = this.currentAudioState.yes;
			break;
		case AudioResponse.No:
			this.currentAudioState.audio.clip = this.currentAudioState.no;
			break;
		case AudioResponse.Spit:
			this.currentAudioState.audio.clip = this.currentAudioState.spit;
			break;
		}
	}
}
