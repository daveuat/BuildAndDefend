using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    private Camera mainCamera;

    // Assign in Inspector (recommended). If left null, we'll try Resources.Load.
    [SerializeField] private BuildingTypeListSO buildingTypeList;

    private BuildingTypeSO buildingType;

    private void Start() // <-- must be capital S
    {
        mainCamera = Camera.main;

        // Fallback to Resources if not set in Inspector
        if (buildingTypeList == null)
        {
            buildingTypeList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        }

        if (buildingTypeList == null)
        {
            Debug.LogError("BuildingTypeListSO not found. Assign it in the Inspector, or place an asset named 'BuildingTypeListSO' in a Resources folder.");
            return;
        }

        if (buildingTypeList.list == null || buildingTypeList.list.Count == 0)
        {
            Debug.LogError("BuildingTypeListSO.list is empty.");
            return;
        }

        buildingType = buildingTypeList.list[0];
    }

    private void Update()
    {
        if (buildingTypeList == null || buildingType == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (buildingType.prefab == null)
            {
                Debug.LogError($"Prefab is missing on building type '{buildingType.name}'.");
            }
            else
            {
                Instantiate(buildingType.prefab, GetMouseWorldPosition(), Quaternion.identity);
            }
        }

        if (Input.GetKeyDown(KeyCode.T) && buildingTypeList.list.Count > 0)
        {
            buildingType = buildingTypeList.list[0];
        }
        if (Input.GetKeyDown(KeyCode.Y) && buildingTypeList.list.Count > 1)
        {
            buildingType = buildingTypeList.list[1];
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition); // <-- capital P
        mouseWorldPosition.z = 0f; // 2D
        return mouseWorldPosition;
    }
}
