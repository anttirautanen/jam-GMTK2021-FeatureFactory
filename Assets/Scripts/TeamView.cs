using System;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class TeamView : MonoBehaviour
{
    public static event Action OnTeamUpdated;

    public Text teamNameText;
    public Text teamSpecsText;
    private int selectedTeamIndex = 0;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            SetSelectedTeamIndex(selectedTeamIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            SetSelectedTeamIndex(selectedTeamIndex - 1);
        }
    }

    private void SetSelectedTeamIndex(int nextIndex)
    {
        if (nextIndex >= Company.Instance.GetTeamCount())
        {
            selectedTeamIndex = 0;
        }
        else if (nextIndex < 0)
        {
            selectedTeamIndex = Company.Instance.GetTeamCount() - 1;
        }
        else
        {
            selectedTeamIndex = nextIndex;
        }

        UpdateTeamList();
        UpdateTeamSpecsText();
    }

    private void UpdateTeamList()
    {
        teamNameText.text = Company.Instance.GetTeamCount() > 0
            ? GetSelectedTeam().Feature.Name
            : "No teams yet - create a feature first";
    }

    private void ShowTeamWithDeveloper(Developer developer)
    {
        UpdateTeamSpecsText(developer);
    }

    private void UpdateTeamSpecsText(Developer nextDeveloper = null)
    {
        if (Company.Instance.GetTeamCount() > 0)
        {
            var team = GetSelectedTeam();
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
        GetSelectedTeam().AddMember(developer);
        UpdateTeamSpecsText();
        OnTeamUpdated?.Invoke();
    }

    private DevTeam GetSelectedTeam()
    {
        if (Company.Instance.GetTeamCount() > 0)
        {
            return Company.Instance.GetTeams().ToArray()[selectedTeamIndex];
        }

        return null;
    }
}
