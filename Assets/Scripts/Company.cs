using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

public class Company : MonoBehaviour
{
    public static event Action CompanyMonthlyUpdate;
    public static event Action CompanyStatsUpdated;

    private static readonly Stack<string> NameList = new Stack<string>();
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
        Feature.FeatureReleased += OnFeatureReleased;

        NameList.Push("Conecare");
        NameList.Push("Nemospace");
        NameList.Push("Daltfind");
        NameList.Push("Dreamwood");
        NameList.Push("Moonbank");
        NameList.Push("Hogmaster");
        NameList.Push("Domjotech");
        NameList.Push("Betaworld");
        NameList.Push("Zerace");
        NameList.Push("Unatatex");
        NameList.Push("Hatchwheels");
        NameList.Push("Alligatortechs");
        NameList.Push("Webtales");
        NameList.Push("Zimcare");
        NameList.Push("Jetwood");
        NameList.Push("Howzooming");
        NameList.Push("Hammertronics");
        NameList.Push("Lioncoms");
        NameList.Push("Hummingtube");
        NameList.Push("Globeshadow");
        NameList.Push("Roundzoom");
        NameList.Push("Bigzoom");
        NameList.Push("Rootworth");
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
        var feature = new Feature(NameList.Pop(), team);
        team.SetFeature(feature);
        features.Add(feature);
        CompanyStatsUpdated?.Invoke();
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

        if (money < 0)
        {
            print("RAN OUT OF MONEY");
            Game.Instance.RanOutOfMoney();
        }
        else
        {
            CompanyMonthlyUpdate?.Invoke();
            CompanyStatsUpdated?.Invoke();
        }
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
        var releasedFeatures = GetReleasedFeatures().ToList();
        if (releasedFeatures.Count == 0)
        {
            return Game.Instance.GetCurrentMonth() - 1; // game starts from month 1
        }

        return Game.Instance.GetCurrentMonth() - releasedFeatures.Select(feature => feature.MonthIntroduced).Max();
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

    private static void OnFeatureReleased()
    {
        CompanyStatsUpdated?.Invoke();
    }

    public void SetProductPrice(int nextPrice)
    {
        productPrice = nextPrice < 10 ? 10 : nextPrice;
        CompanyStatsUpdated?.Invoke();
    }
}
