using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GDTV
{
    public class Unit : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        private GridPosition gridPosition;

        // Cached Components //
        private MoveAction moveAction;

        // Cached References //

        // Public Methods //
        public MoveAction GetMoveAction()
        {
            return moveAction;
        }

        public GridPosition GetGridPosition()
        {
            return gridPosition;
        }

        // Private Methods //
        private void Awake()
        {
            moveAction = GetComponent<MoveAction>();
        }

        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

            LevelGrid.Instance.AddUnitToGridPosition(gridPosition, this);
        }
        
        private void Update()
        {
            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

            if (newGridPosition != gridPosition)
            {
                LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);

                gridPosition = newGridPosition;
            }
        }
    }
}