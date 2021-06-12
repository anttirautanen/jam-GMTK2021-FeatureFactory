using UnityEngine;

public class CompanyView : MonoBehaviour
{
    public Transform featureViewPrefab;
    public ColumnView columnView;

    private void Start()
    {
        Company.CompanyUpdated += UpdateMoney;
        TeamView.OnTeamUpdated += UpdateMoney;

        var feature = Company.Instance.GetFirstFeature();
        Instantiate(featureViewPrefab, transform)
            .GetComponent<FeatureView>()
            .SetFeature(feature);
    }

    private void UpdateMoney()
    {
        var currentMoney = Company.Instance.GetMoney();
        var teamsCost = Company.Instance.GetFirstTeam().GetCombinedSalary();
        columnView.Set(new[]
        {
            new TextRow("Money: " + currentMoney),
            new TextRow("Teams cost: " + (teamsCost > 0 ? "-" : "") + teamsCost),
            new TextRow("--> " + (currentMoney - teamsCost))
        });
    }
}
