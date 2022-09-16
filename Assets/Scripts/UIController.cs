using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Text uiUnitType;
    [SerializeField] Text uiUnitCount;
    [SerializeField] Text uiSpeed;
    [SerializeField] Text uiThreshold;
    [SerializeField] Slider speedSlider;
    [SerializeField] Slider thresholdSlider;
    SelectionHandler selectionHandler;
    [SerializeField]List<Unit> units;
    // Start is called before the first frame update
    void Start()
    {
        selectionHandler = FindObjectOfType<SelectionHandler>();
        units = new List<Unit>();
        foreach(Unit unit in FindObjectsOfType<Unit>())
        {
            units.Add(unit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        uiUnitType.text = selectionHandler.UnitType;
        uiUnitCount.text = selectionHandler.UnitCount.ToString();
        foreach(Unit unit in units)
        {
            unit.Speed = speedSlider.value;
            unit.DistanceToDestination = thresholdSlider.value;
        }
    }

    public void UpdateUiSpeed()
    {
        uiSpeed.text = speedSlider.value.ToString();
    }

    public void UpdateUiThreshold()
    {
        uiThreshold.text = thresholdSlider.value.ToString();
    }
}
