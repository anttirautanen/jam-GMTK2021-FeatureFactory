using UnityEngine;

public class CompanyView : MonoBehaviour
{
    public ColumnView moneyColumn;

    private void Start()
    {
        Company.CompanyStatsUpdated += UpdateMoneyColumn;
        TeamView.OnTeamUpdated += UpdateMoneyColumn;
        DevTeam.TeamUpdated += UpdateMoneyColumn;

        UpdateMoneyColumn();
        KeyHelp.Instance.Set(new[] {"(F)eatures", "(Q/E) Change product price", "(Shift+Enter) Next month"});
    }

    private void OnDestroy()
    {
        Company.CompanyStatsUpdated -= UpdateMoneyColumn;
        TeamView.OnTeamUpdated -= UpdateMoneyColumn;
        DevTeam.TeamUpdated -= UpdateMoneyColumn;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Company.Instance.SetProductPrice(Company.Instance.productPrice + 10);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Company.Instance.SetProductPrice(Company.Instance.productPrice - 10);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            UiController.Instance.OpenView(View.Features);
        }
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
            new LabelAndValueRow("Income", $"+ {nextMonthIncome:C0}", TextRowStyle.Positive),
            new LabelAndValueRow("Expenses", $"- {teamsCost:C0}", TextRowStyle.Negative),
            new LabelAndValueRow("Next month balance", $"{(currentMoney - teamsCost + nextMonthIncome):C0}"),
            new SeparatorRow(),
            new LabelAndValueRow("Customers", customerCount.ToString()),
            new LabelAndValueRow("Product price", $"{productPrice:C0}"),
            new LabelAndValueRow("Income", $"{nextMonthIncome:C0}"),
            new SeparatorRow(),
            new LabelAndValueRow("Customer demand",
                $"{Mathf.RoundToInt(Customers.Instance.GetCustomerDemand() * 100)}%"),
            new LabelAndValueRow("Age effect", $"{Mathf.RoundToInt(Customers.Instance.GetOldnessEffect() * 100)}%",
                TextRowStyle.Secondary),
            new LabelAndValueRow("Quality effect", $"{Mathf.RoundToInt(Customers.Instance.GetQualityEffect() * 100)}%",
                TextRowStyle.Secondary),
            new LabelAndValueRow("Price effect", $"{Mathf.RoundToInt(Customers.Instance.GetPriceEffect() * 100)}%",
                TextRowStyle.Secondary)
        });
    }
}
