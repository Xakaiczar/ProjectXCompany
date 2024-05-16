using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace GDTV
{
    public class MoveAction : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        [SerializeField] int maxMoveDistance = 4;

        private Vector3 targetPosition;

        // Cached Components //
        [SerializeField] private Animator unitAnimator;

        private Unit unit;

        // Cached References //

        // Public Methods //
        public void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }

        public List<GridPosition> GetValidActionGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    Debug.Log(testGridPosition);
                }
            }

            return validGridPositionList;
        }

        // Private Methods //
        private void Awake()
        {
            unit = GetComponent<Unit>();

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
