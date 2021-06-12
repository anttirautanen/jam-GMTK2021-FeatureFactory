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
        var customerDemand = GetCustomerDemand();
        customers = Mathf.RoundToInt(customers * customerDemand);

        if (customers == 0 && customerDemand >= 1f)
        {
            customers = 10;
        }
    }

    public static float GetCustomerDemand()
    {
        if (Company.Instance.GetMonthsSinceLastNewFeature() > OldFeaturesThreshold)
        {
            return 0;
        }

        return 1f + GetOldnessEffect() + GetPriceEffect() + GetQualityEffect();
    }

    public static float GetOldnessEffect()
    {
        var monthsSinceLastNewFeature = Company.Instance.GetMonthsSinceLastNewFeature();
        var oldnessIndex = Mathf.Clamp((float) monthsSinceLastNewFeature / OldFeaturesThreshold, 0f, 1f);
        return -(oldnessIndex * 0.5f) + 0.1f;
    }

    public static float GetPriceEffect()
    {
        var pricePerFeature = Company.Instance.productPrice / Company.Instance.GetFeatureCount();
        var priceIndex = Mathf.Clamp((float) pricePerFeature / PriceThreshold, 0f, 1f);
        return -(priceIndex * 0.3f - 0.15f);
    }

    public static float GetQualityEffect()
    {
        var averageQuality = Company.Instance.GetAverageQuality();
        var qualityIndex = Mathf.Clamp(averageQuality / QualityThreshold, 0f, 1f);
        return qualityIndex * 0.4f - 0.2f;
    }

    public int GetCustomerCount()
    {
        return customers;
    }
}
