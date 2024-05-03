using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GDTV
{
    public class UnitSelectedVisual : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        [SerializeField] private Unit unit;

        // Cached Components //
        private MeshRenderer meshRenderer;

        // Cached References //

        // Public Methods //

        // Private Methods //
        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;

            UpdateVisual();
        }

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs empty)
        {
            UpdateVisual();
        }

        private void UpdateVisual()
        {
            bool isSelectedUnit = UnitActionSystem.Instance.GetSelectedUnit() == unit;

            meshRenderer.enabled = isSelectedUnit;
        }
    }
}