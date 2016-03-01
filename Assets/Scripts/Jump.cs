using UnityEngine;
using System.Collections;

public class Jump : MonoBehaviour {

	public float jumpSpeed = 240f;
	public float forwardSpeed = 20;
	private Rigidbody2D body2d;
	private InputState inputState;
	private Invert invert;
	private bool delay = false;

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
		inputState = GetComponent<InputState> ();
		invert = GetComponent<Invert> ();
	}
		
	
	// Update is called once per frame
	void Update () {
		if(inputState.standing){
			if (inputState.actionButton) {
				StartCoroutine (Jumping ());
			}
		}
	}

	IEnumerator Jumping() {
		if (!invert.upsideDown && !delay) {
			body2d.velocity = new Vector2 (transform.position.x < 0 ? forwardSpeed : 0, jumpSpeed);
			delay = true;
		} else if(invert.upsideDown && !delay) {
			body2d.velocity = new Vector2 (transform.position.x < 0 ? forwardSpeed : 0, -jumpSpeed);
			delay = true;
		}

		yield return new WaitForSeconds (.5f);
		delay = false;
	}
}
