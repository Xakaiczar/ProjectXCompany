using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace XCOM
{
    public class Player : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        [SerializeField] private GameObject cursor;
        [SerializeField] private LayerMask floorLayer;
        [SerializeField] private LayerMask unitLayer;
        [SerializeField] private Unit unitPrefab;
        [SerializeField] private int nUnits;

        private List<Unit> units;
        private Unit selectedUnit;

        // Cached Components //

        // Cached References //

        // Public Methods //

        // Private Methods //
        private void Start()
        {
            units = new List<Unit>();

            for (int i = 0; i < nUnits; i++)
            {
                Vector3 nextPos = new Vector3(i * 2f, 0f, 0f);
                Unit unit = Instantiate(unitPrefab, nextPos, Quaternion.identity, transform);

                units.Add(unit);
            }

            SelectUnit(units[0]);
        }

        private void Update()
        {
            cursor.transform.position = GetHitLocation();
            HandleClickEvent();
        }

        private Vector3 GetHitLocation()
        {
            RaycastHit hit = GetHitOnLayer(floorLayer);

            return hit.point;
        }

        private RaycastHit GetHitOnLayer(LayerMask layer)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layer);

            return hit;
        }

        private void GetMultipleHits()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRayFromMousePosition());

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    Debug.Log(hits[i].transform.name);
                }
            }
        }

        private Ray GetRayFromMousePosition()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private void HandleClickEvent()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TrySelectUnit();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                selectedUnit.SetMoveDestination(GetHitLocation());
            }
        }

        private void TrySelectUnit()
        {
            Transform hitTransform = GetHitOnLayer(unitLayer).transform;
            Unit clickedUnit = hitTransform?.GetComponent<Unit>();

            if (clickedUnit)
            {
                SelectUnit(clickedUnit);
            }
        }

        private void SelectUnit(Unit selectedUnit)
        {
            this.selectedUnit = selectedUnit;

            foreach (Unit unit in units)
            {
                unit.ToggleSelectedDisplay(unit == selectedUnit);
            }
        }
    }
}