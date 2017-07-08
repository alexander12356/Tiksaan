using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

public class DiskManager : NetworkBehaviour
{
    [SerializeField]
    private List<Disk> m_Team1 = null;

    [SerializeField]
    private List<Disk> m_Team2 = null;

    public List<Disk> team1
    {
        get
        {
            return m_Team1;
        }
    }

    public List<Disk> team2
    {
        get
        {
            return m_Team2;
        }
    }

    public override void OnStartServer()
    {
        for (int i = 0; i < m_Team1.Count; i++)
        {
            m_Team1[i].OnRemove += RemoveDisk;
        }

        for (int i = 0; i < m_Team2.Count; i++)
        {
            m_Team2[i].OnRemove += RemoveDisk;
        }
    }

    private void RemoveDisk(Disk p_Disk)
    {
        switch(p_Disk.teamId)
        {
            case 0:
                m_Team1.Remove(p_Disk);
                break;
            case 1:
                m_Team2.Remove(p_Disk);
                break;
            default:
                Debug.LogError("Disk without team id");
                p_Disk.GetComponent<SpriteRenderer>().color = Color.black;
                break;
        }

        NetworkServer.Destroy(p_Disk.gameObject);
        GameSession.Instance.CheckWin();
    }

    [Server]
    public void SetLocalPlayerTeam(byte teamId)
    {
        //switch (teamId)
        //{
        //    case 1:
        //        SetColor(m_Team1);
        //        break;
        //    case 2:
        //        SetColor(m_Team2);
        //        break;
        //}
    }

    [Server]
    private void SetColor(List<Disk> playerDiskList)
    {
        for (int i = 0; i < playerDiskList.Count; i++)
        {
            playerDiskList[i].CmdSetColor(Color.blue);
        }
    }

    public bool CanNextTurn()
    {
        for (int i = 0; i < m_Team1.Count; i++)
        {
            if (m_Team1[i].IsIdle() == false)
            {
                return false;
            }
        }

        for (int i = 0; i < m_Team2.Count; i++)
        {
            if (m_Team2[i].IsIdle() == false)
            {
                return false;
            }
        }

        return true;
    }
}
