using UnityEngine;

public class Customers : MonoBehaviour
{
    [Range(1, 1000)] public int priceThreshold = 100;
    [Range(1, 100)] public int priceThresholdMonthThreshold = 50;
    [Range(1, 100)] public int oldFeaturesThreshold = 18;
    [Range(0f, 1f)] public float qualityThreshold = 0.95f;
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
        set => _instance = value;
    }

    private void Start()
    {
        Company.CompanyMonthlyUpdate += OnCompanyMonthlyUpdate;
    }

    private void OnCompanyMonthlyUpdate()
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

    public float GetCustomerDemand()
    {
        return 1f + GetOldnessEffect() + GetPriceEffect() + GetQualityEffect();
    }

    public float GetOldnessEffect()
    {
        var ageInMonths = Company.Instance.GetFeatureCount() > 0
            ? Company.Instance.GetMonthsSinceLastNewFeature()
            : Game.Instance.GetCurrentMonth();
        var oldnessIndex = Mathf.Clamp((float) ageInMonths / oldFeaturesThreshold, 0f, 1f);
        return -(oldnessIndex * 0.5f) + 0.1f;
    }

    public float GetPriceEffect()
    {
        var featureCount = Company.Instance.GetFeatureCount();
        if (featureCount > 0)
        {
            var pricePerFeature = Company.Instance.productPrice / featureCount;
            var currentMonth = Game.Instance.GetCurrentMonth();
            var ageEffectToPricePerFeature =
                Mathf.Clamp((float) (priceThresholdMonthThreshold - currentMonth) / priceThresholdMonthThreshold, 0f,
                    1f) * 0.4f + 0.6f;
            var priceIndex = Mathf.Clamp(pricePerFeature / (priceThreshold * ageEffectToPricePerFeature), 0f, 1f);
            return -(priceIndex * 0.3f - 0.15f);
        }

        return 0f;
    }

    public float GetQualityEffect()
    {
        var featureCount = Company.Instance.GetFeatureCount();
        if (featureCount > 0)
        {
            var averageQuality = Company.Instance.GetAverageQuality();
            var qualityIndex = Mathf.Clamp(averageQuality / qualityThreshold, 0f, 1f);
            return qualityIndex * 0.4f - 0.2f;
        }

        return 0f;
    }

    public int GetCustomerCount()
    {
        return customers;
    }
}
