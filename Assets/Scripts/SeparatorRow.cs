using UnityEngine;

public class SeparatorRow : IRow
{
    public RowType GetRowType()
    {
        return RowType.Separator;
    }

    public void Instantiate(Transform transformInstance)
    {
    }
}
