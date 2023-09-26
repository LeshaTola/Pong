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
	public event EventHandler OnStateChanged;

	[SerializeField] private int ScoreToWin = 5;
	[SerializeField] private float PreparationTime = 3f;

	[SerializeField] private GoalZone PL1Zone;
	[SerializeField] private GoalZone PL2Zone;

	[SerializeField] private Ball ball;

	private int pl1score;
	private int pl2score;

	public float PreparationTimer { get; private set; }
	public GameState State { get; private set; }

	private void OnEnable()
	{
		PL1Zone.OnGoaled += OnPL1Goaled;
		PL2Zone.OnGoaled += OnPL2Goaled;
	}

	private void OnDisable()
	{
		PL1Zone.OnGoaled -= OnPL1Goaled;
		PL2Zone.OnGoaled -= OnPL2Goaled;
	}

	private void Start()
	{
		PreparationTimer = PreparationTime;
		ReloadGame();
	}

	private void Update()
	{
		switch (State)
		{
			case GameState.Preparation:
				PreparationTimer -= Time.deltaTime;
				if (PreparationTimer <= 0)
				{
					PreparationTimer = PreparationTime;
					State = GameState.GameStart;
					OnStateChanged?.Invoke(this, EventArgs.Empty);
				}
				break;
			case GameState.GameStart:
				ball.Push(0f, ball.StartDirection);
				State = GameState.Playing;
				OnStateChanged?.Invoke(this, EventArgs.Empty);
				break;
			case GameState.Playing:

				break;
			case GameState.GameOver:

				State = GameState.GameOver;
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
			State = GameState.GameOver;
			OnStateChanged?.Invoke(this, EventArgs.Empty);
		}
		else
		{
			ball.ReloadBall();
			State = GameState.Preparation;
			OnStateChanged?.Invoke(this, EventArgs.Empty);
			OnGameReloaded?.Invoke(pl1score, pl2score, ball.CurrentSpeed);
		}
	}
}
