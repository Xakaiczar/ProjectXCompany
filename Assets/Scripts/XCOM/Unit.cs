using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace XCOM
{
    [RequireComponent(typeof(Mover))]
    public class Unit : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //
        protected Model Model
        {
            get
            {
                if (!_model) _model = GetComponentInChildren<Model>();
                return _model;
            }
        }

        protected Mover Mover
        {
            get
            {
                if (!_mover) _mover = GetComponent<Mover>();
                return _mover;
            }
        }

        protected UISelectedUnit UISelected
        {
            get
            {
                if (!_uiSelected) _uiSelected = GetComponentInChildren<UISelectedUnit>();
                return _uiSelected;
            }
        }

        // Private Properties //
        private Vector3 moveDestination;

        // Cached Components //
        private Model _model;
        private Mover _mover;
        private UISelectedUnit _uiSelected;

        // Cached References //

        // Public Methods //
        public void ToggleSelectedDisplay(bool isEnabled) => UISelected.ToggleDisplay(isEnabled);

        public void SetMoveDestination(Vector3 newDestination)
        {
            moveDestination = newDestination;
        }

        // Private Methods //
        private void Awake()
        {
            moveDestination = transform.position;
        }

        private void Update()
        {
            bool isMoving = Mover.MoveTowards(moveDestination);
            Vector3 direction = Mover.GetMoveDirection(moveDestination);

            Model.UpdateMoveAnimation(isMoving);

            Model.RotateModel(direction);
        }
    }
}
