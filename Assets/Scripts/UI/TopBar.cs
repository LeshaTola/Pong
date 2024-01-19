using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using YG;

public class TopBar : MonoBehaviour
{
	[Header("Text")]
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private TextMeshProUGUI speedText;

	[Header("Bonus")]
	[SerializeField] private BonusColorChanger bonusTemplate;

	[SerializeField] private Transform firstPlayerBonusContainer;
	[SerializeField] private Transform secondPlayerBonusContainer;

	[Header("Players")]
	[SerializeField] private Player firstPlayer;
	[SerializeField] private Player secondPlayer;

	[Header("Others")]
	[SerializeField] private GameManager gameManager;
	[SerializeField] private Ball ball;


	private List<BonusColorChanger> firstPlayerBonuses;
	private List<BonusColorChanger> secondPlayerBonuses;

	private void Awake()
	{
		firstPlayerBonuses = new List<BonusColorChanger>();
		secondPlayerBonuses = new List<BonusColorChanger>();

		for (int i = 0; i < firstPlayer.MaxBonusCount; i++)
		{
			var newBonusImage = Instantiate(bonusTemplate, firstPlayerBonusContainer);
			newBonusImage.PlayerEnum = PlayerEnum.FirstPlayer;
			firstPlayerBonuses.Add(newBonusImage);
		}

		for (int i = 0; i < secondPlayer.MaxBonusCount; i++)
		{
			var newBonusImage = Instantiate(bonusTemplate, secondPlayerBonusContainer);
			newBonusImage.PlayerEnum = PlayerEnum.SecondPlayer;
			secondPlayerBonuses.Add(newBonusImage);
		}
	}

	private void OnEnable()
	{
		gameManager.OnGameReloaded += OnGameReloaded;
		ball.OnSpeedChanged += OnSpeedChanged;
		firstPlayer.OnBonusCountChanged += OnFirstPlayerBonusCountChanged;
		secondPlayer.OnBonusCountChanged += OnsecondPlayerBonusCountChanged;
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

	private async void UpdateSpeed(float speed)
	{
		while (!YandexGame.SDKEnabled)
		{
			await Task.Delay(200);
		}

		var lang = YandexGame.EnvironmentData.language;

		string speedString = "";
		switch (lang)
		{
			case "ru":
				speedString = "Скорость";
				break;
			case "en":
				speedString = "Speed";
				break;
		}

		int scoreMultiplayer = 10;
		speedText.text = $"{speedString}: {Mathf.RoundToInt(speed * scoreMultiplayer)}";
	}

	private void OnFirstPlayerBonusCountChanged(int index)
	{
		UpdateBonusesList(firstPlayerBonuses, index);
	}

	private void OnsecondPlayerBonusCountChanged(int index)
	{
		UpdateBonusesList(secondPlayerBonuses, index);
	}

	private void UpdateBonusesList(List<BonusColorChanger> images, int index)
	{
		for (int i = 0; i < index; i++)
		{
			images[i].ActivateBonus();
		}

		for (int i = index; i < images.Count; i++)
		{
			images[i].DeactivateBonus();
		}
	}
}
