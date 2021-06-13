using UnityEngine;

public class FeatureRow : IRow
{
    private readonly Feature feature;
    private readonly bool isSelected;

    public FeatureRow(Feature feature, bool isSelected)
    {
        this.feature = feature;
        this.isSelected = isSelected;
    }

    public RowType GetRowType()
    {
        return RowType.Feature;
    }

    public void Instantiate(Transform transformInstance)
    {
        var featureView = transformInstance.GetComponent<FeatureRowView>();
        featureView.Setup(feature, isSelected);
    }
}
