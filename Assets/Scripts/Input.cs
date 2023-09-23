using UnityEngine;

public enum InputType
{
	Arrows,
	Letters
}

public class Input : MonoBehaviour
{
	[SerializeField] private GameObject controllableObject;
	[SerializeField] private GameObject controllableObject2;
	//[SerializeField] private InputType inputType;

	private IControllable controllable;
	private IControllable controllable2;

	private void Awake()
	{
		if (controllableObject.TryGetComponent(out controllable) == false)
		{
			Debug.LogError($"In {gameObject.name} controllableObject is not IControllavle!");
		}
		if (controllableObject2.TryGetComponent(out controllable2) == false)
		{
			Debug.LogError($"In {gameObject.name} controllableObject2 is not IControllavle!");
		}
	}

	private void Update()
	{
		//if (inputType == InputType.Arrows)
		//{
		if (UnityEngine.Input.GetKeyDown(KeyCode.UpArrow))
		{
			controllable2.Move(Vector2.up);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.DownArrow))
		{
			controllable2.Move(-Vector2.up);
		}
		//}
		//else
		//{
		if (UnityEngine.Input.GetKeyDown(KeyCode.W))
		{
			controllable.Move(Vector2.up);
		}
		if (UnityEngine.Input.GetKeyDown(KeyCode.S))
		{
			controllable.Move(-Vector2.up);
		}
		//}
	}
}
