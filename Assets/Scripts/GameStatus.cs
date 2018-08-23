using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour {

	//config parameters
	[Range(0.1f, 10f)][SerializeField] float gameSpeed = 1f;
	[SerializeField] int pointsPerBlockDestroyed = 64;
	//states
	[SerializeField] int currentScore = 0;
	[SerializeField] TextMeshProUGUI scoreText;

	[SerializeField] bool isAutoPlayEnabled;

	private void Awake() 
	{
		int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
		if(gameStatusCount > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start() 
	{
		scoreText.text = currentScore.ToString();
		

	}
	
	// Update is called once per frame
	void Update () {
		Time.timeScale = gameSpeed;
	}

	public void AddPointsToScore()
	{
		currentScore += pointsPerBlockDestroyed;
		scoreText.text = currentScore.ToString();
		
	}

	public void ResetGameStatus()
	{

		Destroy(gameObject);
	}

	public bool IsAutoplayEnabled()
	{
		return isAutoPlayEnabled;
	}
}
