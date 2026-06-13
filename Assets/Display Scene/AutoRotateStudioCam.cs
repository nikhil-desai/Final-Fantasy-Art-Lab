using UnityEngine;
using Unity.Cinemachine;

public class AutoRotateStudioCam : MonoBehaviour
{
    [Header("Camera Controls")]
    [Tooltip("Degrees per second. Positive numbers rotate right, negative rotate left.")]
    public float rotationSpeed = 15f; 

    private CinemachineOrbitalFollow orbitalFollow;

    void Start()
    {
        // Automatically grab the orbital component attached to this camera
        orbitalFollow = GetComponent<CinemachineOrbitalFollow>();

        if (orbitalFollow == null)
        {
            Debug.LogError("AutoRotateStudioCam is missing the Cinemachine Orbital Follow component!");
        }
    }

    void Update()
    {
        if (orbitalFollow != null)
        {
            // Continuously spin the Horizontal Axis Value every frame
            orbitalFollow.HorizontalAxis.Value += rotationSpeed * Time.deltaTime;
        }
    }
}