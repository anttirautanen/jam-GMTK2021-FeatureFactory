using System.Collections.Generic;
using UnityEngine;

public class ColumnView : MonoBehaviour
{
    public Transform textRowPrefab;
    public Transform labelAndValueRowPrefab;
    public Transform featureRowPrefab;
    public Transform developerRowPrefab;
    public Transform separatorRowPrefab;
    private readonly List<Transform> previousInstances = new List<Transform>();

    public void Set(IEnumerable<IRow> rows)
    {
        previousInstances.ForEach(previousTextRow => Destroy(previousTextRow.gameObject));
        previousInstances.Clear();

        foreach (var row in rows)
        {
            var rowTransform = Instantiate(GetPrefab(row), transform);
            row.Instantiate(rowTransform);
            previousInstances.Add(rowTransform);
        }
    }

    private Transform GetPrefab(IRow row)
    {
        return row.GetRowType() switch
        {
            RowType.Text => textRowPrefab,
            RowType.LabelAndValue => labelAndValueRowPrefab,
            RowType.Feature => featureRowPrefab,
            RowType.Developer => developerRowPrefab,
            RowType.Separator => separatorRowPrefab,
            _ => null
        };
    }
}
