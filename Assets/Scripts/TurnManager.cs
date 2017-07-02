using System.Collections;

using UnityEngine;
using UnityEngine.Networking;

public class TurnManager : NetworkBehaviour
{
    public byte m_CurrentTurn = 0;
    public float m_TurnTime;

    private TeamManager m_TeamManager = null;
    private DiskManager m_DiskManager = null;

    public override void OnStartServer()
    {
        m_TeamManager = FindObjectOfType<TeamManager>();
        m_DiskManager = FindObjectOfType<DiskManager>();
    }

    [Server]
    public void StartGame()
    {
        NextTurn();
    }

    [Server]
    public void NextTurn()
    {
        StopAllCoroutines();
        StartCoroutine(CheckingDisksIdle());
    }

    private IEnumerator CheckingDisksIdle()
    {
        bool isIdle = false;
        while (!isIdle)
        {
            isIdle = m_DiskManager.CanNextTurn();
            yield return new WaitForEndOfFrame();
        }

        m_TeamManager.playerSessions[m_CurrentTurn].RpcStartTurn();

        m_CurrentTurn++;

        if (m_CurrentTurn > 1)
        {
            m_CurrentTurn = 0;
        }

        StartCoroutine(RunningTurnTime());
    }

    private IEnumerator RunningTurnTime()
    {
        yield return new WaitForSeconds(m_TurnTime);
        NextTurn();
    }
}
