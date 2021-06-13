using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TeamView : MonoBehaviour
{
    public static event Action OnTeamUpdated;

    public Text teamNameText;
    public Text teamSpecsText;
    private DevTeam team;

    public void Setup(DevTeam team)
    {
        this.team = team;
    }

    private void Start()
    {
        MarketView.OnChangeHighlightedDeveloper += ShowTeamWithDeveloper;
        MarketView.OnHireDeveloper += HireDeveloper;

        UpdateTeamList();
        UpdateTeamSpecsText();
    }

    private void OnDestroy()
    {
        MarketView.OnChangeHighlightedDeveloper -= ShowTeamWithDeveloper;
        MarketView.OnHireDeveloper -= HireDeveloper;
    }

    private void UpdateTeamList()
    {
        teamNameText.text = team.Feature.Name;
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
