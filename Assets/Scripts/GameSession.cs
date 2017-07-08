using UnityEngine;
using UnityEngine.Networking;

public class GameSession : NetworkBehaviour
{
    private static GameSession m_Instance;

    private TurnManager m_TurnManager;
    private DiskManager m_DiskManager;
    private TeamManager m_TeamManager;

    public static GameSession Instance
    {
        get
        {
            return m_Instance;
        }
    }

    public TurnManager turnManager
    {
        get
        {
            return m_TurnManager;
        }
    }

    public DiskManager diskManager
    {
        get
        {
            return m_DiskManager;
        }
    }

    public TeamManager teamManager
    {
        get
        {
            return m_TeamManager;
        }
    }

    public void Awake()
    {
        m_Instance = this;
    }

    public override void OnStartServer()
    {
        m_TurnManager = FindObjectOfType<TurnManager>();
        m_DiskManager = FindObjectOfType<DiskManager>();
        m_TeamManager = FindObjectOfType<TeamManager>();
    }

    public void CheckWin()
    {
        if (m_DiskManager.team1.Count == 0)
        {
            Debug.Log("Team 2 WIN");
        }
        else if (m_DiskManager.team2.Count == 0)
        {
            Debug.Log("Team 1 WIN");
        }
    }

    public void ShowRestartGame()
    {

    }

    public void StartGame()
    {

    }
}
