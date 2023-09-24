using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
	[Header("Speed")]
	[SerializeField] private float startSpeed;
	[SerializeField] private float maxSpeed;
	[SerializeField] private float increaseValue;
	[Header("Angle")]
	[SerializeField, Range(0, 90)] private int deflectionAngle;

	private Rigidbody2D RB;
	private float currentSpeed;
	private Vector2 currentDirection;

	public Vector2 StartDirection { get; private set; }

	private void Awake()
	{
		RB = GetComponent<Rigidbody2D>();
	}

	public void Push(float deflectionFactor, Vector2 direction)
	{
		var correctFactor = Math.Clamp(deflectionFactor, -1f, 1f);
		currentDirection = Quaternion.Euler(0, 0, (deflectionAngle * correctFactor) * (direction.x > 0 ? 1 : -1)) * direction;
		RB.velocity = Vector2.zero;
		RB.AddForce(currentDirection * currentSpeed, ForceMode2D.Impulse);
	}

	public void IncreaseSpeed()
	{
		currentSpeed += currentSpeed >= maxSpeed ? 0 : increaseValue;
	}

	public void ReloadBall()
	{
		RB.velocity = Vector2.zero;
		transform.position = Vector2.zero;
		currentSpeed = (currentSpeed - startSpeed) / 2 + startSpeed;
		StartDirection = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
	}
}

