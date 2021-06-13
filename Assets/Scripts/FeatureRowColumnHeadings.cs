using UnityEngine;

public class FeatureRowColumnHeadings : IRow
{
    public RowType GetRowType()
    {
        return RowType.FeatureColumnHeadings;
    }

    public void Instantiate(Transform transformInstance)
    {
    }
}
