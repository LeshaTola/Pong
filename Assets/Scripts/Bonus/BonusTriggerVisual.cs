using UnityEngine;

public class BonusTriggerVisual : MonoBehaviour
{
	[SerializeField] private ParticleSystem picedUpPSTemplate;
	[SerializeField] private BonusTrigger bonusTrigger;

	private ParticleSystem picedUpPS;
	private void Awake()
	{
		picedUpPS = Instantiate(picedUpPSTemplate);
		picedUpPS.Stop();
	}

	private void OnEnable()
	{
		bonusTrigger.OnPicedUp += OnBonusPicedUp;
	}

	private void OnDisable()
	{
		bonusTrigger.OnPicedUp -= OnBonusPicedUp;
	}

	private void OnBonusPicedUp(BonusTrigger obj)
	{
		picedUpPS.transform.position = transform.position;
		picedUpPS.Play();
	}
}
