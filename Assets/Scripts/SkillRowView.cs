using UnityEngine;
using UnityEngine.UI;

public class SkillRowView : MonoBehaviour
{
    public Text skillNameText;
    public Text skillValueText;
    public Text skillChangeText;
    private string skillName;
    private string skillValue;
    private string skillChange;

    public void Setup(string skillName, string skillValue, string skillChange)
    {
        this.skillName = skillName;
        this.skillValue = skillValue;
        this.skillChange = skillChange;
    }

    private void Start()
    {
        skillNameText.text = skillName;
        skillValueText.text = skillValue;
        skillChangeText.text = skillChange;
    }
}
