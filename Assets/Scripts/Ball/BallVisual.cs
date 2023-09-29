using UnityEngine;

public class BallVisual : MonoBehaviour
{
	[SerializeField] private ParticleSystem chargePSTemplate;
	[SerializeField] private Ball ball;

	private ParticleSystem chargePS;

	private void Awake()
	{
		chargePS = Instantiate(chargePSTemplate, transform);
		chargePS.gameObject.SetActive(false);

		ball.OnBallCharged += OnBallCharged;
		ball.OnBallUncharged += OnBallUncharged;
	}

	private void OnDestroy()
	{
		ball.OnBallCharged -= OnBallCharged;
		ball.OnBallUncharged -= OnBallUncharged;
	}

	private void OnBallUncharged()
	{
		chargePS.gameObject.SetActive(false);
	}

	private void OnBallCharged()
	{
		chargePS.gameObject.SetActive(true);
	}

}
