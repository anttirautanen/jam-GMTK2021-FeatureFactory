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
            members.Select(developer => developer.skills.Design).Max(),
            members.Select(developer => developer.skills.Frontend).Max(),
            members.Select(developer => developer.skills.Backend).Max(),
            members.Select(developer => developer.skills.Database).Max(),
            members.Select(developer => developer.skills.Devops).Max());
    }

    public void AddMember(Developer developer)
    {
        members.Add(developer);
    }
}
