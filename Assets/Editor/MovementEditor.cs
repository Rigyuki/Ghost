using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Movement = MovementController.Movement;

[CustomEditor(typeof(MovementController)), CanEditMultipleObjects]
public class MovementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        MovementController movementController = (MovementController)target;
        if (GUILayout.Button("Add Key Frame"))
        {
            movementController.movements.Add(AddMovement(movementController));
            movementController.movements = movementController.movements;
        }
    }

    public Movement AddMovement(MovementController movementController)
    {
        Movement movement = new Movement();
        movement.isWorldSpace = true;
        movement.curve = movementController.curve;
        if (movementController.movements.Count == 0)
        {
            movement.origin = movementController.transform.position;
            movement.destination = movementController.transform.position;
        }
        else
        {
            Movement last = movementController.movements[movementController.movements.Count - 1];
            last.destination = movementController.transform.position;
            movementController.movements[movementController.movements.Count - 1] = last;

            if (last.isWorldSpace)
                movement.origin = last.destination;
            else
                Debug.LogWarning("必须手动调整起始位置");
            movement.destination = movementController.transform.position;
        }
        return movement;
    }
}
