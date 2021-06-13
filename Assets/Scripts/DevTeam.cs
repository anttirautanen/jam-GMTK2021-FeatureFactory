using System;
using System.Collections.Generic;
using System.Linq;

public class DevTeam
{
    public static event Action TeamUpdated;
    private readonly List<Developer> members = new List<Developer>();
    public Feature Feature;

    public void SetFeature(Feature feature)
    {
        Feature = feature;
    }

    public List<Developer> GetMembers()
    {
        return members.Select(developer => developer).ToList();
    }

    public int GetMemberCount()
    {
        return GetMembers().Count;
    }

    public Skills GetCombinedSkills()
    {
        var developers = GetMembers();
        if (developers.Count == 0)
        {
            return new Skills();
        }

        return new Skills(
            developers.Select(developer => developer.Skills.Design).Max(),
            developers.Select(developer => developer.Skills.Frontend).Max(),
            developers.Select(developer => developer.Skills.Backend).Max(),
            developers.Select(developer => developer.Skills.Database).Max(),
            developers.Select(developer => developer.Skills.Devops).Max());
    }

    public void AddMember(Developer developer)
    {
        members.Add(developer);
    }

    public int GetCombinedSalary()
    {
        return GetMembers().Aggregate(0, (combinedSalary, developer) => combinedSalary + developer.Salary);
    }

    public void FireMember(Developer developer)
    {
        members.Remove(developer);
        TeamUpdated?.Invoke();
    }
}
