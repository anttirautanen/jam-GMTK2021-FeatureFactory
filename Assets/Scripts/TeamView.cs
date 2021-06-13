using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TeamView : MonoBehaviour
{
    public static event Action OnTeamUpdated;

    public Text teamSpecsText;
    private DevTeam team;

    public void Setup(DevTeam team)
    {
        this.team = team;
    }

    private void Start()
    {
        AvailableDevelopersView.OnChangeHighlightedDeveloper += ShowTeamWithDeveloper;
        AvailableDevelopersView.OnHireDeveloper += HireDeveloper;

        UpdateTeamSpecsText();
    }

    private void OnDestroy()
    {
        MarketView.OnChangeHighlightedDeveloper -= ShowTeamWithDeveloper;
        MarketView.OnHireDeveloper -= HireDeveloper;
    }

    private void ShowTeamWithDeveloper(Developer developer)
    {
        UpdateTeamSpecsText(developer);
    }

    private void UpdateTeamSpecsText(Developer nextDeveloper = null)
    {
        if (Company.Instance.GetTeamCount() > 0)
        {
            var teamSpecs = new TeamSpecs(team);
            var teamSpecsString = new StringBuilder().AppendLine($"Members: {team.GetMemberCount()}");

            teamSpecsString.Append(nextDeveloper != null
                ? teamSpecs.SpeculateHiring(nextDeveloper)
                : teamSpecs.GetSkillsString());

            teamSpecsText.text = teamSpecsString.ToString();
        }
        else
        {
            teamSpecsText.text = "";
        }
    }

    private void HireDeveloper(Developer developer)
    {
        team.AddMember(developer);
        UpdateTeamSpecsText();
        OnTeamUpdated?.Invoke();
    }
}
