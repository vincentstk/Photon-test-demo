using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Component Configs

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float smoothTime;

    private Transform localPlayerTarget;
    private Vector3 velocity = Vector3.zero;
    #endregion

    // Update is called once per frame
    void LateUpdate()
    {
        if (localPlayerTarget == null)
        {
            return;
        }

        Vector3 targetPosition = localPlayerTarget.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void SetLocalPlayer(Transform localPlayer)
    {
        localPlayerTarget = localPlayer;
    }
}
