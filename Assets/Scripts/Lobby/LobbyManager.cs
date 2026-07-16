using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_InputField playerNameInput;
    public TMP_InputField roomNameInput;

    public TMP_Text statusText;

    //---------------------------------------------------
    // 建立房間
    //---------------------------------------------------

    public async void CreateRoom()
    {
        statusText.text = "正在建立房間...";

        var result = await FusionManager.Instance.CreateRoom(roomNameInput.text);

        if (result.Ok)
        {
            statusText.text = "房間建立成功";

            SceneManager.LoadScene("Room");
        }
        else
        {
            statusText.text = result.ShutdownReason.ToString();
        }
    }

    //---------------------------------------------------
    // 加入房間
    //---------------------------------------------------

    public async void JoinRoom()
    {
        statusText.text = "正在加入房間...";

        var result = await FusionManager.Instance.JoinRoom(roomNameInput.text);

        if (result.Ok)
        {
            statusText.text = "加入成功";

            SceneManager.LoadScene("Room");
        }
        else
        {
            statusText.text = result.ShutdownReason.ToString();
        }
    }
}