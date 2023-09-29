using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	public event Action<int> OnBonusCountChanged;
	public event Action<PlayerEnum> OnBallCollision;

	[SerializeField] private Transform bounceDirection;

	[field: SerializeField] public Bonus AttackBonus { get; private set; }
	[field: SerializeField] public PlayerEnum PlayerEnum { get; private set; }
	[field: SerializeField] public Bonus DefendBonus { get; private set; }
	[field: SerializeField] public int MaxBonusCount { get; private set; } = 2;

	private int bonusCount;
	private bool isAttackBonus;
	private Ball ball;

	private void Awake()
	{
		AttackBonus.Init(this);
		DefendBonus.Init(this);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent(out ball))
		{
			if (ball.IsCharged)
			{
				ball.LastPlayer.AttackBonus.EndBonus();
			}

			if (isAttackBonus)
			{
				AttackInternal();
			}

			Deflect(collision);
			OnBallCollision?.Invoke(PlayerEnum);
			ball.LastPlayer = this;
		}
	}

	public void MultiplySize(float multiplier)
	{
		var newLocalScale = transform.localScale;
		newLocalScale.y *= multiplier;
		transform.localScale = newLocalScale;
	}

	public void AddBonus(int count)
	{
		bonusCount += count;
		bonusCount = bonusCount > MaxBonusCount ? MaxBonusCount : bonusCount;
		OnBonusCountChanged?.Invoke(bonusCount);
	}

	public void ChargeBall(float value)
	{
		ball.IncreaseSpeed(value);
		ball.IsCharged = true;
	}

	public void UnchargeBall(float value)
	{
		ball.IncreaseSpeed(-value);
		ball.IsCharged = false;
	}

	public void Attack()
	{
		if (AttackBonus.IsActivateOnTouch)
		{
			isAttackBonus = true;
		}
		else
		{
			AttackInternal();
		}
	}

	private void AttackInternal()
	{
		if (bonusCount > 0 && !AttackBonus.IsActive)
		{
			AttackBonus.StartBonus();
			bonusCount--;
			OnBonusCountChanged?.Invoke(bonusCount);
		}
		isAttackBonus = false;
	}

	public void Defend()
	{
		if (bonusCount > 0 && !DefendBonus.IsActive)
		{
			DefendBonus.StartBonus();
			bonusCount--;
			OnBonusCountChanged?.Invoke(bonusCount);
		}
	}

	private void Deflect(Collision2D collision)
	{
		var contact = collision.GetContact(0);

		var distance = contact.point.y - transform.position.y;
		float halfOfPaddle = transform.localScale.y / 2;
		var factor = distance / halfOfPaddle;
		Debug.Log(distance + " " + factor);

		ball.IncreaseSpeed();
		ball.Push(factor, bounceDirection.right);
	}
}
