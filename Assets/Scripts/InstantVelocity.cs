using UnityEngine;
using System.Collections;

public class InstantVelocity : MonoBehaviour {

	public Vector2 velocity = Vector2.zero;
	private Rigidbody2D body2d;

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
	}

	public void SetVelocity(float xSpeed, float ySpeed) {
		velocity.x = xSpeed;
		velocity.y = ySpeed;
	}

	void FixedUpdate(){
		body2d.velocity = velocity;
	}
}
