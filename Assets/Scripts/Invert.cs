using UnityEngine;
using System.Collections;

public class Invert : MonoBehaviour {

	private Rigidbody2D body2d;
	private InputState inputState;
	public bool upsideDown = false;
	private bool delay = false;

	void Awake(){
		body2d = GetComponent<Rigidbody2D> ();
		inputState = GetComponent<InputState> ();
	}


	void Update () {
		if(inputState.standing){
			if (inputState.invertButton) {
				StartCoroutine (InvertPlayer ());
			}
		}
		if(!upsideDown){
			transform.localEulerAngles = new Vector3 (0f, 0, 0);
		} else {
		transform.localEulerAngles = new Vector3 (180f, 0, 0);
		}
	}


	IEnumerator InvertPlayer() {
		if (!upsideDown && !delay) {
			upsideDown = true;
			delay = true;
			body2d.gravityScale = -60;
		} else if(upsideDown && !delay){
			upsideDown = false;
			delay = true;
			body2d.gravityScale = 60;
		}
		yield return new WaitForSeconds (.5f);
		delay = false;
	}
}
