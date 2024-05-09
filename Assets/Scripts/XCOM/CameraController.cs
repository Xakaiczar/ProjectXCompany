using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace XCOM
{
    public class CameraController : MonoBehaviour
    {
        // Public Events //

        // Public Enums //

        // Public Properties //

        // Protected Properties //

        // Private Properties //
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotateSpeed;

        // Cached Components //

        // Cached References //

        // Public Methods //
        public void MoveCamera(Vector3 moveVector)
        {
            Vector3 x = transform.right * moveVector.x;
            Vector3 y = transform.up * moveVector.y;
            Vector3 z = transform.forward * moveVector.z;
            Vector3 v = x + y + z;

            transform.position += v * moveSpeed * Time.deltaTime;
        }

        public void RotateCamera(Vector3 rotationVector)
        {
            transform.Rotate(rotationVector * rotateSpeed * Time.deltaTime);
        }

        // Private Methods //
    }
}