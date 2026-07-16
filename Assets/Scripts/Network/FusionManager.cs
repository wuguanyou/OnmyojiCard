using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(NetworkRunner))]
[RequireComponent(typeof(NetworkSceneManagerDefault))]
[RequireComponent(typeof(FusionCallbacks))]
public class FusionManager : MonoBehaviour
{
    //==========================
    // Singleton
    //==========================

    public static FusionManager Instance { get; private set; }

    //==========================
    // 玩家資料（暫時保留）
    //==========================

    public List<PlayerData> Players = new List<PlayerData>();

    //==========================
    // Fusion
    //==========================

    public NetworkRunner Runner { get; private set; }
    [Header("Player")]

    public string LocalPlayerName = "玩家";

    private NetworkSceneManagerDefault sceneManager;

    [Header("Network Prefab")]
    [SerializeField]
    private NetworkObject playerPrefab;

    //==========================
    // Unity
    //==========================

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

        Runner = GetComponent<NetworkRunner>();
        sceneManager = GetComponent<NetworkSceneManagerDefault>();

        Runner.ProvideInput = true;

        Runner.AddCallbacks(GetComponent<FusionCallbacks>());
    }

    private void Start()
    {
        Debug.Log("FusionManager 啟動成功");

        // 開發模式：直接進 Lobby
        if (SceneManager.GetActiveScene().name == "Launcher")
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    //==========================
    // 建立房間
    //==========================

    public async Task<StartGameResult> CreateRoom(string roomName)
    {
        Debug.Log($"建立房間：{roomName}");

        return await Runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Host,
            SessionName = roomName,
            SceneManager = sceneManager
        });
    }

    //==========================
    // 加入房間
    //==========================

    public async Task<StartGameResult> JoinRoom(string roomName)
    {
        Debug.Log($"加入房間：{roomName}");

        return await Runner.StartGame(new StartGameArgs()
        {
            GameMode = GameMode.Client,
            SessionName = roomName,
            SceneManager = sceneManager
        });
    }

    //==========================
    // Spawn Player
    //==========================

    public NetworkObject SpawnPlayer(PlayerRef player)
    {
        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab 尚未指定！");
            return null;
        }

        Debug.Log($"Spawn Player：{player.PlayerId}");

        return Runner.Spawn(
            playerPrefab,
            Vector3.zero,
            Quaternion.identity,
            player
        );
    }
}