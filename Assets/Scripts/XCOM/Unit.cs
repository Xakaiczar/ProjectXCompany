using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace XCOM
{
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

        // Private Methods //
        private void Update()
        {
            Mover.Move(moveLocation);
        }
    }
}
