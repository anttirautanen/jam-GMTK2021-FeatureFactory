using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AvailableDevelopersView : MonoBehaviour
{
    public static event Action<Developer> OnChangeHighlightedDeveloper;
    public static event Action<Developer> OnHireDeveloper;

    public ColumnView availableDevelopersColumnView;
    private int selectedDeveloperIndex = 0;

    private void Start()
    {
        UpdateAvailableDevelopersList();
        SetSelectedDeveloperIndex(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetSelectedDeveloperIndex(selectedDeveloperIndex + 1);
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetSelectedDeveloperIndex(selectedDeveloperIndex - 1);
        }

        var selectedDeveloper = GetSelectedDeveloper();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selectedDeveloper != null)
            {
                OnHireDeveloper?.Invoke(selectedDeveloper);
            }

            SetSelectedDeveloperIndex(selectedDeveloperIndex);
            return;
        }

        UpdateAvailableDevelopersList();

        OnChangeHighlightedDeveloper?.Invoke(selectedDeveloper);
    }

    private void SetSelectedDeveloperIndex(int nextIndex)
    {
        var availableDevelopers = DeveloperMarket.Instance.GetAvailableDevelopers();
        if (availableDevelopers.Count == 0)
        {
            selectedDeveloperIndex = 0;
        }
        else
        {
            if (nextIndex >= availableDevelopers.Count)
            {
                selectedDeveloperIndex = 0;
            }
            else if (nextIndex < 0)
            {
                selectedDeveloperIndex = availableDevelopers.Count - 1;
            }
            else
            {
                selectedDeveloperIndex = nextIndex;
            }
        }
    }

    private void UpdateAvailableDevelopersList()
    {
        var availableDevelopers = DeveloperMarket.Instance.GetAvailableDevelopers();
        var availableDeveloperRows = new List<IRow>();
        for (var i = 0; i < availableDevelopers.Count; ++i)
        {
            var isSelected = selectedDeveloperIndex == i;
            availableDeveloperRows.Add(new DeveloperRow(availableDevelopers[i], isSelected));
        }

        availableDevelopersColumnView.Set(availableDeveloperRows);
    }

    private Developer GetSelectedDeveloper()
    {
        return DeveloperMarket.Instance.GetAvailableDevelopers().ElementAtOrDefault(selectedDeveloperIndex);
    }
}
