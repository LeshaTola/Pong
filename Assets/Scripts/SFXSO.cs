using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sounds")]
public class SFXSO : ScriptableObject
{
	public List<AudioClip> Attack;
	public List<AudioClip> BallCharge;
	public List<AudioClip> DefendStart;
	public List<AudioClip> DefendEnd;
	public List<AudioClip> Goal;
	public List<AudioClip> Bonus;
	public List<AudioClip> PlayerHit;
	public List<AudioClip> WallHit;
}
