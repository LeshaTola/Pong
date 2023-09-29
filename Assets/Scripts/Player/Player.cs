using System;
using UnityEngine;

public class Player : MonoBehaviour
{
	public event Action<int> OnBonusCountChanged;
	public event Action<PlayerEnum> OnBallCollision;

	public event Action OnAttackStarted;
	public event Action OnAttackEnded;
	public event Action OnDefendStarted;
	public event Action OnDefendEnded;

	[SerializeField] private Transform bounceDirection;
	[SerializeField] private ParticleSystem bounceParticleTemplate;
	[SerializeField] private Ball ball;


	[field: SerializeField] public Bonus AttackBonus { get; private set; }
	[field: SerializeField] public PlayerEnum PlayerEnum { get; private set; }
	[field: SerializeField] public Bonus DefendBonus { get; private set; }
	[field: SerializeField] public int MaxBonusCount { get; private set; } = 2;
	public Ball Ball { get => ball; private set => ball = value; }

	private int bonusCount;
	private bool isAttackBonus;
	private ParticleSystem bounceParticle;

	private void Awake()
	{
		AttackBonus.Init(this);
		DefendBonus.Init(this);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent(out Ball anotherBall))
		{
			if (ball.IsCharged)
			{
				ball.LastPlayer.AttackBonus.EndBonus();
			}

			if (isAttackBonus)
			{
				AttackInternal();
				OnAttackEnded?.Invoke();
			}

			Deflect(collision);
			OnBallCollision?.Invoke(PlayerEnum);
			ball.LastPlayer = this;
		}
	}

	public void Expand(float multiplier)
	{
		var newLocalScale = transform.localScale;
		newLocalScale.y *= multiplier;
		transform.localScale = newLocalScale;
		OnDefendStarted?.Invoke();
	}

	public void Shrink(float multiplier)
	{
		var newLocalScale = transform.localScale;
		newLocalScale.y /= multiplier;
		transform.localScale = newLocalScale;
		OnDefendEnded?.Invoke();
	}

	public void AddBonus(int count)
	{
		bonusCount += count;
		bonusCount = bonusCount > MaxBonusCount ? MaxBonusCount : bonusCount;
		OnBonusCountChanged?.Invoke(bonusCount);
	}

	public void Attack()
	{
		if (bonusCount > 0 && !AttackBonus.IsActive && !isAttackBonus)
		{
			if (AttackBonus.IsActivateOnTouch)
			{
				isAttackBonus = true;
			}
			else
			{
				AttackInternal();
			}
			OnAttackStarted?.Invoke();
			bonusCount--;
			OnBonusCountChanged?.Invoke(bonusCount);
		}
	}

	private void AttackInternal()
	{
		AttackBonus.StartBonus();
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


		if (bounceParticle == null)
		{
			bounceParticle = Instantiate(bounceParticleTemplate, transform);
		}

		bounceParticle.transform.position = contact.point;
		bounceParticle.Play();
		//Destroy(particle, bounceParticle.main.startLifetime.constant);

		ball.IncreaseSpeed();
		ball.Push(factor, bounceDirection.right);
	}
}
