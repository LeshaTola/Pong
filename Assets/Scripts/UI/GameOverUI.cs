using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
	[Header("UI")]
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI winnerText;
	[SerializeField] private Button restartButton;
	[Header("Game manager")]
	[SerializeField] private GameManager gameManager;

	private void Awake()
	{
		restartButton.onClick.AddListener(() =>
		{
			gameManager.RestartGame();
		});
	}

	private void OnEnable()
	{
		gameManager.OnStateChanged += OnGameStateChanged;
	}

	private void Start()
	{
		Hide();
	}

	private void OnDestroy()
	{
		gameManager.OnStateChanged -= OnGameStateChanged;
	}

	private void OnGameStateChanged(object sender, System.EventArgs e)
	{
		if (gameManager.State == GameState.GameOver)
		{
			UpdateUI();
			Show();
		}
	}

	private void UpdateUI()
	{
		scoreText.text = $"{gameManager.Pl1score}:{gameManager.Pl2score}";
		winnerText.text = gameManager.Pl1score == gameManager.ScoreToWin ? "first player" : "second player" + " wins";
	}

	private void Show()
	{
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
}
