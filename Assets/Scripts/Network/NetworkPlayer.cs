using Fusion;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    //==========================
    // Local Player
    //==========================

    public static NetworkPlayer Local { get; private set; }

    //==========================
    // Network Data
    //==========================

    [Networked]
    public int PlayerId { get; set; }

    [Networked]
    public NetworkBool IsReady { get; set; }

    [Networked]
    public int SeatIndex { get; set; }

    //==========================
    // Spawn
    //==========================

    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            Local = this;
            Debug.Log($"Local Player 建立完成：{Object.InputAuthority.PlayerId}");
        }

        if (HasStateAuthority)
        {
            PlayerId = Object.InputAuthority.PlayerId;
            IsReady = false;
            SeatIndex = -1;
        }

        Debug.Log($"NetworkPlayer Spawned：{Object.InputAuthority.PlayerId}");
    }

    //==========================
    // Ready
    //==========================

    public void SetReady()
    {
        if (!HasInputAuthority)
            return;

        RPC_SetReady();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetReady()
    {
        IsReady = true;

        Debug.Log($"Player {PlayerId} Ready");
    }
}