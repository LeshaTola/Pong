using UnityEngine;

public enum PlayerEnum
{
	FirstPlayer,
	SecondPlayer
}

public class ColorChanger : MonoBehaviour
{
	[SerializeField] protected SpriteRenderer spriteRenderer;
	[field: SerializeField] public PlayerEnum PlayerEnum { get; set; }

	public virtual void UpdateColor(Color color)
	{
		spriteRenderer.color = color;
	}
}
