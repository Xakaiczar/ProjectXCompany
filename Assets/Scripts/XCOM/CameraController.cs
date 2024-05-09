using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

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
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    public void RotateCamera(Vector3 rotationVector)
    {
        transform.Rotate(rotationVector * rotateSpeed * Time.deltaTime);
    }

    // Private Methods //
}