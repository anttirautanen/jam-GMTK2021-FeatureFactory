using UnityEngine;
using UnityEngine.UI;

public class FeatureView : MonoBehaviour
{
    public Text satisfiesCustomerNeedText;
    public Text qualityText;
    private Feature feature;

    public void SetFeature(Feature feature)
    {
        this.feature = feature;
    }

    private void Update()
    {
        satisfiesCustomerNeedText.text = Mathf.RoundToInt(feature.SatisfiesCustomerNeed * 100) + "%";
        qualityText.text = Mathf.RoundToInt(feature.Quality * 100) + "%";
    }
}
