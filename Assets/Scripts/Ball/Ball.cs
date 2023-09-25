using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
	public event Action<float> OnSpeedChanged;

	[Header("Speed")]
	[SerializeField] private float startSpeed;
	[SerializeField] private float maxSpeed;
	[SerializeField] private float increaseValue;
	[Header("Angle")]
	[SerializeField, Range(0, 90)] private int deflectionAngle;

	private Rigidbody2D RB;
	private Vector2 currentDirection;

	public float CurrentSpeed { get; private set; }
	public Vector2 StartDirection { get; private set; }

	private void Awake()
	{
		RB = GetComponent<Rigidbody2D>();
	}

	public void Push(float deflectionFactor, Vector2 direction)
	{
		var correctFactor = Math.Clamp(deflectionFactor, -1f, 1f);
		currentDirection = Quaternion.Euler(0, 0, (deflectionAngle * correctFactor) * (direction == Vector2.right ? 1 : -1)) * direction;
		currentDirection = currentDirection.normalized;
		RB.velocity = Vector2.zero;
		RB.AddForce(currentDirection * CurrentSpeed, ForceMode2D.Impulse);
	}

	public void IncreaseSpeed()
	{
		CurrentSpeed += CurrentSpeed >= maxSpeed ? 0 : increaseValue;
		OnSpeedChanged?.Invoke(CurrentSpeed);
	}

	public void ReloadBall()
	{
		RB.velocity = Vector2.zero;
		transform.position = Vector2.zero;
		if (!CurrentSpeed.Equals(0))
		{
			CurrentSpeed = (CurrentSpeed - startSpeed) / 2 + startSpeed;
		}
		else
		{
			CurrentSpeed = startSpeed;
		}
		StartDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
	}
}

