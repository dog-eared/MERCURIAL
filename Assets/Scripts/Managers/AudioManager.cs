using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public List<AudioSource> soundSources = new List<AudioSource>();
	public AudioSource musicSource;


	void Awake() {
		GetAllSoundSources();
	}


	void GetAllSoundSources() {
		GameObject[] allShips = GameObject.FindGameObjectsWithTag("Ship");

		for (var x = 0; x < allShips.Length; x++) {
			soundSources.Add(allShips[x].GetComponent<AudioSource>());
		}
	}


	public void PlaySingleFX(AudioClip clip, int sourceIndex) {
		soundSources[sourceIndex].clip = clip;
		soundSources[sourceIndex].PlayOneShot(clip, 1);
	}

}
