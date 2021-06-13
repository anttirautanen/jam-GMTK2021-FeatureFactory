using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EditTeamView : MonoBehaviour
{
    public Text teamNameText;
    public Text satisfiesCustomerNeedText;
    public Text qualityText;
    public Text isReleasedText;
    public Text teamCostText;
    public Text teamSkillsText;
    public ColumnView membersColumnView;
    private Feature feature;
    private int selectedDeveloperIndex = 0;

    public void Setup(Feature feature)
    {
        this.feature = feature;
    }

    private void Start()
    {
        DevTeam.TeamUpdated += OnTeamUpdated;

        teamNameText.text = feature.Name;
        satisfiesCustomerNeedText.text = Mathf.RoundToInt(feature.SatisfiesCustomerNeed * 100) + "%";
        qualityText.text = Mathf.RoundToInt(feature.Quality * 100) + "%";
        isReleasedText.text = feature.IsReleased() ? "Released" : "Not released";
        UpdateTeamCostAndSkills();
        UpdateMembersColumnView();
    }

    private void OnDestroy()
    {
        DevTeam.TeamUpdated -= OnTeamUpdated;
    }

    private void OnTeamUpdated()
    {
        UpdateTeamCostAndSkills();
        UpdateMembersColumnView();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetSelectedDeveloperIndex(selectedDeveloperIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SetSelectedDeveloperIndex(selectedDeveloperIndex - 1);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            var developerToFire = GetSelectedDeveloper();
            if (developerToFire != null)
            {
                feature.GetTeam().FireMember(developerToFire);
                SetSelectedDeveloperIndex(selectedDeveloperIndex);
            }
        }
    }

    private void SetSelectedDeveloperIndex(int nextIndex)
    {
        var developerCount = feature.GetTeam().GetMemberCount();
        if (nextIndex >= developerCount)
        {
            selectedDeveloperIndex = 0;
        }
        else if (nextIndex < 0)
        {
            selectedDeveloperIndex = developerCount - 1;
        }
        else
        {
            selectedDeveloperIndex = nextIndex;
        }

        UpdateMembersColumnView();
        UpdateTeamCostAndSkills();
    }

    private void UpdateTeamCostAndSkills()
    {
        var team = feature.GetTeam();
        var teamSpecs = new TeamSpecs(team);
        teamCostText.text = $"{team.GetCombinedSalary():C0}";

        var selectedDeveloper = GetSelectedDeveloper();
        teamSkillsText.text = selectedDeveloper != null
            ? teamSpecs.SpeculateFiring(selectedDeveloper)
            : teamSpecs.GetSkillsString();
    }

    private Developer GetSelectedDeveloper()
    {
        return feature.GetTeam().GetMembers().ElementAtOrDefault(selectedDeveloperIndex);
    }

    private void UpdateMembersColumnView()
    {
        var team = feature.GetTeam();
        var developers = team.GetMembers();
        var developerRows = new List<DeveloperRow>();
        for (var i = 0; i < team.GetMemberCount(); ++i)
        {
            var isSelected = selectedDeveloperIndex == i;
            developerRows.Add(new DeveloperRow(developers[i], isSelected));
        }

        membersColumnView.Set(developerRows);
    }
}
