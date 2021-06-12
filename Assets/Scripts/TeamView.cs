using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TeamView : MonoBehaviour
{
    public static event Action OnTeamUpdated;

    private static readonly string[] SkillNames = {"Design", "Frontend", "Backend", "Databases", "Devops"};
    public Text teamSpecsText;
    private DevTeam team;

    private void Start()
    {
        MarketView.OnChangeHighlightedDeveloper += ShowTeamWithDeveloper;
        MarketView.OnHireDeveloper += HireDeveloper;

        team = Company.Instance.GetFirstTeam();
        UpdateTeamSpecsText(team);
    }

    private void OnDestroy()
    {
        MarketView.OnChangeHighlightedDeveloper -= ShowTeamWithDeveloper;
        MarketView.OnHireDeveloper -= HireDeveloper;
    }

    private void ShowTeamWithDeveloper(Developer developer)
    {
        UpdateTeamSpecsText(team, developer);
    }

    private void UpdateTeamSpecsText(DevTeam team, Developer nextDeveloper = null)
    {
        var teamSpecs = new StringBuilder().AppendLine($"Members: {team.GetMemberCount()}");
        teamSpecs.Append(nextDeveloper != null ? GetComparisonSkillRows(team, nextDeveloper) : GetTeamSkillRows(team));
        teamSpecsText.text = teamSpecs.ToString();
    }

    private static string GetTeamSkillRows(DevTeam team)
    {
        var teamSkills = team.GetCombinedSkills();
        var teamSkillRows = GetSkillRows(teamSkills);
        var rows = new StringBuilder();
        for (var i = 0; i < teamSkillRows.Length; i++)
        {
            rows.AppendLine($"{SkillNames[i]} {teamSkillRows[i]}");
        }

        return rows.ToString();
    }

    private static string GetComparisonSkillRows(DevTeam team, Developer nextDeveloper)
    {
        var teamSkills = team.GetCombinedSkills();
        var teamSkillRows = GetSkillRows(teamSkills);
        var comparisonRows = GetComparisonRows(teamSkills, nextDeveloper.Skills);
        var rows = new StringBuilder();
        for (var i = 0; i < teamSkillRows.Length; i++)
        {
            rows.AppendLine($"{SkillNames[i]} {teamSkillRows[i]} {comparisonRows[i]}");
        }

        return rows.ToString();
    }

    private static string[] GetSkillRows(Skills skills)
    {
        return new[]
        {
            skills.Design.ToString(),
            skills.Frontend.ToString(),
            skills.Backend.ToString(),
            skills.Database.ToString(),
            skills.Devops.ToString()
        };
    }

    private static string[] GetComparisonRows(Skills teamSkills, Skills developerSkills)
    {
        var designDiff = developerSkills.Design - teamSkills.Design;
        var frontendDiff = developerSkills.Frontend - teamSkills.Frontend;
        var backendDiff = developerSkills.Backend - teamSkills.Backend;
        var databaseDiff = developerSkills.Database - teamSkills.Database;
        var devopsDiff = developerSkills.Devops - teamSkills.Devops;
        return new[]
        {
            designDiff > 0 ? $"+{designDiff}" : "",
            frontendDiff > 0 ? $"+{frontendDiff}" : "",
            backendDiff > 0 ? $"+{backendDiff}" : "",
            databaseDiff > 0 ? $"+{databaseDiff}" : "",
            devopsDiff > 0 ? $"+{devopsDiff}" : ""
        };
    }

    private void HireDeveloper(Developer developer)
    {
        team.AddMember(developer);
        UpdateTeamSpecsText(team);
        OnTeamUpdated?.Invoke();
    }
}
