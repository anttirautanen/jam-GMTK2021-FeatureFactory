using UnityEngine;

public class Customers : MonoBehaviour
{
    private const float SatisfiesCustomerNeedThreshold = 0.75f;
    private const float QualityThreshold = 0.5f;
    private const int MarketPotentialThreshold = 1000000;
    private int customers = 0;
    private static Customers _instance;

    public static Customers Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.Find("Customers").GetComponent<Customers>();
            }

            return _instance;
        }
    }

    private void Start()
    {
        Company.CompanyUpdated += OnCompanyUpdated;
    }

    private void OnCompanyUpdated()
    {
        var satisfiesCustomerNeed = Company.Instance.GetFirstFeature().SatisfiesCustomerNeed;
        if (satisfiesCustomerNeed < SatisfiesCustomerNeedThreshold)
        {
            var nextCustomers = customers - Mathf.RoundToInt(Mathf.Max(100, customers * 0.1f));
            customers = nextCustomers < 0 ? 0 : nextCustomers;
        }
        else
        {
            var marketPotential = Mathf.Max(customers, 200);
            if (marketPotential > MarketPotentialThreshold)
            {
                var marketPotentialExceedingThreshold = marketPotential - MarketPotentialThreshold;
                marketPotential -= Mathf.RoundToInt(marketPotentialExceedingThreshold * 0.1f);
            }

            customers +=
                Mathf.RoundToInt((satisfiesCustomerNeed - SatisfiesCustomerNeedThreshold + 1f) * marketPotential);
        }
    }

    public int GetCustomerCount()
    {
        return customers;
    }
}
