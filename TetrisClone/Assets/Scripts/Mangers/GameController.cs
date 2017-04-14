using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	Board gameBoard;
	Spawner spawner;

	Shape activeShape;

	float dropInterval = 0.25f;
	float timeToDrop;

	[Range(0.02f, 1f)]
	public float keyRepeatRateLeftRight = 0.25f;

	[Range(0.01f, 1f)]
	public float keyRepeatRateDown = 0.02f;

	[Range(0.02f, 1f)]
	public float keyRepeatRateRotate = 0.25f;

	float timeToNextKeyLeftRight;
	float timeToNextKeyDown;
	float timeToNextKeyRotate;

	bool isGameOver = false;

	public GameObject gameOverPanel;

	SoundManager soundManager;

	// Use this for initialization
	void Start () {

		timeToNextKeyLeftRight = Time.time;
		timeToNextKeyDown = Time.time;
		timeToNextKeyRotate = Time.time;


		gameBoard = GameObject.FindObjectOfType<Board>();
		spawner = GameObject.FindObjectOfType<Spawner>();
		soundManager = GameObject.FindObjectOfType<SoundManager> ();

		if (spawner) {
			spawner.transform.position = Vectorf.Round (spawner.transform.position);
			if(activeShape == null){
				activeShape = spawner.SpawnShape ();
			}
		}

		if(gameOverPanel){
			gameOverPanel.SetActive (false);
		}
	}



	void PlayerInput ()
	{

		if (Input.GetButton ("MoveRight") && (Time.time > timeToNextKeyLeftRight) || Input.GetButtonDown ("MoveRight")) {

			activeShape.MoveRight ();
			timeToNextKeyLeftRight = Time.time + keyRepeatRateLeftRight;

			if (!gameBoard.IsValidPosition (activeShape)) {
				activeShape.MoveLeft ();
				playSound (soundManager.errorSound, 0.5f);
			} else {
				playSound (soundManager.moveSound, 0.5f);
			}
		} else if (Input.GetButton ("MoveLeft") && (Time.time > timeToNextKeyLeftRight) || Input.GetButtonDown ("MoveLeft")) {
			activeShape.MoveLeft ();
			timeToNextKeyLeftRight = Time.time + keyRepeatRateLeftRight;

			if (!gameBoard.IsValidPosition (activeShape)) {
				activeShape.MoveRight ();
				playSound (soundManager.errorSound, 0.5f);
			} else {
				playSound (soundManager.moveSound, 0.5f);
			}
		} 
		else if (Input.GetButtonDown ("Rotate") && (Time.time > timeToNextKeyRotate)) {
			activeShape.RotateRight ();
			timeToNextKeyRotate = Time.time + keyRepeatRateRotate;

			if (!gameBoard.IsValidPosition(activeShape)){
				activeShape.RotateLeft ();
				playSound (soundManager.errorSound, 0.5f);
			} else {
				playSound (soundManager.moveSound, 0.5f);
			}
		}
		//自动下落和按键下落都放在这里了
		else if (Input.GetButton("MoveDown") && (Time.time > timeToNextKeyDown) || (Time.time > timeToDrop)){
			timeToDrop = Time.time + dropInterval;
			timeToNextKeyDown = Time.time + keyRepeatRateDown;
			activeShape.MoveDown ();

			//到底了
			if (!gameBoard.IsValidPosition (activeShape)) {
				if (gameBoard.IsOverLimit (activeShape)) {
					gameOver ();

				} else {
					landShape ();
				}

			}

		}
	}

	void landShape(){
		activeShape.MoveUp ();
		gameBoard.StoreShapeInGrid (activeShape);
		activeShape = spawner.SpawnShape ();

		timeToNextKeyLeftRight = Time.time;
		timeToNextKeyDown = Time.time;
		timeToNextKeyRotate = Time.time;

		gameBoard.ClearAllRows ();

		playSound (soundManager.dropSound, 0.75f);

		if (gameBoard.completedRows > 0) {

			if (gameBoard.completedRows > 1){
				AudioClip randomVocal = soundManager.GetRandomClip (soundManager.vocalClips);
				playSound (randomVocal, 0.7f);
			}

			playSound (soundManager.clearRowSound, 1f);
		}
	}
		

	// Update is called once per frame
	void Update () {

		if (!gameBoard || !spawner || !activeShape || isGameOver || !soundManager)
			return;

		PlayerInput ();
	}

	void gameOver ()
	{
		activeShape.MoveUp ();
		isGameOver = true;
		if (gameOverPanel) {
			gameOverPanel.SetActive (true);
		}

		playSound (soundManager.gameOverSound, 5f);
		playSound (soundManager.gameOverVocalClip, 5f);
	}

	public void Restart(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	void playSound(AudioClip clip, float volMultiplier){
		if(soundManager.fxEnabled && clip){
			AudioSource.PlayClipAtPoint (clip, Camera.main.transform.position, Mathf.Clamp(soundManager.fxVolume * volMultiplier, 0.05f,1f));	
		}
	}
}
 