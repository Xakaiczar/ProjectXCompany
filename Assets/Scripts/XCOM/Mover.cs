using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace XCOM
{
    public class Mover : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        [SerializeField] private float speed;
        [SerializeField] private float accuracyTolerance;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public void Move(Vector3 newLocation)
        {
            if (Vector3.Distance(transform.position, newLocation) < accuracyTolerance)
            {
                transform.position = newLocation;
            }
            else
            {
                Vector3 direction = newLocation - transform.position;
                transform.position += direction.normalized * speed * Time.deltaTime;
            }
        }

        // Private Methods //
    }
}