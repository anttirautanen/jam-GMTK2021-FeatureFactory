using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColumnView : MonoBehaviour
{
    public Transform textRowPrefab;
    private readonly List<Transform> previousInstances = new List<Transform>();

    public void Set(IEnumerable<TextRow> rows)
    {
        previousInstances.ForEach(previousTextRow => Destroy(previousTextRow.gameObject));
        previousInstances.Clear();

        foreach (var row in rows)
        {
            var textRowTransform = Instantiate(textRowPrefab, transform);
            previousInstances.Add(textRowTransform);
            textRowTransform.GetComponent<Text>().text = row.Text;
        }
    }
}
