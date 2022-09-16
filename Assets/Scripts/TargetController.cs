using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField] List<Unit> assignedUnits;
    List<string> assignedUnitNames;

    bool hasUnitsAssigned = false;
    bool isMoving = false;
    bool isIngameBoundaries = true;
    public bool IsMoving { get { return isMoving; } set { isMoving = value; } }
    public bool IsIngameBoundaries { get { return isIngameBoundaries; } set { isIngameBoundaries = value; } }
    Vector3 position;
    private void Start()
    {
        assignedUnits = new List<Unit>();
        assignedUnitNames = new List<string>();
    }
    private void Update()
    {
        position.x = Mathf.Clamp(transform.position.x, -100f, 100f);
        position.y = 6;
        position.z = Mathf.Clamp(transform.position.z, -100f, 100f);
        transform.position = position;
        if(assignedUnits != null)
        {
            hasUnitsAssigned = true;
        }

        if (hasUnitsAssigned && isMoving)
        {
            List<Vector3> targetPosList;
            targetPosList = GetPosListAround(transform.position, 15f, 12);
            int listIndex = 0;
            foreach (Unit unit in assignedUnits)
            {
                unit.SetDestination(targetPosList[listIndex]);
                listIndex = (listIndex + 1) % targetPosList.Count;
            }
        }
    }

    public void GetAssignedUnits(List<Unit> units, List<string> names)
    {
            foreach(Unit unit in units)
            {
                if (!assignedUnitNames.Contains(unit.name))
                    assignedUnits.Add(unit);
                else
                    continue;
            }

            foreach(string s in names)
            {
                if (!assignedUnitNames.Contains(s))
                    assignedUnitNames.Add(s);
                else
                    continue;
            }
    }
    public void ClearAssignedUnits()
    {
        assignedUnits.Clear();
        assignedUnitNames.Clear();
        hasUnitsAssigned = false;
    }
    #region Hedef noktasýnýn etrafýnda noktalar alabilmek için yazdýðýmýz fonksiyonlar
    private List<Vector3> GetPosListAround(Vector3 start, float distance, int posCount)
    {
        List<Vector3> posList = new List<Vector3>();
        for (int i = 0; i < posCount; i++)
        {
            float angle = i * (360f / posCount);
            Vector3 direction = ApplyRotation(new Vector3(1, 0,1), angle);
            Vector3 pos = start + direction * distance;
            posList.Add(pos);
        }
        return posList;
    }
    private Vector3 ApplyRotation(Vector3 vec, float angle)
    {
        return Quaternion.Euler(angle, 0, angle) * vec;
    }
    #endregion
}
