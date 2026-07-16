using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

public class FusionCallbacks : MonoBehaviour, INetworkRunnerCallbacks
{
    //==========================
    // 玩家加入
    //==========================

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"玩家加入：{player.PlayerId}");

        // 建立玩家資料（暫時保留）
        PlayerData data = new PlayerData();

        data.playerRef = player;
        data.playerName = $"Player {player.PlayerId}";
        data.isReady = false;

        FusionManager.Instance.Players.Add(data);

        // 只有 Host 可以 Spawn Player
        if (runner.IsServer)
        {
            Debug.Log($"Host Spawn Player：{player.PlayerId}");

            FusionManager.Instance.SpawnPlayer(player);
        }
    }

    //==========================
    // 玩家離開
    //==========================

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"玩家離開：{player.PlayerId}");

        FusionManager.Instance.Players.RemoveAll(
            p => p.playerRef == player);
    }

    public void OnInput(NetworkRunner runner, NetworkInput input) { }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
        Debug.Log($"Shutdown：{shutdownReason}");
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        Debug.Log("成功連線 Photon");
    }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
        Debug.Log($"斷線：{reason}");
    }

    public void OnConnectRequest(NetworkRunner runner,
        NetworkRunnerCallbackArgs.ConnectRequest request,
        byte[] token) { }

    public void OnConnectFailed(NetworkRunner runner,
        NetAddress remoteAddress,
        NetConnectFailedReason reason)
    {
        Debug.Log($"連線失敗：{reason}");
    }

    public void OnSessionListUpdated(NetworkRunner runner,
        List<SessionInfo> sessionList) { }

    public void OnCustomAuthenticationResponse(NetworkRunner runner,
        Dictionary<string, object> data) { }

    public void OnHostMigration(NetworkRunner runner,
        HostMigrationToken hostMigrationToken) { }

    public void OnReliableDataReceived(NetworkRunner runner,
        PlayerRef player,
        ReliableKey key,
        ArraySegment<byte> data) { }

    public void OnReliableDataProgress(NetworkRunner runner,
        PlayerRef player,
        ReliableKey key,
        float progress) { }

    public void OnSceneLoadDone(NetworkRunner runner)
    {
        Debug.Log("場景載入完成");
    }

    public void OnSceneLoadStart(NetworkRunner runner)
    {
        Debug.Log("開始載入場景");
    }

    public void OnUserSimulationMessage(NetworkRunner runner,
        SimulationMessagePtr message) { }

    public void OnObjectEnterAOI(NetworkRunner runner,
        NetworkObject obj,
        PlayerRef player) { }

    public void OnObjectExitAOI(NetworkRunner runner,
        NetworkObject obj,
        PlayerRef player) { }
}