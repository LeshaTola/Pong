using System;
using UnityEngine;

public class GoalZone : MonoBehaviour
{
	public event Action OnGoaled;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.TryGetComponent(out Ball ball))
		{
			OnGoaled?.Invoke();
		}
	}
}
