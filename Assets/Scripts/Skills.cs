using System;
using UnityEngine;
using Random = UnityEngine.Random;

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
        var focusIndex = Random.Range(0, 5);
        var maxSkill = Random.Range(4, 10);
        var skills = new int[5];
        for (var i = 0; i < 5; ++i)
        {
            skills[i] = Mathf.Clamp(maxSkill - Mathf.Abs(focusIndex - i) * 2, 0, 10);
        }

        return new Skills(skills[0], skills[1], skills[2], skills[3], skills[4]);
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

    public int GetByIndex(int skillIndex)
    {
        return skillIndex switch
        {
            0 => Design,
            1 => Frontend,
            2 => Backend,
            3 => Database,
            4 => Devops,
            _ => throw new IndexOutOfRangeException($"No skills found with index {skillIndex}.")
        };
    }
}
