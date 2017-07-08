using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TeamManager : NetworkBehaviour
{
    [Header("Debugging")]
    [SerializeField]
    private List<PlayerSession> m_PlayerSessions = new List<PlayerSession>();

    public List<PlayerSession> playerSessions
    {
        get { return m_PlayerSessions; }
    }

    [Server]
    public void ExecuteAddPlayerSession(GameObject playerSessionGameObject)
    {
        var l_PlayerSessionInstance = playerSessionGameObject.GetComponent<PlayerSession>();
        l_PlayerSessionInstance.RpcSetTeamId((byte)m_PlayerSessions.Count);
        m_PlayerSessions.Add(l_PlayerSessionInstance);

        if (m_PlayerSessions.Count == 2)
        {
            GameSession.Instance.turnManager.StartGame();
        }
    }
}
