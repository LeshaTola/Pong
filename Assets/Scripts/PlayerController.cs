using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour, IControllable
{
	[SerializeField] private float speed;

	private CharacterController characterController;
	private Vector2 moveDir;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
	}

	private void FixedUpdate()
	{
		MoveIternal();
	}

	public void Move(Vector2 direction)
	{
		moveDir = direction;
	}

	private void MoveIternal()
	{
		characterController.Move(moveDir * Time.deltaTime * speed);
	}

}
