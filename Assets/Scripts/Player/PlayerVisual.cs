
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
	[SerializeField] private GameObject attackPSTemplate;
	[SerializeField] private Player player;

	private GameObject attackPS;

	private void Awake()
	{
		attackPS = Instantiate(attackPSTemplate/*.GetComponentInChildren<ParticleSystem>()*/, transform);
		attackPS.transform.localScale = Vector3.one;
		attackPS.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		player.OnAttackStarted += OnAttackStarted;
		player.OnAttackEnded += OnAttackEnded;
	}

	private void OnDisable()
	{
		player.OnAttackStarted -= OnAttackStarted;
		player.OnAttackEnded -= OnAttackEnded;
	}

	private void OnAttackStarted()
	{
		attackPS.gameObject.SetActive(true);
	}

	private void OnAttackEnded()
	{
		attackPS.gameObject.SetActive(false);
	}
}

