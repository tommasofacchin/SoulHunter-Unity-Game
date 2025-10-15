using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    private Vector2 targetPosition;

    public float rotationSpeed;

    private void Start()
    {
        Cursor.visible = false;
    }
    private void Update()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = targetPosition;
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
