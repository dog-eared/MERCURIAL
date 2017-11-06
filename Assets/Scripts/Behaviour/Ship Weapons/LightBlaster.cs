using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlaster : ProjectileWeapon {

	public override void Awake() {

		weaponName = "Light Blaster";

		if (projectile == null) {
			projectile = Resources.Load("/Projectiles/LightBlasterShot", typeof(GameObject))  as GameObject;
		}

		base.Awake();

	}

}
