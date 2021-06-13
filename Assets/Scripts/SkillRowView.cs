using UnityEngine;
using UnityEngine.UI;

public class SkillRowView : MonoBehaviour
{
    public Text skillNameText;
    public Text skillValueText;
    public Text skillChangeText;
    private string skillName;
    private string skillValue;
    private int skillChange;

    public void Setup(string skillName, string skillValue, int skillChange)
    {
        this.skillName = skillName;
        this.skillValue = skillValue;
        this.skillChange = skillChange;
    }

    private void Start()
    {
        skillNameText.text = skillName;
        skillValueText.text = skillValue;
        skillChangeText.text = GetSkillChange();
    }

    private string GetSkillChange()
    {
        if (skillChange > 0)
        {
            skillChangeText.color = Colors.Positive;
            return $"+{skillChange}";
        }

        if (skillChange < 0)
        {
            skillChangeText.color = Colors.Negative;
            return $"{skillChange}";
        }

        return "";
    }
}
