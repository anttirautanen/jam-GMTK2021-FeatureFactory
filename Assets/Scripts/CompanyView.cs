using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompanyView : MonoBehaviour
{
    public ColumnView moneyColumn;
    public ColumnView featureColumn;
    public Transform editTeamView;
    public Transform hiringView;
    private int selectedFeatureIndex = 0;

    private void Start()
    {
        Company.CompanyStatsUpdated += UpdateMoneyColumn;
        Company.CompanyStatsUpdated += UpdateFeatureColumn;
        TeamView.OnTeamUpdated += UpdateMoneyColumn;
        DevTeam.TeamUpdated += UpdateMoneyColumn;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Company.Instance.CreateFeatureAndTeam();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            var selectedFeature = GetSelectedFeature();
            selectedFeature?.Release();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SetSelectedFeature(selectedFeatureIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SetSelectedFeature(selectedFeatureIndex - 1);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Company.Instance.SetProductPrice(Company.Instance.productPrice + 10);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Company.Instance.SetProductPrice(Company.Instance.productPrice - 10);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            var selectedFeature = GetSelectedFeature();
            if (selectedFeature != null)
            {
                var transformInstance = UiController.Instance.OpenView(editTeamView);
                transformInstance.GetComponent<EditTeamView>().Setup(selectedFeature);
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            var selectedFeature = GetSelectedFeature();
            if (selectedFeature != null)
            {
                var transformInstance = UiController.Instance.OpenView(hiringView);
                transformInstance.GetComponentInChildren<TeamView>().Setup(GetSelectedFeature().GetTeam());
            }
        }
    }

    private void SetSelectedFeature(int nextIndex)
    {
        var featuresCount = Company.Instance.GetAllFeatures().Count();
        if (nextIndex >= featuresCount)
        {
            selectedFeatureIndex = 0;
        }
        else if (nextIndex < 0)
        {
            if (featuresCount == 0)
            {
                selectedFeatureIndex = 0;
            }
            else
            {
                selectedFeatureIndex = featuresCount - 1;
            }
        }
        else
        {
            selectedFeatureIndex = nextIndex;
        }

        UpdateFeatureColumn();
    }

    private void UpdateMoneyColumn()
    {
        var currentMoney = Company.Instance.GetMoney();
        var teamsCost = Company.Instance.GetCombinedSalary();
        var productPrice = Company.Instance.productPrice;
        var customerCount = Customers.Instance.GetCustomerCount();
        var nextMonthIncome = productPrice * customerCount;
        moneyColumn.Set(new IRow[]
        {
            new LabelAndValueRow("Money", $"{currentMoney:C0}", TextRowStyle.Heading),
            new LabelAndValueRow("Income", $"{nextMonthIncome:C0}", TextRowStyle.Positive),
            new LabelAndValueRow("Expenses", $"{teamsCost:C0}", TextRowStyle.Negative),
            new LabelAndValueRow("Next month balance", $"{(currentMoney - teamsCost + nextMonthIncome):C0}"),
            new SeparatorRow(),
            new LabelAndValueRow("Customers", customerCount.ToString()),
            new LabelAndValueRow("Product price", $"{productPrice:C0}"),
            new SeparatorRow(),
            new LabelAndValueRow("Customer demand",
                $"{Mathf.RoundToInt(Customers.Instance.GetCustomerDemand() * 100)}%"),
            new LabelAndValueRow("Age effect", $"{Mathf.RoundToInt(Customers.Instance.GetOldnessEffect() * 100)}%"),
            new LabelAndValueRow("Quality effect", $"{Mathf.RoundToInt(Customers.Instance.GetQualityEffect() * 100)}%"),
            new LabelAndValueRow("Price effect", $"{Mathf.RoundToInt(Customers.Instance.GetPriceEffect() * 100)}%")
        });
    }

    private void UpdateFeatureColumn()
    {
        var features = Company.Instance.GetAllFeatures().ToList();
        var featureRows = new List<FeatureRow>();
        for (var i = 0; i < features.Count; ++i)
        {
            var isSelected = selectedFeatureIndex == i;
            featureRows.Add(new FeatureRow(features[i], isSelected));
        }

        featureColumn.Set(featureRows);
    }

    private Feature GetSelectedFeature()
    {
        var features = Company.Instance.GetAllFeatures().ToArray();
        return features.ElementAtOrDefault(selectedFeatureIndex);
    }
}
