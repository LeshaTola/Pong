using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
	public event Action OnPlayerCollision;
	public event Action OnGoalZoneCollision;
	public event Action OnWallCollision;

	public event Action OnBallCharged;
	public event Action OnBallUncharged;
	public event Action<float> OnSpeedChanged;

	[Header("Speed")]
	[SerializeField] private float startSpeed;
	[SerializeField] private float maxSpeed;
	[SerializeField] private float increaseValue;
	[Header("Angle")]
	[SerializeField, Range(0, 45)] private int deflectionAngle;

	private Rigidbody2D rigidBody;
	private Vector2 currentDirection;

	public Player LastPlayer { get; set; }
	public float CurrentSpeed { get; private set; }
	public Vector2 StartDirection { get; private set; }
	public bool IsCharged { get; set; } = false;

	private void Awake()
	{
		rigidBody = GetComponent<Rigidbody2D>();
	}

	public void Push(float deflectionFactor, Vector2 direction)
	{
		var correctFactor = Math.Clamp(deflectionFactor, -1f, 1f);
		currentDirection = Quaternion.Euler(0, 0, (deflectionAngle * correctFactor) * (direction == Vector2.right ? 1 : -1)) * direction;
		currentDirection = currentDirection.normalized;
		rigidBody.velocity = Vector2.zero;
		rigidBody.AddForce(currentDirection * CurrentSpeed, ForceMode2D.Impulse);
	}

	public void IncreaseSpeed()
	{
		IncreaseSpeed(increaseValue);
	}

	public void IncreaseSpeed(float increaseValue)
	{
		if (!IsCharged)
		{
			CurrentSpeed += CurrentSpeed >= maxSpeed ? 0 : increaseValue;
		}
		else
		{
			CurrentSpeed += increaseValue;
		}

		OnSpeedChanged?.Invoke(CurrentSpeed);
	}

	public void Reload()
	{
		rigidBody.velocity = Vector2.zero;
		transform.position = Vector2.zero;
		if (!CurrentSpeed.Equals(0))
		{
			CurrentSpeed = (CurrentSpeed - startSpeed) / 2 + startSpeed;
		}
		else
		{
			CurrentSpeed = startSpeed;
		}
		StartDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));//TODO: FIX	
	}

	public void ChargeBall(float value)
	{
		IncreaseSpeed(value);
		IsCharged = true;
		OnBallCharged?.Invoke();
	}

	public void UnchargeBall(float value)
	{
		IncreaseSpeed(-value);
		IsCharged = false;
		OnBallUncharged?.Invoke();
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent(out Player player))
		{
			OnPlayerCollision?.Invoke();
		}
		else if (collision.gameObject.TryGetComponent(out GoalZone zone))
		{
			OnGoalZoneCollision?.Invoke();
		}
		else
		{
			OnWallCollision?.Invoke();
		}
	}
}

