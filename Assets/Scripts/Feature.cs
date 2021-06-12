using UnityEngine;

public class Feature
{
    public float SatisfiesCustomerNeed = 0f;
    public float Quality = 0f;

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
}
