using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using XCOM.Grid;

namespace XCOM
{
    public class UnitController : MonoBehaviour
    {
        // Public Events //
        public event EventHandler<Unit> OnUnitSelected;

        // Public Enums //

        // Public Properties //

        // Protected Properties //
        protected GridSystem GridSystem
        {
            get
            {
                if (!_gridSystem) _gridSystem = FindObjectOfType<GridSystem>();
                return _gridSystem;
            }
        }

        // Private Properties //
        [SerializeField] private Unit unitPrefab;
        [SerializeField] private int nUnits;

        private List<Unit> units;
        private Unit selectedUnit;

        // Cached Components //

        // Cached References //
        private GridSystem _gridSystem;

        // Public Methods //
        public void CreateUnits()
        {
            units = new List<Unit>();

            for (int i = 0; i < nUnits; i++)
            {
                Vector3 nextPos = new Vector3(i * GridSystem.CellSize, 0f, 0f);

                CreateUnit(i, nextPos);
            }

            SelectUnit(units[0]);
        }

        public void SelectUnit(Unit selectedUnit)
        {
            this.selectedUnit = selectedUnit;

            OnUnitSelected?.Invoke(this, this.selectedUnit);
        }

        public void MoveSelectedUnit(GridObject destination)
        {
            GridPosition d = GridSystem.GetGridPosition(destination.transform.position);
            GridPosition unit = GridSystem.GetGridPosition(selectedUnit.transform.position);

            float distance = GridPosition.Distance(unit, d);

            if (distance > selectedUnit.MaxMoveDistance) return;

            StartCoroutine(UpdateTile(destination.transform.position));

            selectedUnit?.SetMoveDestination(destination.transform.position);
        }

        public void MoveSelectedUnit(Vector3 destination)
        {
            GridPosition d = GridSystem.GetGridPosition(destination);
            GridPosition unit = GridSystem.GetGridPosition(selectedUnit.transform.position);

            float distance = GridPosition.Distance(unit, d);

            if (distance > selectedUnit.MaxMoveDistance) return;

            StartCoroutine(UpdateTile(destination));

            selectedUnit?.SetMoveDestination(destination);
        }

        // Private Methods //
        private void CreateUnit(int id, Vector3 position)
        {
            Unit unit = Instantiate(unitPrefab, position, Quaternion.identity, transform);

            unit.name = $"Player Unit {id}";

            units.Add(unit);

            AddUnitToCurrentTile(unit);
        }

        private void AddUnitToCurrentTile(Unit unit)
        {
            GridObject gridObject = GridSystem.GetGridObject(unit.transform.position);

            gridObject.AddEntityToTile(unit);
        }

        private IEnumerator UpdateTile(Vector3 destination)
        {
            Vector3 start = selectedUnit.transform.position;

            while (!selectedUnit.HasReachedDestination(destination))
            {
                Vector3 current = selectedUnit.transform.position;

                GridObject before = GridSystem.GetGridObject(start);
                GridObject after = GridSystem.GetGridObject(current);

                if (before != after)
                {
                    before.RemoveEntityFromTile(selectedUnit);
                    AddUnitToCurrentTile(selectedUnit);

                    start = current;
                }

                yield return null;
            }
        }
    }
}
