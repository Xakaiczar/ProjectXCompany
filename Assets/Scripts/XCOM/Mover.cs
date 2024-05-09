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
        [SerializeField] private float stoppingDistance;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public bool MoveTowards(Vector3 destination)
        {
            if (HasReachedDestination(destination))
            {
                transform.position = destination;

                return false;
            }
            else
            {
                Vector3 direction = GetMoveDirection(destination);

                transform.position += direction.normalized * speed * Time.deltaTime;

                return true;
            }
        }

        public bool HasReachedDestination(Vector3 destination)
        {
            return Vector3.Distance(transform.position, destination) < stoppingDistance;
        }

        public Vector3 GetMoveDirection(Vector3 destination)
        {
            return destination - transform.position;
        }

        // Private Methods //
    }
}