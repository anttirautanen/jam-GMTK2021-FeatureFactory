using UnityEngine;

public class CompanyView : MonoBehaviour
{
    public Transform featureViewPrefab;

    private void Start()
    {
        var feature = Company.Instance.GetFirstFeature();
        Instantiate(featureViewPrefab, transform)
            .GetComponent<FeatureView>()
            .SetFeature(feature);
    }
}
