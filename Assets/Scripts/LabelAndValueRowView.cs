using UnityEngine;
using UnityEngine.UI;

public class LabelAndValueRowView : MonoBehaviour
{
    public Text labelText;
    public Text valueText;
    private string label;
    private string value;
    private TextRowStyle style;
    private const int DefaultFontSize = 20;
    private const int DefaultHeight = 25;
    private static readonly Color DefaultColor = new Color(0f, 0f, 0f);
    private static readonly Color PositiveColor = new Color(0.318f, 0.502f, 0.255f);
    private static readonly Color NegativeColor = new Color(0.698f, 0.2f, 0.2f);

    public void Setup(string label, string value, TextRowStyle style)
    {
        this.label = label;
        this.value = value;
        this.style = style;
    }

    private void Start()
    {
        labelText.text = label;
        valueText.text = value;

        if (style != null)
        {
            if (style.IsHeading)
            {
                SetStyle(DefaultColor, 30, 50);
            }
            else if (style.IsPositive)
            {
                SetStyle(PositiveColor);
            }
            else if (style.IsNegative)
            {
                SetStyle(NegativeColor);
            }
        }
        else
        {
            SetStyle(DefaultColor);
        }
    }

    private void SetStyle(Color color, int fontSize = DefaultFontSize, int height = DefaultHeight)
    {
        labelText.fontSize = fontSize;
        valueText.fontSize = fontSize;
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        labelText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        valueText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        labelText.color = color;
        valueText.color = color;
    }
}
