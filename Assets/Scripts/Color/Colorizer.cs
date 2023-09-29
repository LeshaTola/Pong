using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Colorizer : MonoBehaviour
{
	[SerializeField] private Color firstPlayerColor;
	[SerializeField] private Color secondPlayerColor;

	private void Start()
	{
		List<ColorChanger> colorChangersList = FindAllColorChangers();
		foreach (var colorChanger in colorChangersList)
		{
			UpdateColor(colorChanger);
		}
	}
	private List<ColorChanger> FindAllColorChangers()
	{
		var list = FindObjectsOfType<ColorChanger>();
		return list.ToList();
	}

	private void UpdateColor(ColorChanger colorChanger)
	{
		if (colorChanger.PlayerEnum == PlayerEnum.FirstPlayer)
		{
			colorChanger.UpdateColor(firstPlayerColor);
		}
		else
		{
			colorChanger.UpdateColor(secondPlayerColor);
		}
	}
}
