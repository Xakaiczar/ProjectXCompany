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
        private Vector3 targetPosition;

        // Cached Components //

        // Cached References //

        // Public Methods //

        // Private Methods //
        private void Update()
        {
            float stoppingDistance = 0.1f;

            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                float moveSpeed = 4f;

                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Move(MouseWorld.GetPosition());
            }
        }

        private void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }
    }
}