using UnityEngine;

public class Skills
{
    private const int MaxSkill = 10;
    private const int SatisfiesCustomerNeedSkillThreshold = 5;
    private const float MaxSatisfiesCustomerNeedImprovement = 0.16f;
    private const int QualitySkillThreshold = 7;
    private const float MaxQualityImprovement = 0.12f;
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
        return Mathf.RoundToInt(Random.Range(0, MaxSkill + 1));
    }

    public float GetSatisfiesCustomerNeedImprovement()
    {
        var teamSkillMultiplier = GetTeamSkillMultiplier(SatisfiesCustomerNeedSkillThreshold);
        return teamSkillMultiplier * MaxSatisfiesCustomerNeedImprovement;
    }

    public float GetQualityImprovement()
    {
        var teamSkillMultiplier = GetTeamSkillMultiplier(QualitySkillThreshold);
        return teamSkillMultiplier * MaxQualityImprovement;
    }

    private int GetLowestSkillPoint()
    {
        return Mathf.Min(new[] {Design, Frontend, Backend, Database, Devops});
    }

    private float GetTeamSkillMultiplier(int threshold)
    {
        var lowestSkillPoint = GetLowestSkillPoint();
        if (lowestSkillPoint < threshold)
        {
            return (float) lowestSkillPoint / threshold;
        }

        if (lowestSkillPoint > threshold)
        {
            return ((float) lowestSkillPoint - threshold) / (MaxSkill - threshold) + 0.9f;
        }

        return 0.9f;
    }

    public float GetAverageSkillPercentage()
    {
        return (float) (Design + Frontend + Backend + Database + Devops) / (MaxSkill * 5);
    }
}
