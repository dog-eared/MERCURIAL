using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public List<AudioSource> soundSources = new List<AudioSource>();
	public AudioSource musicSource;


	void Awake() {
		//GetAllSoundSources();
	}



	public void PlaySingleFX(AudioClip clip, int sourceIndex) {
		soundSources[sourceIndex].clip = clip;
		soundSources[sourceIndex].PlayOneShot(clip, 1);
	}

}
