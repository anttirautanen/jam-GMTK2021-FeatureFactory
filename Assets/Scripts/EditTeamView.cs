using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EditTeamView : MonoBehaviour
{
    public Text headingText;
    public ColumnView membersColumnView;
    public ColumnView teamSkillColumnView;
    private Feature feature;
    private int selectedDeveloperIndex = 0;

    public void Setup(Feature feature)
    {
        this.feature = feature;
    }

    private void Start()
    {
        DevTeam.TeamUpdated += OnTeamUpdated;

        headingText.text = $"Team {feature.Name}";
        UpdateMembersColumn();
        UpdateTeamSkillColumn();
    }

    private void OnDestroy()
    {
        DevTeam.TeamUpdated -= OnTeamUpdated;
    }

    private void OnTeamUpdated()
    {
        UpdateTeamSkillColumn();
        UpdateMembersColumn();
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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UiController.Instance.OpenView(View.Features);
        }
    }

    private void SetSelectedDeveloperIndex(int nextIndex)
    {
        var developerCount = feature.GetTeam().GetMemberCount();
        if (developerCount == 0)
        {
            selectedDeveloperIndex = 0;
        }
        else
        {
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
        }

        UpdateMembersColumn();
        UpdateTeamSkillColumn();
    }

    private void UpdateTeamSkillColumn()
    {
        var teamSpecs = new TeamSpecs(feature.GetTeam());
        var skillChanges = teamSpecs.GetComparisonRowsWhenFiring(GetSelectedDeveloper());
        teamSkillColumnView.Set(TeamSpecs
            .GetSkillRows(feature.GetTeam().GetCombinedSkills())
            .Select((currentSkill, index) => new SkillRow(TeamSpecs.SkillNames[index], currentSkill.ToString(), skillChanges[index]))
        );
    }

    private Developer GetSelectedDeveloper()
    {
        return feature.GetTeam().GetMembers().ElementAtOrDefault(selectedDeveloperIndex);
    }

    private void UpdateMembersColumn()
    {
        var team = feature.GetTeam();
        var developers = team.GetMembers();
        var developerRows = new List<IRow>
        {
            new TextRow("Developers", TextRowStyle.ColumnHeading)
        };
        for (var i = 0; i < team.GetMemberCount(); ++i)
        {
            var isSelected = selectedDeveloperIndex == i;
            developerRows.Add(new DeveloperRow(developers[i], isSelected));
        }

        if (developers.Count == 0)
        {
            developerRows.Add(new TextRow("No developers"));
        }

        developerRows.Add(new SeparatorRow());
        developerRows.Add(new LabelAndValueRow("Total cost",
            $"{developers.Aggregate(0, (totalSalary, developer) => totalSalary + developer.Salary):C0}"));

        membersColumnView.Set(developerRows);
    }
}
