using System;
using UnityEngine;

public class BonusTrigger : MonoBehaviour
{
	public event Action<BonusTrigger> OnPicedUp;

	[SerializeField] private PlayerEnum player;

	public PlayerEnum Player { get => player; private set => player = value; }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.TryGetComponent(out Ball ball))
		{
			int bonusCount = 1;
			if (ball.LastPlayer.PlayerEnum == Player)
			{
				ball.LastPlayer.AddBonus(bonusCount);
				OnPicedUp?.Invoke(this);
				gameObject.SetActive(false);
			}
		}
	}
}
