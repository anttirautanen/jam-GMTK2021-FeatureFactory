using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MarketView : LayoutGroup
{
    public static event Action<Developer> OnChangeHighlightedDeveloper;
    public static event Action<Developer> OnHireDeveloper;

    public DeveloperMarket developerMarket;
    public RectTransform hireDeveloperRow;
    private List<Developer> availableDevelopersCache = new List<Developer>();
    private readonly List<HireDeveloperRow> hireDeveloperRows = new List<HireDeveloperRow>();
    private int selectedDeveloperRowIndex = 0;

    private void Update()
    {
        var availableDevelopers = developerMarket.GetAvailableDevelopers();
        if (IsAvailableDevelopersListChanged(availableDevelopers))
        {
            hireDeveloperRows.ForEach(rowTransform => Destroy(rowTransform.gameObject));
            hireDeveloperRows.Clear();
            availableDevelopersCache = availableDevelopers;

            availableDevelopers.ForEach(developer =>
            {
                var hireDeveloperRowTransform = Instantiate(hireDeveloperRow, transform);
                var hireDeveloperRowInstance = hireDeveloperRowTransform.GetComponent<HireDeveloperRow>();
                hireDeveloperRowInstance.SetDeveloper(developer);
                hireDeveloperRows.Add(hireDeveloperRowInstance);
            });

            SetSelectedDeveloperIndex(0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SetSelectedDeveloperIndex(selectedDeveloperRowIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            SetSelectedDeveloperIndex(selectedDeveloperRowIndex - 1);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!hireDeveloperRows[selectedDeveloperRowIndex].isHired)
            {
                OnHireDeveloper?.Invoke(availableDevelopersCache[selectedDeveloperRowIndex]);
                hireDeveloperRows[selectedDeveloperRowIndex].SetIsHired();
            }
        }
    }

    private bool IsAvailableDevelopersListChanged(List<Developer> availableDevelopers)
    {
        var isSameSize = availableDevelopers.Count == availableDevelopersCache.Count;
        if (!isSameSize)
        {
            return true;
        }

        return availableDevelopers.Any(developer => !availableDevelopersCache.Contains(developer));
    }

    private void SetSelectedDeveloperIndex(int nextIndex)
    {
        hireDeveloperRows[selectedDeveloperRowIndex].SetIsHighlighted(false);

        if (nextIndex >= hireDeveloperRows.Count)
        {
            selectedDeveloperRowIndex = 0;
        }
        else if (nextIndex < 0)
        {
            selectedDeveloperRowIndex = hireDeveloperRows.Count - 1;
        }
        else
        {
            selectedDeveloperRowIndex = nextIndex;
        }

        hireDeveloperRows[selectedDeveloperRowIndex].SetIsHighlighted(true);

        OnChangeHighlightedDeveloper?.Invoke(hireDeveloperRows[selectedDeveloperRowIndex].isHired
            ? null
            : availableDevelopersCache[selectedDeveloperRowIndex]);
    }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        var parentWidth = rectTransform.rect.width;
        var previousRowHeights = 0.0f;

        foreach (var child in rectChildren)
        {
            SetChildAlongAxis(child, 0, 0, parentWidth);
            SetChildAlongAxis(child, 1, previousRowHeights);
            previousRowHeights += child.rect.height;
        }
    }

    public override void CalculateLayoutInputVertical()
    {
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }
}
