using UnityEngine;

public class PaddleSurface : MonoBehaviour
{
	[SerializeField] private Transform bounceDirection;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.TryGetComponent(out Ball ball))
		{
			var contact = collision.GetContact(0);

			var distance = collision.transform.position.y - transform.position.y;
			float halfOfPaddle = transform.localScale.y / 2;
			var factor = distance / halfOfPaddle;
			Debug.Log(distance + " " + factor);

			ball.IncreaseSpeed();
			ball.Push(factor, bounceDirection.right);
		}
	}
}
