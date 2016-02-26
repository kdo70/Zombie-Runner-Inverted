using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour {

	private Animator anim;
	private InputState inputState;
	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		inputState = GetComponent<InputState> ();
	}
	
	// Update is called once per frame
	void Update () {
		var running = true;

		if(inputState.absVelX > 0 && inputState.absVelY < inputState.standingThreshold){
			running = false;
		}

		anim.SetBool ("Running", running);
	}
}
