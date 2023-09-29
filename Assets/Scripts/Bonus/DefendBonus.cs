using UnityEngine;

public class DefendBonus : Bonus
{
	[SerializeField] private float sizeMultiplier = 1.2f;

	public override void StartBonus()
	{
		player.MultiplySize(sizeMultiplier);
		base.StartBonus();
	}

	public override void EndBonus()
	{
		player.MultiplySize(1 / sizeMultiplier);
		base.EndBonus();
	}
}