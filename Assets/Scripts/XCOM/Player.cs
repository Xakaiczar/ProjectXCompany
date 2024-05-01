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
        [SerializeField] private Unit unit;

        // Cached Components //

        // Cached References //

        // Public Methods //

        // Private Methods //
        private void Update()
        {
            cursor.transform.position = GetHitLocation();
            MoveUnit();
        }

        private Vector3 GetHitLocation()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, floorLayer);

            return hit.point;
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

        private void MoveUnit()
        {
            if (Input.GetMouseButtonDown(0))
            {
                unit.SetMoveLocation(GetHitLocation());
            }
        }
    }
}