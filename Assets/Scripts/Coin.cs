using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {


	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "Player") {
			GameManager.points += 10;
			GameObjectUtil.Destroy (gameObject);
		}

	}
}
