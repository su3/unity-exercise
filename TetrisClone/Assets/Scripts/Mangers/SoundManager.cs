using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public bool musicEnabled = true;
	public bool fxEnabled = true;

	[Range(0,1)]
	public float musicVolume = 1.0f;

	[Range(0,1)]
	public float fxVolume = 1.0f;

	public AudioClip clearRowSound;
	public AudioClip moveSound;
	public AudioClip dropSound;
	public AudioClip gameOverSound;
	public AudioClip errorSound;

	public AudioSource musicSource;


	public AudioClip[] musicClips;
	private AudioClip randomMusicClip;

	public AudioClip[] vocalClips;

	public AudioClip gameOverVocalClip;

	public IconToggle musicIconToggle;
	public IconToggle fxIconToggle;

	// Use this for initialization
	void Start () {
		randomMusicClip = GetRandomClip (musicClips);
		playBackgroundMusic (randomMusicClip);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public AudioClip GetRandomClip(AudioClip[] clips){
		AudioClip randomClip = clips [Random.Range (0, clips.Length)];
		return randomClip;
	}

	public void playBackgroundMusic(AudioClip musicClip){
		if(!musicEnabled || !musicClip || !musicSource){
			return;
		}

		musicSource.Stop ();
		musicSource.clip = musicClip;

		musicSource.volume = musicVolume;
		musicSource.loop = true;
		musicSource.Play ();
	}

	void UpdateMusic(){
		if (musicSource.isPlaying != musicEnabled) {
			if (musicEnabled) {
				randomMusicClip = GetRandomClip (musicClips);
				playBackgroundMusic (randomMusicClip);
			} else {
				musicSource.Stop ();
			}
		}
	}

	public void ToggleMusic(){
		musicEnabled = !musicEnabled;
		UpdateMusic ();

		if(musicIconToggle){
			musicIconToggle.ToggleIcon (musicEnabled);
		}
	}

	public void ToggleFX(){
		fxEnabled = !fxEnabled;

		if (fxIconToggle) {
			fxIconToggle.ToggleIcon (fxEnabled);
		}
	}
}
