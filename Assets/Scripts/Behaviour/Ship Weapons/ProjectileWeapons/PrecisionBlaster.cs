using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrecisionBlaster : ProjectileWeapon {

	public override void Awake() {

		weaponName = "Precision Blaster";

		if (projectile == null) {
			projectile = Resources.Load("Prefabs/Projectiles/" + GetColor(color) + "PrecisionBlasterShot", typeof(GameObject))  as GameObject;
		}

		base.Awake();

	}

}
