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
            if (Vector3.Distance(transform.position, destination) < stoppingDistance)
            {
                transform.position = destination;

                return false;
            }
            else
            {
                Vector3 direction = destination - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

                transform.position += direction.normalized * speed * Time.deltaTime;
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

                return true;
            }
        }

        // Private Methods //
    }
}