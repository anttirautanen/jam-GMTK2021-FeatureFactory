using UnityEngine;

public class SkillRow : IRow
{
    private readonly string skillName;
    private readonly string skillValue;
    private readonly string skillChange;

    public SkillRow(string skillName, string skillValue, string skillChange)
    {
        this.skillName = skillName;
        this.skillValue = skillValue;
        this.skillChange = skillChange;
    }

    public RowType GetRowType()
    {
        return RowType.Skill;
    }

    public void Instantiate(Transform transformInstance)
    {
        transformInstance.GetComponent<SkillRowView>().Setup(skillName, skillValue, skillChange);
    }
}
