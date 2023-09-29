using TMPro;
using UnityEngine;

public class PreporationTimerUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI timerText;
	[SerializeField] private GameManager gameManager;

	private void OnEnable()
	{
		gameManager.OnStateChanged += OnGameStateChanged;
		//Hide();
	}

	private void OnDestroy()
	{
		gameManager.OnStateChanged -= OnGameStateChanged;
	}

	private void Update()
	{
		timerText.text = Mathf.Ceil(gameManager.PreparationTimer).ToString();
	}

	private void OnGameStateChanged(object sender, System.EventArgs e)
	{
		if (gameManager.State == GameState.Preparation)
		{
			Show();
		}
		else
		{
			Hide();
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
