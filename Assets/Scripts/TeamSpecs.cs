using System;
using System.Linq;
using System.Text;

public class TeamSpecs
{
    private readonly DevTeam team;
    private static readonly string[] SkillNames = {"Design", "Frontend", "Backend", "Databases", "Devops"};

    public TeamSpecs(DevTeam team)
    {
        this.team = team;
    }

    public string SpeculateHiring(Developer nextDeveloper)
    {
        var teamSpecs = new StringBuilder().AppendLine($"Members: {team.GetMemberCount()}");
        var teamSkills = team.GetCombinedSkills();
        var teamSkillRows = GetSkillRows(teamSkills);
        var comparisonRows = GetComparisonRows(teamSkills, nextDeveloper.Skills);
        for (var i = 0; i < teamSkillRows.Length; i++)
        {
            teamSpecs.AppendLine($"{SkillNames[i]} {teamSkillRows[i]} {comparisonRows[i]}");
        }

        return teamSpecs.ToString();
    }

    public string SpeculateFiring(Developer developerToFire)
    {
        var teamSpecs = new StringBuilder().AppendLine($"Members: {team.GetMemberCount()}");
        var teamSkills = team.GetCombinedSkills();
        var teamSkillRows = GetSkillRows(teamSkills);
        var comparisonRows = GetComparisonRowsWhenFiring(developerToFire);
        for (var i = 0; i < teamSkillRows.Length; i++)
        {
            teamSpecs.AppendLine($"{SkillNames[i]} {teamSkillRows[i]} {comparisonRows[i]}");
        }

        return teamSpecs.ToString();
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

    private string[] GetComparisonRowsWhenFiring(Developer developerToFire)
    {
        var designDiff = GetDiffIfDeveloperFired(developerToFire, developer => developer.Skills.Design);
        var frontendDiff = GetDiffIfDeveloperFired(developerToFire, developer => developer.Skills.Frontend);
        var backendDiff = GetDiffIfDeveloperFired(developerToFire, developer => developer.Skills.Backend);
        var databaseDiff = GetDiffIfDeveloperFired(developerToFire, developer => developer.Skills.Database);
        var devopsDiff = GetDiffIfDeveloperFired(developerToFire, developer => developer.Skills.Devops);
        return new[]
        {
            designDiff > 0 ? $"-{designDiff}" : "",
            frontendDiff > 0 ? $"-{frontendDiff}" : "",
            backendDiff > 0 ? $"-{backendDiff}" : "",
            databaseDiff > 0 ? $"-{databaseDiff}" : "",
            devopsDiff > 0 ? $"-{devopsDiff}" : "",
        };
    }

    private int GetDiffIfDeveloperFired(Developer developerToFire, Func<Developer, int> getSkillFunc)
    {
        if (team.GetMemberCount() > 1)
        {
            var maxSkillWithoutDeveloperToFire = team
                .GetMembers()
                .Where(developer => developer != developerToFire)
                .Select(getSkillFunc)
                .Max();
            return getSkillFunc(developerToFire) - maxSkillWithoutDeveloperToFire;
        }

        return getSkillFunc(developerToFire);
    }

    public string GetSkillsString()
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
}
