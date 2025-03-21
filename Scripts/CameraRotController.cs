using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class CameraRotController : MonoBehaviour
{
    [SerializeField] private float rotSpeed; //velocidade de rotação da camera
    private float yaw = 0f;
    private float pitch = 0f;
    private Vector3 angle;

    void Start()
    {
        angle = transform.eulerAngles;
        pitch = angle.x;
        yaw = angle.y;
    }
    void Update()
    {
         if(Mouse.current.rightButton.isPressed)
         {
            float mouseX = Mouse.current.delta.x.ReadValue();
            float mouseY = Mouse.current.delta.y.ReadValue();

            yaw += mouseX * rotSpeed;
            pitch -= mouseY * rotSpeed;

            pitch = Mathf.Clamp(pitch, -45f, 45f);

            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
         }
            
     }
}
