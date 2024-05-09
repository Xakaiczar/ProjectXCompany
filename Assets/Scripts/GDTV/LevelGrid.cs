using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GDTV
{
    public class LevelGrid : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //
        public static LevelGrid Instance { get; private set; }

        // Protected Properties //

        // Private Properties //
        [SerializeField] private Transform gridDebugObjectPrefab;

        private GridSystem gridSystem;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

        public void AddUnitToGridPosition(GridPosition gridPosition, Unit unit)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);

            gridObject.AddUnit(unit);
        }

        public List<Unit> GetUnitsAtGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);

            return gridObject.GetUnits();
        }

        public void RemoveUnitFromGridPosition(GridPosition gridPosition, Unit unit)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);

            gridObject.RemoveUnit(unit);
        }

        public void UnitMovedGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
        {
            RemoveUnitFromGridPosition(fromGridPosition, unit);
            AddUnitToGridPosition(toGridPosition, unit);
        }

        // Private Methods //
        private void Awake()
        {
            gridSystem = new GridSystem(10, 10, 2f);
            gridSystem.CreateDebugObjects(gridDebugObjectPrefab);

            if (Instance != null)
            {
                Debug.Log("There's more than one UAS!");

                Destroy(gameObject);

                return;
            }

            Instance = this;
        }
    }
}
