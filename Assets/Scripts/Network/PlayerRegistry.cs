using System.Collections.Generic;
using UnityEngine;

public class PlayerRegistry : MonoBehaviour
{
    public static PlayerRegistry Instance { get; private set; }

    public readonly List<NetworkPlayer> Players = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Register(NetworkPlayer player)
    {
        if (!Players.Contains(player))
        {
            Players.Add(player);

            Debug.Log($"Register : {player.name}");
        }
    }

    public void Unregister(NetworkPlayer player)
    {
        if (Players.Remove(player))
        {
            Debug.Log($"Unregister : {player.name}");
        }
    }
}