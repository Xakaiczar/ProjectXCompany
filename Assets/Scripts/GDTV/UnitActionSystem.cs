using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GDTV
{
    public class UnitActionSystem : MonoBehaviour
    {
        // Public Events //
        public event EventHandler OnSelectedUnitChanged;

        // Public Enums //

        // Public Properties //
        public static UnitActionSystem Instance { get; private set; }

        // Protected Properties //

        // Private Properties //
        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitLayer;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public Unit GetSelectedUnit()
        {
            return selectedUnit;
        }

        // Private Methods //
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.Log("There's more than one UAS!");

                Destroy(gameObject);
                
                return;
            }

            Instance = this;
        }

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

            bool isUnit = raycastHit.transform.TryGetComponent(out Unit unit);

            if (isUnit) SetSelectedUnit(unit);

            return isUnit;
        }

        private void SetSelectedUnit(Unit unit)
        {
            selectedUnit = unit;

            OnSelectedUnitChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}