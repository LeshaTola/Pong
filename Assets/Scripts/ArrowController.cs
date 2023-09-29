using UnityEngine;

public class ArrowController : MonoBehaviour
{
	[SerializeField] private GameManager gameManager;
	[SerializeField] private Ball ball;

	private void OnEnable()
	{
		gameManager.OnStateChanged += OnGameStateChanged;
		//Hide();
	}

	private void OnDestroy()
	{
		gameManager.OnStateChanged -= OnGameStateChanged;
	}

	private void OnGameStateChanged(object sender, System.EventArgs e)
	{
		if (gameManager.State == GameState.Preparation)
		{
			Reload();
			Show();
		}
		else
		{
			Hide();
		}
	}

	private void Reload()
	{
		transform.rotation = Quaternion.Euler(Vector2.up);
		var angle = Vector2.Angle(Vector2.up, ball.StartDirection.normalized);
		//Debug.Log(Vector2.up + " " + ball.StartDirection + " " + angle);
		transform.rotation = Quaternion.Euler(0f, 0f, angle * (ball.StartDirection.x > 0 ? -1 : 1));
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
