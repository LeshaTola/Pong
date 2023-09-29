using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Player))]
public class PlayerController : MonoBehaviour, IControllable
{
	[SerializeField] private float speed;

	private Rigidbody2D RB;
	private Vector2 moveDir;
	private Player player;

	private void Awake()
	{
		RB = GetComponent<Rigidbody2D>();
		player = GetComponent<Player>();
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
		player.Attack();
	}

	public void Defend()
	{
		player.Defend();
	}

	private void MoveIternal()
	{
		RB.velocity = moveDir * speed;
	}
}
