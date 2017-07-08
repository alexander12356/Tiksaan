using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

public class DiskManager : NetworkBehaviour
{
    [SerializeField]
    private List<Transform> m_Team1SpawnPositions = new List<Transform>();
    [SerializeField]
    private List<Transform> m_Team2SpawnPositions = new List<Transform>();

    [SerializeField]
    private Disk m_DiskPrefab = null;

    [Header("Debugging")]
    [SerializeField]
    private List<Disk> m_Team1Disks = new List<Disk>();
    [SerializeField]
    private List<Disk> m_Team2Disks = new List<Disk>();

    public List<Disk> team1Disks
    {
        get
        {
            return m_Team1Disks;
        }
    }

    public List<Disk> team2Disks
    {
        get
        {
            return m_Team2Disks;
        }
    }

    public override void OnStartServer()
    {
        for (int i = 0; i < m_Team1SpawnPositions.Count; i++)
        {
            var l_DiskObject = Instantiate(m_DiskPrefab, m_Team1SpawnPositions[i], false);
            l_DiskObject.teamId = 0;
            m_Team1Disks.Add(l_DiskObject);
            NetworkServer.Spawn(l_DiskObject.gameObject);
        }

        for (int i = 0; i < m_Team2SpawnPositions.Count; i++)
        {
            var l_DiskObject = Instantiate(m_DiskPrefab, m_Team2SpawnPositions[i], false);
            l_DiskObject.teamId = 1;
            m_Team2Disks.Add(l_DiskObject);
            NetworkServer.Spawn(l_DiskObject.gameObject);
        }
    }

    public void RemoveDisk(Disk p_Disk)
    {
        switch(p_Disk.teamId)
        {
            case 0:
                m_Team1Disks.Remove(p_Disk);
                break;
            case 1:
                m_Team2Disks.Remove(p_Disk);
                break;
            default:
                Debug.LogError("Disk without team id");
                p_Disk.GetComponent<SpriteRenderer>().color = Color.black;
                break;
        }

        NetworkServer.Destroy(p_Disk.gameObject);
        GameSession.Instance.CheckWin();
    }

    public bool CanNextTurn()
    {
        for (int i = 0; i < m_Team1Disks.Count; i++)
        {
            if (m_Team1Disks[i].IsIdle() == false)
            {
                return false;
            }
        }

        for (int i = 0; i < m_Team2Disks.Count; i++)
        {
            if (m_Team2Disks[i].IsIdle() == false)
            {
                return false;
            }
        }

        return true;
    }
}
