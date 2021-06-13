using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeveloperMarket : MonoBehaviour
{
    private const int MinSalary = 2500;
    private const int MaxSalary = 10000;

    private static DeveloperMarket _instance;

    public static DeveloperMarket Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("DeveloperMarket").GetComponent<DeveloperMarket>();
            }

            return _instance;
        }
    }

    private readonly List<Developer> availableDevelopers = new List<Developer>();
    private int developerIndex = 0;

    private void Start()
    {
        Game.OnMonthChange += UpdateMarket;
        DevTeam.DeveloperHired += OnDeveloperHired;
    }

    private void UpdateMarket(int month)
    {
        availableDevelopers.Clear();
        AddDevelopersToMarket();
    }

    private void OnDeveloperHired(Developer developer)
    {
        availableDevelopers.Remove(developer);
    }

    private void AddDevelopersToMarket()
    {
        var marketSize = Random.Range(4, 10);
        for (var i = 0; i < marketSize; i++)
        {
            var skills = Skills.GetRandomSkills();
            var salary = Mathf.RoundToInt(skills.GetAverageSkillPercentage() * (MaxSalary - MinSalary) + MinSalary);
            availableDevelopers.Add(new Developer(developerIndex, skills, salary));
            developerIndex++;
        }
    }

    public List<Developer> GetAvailableDevelopers()
    {
        return availableDevelopers.Select(developer => developer).ToList();
    }
}
