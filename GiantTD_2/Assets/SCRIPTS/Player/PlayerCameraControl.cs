using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControl : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float mouseSensitivity = 3.5f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0f;

    void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    void Update()
    {
        UpdateMouseLook();
    }

    void UpdateMouseLook()
    {
        // Get Mouse position
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"),
                                        Input.GetAxis("Mouse Y"));
        // Move the Camera left to right
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);

        
        // Move the Camera up and down
        cameraPitch -= mouseDelta.y * mouseSensitivity;
        // Stop the Camera from rolling over
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;





    }
}
