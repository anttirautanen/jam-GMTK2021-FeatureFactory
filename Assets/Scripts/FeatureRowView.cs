using UnityEngine;
using UnityEngine.UI;

public class FeatureRowView : MonoBehaviour
{
    public Text featureNameText;
    public Text satisfiesCustomerNeedText;
    public Text qualityText;
    public Text isSelectedText;
    public Text isReleasedText;
    private Feature feature;
    private bool isSelected;

    public void Setup(Feature feature, bool isSelected)
    {
        this.feature = feature;
        this.isSelected = isSelected;
    }

    private void Start()
    {
        featureNameText.text = feature.Name;
        satisfiesCustomerNeedText.text = Mathf.RoundToInt(feature.SatisfiesCustomerNeed * 100) + "%";
        qualityText.text = Mathf.RoundToInt(feature.Quality * 100) + "%";
        isSelectedText.text = isSelected ? "×" : "";
        isReleasedText.text = feature.IsReleased() ? "Released" : "Not released";
    }
}
