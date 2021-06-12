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

    public int productPrice = 10;
    private BigInteger money = 500000;
    private readonly List<DevTeam> teams = new List<DevTeam>();
    private readonly List<Feature> features = new List<Feature>();

    private void Start()
    {
        Game.OnMonthChange += MonthUpdate;
    }

    public BigInteger GetMoney()
    {
        return money;
    }

    public IEnumerable<DevTeam> GetTeams()
    {
        return teams.Select(team => team);
    }

    public int GetTeamCount()
    {
        return GetTeams().Count();
    }

    public void CreateFeatureAndTeam()
    {
        var team = new DevTeam();
        teams.Add(team);
        features.Add(new Feature(team, Game.Instance.GetCurrentMonth()));
        CompanyUpdated?.Invoke();
    }

    public int GetCombinedSalary()
    {
        return teams.Aggregate(0, (combinedSalary, team) => combinedSalary + team.GetCombinedSalary());
    }

    private void MonthUpdate(int month)
    {
        money += Customers.Instance.GetCustomerCount() * productPrice;

        GetAllFeatures().ToList().ForEach(feature =>
        {
            feature.AgeOneMonth();
            var teamSkills = feature.GetTeam().GetCombinedSkills();
            feature.Improve(teamSkills.GetSatisfiesCustomerNeedImprovement(), teamSkills.GetQualityImprovement());
        });

        teams.ForEach(team => money -= team.GetCombinedSalary());

        CompanyUpdated?.Invoke();
    }

    public IEnumerable<Feature> GetAllFeatures()
    {
        return features.Select(feature => feature);
    }

    private IEnumerable<Feature> GetReleasedFeatures()
    {
        return GetAllFeatures().Where(feature => feature.MonthIntroduced > 0);
    }

    public int GetMonthsSinceLastNewFeature()
    {
        return Game.Instance.GetCurrentMonth() - GetReleasedFeatures().Select(feature => feature.MonthIntroduced).Max();
    }

    public int GetFeatureCount()
    {
        return GetReleasedFeatures().Count();
    }

    public float GetAverageQuality()
    {
        return GetReleasedFeatures().Aggregate(0f, (qualitySum, feature) => qualitySum + feature.Quality)
               / GetReleasedFeatures().Count();
    }
}
