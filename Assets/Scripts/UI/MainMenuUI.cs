using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
	[SerializeField] private Button playButton;
	[SerializeField] private Button optionsButton;
	[SerializeField] private Button exitButton;

	private void Awake()
	{
		playButton.onClick.AddListener(() =>
		{
			Loader.Load(Loader.Scene.GameScene);
		});

		exitButton.onClick.AddListener(() =>
		{
			Application.Quit();
		});
	}
}
