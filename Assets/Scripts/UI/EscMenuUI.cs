using UnityEngine;
using UnityEngine.UI;

public class EscMenuUI : MonoBehaviour
{
	[SerializeField] private Button continueButton;
	[SerializeField] private Button exitButton;
	[SerializeField] private GameManager gameManager;

	private void Awake()
	{
		continueButton.onClick.AddListener(() =>
		{
			gameManager.UnpauseGame();
		});

		exitButton.onClick.AddListener(() =>
		{
			Loader.Load(Loader.Scene.MainMenu);
		});
	}

	private void Start()
	{
		gameManager.OnGamePaused += Show;
		gameManager.OnGameUnaused += Hide;

		Hide();
	}

	private void OnDestroy()
	{
		gameManager.OnGamePaused -= Show;
		gameManager.OnGameUnaused -= Hide;
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
