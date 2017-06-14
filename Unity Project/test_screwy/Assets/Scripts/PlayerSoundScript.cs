using UnityEngine;
using System.Collections;

public class PlayerSoundScript : MonoBehaviour {

	public AudioClip applauseSound;
	public float applauseVolume;

	public AudioClip jumpSound;
	public float jumpVolume;

	public AudioClip levelStartSound;
	public float levelStartVolume;

	public AudioClip pickupSound;
	public float pickupVolume;

	public AudioClip splatLaughterSound;
	public float splatLaughterVolume;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void playApplause () {
		playSound(applauseSound, applauseVolume);
	}

	public void playJumpSound () {
		playSound(jumpSound, jumpVolume);
	}

	public void playLevelStartSound () {
		playSound(levelStartSound, levelStartVolume);
	}

	public void playPickupSound () {
		playSound(pickupSound, pickupVolume);
	}

	public void playSplatLaughter () {
		playSound(splatLaughterSound, (splatLaughterVolume));
	}

	private void playSound (AudioClip sound, float volume) {
		audio.volume = volume;
		audio.clip = sound;
		audio.Play();
	}
}
