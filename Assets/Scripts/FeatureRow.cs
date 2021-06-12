using UnityEngine;

public class FeatureRow : IRow
{
    private readonly Feature feature;

    public FeatureRow(Feature feature)
    {
        this.feature = feature;
    }

    public RowType GetRowType()
    {
        return RowType.Feature;
    }

    public void Instantiate(Transform transformInstance)
    {
        transformInstance.GetComponent<FeatureView>().SetFeature(feature);
    }
}
