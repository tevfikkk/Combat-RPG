using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : Singleton<CameraController>
{
    private CinemachineVirtualCamera virtualCamera;

    /// <summary>
    /// Sets the player as the camera's follow target.
    /// </summary>
    public void SetPlayerCameraFollow()
    {
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCamera.Follow = PlayerController.Instance.transform;
    }
}
