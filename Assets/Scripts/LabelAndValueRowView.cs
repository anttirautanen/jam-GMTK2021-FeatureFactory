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
                SetStyle(Colors.Default, 30, 50);
            }
            else if (style.IsPositive)
            {
                SetStyle(Colors.Positive);
            }
            else if (style.IsNegative)
            {
                SetStyle(Colors.Negative);
            }
            else if (style.IsSecondary)
            {
                SetStyle(Colors.Secondary);
            }
            else
            {
                SetStyle(Colors.Default);
            }
        }
        else
        {
            SetStyle(Colors.Default);
        }
    }

    private void SetStyle(Color colors, int fontSize = DefaultFontSize, int height = DefaultHeight)
    {
        labelText.fontSize = fontSize;
        valueText.fontSize = fontSize;
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        labelText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        valueText.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
        labelText.color = colors;
        valueText.color = colors;
    }
}
