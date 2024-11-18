using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    void Update()
    {
        // Get the mouse position in world space, with the same Z as the object
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the direction from the object to the mouse position
        Vector3 direction = mousePosition - transform.position;
        
        // Calculate the angle from the direction vector
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        // Apply the rotation to the object (z-axis only)
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}