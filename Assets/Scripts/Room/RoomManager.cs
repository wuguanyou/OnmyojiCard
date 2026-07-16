using TMPro;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text roomNameText;

    public TMP_Text player1Name;

    public TMP_Text player2Name;

    public TMP_Text player1Ready;

    public TMP_Text player2Ready;

    public GameObject readyButton;

    public GameObject startButton;

    private void Start()
    {
        roomNameText.text = "房間：TestRoom";

        player1Name.text = "玩家一";

        player2Name.text = "等待玩家加入...";

        player1Ready.text = "未準備";

        player2Ready.text = "未準備";

        Debug.Log("Room Scene 已載入");
    }

    private void Update()
    {
    if (FusionManager.Instance.Players.Count > 0)
    {
        player1Name.text =
            FusionManager.Instance.Players[0].playerName;
    }

    if (FusionManager.Instance.Players.Count > 1)
    {
        player2Name.text =
            FusionManager.Instance.Players[1].playerName;
    }
    }

}