using System;
using System.Collections;
using System.Collections.Generic;
using GDTV;
using UnityEngine;

namespace GDTV
{
    public class UnitActionSystem : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitLayer;

        // Cached Components //

        // Cached References //

        // Public Methods //

        // Private Methods //
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                bool unitSelected = TryHandleUnitSelection();

                if (unitSelected) return;

                selectedUnit.Move(MouseWorld.GetPosition());
            }
        }

        private bool TryHandleUnitSelection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hasHit = Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayer);

            if (!hasHit) return false;

            bool isUnit = raycastHit.transform.TryGetComponent<Unit>(out Unit unit);

            if (isUnit) selectedUnit = unit;

            return isUnit;
        }
    }
}