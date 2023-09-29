using UnityEngine;
using UnityEngine.UI;

public class BonusColorChanger : ColorChanger
{
	[SerializeField] private Image image;
	[SerializeField, Range(0f, 1f)] private float valueRange = 0.1f;

	private Color defaultColor;
	private Color activeColor;
	private Color deactiveColor;

	public override void UpdateColor(Color color)
	{
		image.color = color;

		if (defaultColor == default)
		{
			defaultColor = color;

			activeColor = defaultColor;

			Color.RGBToHSV(defaultColor, out float hue, out float sat, out float val);
			Color newColor = Color.HSVToRGB(hue, sat, val - valueRange);
			deactiveColor = newColor;

			DeactivateBonus();
		}
	}

	public void ActivateBonus()
	{
		UpdateColor(activeColor);
	}

	public void DeactivateBonus()
	{
		UpdateColor(deactiveColor);
	}
}
