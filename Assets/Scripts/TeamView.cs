using System;
using System.Linq;
using UnityEngine;

public class TeamView : MonoBehaviour
{
    public static event Action OnTeamUpdated;

    public ColumnView teamSkillColumnView;
    private DevTeam team;

    public void Setup(DevTeam team)
    {
        this.team = team;
    }

    private void Start()
    {
        AvailableDevelopersView.OnChangeHighlightedDeveloper += ShowTeamWithDeveloper;
        AvailableDevelopersView.OnHireDeveloper += HireDeveloper;

        UpdateTeamSpecs();
    }

    private void OnDestroy()
    {
        MarketView.OnChangeHighlightedDeveloper -= ShowTeamWithDeveloper;
        MarketView.OnHireDeveloper -= HireDeveloper;
    }

    private void ShowTeamWithDeveloper(Developer developer)
    {
        UpdateTeamSpecs(developer);
    }

    private void UpdateTeamSpecs(Developer nextDeveloper = null)
    {
        teamSkillColumnView.Set(TeamSpecs
            .GetSkillRows(team.GetCombinedSkills())
            .Select((currentSkill, index) => new SkillRow(TeamSpecs.SkillNames[index], currentSkill.ToString(),
                GetSkillChange(currentSkill, index, nextDeveloper)))
        );
    }

    private string GetSkillChange(int originalSkill, int skillIndex, Developer developer = null)
    {
        if (developer == null)
        {
            return "";
        }

        var skillChange = developer.Skills.GetByIndex(skillIndex) - originalSkill;
        if (skillChange > 0)
        {
            return $"+{skillChange}";
        }

        return "";
    }

    private void HireDeveloper(Developer developer)
    {
        team.AddMember(developer);
        UpdateTeamSpecs();
        OnTeamUpdated?.Invoke();
    }
}
