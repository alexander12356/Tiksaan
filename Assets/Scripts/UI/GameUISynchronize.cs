using UnityEngine;
using UnityEngine.Networking;

public class GameUISynchronize : NetworkBehaviour
{
    public GameUI m_GameUI;

    [ClientRpc]
    public void RpcShowGameStartingPanel()
    {
        m_GameUI.ShowGameStartingPanel();
    }
}
