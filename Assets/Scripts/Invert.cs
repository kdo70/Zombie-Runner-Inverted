using UnityEngine;
using System.Collections;

public class Invert : MonoBehaviour {

	private Rigidbody2D body2d;
	private InputState inputState;
	public bool upsideDown = false;
	private float delay = .5f;
	private float clock = 0;

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
		inputState = GetComponent<InputState> ();
	}


	void Update () {
		clock += Time.deltaTime;
		if(inputState.standing){
			if (inputState.invertButton) {
				InvertPlayer ();
			}
		}
		if(!upsideDown){
			transform.localEulerAngles = new Vector3 (0f, 0, 0);
		} else {
		transform.localEulerAngles = new Vector3 (180f, 0, 0);
		}
	}

	void InvertPlayer(){
		if (clock > delay && !upsideDown) {
			clock = 0;
			upsideDown = true;
			body2d.gravityScale = -60;
		} else if(clock > delay && upsideDown){
			clock = 0;
			upsideDown = false;
			body2d.gravityScale = 60;
		}
	}
}
