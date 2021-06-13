using UnityEngine;

public class LabelAndValueRow : IRow
{
    private readonly string label;
    private readonly string value;
    private readonly TextRowStyle style;

    public LabelAndValueRow(string label, string value, TextRowStyle style = null)
    {
        this.label = label;
        this.value = value;
        this.style = style;
    }

    public RowType GetRowType()
    {
        return RowType.LabelAndValue;
    }

    public void Instantiate(Transform transformInstance)
    {
        transformInstance.GetComponent<LabelAndValueRowView>().Setup(label, value, style);
    }
}
