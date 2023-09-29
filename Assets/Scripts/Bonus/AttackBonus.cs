using UnityEngine;

public class AttackBonus : Bonus
{
	[SerializeField] private float speedBonus = 1f;

	public override void StartBonus()
	{
		player.ChargeBall(speedBonus);
		base.StartBonus();
	}

	public override void EndBonus()
	{
		player.UnchargeBall(speedBonus);
		base.EndBonus();
	}

}
