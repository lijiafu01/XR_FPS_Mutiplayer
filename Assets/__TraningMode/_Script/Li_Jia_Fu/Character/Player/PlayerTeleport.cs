using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TraningMode;
public class PlayerTeleport : MonoBehaviour
{
    public PlayerTeleporter teleporter;
    private float teleportCooldown = 1f;
    private float lastTeleportTime = -1f;
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One) && Time.time - lastTeleportTime >= teleportCooldown)
        {
            teleporter.ToggleDisplay(true);
        }

        if (OVRInput.GetUp(OVRInput.Button.One) && Time.time - lastTeleportTime >= teleportCooldown)
        {
            teleporter.ToggleDisplay(false);
            teleporter.Teleport();

            lastTeleportTime = Time.time;
        }
    }
}
