using UnityEngine;

public class Customers : MonoBehaviour
{
    private const int OldFeaturesThreshold = 18;
    private const int PriceThreshold = 50;
    private const float QualityThreshold = 0.95f;
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
        if (Company.Instance.GetFeatureCount() > 0)
        {
            var customerDemand = GetCustomerDemand();
            customers = Mathf.RoundToInt(customers * customerDemand);

            if (customers == 0 && customerDemand >= 1f)
            {
                customers = 10;
            }
        }
        else
        {
            customers = 0;
        }
    }

    public static float GetCustomerDemand()
    {
        return 1f + GetOldnessEffect() + GetPriceEffect() + GetQualityEffect();
    }

    public static float GetOldnessEffect()
    {
        var ageInMonths = Company.Instance.GetFeatureCount() > 0
            ? Company.Instance.GetMonthsSinceLastNewFeature()
            : Game.Instance.GetCurrentMonth();
        var oldnessIndex = Mathf.Clamp((float) ageInMonths / OldFeaturesThreshold, 0f, 1f);
        return -(oldnessIndex * 0.5f) + 0.1f;
    }

    public static float GetPriceEffect()
    {
        var featureCount = Company.Instance.GetFeatureCount();
        if (featureCount > 0)
        {
            var pricePerFeature = Company.Instance.productPrice / featureCount;

            var priceIndex = Mathf.Clamp((float) pricePerFeature / PriceThreshold, 0f, 1f);
            return -(priceIndex * 0.3f - 0.15f);
        }

        return 0f;
    }

    public static float GetQualityEffect()
    {
        var featureCount = Company.Instance.GetFeatureCount();
        if (featureCount > 0)
        {
            var averageQuality = Company.Instance.GetAverageQuality();
            var qualityIndex = Mathf.Clamp(averageQuality / QualityThreshold, 0f, 1f);
            return qualityIndex * 0.4f - 0.2f;
        }

        return 0f;
    }

    public int GetCustomerCount()
    {
        return customers;
    }
}
