using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	int score = 0;
	int lines; //当前 level 需要清除多少行才能 level up
	public int level = 1;

	//到达下一个 level 需要清除多少行
	public int linesPerLevel = 5;

	//可以清除的行数
	const int minLines = 1;
	const int maxLines = 4;

	public Text linesText;
	public Text levelText;
	public Text scoreText;

	public bool didLevelUp = false;

	public void ScoreLines(int n){
		didLevelUp = false;
		n = Mathf.Clamp (n, minLines, maxLines);

		switch(n){
		case 1:
			score += 40 * level;
			break;
		case 2:
			score += 100 * level;
			break;
		case 3:
			score += 300 * level;
			break;
		case 4:
			score += 1200 * level;
			break;
		}

		//每次清除行之后，目标 lines 的数量相应减少
		lines -= n;

		if (lines <= 0) {
			LevelUp ();
		}

		UpdateUIText ();
	}

	public void Reset(){
		level = 1;
		lines = linesPerLevel * level;

		UpdateUIText ();
	}

	// Use this for initialization
	void Start () {
		Reset ();

	}
	
	void UpdateUIText(){
		linesText.text = lines.ToString ();
		levelText.text = level.ToString ();
		scoreText.text = PadZero(score, 5);
	}

	string PadZero(int n, int padDigits){
		string nStr = n.ToString();
		while (nStr.Length < padDigits) {
			nStr = "0" + nStr;
		}
		return nStr;
	}

	public void LevelUp(){
		level++;
		lines = linesPerLevel * level;
		didLevelUp = true;
	}
}
