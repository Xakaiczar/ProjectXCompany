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
        [SerializeField] private Unit selectedUnit;

        // Cached Components //

        // Cached References //

        // Public Methods //

        // Private Methods //
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
                Transform hitTransform = GetHitOnLayer(unitLayer).transform;
                Unit clickedUnit = hitTransform?.GetComponent<Unit>();

                if (clickedUnit) selectedUnit = clickedUnit;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                selectedUnit.SetMoveDestination(GetHitLocation());
            }
        }
    }
}