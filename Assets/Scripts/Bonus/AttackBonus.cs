using UnityEngine;

public class AttackBonus : Bonus
{
	[SerializeField] private float speedBonus = 1f;

	public override void StartBonus()
	{
		player.Ball.ChargeBall(speedBonus);
		base.StartBonus();
	}

	public override void EndBonus()
	{
		player.Ball.UnchargeBall(speedBonus);
		base.EndBonus();
	}

}
