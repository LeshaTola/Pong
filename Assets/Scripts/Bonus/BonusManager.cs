using UnityEngine;

public class BonusManager : MonoBehaviour
{
	[SerializeField] private BonusTrigger firstPlayerBonus;
	[SerializeField] private BonusTrigger secondPlayerBonus;

	[SerializeField] private Player firstPlayer;
	[SerializeField] private Player secondPlayer;

	[SerializeField] private float rangeInX;
	[SerializeField] private float rangeInY;

	private void OnEnable()
	{

		firstPlayer.OnBallCollision += OnBallCollision;
		secondPlayer.OnBallCollision += OnBallCollision;
	}

	private void OnDisable()
	{
		firstPlayer.OnBallCollision -= OnBallCollision;
		secondPlayer.OnBallCollision -= OnBallCollision;
	}

	private void OnBallCollision(PlayerEnum player)
	{
		if (player == PlayerEnum.FirstPlayer)
		{
			UpdatePosition(secondPlayerBonus);
		}
		else
		{
			UpdatePosition(firstPlayerBonus);
		}
	}

	private void UpdatePosition(BonusTrigger bonus)
	{
		if (bonus.gameObject.activeSelf == false)
		{
			bonus.gameObject.SetActive(true);
		}
		bonus.transform.localPosition = new Vector2(Random.Range(-rangeInX, rangeInX),
			Random.Range(-rangeInY, rangeInY));
	}

	public void Reload()
	{
		firstPlayerBonus.gameObject.SetActive(false);
		secondPlayerBonus.gameObject.SetActive(false);
	}
}
