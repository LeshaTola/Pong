using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

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

	private async void UpdateUI()
	{
		while (!YandexGame.SDKEnabled)
		{
			await Task.Delay(200);
		}

		var lang = YandexGame.EnvironmentData.language;

		string firstPlayer = "";
		string secondPlayer = "";
		switch (lang)
		{
			case "ru":
				firstPlayer = "Первый игрок победил";
				secondPlayer = "Второй игрок победил";
				break;
			case "en":
				firstPlayer = "First player wins";
				secondPlayer = "Second player wins";
				break;
		}

		scoreText.text = $"{gameManager.Pl1score}:{gameManager.Pl2score}";
		winnerText.text = gameManager.Pl1score == gameManager.ScoreToWin ? firstPlayer : secondPlayer;
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
