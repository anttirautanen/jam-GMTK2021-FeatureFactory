using UnityEngine;
using UnityEngine.UI;

public class DeveloperRowView : MonoBehaviour
{
    public Text isSelectedText;
    public Text nameText;
    public Text salaryText;
    private Developer developer;
    private bool isSelected;

    public void Setup(Developer developer, bool isSelected)
    {
        this.developer = developer;
        this.isSelected = isSelected;
    }

    private void Start()
    {
        isSelectedText.text = isSelected ? "×" : "";
        nameText.text = $"Dev #{developer.ID},";
        salaryText.text = $"{developer.Salary:C0}";
    }
}
