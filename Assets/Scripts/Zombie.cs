using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	private InstantVelocity velo;

	void Awake () {
		velo = GetComponent<InstantVelocity> ();
	}
	

	void Update () {
		if(GameManager.gameStarted && velo.velocity.x > -200 && GameManager.timeElapsed > 45){
			velo.velocity.x -= Time.deltaTime;
		} else {
			velo.velocity.x = -130;
		}
	}
}
