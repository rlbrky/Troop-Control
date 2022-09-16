using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    Plane plane = new Plane(Vector3.up, -3);
    List<Unit> selectedUnits;
    List<string> selectedUnitNames;
    LayerMask mask;
    Transform target;
    TargetController targetController;
    string unitType;
    int unitCount;
    public string UnitType { get { return unitType; }}
    public int UnitCount { get { return unitCount; }}

    private void Awake()
    {
        selectedUnits = new List<Unit>();
        selectedUnitNames = new List<string>();
        mask = LayerMask.GetMask("Unit");
    }
    private void Update()
    {
        #region Farenin Sol týký ile asker seçimi
        if (Input.GetMouseButtonDown(0))
        {
            startPos = GetMouseInput();
        }

        if (Input.GetMouseButtonUp(0))
        {
            endPos = GetMouseInput();

            float distanceBetweenCoordinates = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(startPos.x) - Mathf.Abs(endPos.x), 2) + Mathf.Pow(Mathf.Abs(startPos.z) - Mathf.Abs(endPos.z), 2));
            //Hata var çizilen þey seçilen iki nokta arasýna deðil girilen uzunluklara göre ortadan çiziliyor, gereðinden fazla seçme veya seçememe gibi sorunlar oluþacaktýr.
            Collider[] colliderArray = Physics.OverlapBox((startPos+endPos)/2,new Vector3(distanceBetweenCoordinates/2,1.5f, distanceBetweenCoordinates/2), Quaternion.identity, mask);


            if (!Input.GetKey(KeyCode.LeftShift))
            {
                foreach (Unit unit in selectedUnits)
                {
                    unit.SetSelectedVisibility(false);
                }
                selectedUnits.Clear();
                selectedUnitNames.Clear();
            }
           foreach (Collider collider in colliderArray)
           {
               Unit unit = collider.GetComponent<Unit>();
               if (unit != null)
               {
                   unit.SetSelectedVisibility(true);
                   selectedUnits.Add(unit);
                   selectedUnitNames.Add(unit.name);
                    unitType = unit.tag;
                    unitCount = selectedUnits.Count;
               }
           }
        }
        #endregion

        /*#region Farenin Sað týký ile seçili objelere yürüyecekleri noktayý vermek.
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 moveToPos = GetMouseInput();
            List<Vector3> moveToPosList = GetPosListAround(moveToPos, 10f, 12);
            int listIndex = 0;
            foreach(Unit unit in selectedUnits)
            {
                unit.SetDestination(moveToPosList[listIndex]);
                //Index listeyi geçmesin diye modunu alýyoruz.
                listIndex = (listIndex + 1) % moveToPosList.Count;
            }
        }
        #endregion*/
        #region Hedeflerin hareket ettirilmesi
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit) && hit.transform.tag == "Target")
            {
                targetController = hit.transform.GetComponent<TargetController>();
                hit.transform.position = GetMouseInput();
                if (targetController != null)
                    targetController.IsMoving = true;
            }
        }
        if (Input.GetMouseButtonUp(1) && targetController != null)
        {
            targetController.IsMoving = false;
        }
        #endregion

        #region Seçili askerlerin hedef objelere doðru yürümesi
        if (Input.GetMouseButtonDown(2))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit) &&  hit.transform.tag == "Target")
            {
                target = hit.transform;
                targetController = hit.transform.GetComponent<TargetController>();
                targetController.GetAssignedUnits(selectedUnits, selectedUnitNames);

                List<Vector3> targetPosList;
                targetPosList = GetPosListAround(target.position, 15f, 12);
                int listIndex = 0;
                foreach (Unit unit in selectedUnits)
                {
                    unit.SetDestination(targetPosList[listIndex]);
                    listIndex = (listIndex + 1) % targetPosList.Count;
                }
            }
        }
        #endregion
        //Update sonu
    }

    //Belli bir uzaklýkta sahip olunan obje kadar nokta belirliyoruz ki objelerimiz üst üste gelmesin.
    private List<Vector3> GetPosListAround(Vector3 start, float distance, int posCount)
    {
        List<Vector3> posList = new List<Vector3>();
        for(int i = 0; i < posCount; i++)
        {
            float angle = i * (360f / posCount);
            Vector3 direction = ApplyRotation(new Vector3(1, 0,1 ), angle);
            Vector3 pos = start + direction * distance;
            posList.Add(pos);
        }
        return posList;
    }
    private Vector3 ApplyRotation(Vector3 vec, float angle)
    {
        return Quaternion.Euler(angle, 0, angle) * vec;
    }

    //Kod tekrarý yapmamak için fonksiyon yazdýk.
    Vector3 GetMouseInput()
    {
        float distance;
        Vector3 coordinates = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out distance))
        {
            coordinates = ray.GetPoint(distance);
        }
        return coordinates;
    }
    //Tüm birimlerin seçilmesi
    public void SelectAllUnits()
    {
        Unit[] units = FindObjectsOfType<Unit>();
        foreach (Unit unit in units)
        {
            unit.SetSelectedVisibility(true);
            selectedUnits.Add(unit);
            selectedUnitNames.Add(unit.name);
            unitType = unit.tag;
            unitCount = selectedUnits.Count;
        }
    }

    private void OnDrawGizmos()
    {
        float distanceBetweenCoordinates = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(startPos.x) - Mathf.Abs(endPos.x), 2) + Mathf.Pow(Mathf.Abs(startPos.z) - Mathf.Abs(endPos.z), 2));
        Gizmos.color = Color.green;
        Gizmos.DrawCube((startPos + endPos) / 2, new Vector3(distanceBetweenCoordinates / 2, 1.5f, distanceBetweenCoordinates / 2));
    }
}
