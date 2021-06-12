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
        UpdateView();
    }

    public void SetIsHighlighted(bool isHighlighted)
    {
        UpdateView(isHighlighted);
    }

    public void SetIsHired()
    {
        isHired = true;
        UpdateView(true);
    }

    private void UpdateView(bool isHighlighted = false)
    {
        nameText.text = $"Developer #{developer.ID}" + (isHighlighted ? " <--" : "");
        salaryText.text = developer.Salary.ToString();

        if (isHired)
        {
            nameText.color = new Color(1f, 1f, 1f, 0.4f);
        }
    }
}
