using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IControllable
{
	[SerializeField] private float speed;

	private Rigidbody2D RB;
	private Vector2 moveDir;

	private void Awake()
	{
		RB = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		MoveIternal();
	}

	public void Move(Vector2 direction)
	{
		moveDir = direction;
	}

	public void Attack()
	{
		throw new System.NotImplementedException();
	}

	public void Defend()
	{
		throw new System.NotImplementedException();
	}

	private void MoveIternal()
	{
		RB.velocity = moveDir * speed;
	}
}
