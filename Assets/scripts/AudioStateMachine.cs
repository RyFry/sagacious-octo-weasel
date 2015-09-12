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
	private CircularAudioState circles;
	private bool isACircle = false;
	private bool responded;

	// Use this for initialization
	void Start () {
		this.guestIndex = 0;
		this.currentAudioState = GameObject.Instantiate(guestList[guestIndex]).GetComponent<AudioState>();
		this.responded = false;
		StartIntro ();
	}
	
	// Update is called once per frame
	void Update () {
		if (! this.currentAudioState.audioSource.isPlaying && this.responded == true) {
			print("changing guest");
			guestIndex++;
			isACircle = false;
			if (!guestList[guestIndex].name.Equals( "CircularGuest 1")) {

				this.currentAudioState = GameObject.Instantiate(guestList[guestIndex]).GetComponent<AudioState>();
			}
			else {
				print ("circles");
				isACircle = true;
				circles = GameObject.Instantiate(guestList[guestIndex]).GetComponent<CircularAudioState>();
			}
			this.responded = false;
			StartIntro ();
		}
	}

	void StartIntro () {
		if(isACircle) {
			circles.StartIntro();
		} else {
			this.currentAudioState.StartIntro ();
		}
	}

	public void ChangeAudioState (AudioResponse response) {

		if(!responded) {
			if(isACircle) {
				switch(response){
				case AudioResponse.Yes:
					this.circles.StartYes ();
					return;
				case AudioResponse.No:
				this.circles.StartNo ();
					return;
				case AudioResponse.Spit:
				this.circles.StartSpit ();
				break;
				}
			} else {
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
			}
			this.responded = true;
		}
	}
}