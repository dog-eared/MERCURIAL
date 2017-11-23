using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : MonoBehaviour {

	public string shipModel; //Style of ship
	public string shipName; //Name of ship
	AudioSource _audioSource;
	ShipChassis _chassis;

	[Space(10)]

	[Tooltip("translates to: Player, Faction1, Faction2, Faction3, Faction4")]
	public FactionsList faction; //Pick from list, will be translated to value factionString;
	string factionString;

	[Space(10)]

	[Header("Combat Stats:")]

	public float shipHullCurrent;
	public float shipHullMax;

	public float shipShieldCurrent;
	public float shipShieldMax;

	public int shieldRegenAmount = 1;
	public int shieldRegenFrequency;

	[Space(10)]

	public GameObject explosionAnimation;
	public AudioClip explosionSound;

	public float invincibilityFramesTime = 0.1f;
	bool invincibilityFramesOn = false;

	Renderer _renderer;

	//Has this reference so it can post messages on death, communications, etc!
	GUIBehaviour _guiBehaviour;

	/*
	FIXME: GUI manager is set by InputManager if it's supposed to be attached to the player.
	*/
	[HideInInspector]
	public GUIManager _guiManager;

	void Awake() {
		this.name = shipName;
		this.tag = SetFactionString(faction) + "Ship";
		_audioSource = GetComponent<AudioSource>();
		_chassis = GetComponent<ShipChassis>();
		_renderer = GetComponent<Renderer>();
		_guiBehaviour = GameObject.FindGameObjectWithTag("GUI_Listener").GetComponent<GUIBehaviour>();

		InvokeRepeating("ShieldsRegenerate", 6, shieldRegenFrequency);
	}


	public void TakeDamage(int damage) {

		if (!invincibilityFramesOn) {

			InvincibilityOn();
			Invoke("InvincibilityOff", invincibilityFramesTime);

			if (shipShieldCurrent > damage) {
				shipShieldCurrent -= damage;
			} else if (shipShieldCurrent < damage) {
				shipHullCurrent -= (damage - shipShieldCurrent);
				shipShieldCurrent = 0;
			} else if (shipShieldCurrent <= 0) {
				shipHullCurrent -= damage;
			}

			if (_guiManager != null) {
				_guiManager.SetBarAmount("hull", (shipHullCurrent / shipHullMax));
				_guiManager.SetBarAmount("shield", (shipShieldCurrent / shipShieldMax));
			}

			if (shipHullCurrent <= 0) {
				_guiBehaviour.ReceiveMessage(this.name + " has been destroyed.", true);
				Death();
			}

		}

	}

	void InvincibilityOn() {
		invincibilityFramesOn = true;
		_renderer.material.color = new Color(4, 0, 0);
	}


	void InvincibilityOff() {
		invincibilityFramesOn = false;
		_renderer.material.color = new Color(1, 1, 1);
	}


	void ShieldsRegenerate() {
		if (shipShieldCurrent < shipShieldMax) {

			shipShieldCurrent += shieldRegenAmount;

			if (shipShieldCurrent > shipShieldMax) {
				shipShieldCurrent = shipShieldMax;
			}

			if (_guiManager != null) {
				_guiManager.SetBarAmount("shield", (shipShieldCurrent / shipHullMax));
			}

		}
	}


	void Death() {

		Instantiate(explosionAnimation, new Vector3(transform.position.x, transform.position.y, 2), Quaternion.identity);
		Destroy(gameObject);
	}


	public string SetFactionString(FactionsList faction) {
			//For myself later:
			//By casting the int to the FactionsList, we get the faction at the enum location
			//This way if I'm crazy and decide to rename a faction in this enum, it'll
			//still get the right value

			switch (faction) {
				case (FactionsList)0:
					return "Player";
				case (FactionsList)1:
					return "Faction1";
				case (FactionsList)2:
					return "Faction2";
				case (FactionsList)3:
					return "Faction3";
				case (FactionsList)4:
					return "Faction4";
				case (FactionsList)5:
					return "Faction5";
				case (FactionsList)6:
					return "Faction6";
				default:
					return "Faction7";
			}
		}


}

public enum FactionsList {
	Player,
	Earth,
	TradeAlliance,
	Rebels,
	Scourge,
	Pirate,
	Special,
	Neutral
}
