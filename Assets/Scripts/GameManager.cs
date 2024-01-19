using System;
using UnityEngine;
using YG;

public enum GameState
{
	WaitingForRediness,
	Preparation,
	GameStart,
	Playing,
	GameOver
}

public class GameManager : MonoBehaviour
{
	public event Action<int, int, float> OnGameReloaded;
	public event Action OnGamePaused;
	public event Action OnGameUnaused;
	public event EventHandler OnStateChanged;

	[field: SerializeField] public int ScoreToWin { get; private set; } = 5;
	[SerializeField] private float PreparationTime = 3f;

	[SerializeField] private GoalZone PL1Zone;
	[SerializeField] private GoalZone PL2Zone;

	[SerializeField] private Ball ball;
	[SerializeField] private BonusManager bonusManager;

	public int Pl1score { get; private set; }
	public int Pl2score { get; private set; }
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
		//ReloadGame();
	}

	private void Update()
	{
		switch (State)
		{
			case GameState.WaitingForRediness:
				ReloadGame();
				State = GameState.Preparation;
				break;
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
				YandexGame.FullscreenShow();
				break;
		}
	}

	public void RestartGame()
	{
		UnpauseGame();
		Loader.Load(Loader.Scene.GameScene);
	}

	private void OnPL2Goaled()
	{
		Pl1score++;
		ReloadGame();
	}

	private void OnPL1Goaled()
	{
		Pl2score++;
		ReloadGame();
	}

	public void TogglePause()
	{
		if (Time.timeScale == 0)
		{
			UnpauseGame();
		}
		else
		{
			PauseGame();
		}
	}

	public void PauseGame()
	{
		OnGamePaused?.Invoke();
		Time.timeScale = 0f;
	}

	public void UnpauseGame()
	{
		OnGameUnaused?.Invoke();
		Time.timeScale = 1f;
	}

	private void ReloadGame()
	{
		if (Pl1score == ScoreToWin || Pl2score == ScoreToWin)
		{
			State = GameState.GameOver;
			OnStateChanged?.Invoke(this, EventArgs.Empty);
		}
		else
		{
			if (ball.IsCharged)
			{
				ball.LastPlayer.AttackBonus.EndBonus();
			}

			bonusManager.Reload();
			ball.Reload();

			State = GameState.Preparation;
			OnStateChanged?.Invoke(this, EventArgs.Empty);
			OnGameReloaded?.Invoke(Pl1score, Pl2score, ball.CurrentSpeed);
		}
	}
}
