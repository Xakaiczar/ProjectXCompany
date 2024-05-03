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
        [SerializeField] private Animator unitAnimator;

        private Vector3 targetPosition;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }

        // Private Methods //
        private void Awake()
        {
            targetPosition = transform.position;
        }
        
        private void Update()
        {
            float stoppingDistance = 0.1f;

            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                float moveSpeed = 4f;
                float rotateSpeed = 10f;

                transform.position += moveDirection * moveSpeed * Time.deltaTime;

                transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);

                unitAnimator.SetBool("isMoving", true);
            }
            else
            {
                unitAnimator.SetBool("isMoving", false);
            }
        }
    }
}