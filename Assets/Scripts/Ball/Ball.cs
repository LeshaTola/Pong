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

		currentSpeed = startSpeed;

		SetCurentDirection(0f, Vector2.right);
	}

	public void SetCurentDirection(float factor, Vector2 frontVector)
	{
		var correctFactor = Math.Clamp(factor, -1f, 1f);
		currentDirection = Quaternion.Euler(0, 0, (deflectionAngle * correctFactor) * (frontVector.x > 0 ? 1 : -1)) * frontVector;
		RB.velocity = Vector2.zero;
		RB.AddForce(currentDirection * currentSpeed, ForceMode2D.Impulse);
	}

	public void IncreaseSpeed()
	{
		currentSpeed += currentSpeed >= maxSpeed ? 0 : increaseValue;
	}
}

