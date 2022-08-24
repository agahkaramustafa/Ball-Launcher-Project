using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallHandler : MonoBehaviour
{
    
    [SerializeField] private Rigidbody2D currentBallRigidbody;
    [SerializeField] private SpringJoint2D currentBallSprintJoint;

    private Camera mainCamera;
    private bool isDragging;
    [SerializeField] private float second;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBallRigidbody == null) { return; }

        if (!Touchscreen.current.primaryTouch.press.IsPressed())
        {
            if (isDragging)
            {
                LaunchBall();
            }

            isDragging = false;

            
        }

        else
        {
            isDragging = true;
            Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
            currentBallRigidbody.position = worldPosition;
            currentBallRigidbody.isKinematic = true;
        }
    }

    private void LaunchBall()
    {
        currentBallRigidbody.isKinematic = false;
        currentBallRigidbody = null;

        Invoke(nameof(DetachBall), second);

    }

    private void DetachBall()
    {
        currentBallSprintJoint.enabled = false;
        currentBallSprintJoint = null;
    }
}
