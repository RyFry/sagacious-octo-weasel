using UnityEngine;
using System.Collections;

public class AudioStateMachine : MonoBehaviour {

	public enum AudioResponse {
		Yes,
		No,
		Spit
	};

	public GameObject[] guestList;

	private int guestIndex;

	private AudioState currentAudioState;
	private bool responded;

	// Use this for initialization
	void Start () {
		this.guestIndex = 0;
		this.currentAudioState = this.guestList [0].GetComponent<AudioState> ();
		this.responded = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (! this.currentAudioState.audio.isPlaying && this.responded == true) {
			this.currentAudioState = this.guestList [guestIndex].GetComponent<AudioState> ();
			this.responded = false;
		}
	}

	void StartIntro () {
		this.currentAudioState.StartIntro ();
	}

	void ChangeAudioState (AudioResponse response) {
		// Don't accept input if audio is already playing
		if (this.currentAudioState.audio.isPlaying)
			return;

		switch (response) {
		case AudioResponse.Yes:
			this.currentAudioState.StartYes ();
			break;
		case AudioResponse.No:
			this.currentAudioState.StartNo ();
			break;
		case AudioResponse.Spit:
			this.currentAudioState.StartSpit ();
			break;
		}

		this.responded = true;
	}
}
