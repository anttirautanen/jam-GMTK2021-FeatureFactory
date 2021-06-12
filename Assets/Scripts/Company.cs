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

    private void Start()
    {
        print("Start company");
        teams.Add(new DevTeam());
    }

    public DevTeam GetFirstTeam()
    {
        return teams.First();
    }
}
