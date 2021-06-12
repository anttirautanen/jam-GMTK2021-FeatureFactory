using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

public class Company : MonoBehaviour
{
    public static event Action CompanyUpdated;

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

    private BigInteger money = 500000;
    private readonly List<DevTeam> teams = new List<DevTeam>();
    private readonly List<Feature> features = new List<Feature>();

    private void Start()
    {
        print("Start company");

        Game.OnMonthChange += MonthUpdate;

        teams.Add(new DevTeam());
        features.Add(new Feature());
    }

    public BigInteger GetMoney()
    {
        return money;
    }

    public DevTeam GetFirstTeam()
    {
        return teams.First();
    }

    public Feature GetFirstFeature()
    {
        return features.First();
    }

    private void MonthUpdate(int month)
    {
        features.ForEach(feature =>
        {
            feature.AgeOneMonth();
            var teamSkills = GetFirstTeam().GetCombinedSkills();
            feature.Improve(teamSkills.GetSatisfiesCustomerNeedImprovement(), teamSkills.GetQualityImprovement());
        });

        teams.ForEach(team => money -= team.GetCombinedSalary());

        CompanyUpdated?.Invoke();
    }
}
