using System.Collections.Generic;
using System.Linq;

public class DevTeam
{
    private readonly List<Developer> members = new List<Developer>();

    public int GetMemberCount()
    {
        return members.Count;
    }

    public Skills GetCombinedSkills()
    {
        if (members.Count == 0)
        {
            return new Skills();
        }

        return new Skills(
            members.Select(developer => developer.Skills.Design).Max(),
            members.Select(developer => developer.Skills.Frontend).Max(),
            members.Select(developer => developer.Skills.Backend).Max(),
            members.Select(developer => developer.Skills.Database).Max(),
            members.Select(developer => developer.Skills.Devops).Max());
    }

    public void AddMember(Developer developer)
    {
        members.Add(developer);
    }

    public int GetCombinedSalary()
    {
        return members.Aggregate(0, (combinedSalary, developer) => combinedSalary + developer.Salary);
    }
}
