using TMPro;
using UnityEngine;

public class TopBar : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI speedText;
	[SerializeField] private GameManager gameManager;
	[SerializeField] private Ball ball;

	private void OnEnable()
	{
		gameManager.OnGameReloaded += OnGameReloaded;
		ball.OnSpeedChanged += OnSpeedChanged;
	}

	private void OnDisable()
	{
		gameManager.OnGameReloaded -= OnGameReloaded;
		ball.OnSpeedChanged -= OnSpeedChanged;
	}

	private void OnGameReloaded(int pl1Score, int pl2Score, float speed)
	{
		UpdateUI(pl1Score, pl2Score, speed);
	}

	private void OnSpeedChanged(float speed)
	{
		UpdateSpeed(speed);
	}

	private void UpdateUI(int pl1Score, int pl2Score, float speed)
	{
		UpdateScore(pl1Score, pl2Score);
		UpdateSpeed(speed);
	}

	private void UpdateScore(int pl1Score, int pl2Score)
	{
		scoreText.text = $"{pl1Score}:{pl2Score}";
	}

	private void UpdateSpeed(float speed)
	{
		speedText.text = $"Speed: {speed}";
	}
}
