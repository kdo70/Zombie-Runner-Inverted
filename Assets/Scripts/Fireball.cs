using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	private InstantVelocity velo;

	void Awake(){
		velo = GetComponent<InstantVelocity> ();
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Obstacles" || col.gameObject.tag == "Coin") {
			GameObjectUtil.Destroy (col.gameObject);
		}

		if (col.gameObject.tag == "Player") {
			velo.SetVelocity (-500, 0);
		}

		if (col.gameObject.tag != "Player") {
			velo.SetVelocity (-225, 0);
		}
	}

}
