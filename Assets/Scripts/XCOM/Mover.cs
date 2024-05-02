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
        public void MoveTowards(Vector3 destination)
        {
            if (Vector3.Distance(transform.position, destination) < stoppingDistance)
            {
                transform.position = destination;
            }
            else
            {
                Vector3 direction = destination - transform.position;
                transform.position += direction.normalized * speed * Time.deltaTime;
            }
        }

        // Private Methods //
    }
}