using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Company : MonoBehaviour
{
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
