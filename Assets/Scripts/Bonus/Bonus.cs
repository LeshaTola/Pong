using System.Collections;
using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
	[SerializeField] protected bool isOnTime;
	[field: SerializeField] public bool IsActivateOnTouch { get; private set; }
	[SerializeField] protected float durationTime = 5f;

	public bool IsActive { get; private set; }

	protected Player player;

	public void Init(Player player)
	{
		this.player = player;
		//Instantiate(gameObject, player.transform); 
	}

	public virtual void StartBonus()
	{
		IsActive = true;
		if (isOnTime)
		{
			StartCoroutine(BonusTimer());
		}
	}

	public virtual void EndBonus()
	{
		IsActive = false;
	}

	protected IEnumerator BonusTimer()
	{
		yield return new WaitForSeconds(durationTime);
		EndBonus();
	}
}
