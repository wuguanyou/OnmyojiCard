using Fusion;
using UnityEngine;

public class NetworkLauncher : MonoBehaviour
{
    public static NetworkLauncher Instance { get; private set; }

    private NetworkRunner runner;

    public NetworkRunner Runner => runner;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 建立 NetworkRunner
        runner = gameObject.AddComponent<NetworkRunner>();

        // 之後場景同步會用到
        runner.ProvideInput = true;
    }
}