using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GDTV
{
    public class GridObject
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        private GridSystem gridSystem;
        private GridPosition gridPosition;
        private List<Unit> unitList;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public GridObject(GridSystem gridSystem, GridPosition gridPosition)
        {
            this.gridSystem = gridSystem;
            this.gridPosition = gridPosition;

            unitList = new List<Unit>();
        }

        public override string ToString()
        {
            string unitString = "";

            foreach (var unit in unitList)
            {
                unitString += unit + "\n";
            }

            return gridPosition.ToString() + "\n" + unitString;
        }

        public void AddUnit(Unit unit)
        {
            unitList.Add(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            unitList.Remove(unit);
        }

        public List<Unit> GetUnits()
        {
            return unitList;
        }

        // Private Methods //
    }
}
