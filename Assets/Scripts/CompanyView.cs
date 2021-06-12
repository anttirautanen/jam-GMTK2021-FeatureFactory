using UnityEngine;
using UnityEngine.UI;

public class CompanyView : MonoBehaviour
{
    public Transform featureViewPrefab;
    public Text moneyText;
    public Text teamsCostText;
    public Text nextMonthMoneyText;

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
        moneyText.text = "Money: " + currentMoney;
        teamsCostText.text = "Teams cost: " + (teamsCost > 0 ? "-" : "") + teamsCost;
        nextMonthMoneyText.text = "--> " + (currentMoney - teamsCost);
    }
}
