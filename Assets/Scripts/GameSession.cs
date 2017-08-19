using System.Collections;

using UnityEngine;
using UnityEngine.Networking;

public class GameSession : NetworkBehaviour
{
    private static GameSession m_Instance;
    private TurnManager m_TurnManager;
    private DiskManager m_DiskManager;
    private TeamManager m_TeamManager;
    private bool m_IsGameEnd = false;

    public GameUISynchronize gameUISynchronize;

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

    public bool isGameEnd
    {
        get { return m_IsGameEnd; }
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
        if (m_DiskManager.team1Disks.Count == 0)
        {
            m_IsGameEnd = true;
            teamManager.playerSessions[1].ExecuteWin();
            Debug.Log("GAME_LOG: Team 2 WIN");
        }
        else if (m_DiskManager.team2Disks.Count == 0)
        {
            m_IsGameEnd = true;
            teamManager.playerSessions[0].ExecuteWin();
            Debug.Log("GAME_LOG: Team 1 WIN");
        }
    }

    public void ShowRestartGame()
    {

    }

    [Server]
    public void ExecuteShowStartGame()
    {
        gameUISynchronize.RpcShowGameStartingPanel();
        StartCoroutine(KostilRunGame());
    }

    private IEnumerator KostilRunGame()
    {
        yield return new WaitForSeconds(6.0f);

        turnManager.StartGame();
    }
}
