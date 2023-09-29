using UnityEngine;

public class DefendBonus : Bonus
{
	[SerializeField] private float sizeMultiplier = 1.2f;

	public override void StartBonus()
	{
		player.Expand(sizeMultiplier);
		base.StartBonus();
	}

	public override void EndBonus()
	{
		player.Shrink(sizeMultiplier);
		base.EndBonus();
	}
}