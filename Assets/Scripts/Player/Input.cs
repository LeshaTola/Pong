using UnityEngine;

public enum InputType
{
	Arrows,
	Letters
}

public class Input : MonoBehaviour
{
	[SerializeField] private GameObject firstPlayer;
	[SerializeField] private GameObject secondPlayer;
	//[SerializeField] private InputType inputType;

	private IControllable firstControllable;
	private IControllable secondControllable;

	private void Awake()
	{
		if (firstPlayer.TryGetComponent(out firstControllable) == false)
		{
			Debug.LogError($"In {gameObject.name} firstControllable is not IControllavle!");
		}
		if (secondPlayer.TryGetComponent(out secondControllable) == false)
		{
			Debug.LogError($"In {gameObject.name} secondControllable is not IControllavle!");
		}
	}

	private void Update()
	{

		if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
		{
			secondControllable.Move(Vector2.up);
		}
		else if (UnityEngine.Input.GetKey(KeyCode.DownArrow))
		{
			secondControllable.Move(-Vector2.up);
		}
		else
		{
			secondControllable.Move(Vector2.zero);
		}

		if (UnityEngine.Input.GetKey(KeyCode.W))
		{
			firstControllable.Move(Vector2.up);
		}
		else if (UnityEngine.Input.GetKey(KeyCode.S))
		{
			firstControllable.Move(-Vector2.up);
		}
		else
		{
			firstControllable.Move(Vector2.zero);
		}

		if (UnityEngine.Input.GetKeyDown(KeyCode.A))
		{
			firstControllable.Defend();
		}

		if (UnityEngine.Input.GetKeyDown(KeyCode.D))
		{
			firstControllable.Attack();
		}

		if (UnityEngine.Input.GetKeyDown(KeyCode.RightArrow))
		{
			secondControllable.Defend();
		}

		if (UnityEngine.Input.GetKeyDown(KeyCode.LeftArrow))
		{
			secondControllable.Attack();
		}
	}
}
