using UnityEngine;

public class Skills
{
    public readonly int Design;
    public readonly int Frontend;
    public readonly int Backend;
    public readonly int Database;
    public readonly int Devops;

    public Skills(int design, int frontend, int backend, int database, int devops)
    {
        Design = design;
        Frontend = frontend;
        Backend = backend;
        Database = database;
        Devops = devops;
    }

    public Skills() : this(0, 0, 0, 0, 0)
    {
    }

    public static Skills GetRandomSkills()
    {
        return new Skills(
            GetRandomSkillPoint(),
            GetRandomSkillPoint(),
            GetRandomSkillPoint(),
            GetRandomSkillPoint(),
            GetRandomSkillPoint());
    }

    private static int GetRandomSkillPoint()
    {
        return Mathf.RoundToInt(Random.Range(0, 10));
    }
}
