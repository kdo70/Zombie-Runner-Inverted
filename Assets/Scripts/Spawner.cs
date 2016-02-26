using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] prefabs;
	public float delay = 5.0f;
	public bool active = true;
	public Vector2 delayRange = new Vector2(.5f,5);
	public float clock = 0;

	// Use this for initialization
	void Start () {
		ResetDelay ();
		StartCoroutine(EnemyGenerator());
	}

	void Update(){
		clock += Time.deltaTime;
		if(clock > 15 && delayRange.y > 2){
			clock = 0;
			delayRange.y -= 1;
		}


	}

	IEnumerator EnemyGenerator(){
		yield return new WaitForSeconds (delay);
		if(active){
			var newTransform = transform;
			GameObjectUtil.Instantiate (prefabs [Random.Range (0, prefabs.Length)], newTransform.position);
			ResetDelay ();
		}
		StartCoroutine(EnemyGenerator());
	}

	public void ResetSpawner(){
		clock = 0;
		delayRange.y = 5;
	}

	void ResetDelay(){
		delay = Random.Range (delayRange.x, delayRange.y);
	}
}
