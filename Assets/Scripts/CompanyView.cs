using System.Linq;
using UnityEngine;

public class CompanyView : MonoBehaviour
{
    public ColumnView moneyColumn;
    public ColumnView featureColumn;

    private void Start()
    {
        Company.CompanyUpdated += UpdateMoney;
        Company.CompanyUpdated += UpdateFeatures;
        TeamView.OnTeamUpdated += UpdateMoney;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Company.Instance.CreateFeatureAndTeam();
        }
    }

    private void UpdateMoney()
    {
        var currentMoney = Company.Instance.GetMoney();
        var teamsCost = Company.Instance.GetCombinedSalary();
        var productPrice = Company.Instance.productPrice;
        var customerCount = Customers.Instance.GetCustomerCount();
        var nextMonthIncome = productPrice * customerCount;
        moneyColumn.Set(new[]
        {
            new TextRow($"Product price: {productPrice:C0}"),
            new TextRow($"Current money: {currentMoney:C0}"),
            new TextRow("Current customers: " + customerCount),
            new TextRow($"Oldness effect: {Mathf.RoundToInt(Customers.GetOldnessEffect() * 100)}%"),
            new TextRow($"Quality effect: {Mathf.RoundToInt(Customers.GetQualityEffect() * 100)}%"),
            new TextRow($"Price effect: {Mathf.RoundToInt(Customers.GetPriceEffect() * 100)}%"),
            new TextRow($"Total customer demand: {Mathf.RoundToInt(Customers.GetCustomerDemand() * 100)}%"),
            new TextRow($"Next month costs: {teamsCost:C0}"),
            new TextRow($"Next month income: {nextMonthIncome:C0}"),
            new TextRow($"Next month balance: {(currentMoney - teamsCost + nextMonthIncome):C0}")
        });
    }

    private void UpdateFeatures()
    {
        featureColumn.Set(Company.Instance.GetAllFeatures().Select(feature => new FeatureRow(feature)).ToArray());
    }
}
