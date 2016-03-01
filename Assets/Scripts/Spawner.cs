using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] prefabs;
	public float delay = 5.0f;
	public bool active = true;
	public Vector2 delayRange = new Vector2(.5f,5);

	// Use this for initialization
	void Start () {
		ResetDelay ();
		StartCoroutine(EnemyGenerator());
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

	IEnumerator ShortenDelayRange() {
		yield return new WaitForSeconds (15f);
		if(delayRange.y > 2) {
			delayRange.y -= 1;
			StartCoroutine (ShortenDelayRange ());
		}
	}

	public void ResetSpawner(){
		delayRange.y = 5;
		StartCoroutine (ShortenDelayRange ());
	}

	void ResetDelay(){
		delay = Random.Range (delayRange.x, delayRange.y);
	}
}
