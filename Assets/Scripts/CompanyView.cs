using UnityEngine;

public class CompanyView : MonoBehaviour
{
    public Transform featureViewPrefab;
    public ColumnView moneyColumn;

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
        var productPrice = Company.Instance.productPrice;
        var customerCount = Customers.Instance.GetCustomerCount();
        var nextMonthIncome = productPrice * customerCount;
        moneyColumn.Set(new[]
        {
            new TextRow($"Current money: {currentMoney:C0}"),
            new TextRow("Current customers: " + customerCount),
            new TextRow($"Product price: {productPrice:C0}"),
            new TextRow($"Next month costs: {teamsCost:C0}"),
            new TextRow($"Next month income: {nextMonthIncome:C0}"),
            new TextRow($"Next month balance: {(currentMoney - teamsCost + nextMonthIncome):C0}")
        });
    }
}
