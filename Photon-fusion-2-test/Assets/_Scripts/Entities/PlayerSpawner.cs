using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    #region Component Configs

    [SerializeField]
    private GameObject playerPrefab;

    #endregion

    public void PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            NetworkObject plObject = Runner.Spawn(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity);
            if (!plObject.HasStateAuthority)
            {
                return;
            }
            Runner.SetPlayerObject(player, plObject);
        }
    }
}
