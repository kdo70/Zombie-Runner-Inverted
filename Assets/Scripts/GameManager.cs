using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	public Text continueText;
	private float blinkTime = 0f;
	private bool blink;
	private GameObject resetBestScoreButton;

	public Text scoreText;
	public Text pointText;
	public static float timeElapsed = 0f;
	private float bestTime = 0f;
	private bool bestBestTime;

	public GameObject playerPrefab;
	public static float points;
	private float bestPoints;


	public static bool gameStarted;
	private GameObject player;
	private TimeManager timeManager;
	private GameObject floor;
	private Spawner spawner;
	private Spawner spawner2;


	void Awake(){
		resetBestScoreButton = GameObject.Find ("ResetBestScoreButton");
		floor = GameObject.Find ("Foreground");
		spawner = GameObject.Find ("Spawner").GetComponent<Spawner> ();
		spawner2 = GameObject.Find ("Spawner 2").GetComponent<Spawner> ();
		timeManager = GetComponent<TimeManager> ();
	}
	// Use this for initialization
	void Start () {
		var floorHeight = floor.transform.localScale.y;

		var pos = floor.transform.position;
		pos.x = 0;
		pos.y = -((Screen.height / PixelPerfectCamera.pixelsToUnits) / 2) + (floorHeight / 2);
		floor.transform.position = pos;

		spawner.active = false;
		Time.timeScale = 0;

		continueText.text = "PRESS SPACE TO START";

		bestTime = PlayerPrefs.GetFloat("BestTime");
		bestPoints = PlayerPrefs.GetFloat("BestPoints");
	}
	
	// Update is called once per frame
	void Update () {
		if(!gameStarted && Time.timeScale == 0){
			if(Input.GetKeyDown(KeyCode.Space)){
				timeManager.ManipulateTime (1, 1f);
				ResetGame ();
			}
		}
		if(!gameStarted){
			blinkTime++;
			if(blinkTime% 40 == 0){
				blink = !blink;
			}
			continueText.canvasRenderer.SetAlpha (blink ? 0 : 1);

			var textColor = bestBestTime ? "#FF0" : "#FFF";

			scoreText.text = "TIME: " + FormatTime (timeElapsed) + "\n<color=" + textColor + ">BEST: " + FormatTime (bestTime) + "</color>";
			pointText.text = "BEST POINTS: " + bestPoints;
		} else {
			timeElapsed += Time.deltaTime;
			scoreText.text = "TIME: " + FormatTime (timeElapsed);
			pointText.text = "POINTS: " + points;
		}
	}

	void OnPlayerKilled(){
		spawner.active = false;
		var playerDestroyScript = player.GetComponent<DestroyOffscreen> ();
		playerDestroyScript.DestroyCallBack -= OnPlayerKilled;

		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		timeManager.ManipulateTime (0, 5.5f);
		gameStarted = false;

		continueText.text = "PRESS SPACE TO START";
		resetBestScoreButton.SetActive (true);

		if(timeElapsed > bestTime){
			bestTime = timeElapsed;
			PlayerPrefs.SetFloat ("BestTime", bestTime);
			bestBestTime = true;
		}
		if(points > bestPoints){
			bestPoints = points;
			PlayerPrefs.SetFloat ("BestPoints", bestPoints);
		}
	}

	public void ResetBestScore(){
		PlayerPrefs.SetFloat ("BestTime", 0);
		PlayerPrefs.SetFloat ("BestPoints", 0);
		bestTime = PlayerPrefs.GetFloat("BestTime");
		bestPoints = PlayerPrefs.GetFloat("BestPoints");
		print ("Test");
	}

	void ResetGame() {
		spawner.active = true;
		resetBestScoreButton.SetActive (false);

		spawner.ResetSpawner();
		spawner2.ResetSpawner();

		player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(0,0,0));

		var playerDestroyScript = player.GetComponent<DestroyOffscreen> ();
		playerDestroyScript.DestroyCallBack += OnPlayerKilled;

		gameStarted = true;

		continueText.canvasRenderer.SetAlpha (0);

		points = 0;
		timeElapsed = 0;

		bestBestTime = false;
	}

	string FormatTime(float value){
		TimeSpan t = TimeSpan.FromSeconds (value);

		return string.Format("{0:D2}:{1:D2}",t.Minutes,t.Seconds);
	}

}
