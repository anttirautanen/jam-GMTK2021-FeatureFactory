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
            var features = Company.Instance.GetAllFeatures().ToList();
            var featureCount = features.Count;
            if (featureCount > 0)
            {
                var hiringViewTransformInstance = UiController.Instance.OpenView(View.Hire);
                var selectedTeam = features[selectedFeatureIndex].GetTeam();
                hiringViewTransformInstance.GetComponentInChildren<TeamView>().Setup(selectedTeam);
                hiringViewTransformInstance.GetComponentInChildren<HiringView>().Setup(selectedTeam);
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

        featureColumnView.Set(rows);
    }
}
