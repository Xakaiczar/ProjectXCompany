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
        protected Mover Mover
        {
            get
            {
                if (!_mover) _mover = GetComponent<Mover>();
                return _mover;
            }
        }


        // Private Properties //
        [SerializeField] private Vector3 moveLocation;

        // Cached Components //
        private Mover _mover;

        // Cached References //

        // Public Methods //
        public void SetMoveLocation(Vector3 moveLocation)
        {
            this.moveLocation = moveLocation;
        }

        // Private Methods //
        private void Update()
        {
            Mover.MoveTowards(moveLocation);
        }
    }
}
