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
	public event Action OnGameReloaded;

	[SerializeField] private int ScoreToWin = 5;

	[SerializeField] private GoalZone PL1Zone;
	[SerializeField] private GoalZone PL2Zone;

	[SerializeField] private Ball ball;

	private GameState state;

	public float PL1score { get; private set; }
	public float PL2score { get; private set; }

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
				state = GameState.Playing;
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
		PL2score++;
		ReloadGame();
	}

	private void OnPL1Goaled()
	{
		PL1score++;
		ReloadGame();
	}

	private void ReloadGame()
	{
		if (PL1score == ScoreToWin || PL2score == ScoreToWin)
		{
			state = GameState.GameOver;
		}
		else
		{
			ball.ReloadBall();
			state = GameState.Preparation;
			OnGameReloaded?.Invoke();
		}
	}
}
