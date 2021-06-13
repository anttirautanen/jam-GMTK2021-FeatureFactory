using System;
using UnityEngine;

public class Feature
{
    public static event Action FeatureReleased;

    public float SatisfiesCustomerNeed = 0f;
    public float Quality = 0f;
    public int MonthIntroduced;
    private readonly DevTeam team;

    public Feature(DevTeam team)
    {
        this.team = team;
    }

    public void AgeOneMonth()
    {
        SatisfiesCustomerNeed *= 0.95f;
        Quality *= 0.93f;
    }

    public void Improve(float satisfiesCustomerNeedImprovement, float qualityImprovement)
    {
        SatisfiesCustomerNeed = Mathf.Clamp(SatisfiesCustomerNeed + satisfiesCustomerNeedImprovement, 0, 1f);
        Quality = Mathf.Clamp(Quality + qualityImprovement, 0, 1f);
    }

    public DevTeam GetTeam()
    {
        return team;
    }

    public void Release()
    {
        if (!IsReleased())
        {
            MonthIntroduced = Game.Instance.GetCurrentMonth();
            FeatureReleased?.Invoke();
        }
    }

    public bool IsReleased()
    {
        return MonthIntroduced > 0;
    }
}
