using UnityEngine;
using UnityEngine.UI;

public class DeveloperRowView : MonoBehaviour
{
    public Text isSelectedText;
    public Text nameText;
    private Developer developer;
    private bool isSelected;

    public void Setup(Developer developer, bool isSelected)
    {
        this.developer = developer;
        this.isSelected = isSelected;
    }

    private void Start()
    {
        isSelectedText.text = isSelected ? "-->" : "";
        nameText.text = $"#{developer.ID}, {developer.Salary:C0}";
    }
}
