using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Company : MonoBehaviour
{
    private static Company _instance;

    public static Company Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Company").GetComponent<Company>();
            }

            return _instance;
        }
    }

    private readonly List<DevTeam> teams = new List<DevTeam>();
    private readonly List<Feature> features = new List<Feature>();

    private void Start()
    {
        print("Start company");

        Game.OnMonthChange += UpdateFeatures;

        teams.Add(new DevTeam());
        features.Add(new Feature());
    }

    public DevTeam GetFirstTeam()
    {
        return teams.First();
    }

    public Feature GetFirstFeature()
    {
        return features.First();
    }

    private void UpdateFeatures(int month)
    {
        features.ForEach(feature =>
        {
            feature.AgeOneMonth();
            var teamSkills = GetFirstTeam().GetCombinedSkills();
            feature.Improve(teamSkills.GetSatisfiesCustomerNeedImprovement(), teamSkills.GetQualityImprovement());
        });
    }
}
