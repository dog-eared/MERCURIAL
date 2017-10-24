using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlaster : ProjectileWeapon {

	public override void Awake() {

		weaponName = "Light Blaster";
		rateOfFire = 2f;
		bulletLifespan = 6f;

		//GameObject projectile;

		base.Awake();
	}

}
