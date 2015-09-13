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
	private GameObject current;

	// Use this for initialization
	void Start () {
		this.guestIndex = 0;
		current = (GameObject)GameObject.Instantiate(guestList[guestIndex], this.transform.position, this.transform.rotation);
		this.currentAudioState = current.GetComponent<AudioState>();

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
				GameObject.Destroy(current);
				current = (GameObject)GameObject.Instantiate(guestList[guestIndex], this.transform.position, this.transform.rotation);
				this.currentAudioState = current.GetComponent<AudioState>();
				this.responded = false;
			}
			else {
				print ("circles");
				isACircle = true;
				circles = GameObject.Instantiate(guestList[guestIndex]).GetComponent<CircularAudioState>();
			}
			StartIntro ();
		}
	}

	void StartIntro () {
		this.responded = false;
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
					if(!currentAudioState.audioSource.isPlaying) {
						this.responded = true;
					}
					this.currentAudioState.StartYes ();
					if(!currentAudioState.audioSource.isPlaying) {
						this.responded = true;
					}
					break;
				case AudioResponse.No:
					if(!currentAudioState.audioSource.isPlaying) {
						this.responded = true;
					}
					this.currentAudioState.StartNo ();
					if(!currentAudioState.audioSource.isPlaying) {
						this.responded = true;
					}
					break;
				case AudioResponse.Spit:
					this.currentAudioState.StartSpit ();
					this.responded = true;
					break;
				}

			}
		}
	}
}