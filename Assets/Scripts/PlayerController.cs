using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("InputActions For Unity's New Input System")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction firing;

    [Header("Ship's Allowed Range On Screen")]
    [SerializeField] float xRange = 34f;
    [SerializeField] Vector2 yRange = new Vector2(-10, 25f);

    [Header("Transition & Rotation Tuning")]
    [SerializeField] float controlSpeed = 35f;
    [SerializeField] float controlPitch = -17f;
    [SerializeField] float controlRoll = -20;
    [SerializeField] float positionPitch = -1.34f;
    [SerializeField] float positionYaw = 1.25f;

    //A list of lasers in the scene
    List<ParticleSystem> lasers = new List<ParticleSystem>();

    //Input values from movement inputAction 
    float horizontal, vertical;

    //Smoothing horizontal/vertical values
    float smoothingVelocityX = 0.0f;
    float smoothingVelocityY = 0.0f;

    void OnEnable()
    {
        movement.Enable();
        firing.Enable();
    }
    void OnDisable()
    {
        movement.Disable();
        firing.Disable();
    }
    void Start()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Laser"))
            lasers.Add(g.GetComponent<ParticleSystem>());
    }
    void Update()
    {

        Vector2 movementInput = movement.ReadValue<Vector2>();

        //Smoothing movement input so it's gradually changing
        horizontal = Mathf.SmoothDamp(horizontal, movementInput.x, ref smoothingVelocityX, 0.15f);
        vertical = Mathf.SmoothDamp(vertical, movementInput.y, ref smoothingVelocityY, 0.15f);

        Transition();
        Rotation();
        Fire();
        
    }
    void Transition()
    {
        Vector3 newPos = transform.localPosition +
            new Vector3(horizontal, vertical, 0) * controlSpeed * Time.deltaTime;
        newPos.x = Mathf.Clamp(newPos.x, -xRange, xRange);
        newPos.y = Mathf.Clamp(newPos.y, yRange.x, yRange.y);
        transform.localPosition = newPos;
    }

    void Rotation()
    {
        float pitch = transform.localPosition.y * positionPitch + vertical * controlPitch;
        float yaw = transform.localPosition.x * positionYaw;
        float roll = horizontal * controlRoll;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void Fire()
    {
        if (!firing.triggered) return;
        bool fire = firing.ReadValue<float>() > 0.5f;
        if (!fire)
        {
            SetLasersActive(false);
            return;
        }
        SetLasersActive(true);
    }
    public void SetLasersActive(bool active)
    {
        foreach (ParticleSystem laser in lasers)
        {
            var emission = laser.emission;
            emission.enabled = active;
        }
    }
}
