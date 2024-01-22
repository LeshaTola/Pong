using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class TutorialUI : MonoBehaviour
{
	[SerializeField] private Button startButton;
	[SerializeField] private Image tutorialImage;
	[SerializeField] private GameManager gameManager;

	[Header("Tutorials")]
	[SerializeField] private Sprite ruTutorial;
	[SerializeField] private Sprite engTutorial;

	private void Awake()
	{
		startButton.onClick.AddListener(() =>
		{
			gameManager.UnpauseGame();
			Destroy(gameObject);
		});
	}

	private void Start()
	{
		gameManager.PauseGame();

		gameManager.OnGamePaused += Hide;
		gameManager.OnGameUnPaused += Show;

		UpdateUI();
	}

	private void OnDestroy()
	{
		gameManager.OnGamePaused -= Hide;
		gameManager.OnGameUnPaused -= Show;
	}

	private async void UpdateUI()
	{
		while (!YandexGame.SDKEnabled)
		{
			await Task.Delay(200);
		}

		var lang = YandexGame.EnvironmentData.language;

		switch (lang)
		{
			case "ru":
				tutorialImage.sprite = ruTutorial;
				break;
			case "en":
				tutorialImage.sprite = engTutorial;
				break;
		}
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
