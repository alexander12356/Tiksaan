using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TeamManager : NetworkBehaviour
{
    [SerializeField]
    private List<PlayerSession> m_PlayerSessions = new List<PlayerSession>();

    public List<PlayerSession> playerSessions
    {
        get { return m_PlayerSessions; }
    }

    [Server]
    public void AddPlayerSession(GameObject playerSessionGameObject)
    {
        var playerSessionInstance = playerSessionGameObject.GetComponent<PlayerSession>();
        m_PlayerSessions.Add(playerSessionInstance);
        playerSessionInstance.RpcSetTeamId((byte)m_PlayerSessions.Count);

        if (m_PlayerSessions.Count == 2)
        {
            FindObjectOfType<TurnManager>().StartGame();
        }
    }
}
