using System;
using System.Collections.Generic;
using System.Linq;

public class TeamSpecs
{
    private readonly DevTeam team;
    public static readonly string[] SkillNames = {"Design", "Frontend", "Backend", "Databases", "Devops"};

    public TeamSpecs(DevTeam team)
    {
        this.team = team;
    }

    public static IEnumerable<int> GetSkillRows(Skills skills)
    {
        return new[]
        {
            skills.Design,
            skills.Frontend,
            skills.Backend,
            skills.Database,
            skills.Devops
        };
    }

    public int[] GetComparisonRowsWhenFiring(Developer developerToFire)
    {
        if (developerToFire == null)
        {
            return new[] {0, 0, 0, 0, 0};
        }

        var designDiff = GetDiffIfDeveloperFired(developerToFire, developer => developer.Skills.Design);
        var frontendDiff = GetDiffIfDeveloperFired(developerToFire, developer => developer.Skills.Frontend);
        var backendDiff = GetDiffIfDeveloperFired(developerToFire, developer => developer.Skills.Backend);
        var databaseDiff = GetDiffIfDeveloperFired(developerToFire, developer => developer.Skills.Database);
        var devopsDiff = GetDiffIfDeveloperFired(developerToFire, developer => developer.Skills.Devops);
        return new[]
        {
            designDiff > 0 ? -designDiff : 0,
            frontendDiff > 0 ? -frontendDiff : 0,
            backendDiff > 0 ? -backendDiff : 0,
            databaseDiff > 0 ? -databaseDiff : 0,
            devopsDiff > 0 ? -devopsDiff : 0,
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
}
