using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	private float volume = 10f;

	[SerializeField] private Player firstPlayer;
	[SerializeField] private Player secondPlayer;
	[SerializeField] private Ball ball;
	[SerializeField] private BonusTrigger firstPlayerBonus;
	[SerializeField] private BonusTrigger secondPlayerBonus;

	[SerializeField] private SFXSO SFX;

	private void OnEnable()
	{
		firstPlayer.OnAttacked += OnAttacked;
		firstPlayer.OnDefendStarted += OnDefendStarted;
		firstPlayer.OnDefendEnded += OnDefendEnded;

		secondPlayer.OnAttacked += OnAttacked;
		secondPlayer.OnDefendStarted += OnDefendStarted;
		secondPlayer.OnDefendEnded += OnDefendEnded;

		ball.OnPlayerCollision += OnBallPlayerCollision;
		ball.OnWallCollision += OnBallWallCollision;
		ball.OnGoalZoneCollision += OnBallGoalZoneCollision;

		firstPlayerBonus.OnPicedUp += OnBonusPicedUp;
		secondPlayerBonus.OnPicedUp += OnBonusPicedUp;

		ball.OnBallCharged += OnBallCharged;
	}

	private void OnDisable()
	{
		firstPlayer.OnAttacked -= OnAttacked;
		firstPlayer.OnDefendStarted -= OnDefendStarted;
		firstPlayer.OnDefendEnded -= OnDefendEnded;

		secondPlayer.OnAttacked -= OnAttacked;
		secondPlayer.OnDefendStarted -= OnDefendStarted;
		secondPlayer.OnDefendEnded -= OnDefendEnded;

		ball.OnPlayerCollision -= OnBallPlayerCollision;
		ball.OnWallCollision -= OnBallWallCollision;
		ball.OnGoalZoneCollision -= OnBallGoalZoneCollision;

		firstPlayerBonus.OnPicedUp += OnBonusPicedUp;
		secondPlayerBonus.OnPicedUp += OnBonusPicedUp;

		ball.OnBallCharged -= OnBallCharged;
	}

	private void OnBonusPicedUp(BonusTrigger obj)
	{
		PlaySound(SFX.Bonus, transform.position);
	}

	private void OnBallCharged()
	{
		PlaySound(SFX.BallCharge, transform.position);
	}

	private void OnBallGoalZoneCollision()
	{
		PlaySound(SFX.Goal, transform.position);
	}

	private void OnBallWallCollision()
	{
		PlaySound(SFX.WallHit, transform.position);
	}

	private void OnBallPlayerCollision()
	{
		PlaySound(SFX.PlayerHit, transform.position);
	}

	private void OnDefendEnded()
	{
		PlaySound(SFX.DefendEnd, transform.position);
	}

	private void OnDefendStarted()
	{
		PlaySound(SFX.DefendStart, transform.position);
	}

	private void OnAttacked()
	{
		PlaySound(SFX.Attack, transform.position);
	}

	private void PlaySound(List<AudioClip> audioClipArray, Vector3 position, float volume = 1f)
	{
		AudioClip audioClip = audioClipArray[Random.Range(0, audioClipArray.Count)];
		PlaySound(audioClip, position, volume);
	}
	private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplayer = 1f)
	{
		AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplayer * volume);
	}
}