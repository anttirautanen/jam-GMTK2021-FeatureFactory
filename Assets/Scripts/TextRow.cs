using UnityEngine;
using UnityEngine.UI;

public class TextRow : IRow
{
    private readonly string text;
    private readonly TextRowStyle style;

    public TextRow(string text, TextRowStyle textRowStyle = null)
    {
        this.text = text;
        style = textRowStyle ?? new TextRowStyle();
    }

    public RowType GetRowType()
    {
        return RowType.Text;
    }

    public void Instantiate(Transform transformInstance)
    {
        var textComponent = transformInstance.GetComponent<Text>();
        textComponent.text = text;

        if (style.IsHeading)
        {
            textComponent.fontSize = 30;
            textComponent.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
        }
    }
}
