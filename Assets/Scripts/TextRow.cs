using UnityEngine;
using UnityEngine.UI;

public class TextRow : IRow
{
    private readonly string text;

    public TextRow(string text)
    {
        this.text = text;
    }

    public RowType GetRowType()
    {
        return RowType.Text;
    }

    public void Instantiate(Transform transformInstance)
    {
        transformInstance.GetComponent<Text>().text = text;
    }
}
