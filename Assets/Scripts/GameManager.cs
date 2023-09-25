using System;
using UnityEngine;

public enum GameState
{
	Preparation,
	GameStart,
	Playing,
	GameOver
}

public class GameManager : MonoBehaviour
{
	public event Action<int, int, float> OnGameReloaded;

	[SerializeField] private int ScoreToWin = 5;

	[SerializeField] private GoalZone PL1Zone;
	[SerializeField] private GoalZone PL2Zone;

	[SerializeField] private Ball ball;

	private GameState state;
	private int pl1score;
	private int pl2score;

	private void Awake()
	{
		state = GameState.Preparation;
		PL1Zone.OnGoaled += OnPL1Goaled;
		PL2Zone.OnGoaled += OnPL2Goaled; ;
	}

	private void Update()
	{
		switch (state)
		{
			case GameState.Preparation:
				ReloadGame();
				state = GameState.GameStart;
				break;
			case GameState.GameStart:
				ball.Push(0f, ball.StartDirection);
				state = GameState.Playing;
				break;
			case GameState.Playing:

				break;
			case GameState.GameOver:

				state = GameState.GameOver;
				break;
		}
	}

	private void OnPL2Goaled()
	{
		pl2score++;
		ReloadGame();
	}

	private void OnPL1Goaled()
	{
		pl1score++;
		ReloadGame();
	}

	private void ReloadGame()
	{
		if (pl1score == ScoreToWin || pl2score == ScoreToWin)
		{
			state = GameState.GameOver;
		}
		else
		{
			ball.ReloadBall();
			state = GameState.Preparation;
			OnGameReloaded?.Invoke(pl1score, pl2score, ball.CurrentSpeed);
		}
	}
}
