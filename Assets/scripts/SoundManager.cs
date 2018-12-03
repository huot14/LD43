using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	
	public static SoundManager instance;
	public AudioSource effects;
	public AudioSource background;

	public AudioClip teleport;
	public AudioClip trap;

	public enum SoundEffect {
		TELEPORT,
		TRAP,
	}

	void Start() {
		instance = this;
	}

	public void playEffect(SoundEffect effect) {
		switch(effect) {
		case  SoundEffect.TELEPORT:
			this.effects.clip = this.teleport;
			break;
		case SoundEffect.TRAP:
			this.effects.clip  = this.trap;
			break;
		}

		this.effects.Play ();
	}

}
