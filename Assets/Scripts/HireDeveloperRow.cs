using UnityEngine;
using UnityEngine.UI;

public class HireDeveloperRow : MonoBehaviour
{
    public Text nameText;
    public Text skillPointsChangeText;
    public Text salaryText;
    private Developer developer;
    public bool isHired = false;

    public void SetDeveloper(Developer developer)
    {
        this.developer = developer;
        UpdateName();
    }

    public void SetIsHighlighted(bool isHighlighted)
    {
        UpdateName(isHighlighted);
    }

    public void SetIsHired()
    {
        isHired = true;
        UpdateName(true);
    }

    private void UpdateName(bool isHighlighted = false)
    {
        nameText.text = $"Developer #{developer.ID}" + (isHighlighted ? " <--" : "");

        if (isHired)
        {
            nameText.color = new Color(1f, 1f, 1f, 0.4f);
        }
    }
}
