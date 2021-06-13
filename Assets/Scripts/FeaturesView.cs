using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FeaturesView : MonoBehaviour
{
    public ColumnView featureColumnView;
    private int selectedFeatureIndex = 0;

    private void Start()
    {
        Company.CompanyStatsUpdated += UpdateFeatureList;

        UpdateFeatureList();
        KeyHelp.Instance.Set(new[] {"(Esc) Company", "(N)ew feature", "(H)ire", "(R)elease", "(E)dit team"});
    }

    private void OnDestroy()
    {
        Company.CompanyStatsUpdated -= UpdateFeatureList;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Company.Instance.CreateFeatureAndTeam();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            var selectedFeature = GetSelectedFeature();
            if (selectedFeature != null)
            {
                var hiringViewTransformInstance = UiController.Instance.OpenView(View.Hire);
                var selectedTeam = selectedFeature.GetTeam();
                hiringViewTransformInstance.GetComponentInChildren<TeamView>().Setup(selectedTeam);
                hiringViewTransformInstance.GetComponentInChildren<HiringView>().Setup(selectedTeam);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            var selectedFeature = GetSelectedFeature();
            selectedFeature?.Release();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            var selectedFeature = GetSelectedFeature();
            if (selectedFeature != null)
            {
                var hiringViewTransformInstance = UiController.Instance.OpenView(View.EditTeam);
                hiringViewTransformInstance.GetComponent<EditTeamView>().Setup(selectedFeature);
            }
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetSelectedFeatureIndex(selectedFeatureIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetSelectedFeatureIndex(selectedFeatureIndex - 1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UiController.Instance.OpenView(View.Company);
        }
    }

    private void SetSelectedFeatureIndex(int nextIndex)
    {
        var featureCount = Company.Instance.GetAllFeatures().Count();
        if (featureCount == 0)
        {
            selectedFeatureIndex = 0;
        }
        else
        {
            if (nextIndex >= featureCount)
            {
                selectedFeatureIndex = 0;
            }
            else if (nextIndex < 0)
            {
                selectedFeatureIndex = featureCount - 1;
            }
            else
            {
                selectedFeatureIndex = nextIndex;
            }
        }

        UpdateFeatureList();
    }

    private void UpdateFeatureList()
    {
        var features = Company.Instance.GetAllFeatures().ToList();
        var rows = new List<IRow>
        {
            new LabelAndValueRow("Features", "", TextRowStyle.Heading),
            new FeatureRowColumnHeadings()
        };

        for (var i = 0; i < features.Count; ++i)
        {
            var isSelected = selectedFeatureIndex == i;
            rows.Add(new FeatureRow(features[i], isSelected));
        }

        if (features.Count == 0)
        {
            rows.Add(new TextRow("No features"));
        }

        featureColumnView.Set(rows);
    }

    private Feature GetSelectedFeature()
    {
        return Company.Instance.GetAllFeatures().ToArray().ElementAtOrDefault(selectedFeatureIndex);
    }
}
