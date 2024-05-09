using System;
using System.Collections;
using System.Collections.Generic;

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

        // Cached Components //

        // Cached References //

        // Public Methods //

        // Private Methods //
        private void Update()
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
    }
}
