using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace GDTV
{
    public class CameraController : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        private const float MIN_FOLLOW_Y = 2f;
        private const float MAX_FOLLOW_Y = 12f;

        // Cached Components //
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
        private CinemachineTransposer cinemachineTransposer;


        private Vector3 targetFollowOffset;


        // Cached References //

        // Public Methods //

        // Private Methods //
        private void Start()
        {
            cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();

            targetFollowOffset = cinemachineTransposer.m_FollowOffset;
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleZoom();
        }

        private void HandleMovement()
        {
            Vector3 inputMoveDirection = Vector3.zero;

            if (Input.GetKey(KeyCode.W))
            {
                inputMoveDirection.z += 1f;
            }

            if (Input.GetKey(KeyCode.S))
            {
                inputMoveDirection.z -= 1f;
            }

            if (Input.GetKey(KeyCode.A))
            {
                inputMoveDirection.x -= 1f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                inputMoveDirection.x += 1f;
            }

            float moveSpeed = 10f;
            Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;

            transform.position += moveVector * moveSpeed * Time.deltaTime;
        }

        private void HandleRotation()
        {
            Vector3 rotationVector = Vector3.zero;

            if (Input.GetKey(KeyCode.Q))
            {
                rotationVector.y += 1f;
            }

            if (Input.GetKey(KeyCode.E))
            {
                rotationVector.y -= 1f;
            }

            float rotationSpeed = 100f;
            transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
        }

        private void HandleZoom()
        {
            float zoomAmount = 1f;

            if (Input.mouseScrollDelta.y > 0)
            {
                targetFollowOffset.y -= zoomAmount;
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                targetFollowOffset.y += zoomAmount;
            }

            targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y, MAX_FOLLOW_Y);

            float zoomSpeed = 5f;
            cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, zoomSpeed * Time.deltaTime);
        }
    }
}
