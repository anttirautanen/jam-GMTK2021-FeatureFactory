using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeveloperMarket : MonoBehaviour
{
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

    public int marketSize = 10;
    private readonly List<Developer> availableDevelopers = new List<Developer>();
    private int developerIndex = 0;

    private void Start()
    {
        Game.OnMonthChange += UpdateMarket;
    }

    private void UpdateMarket(int month)
    {
        availableDevelopers.Clear();
        AddDevelopersToMarket();
    }

    private void AddDevelopersToMarket()
    {
        for (var i = 0; i < marketSize; i++)
        {
            availableDevelopers.Add(new Developer(developerIndex, Skills.GetRandomSkills()));
            developerIndex++;
        }
    }

    public List<Developer> GetAvailableDevelopers()
    {
        return availableDevelopers.Select(developer => developer).ToList();
    }
}
