using System;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSession : NetworkBehaviour
{
    [SerializeField]
    private LayerMask m_LayerMask;

    private Disk m_SelectedDisk;

    [SerializeField]
    [SyncVar]
    private byte m_TeamId;

    private bool m_IsCanTurn = false;

    public override void OnStartAuthority()
    {
        base.OnStartAuthority();

        enabled = true;

        CmdSetPlayerSession();
    }

    [Command]
    private void CmdSetPlayerSession()
    {
        GameSession.Instance.teamManager.ExecuteAddPlayerSession(gameObject);
    }

    private void Update()
    {
        if (!m_IsCanTurn)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D l_Hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, m_LayerMask);
            if (l_Hit.collider != null)
            {
                m_SelectedDisk = l_Hit.collider.GetComponent<Disk>();
                if (m_SelectedDisk.teamId != m_TeamId)
                {
                    m_SelectedDisk = null;
                }
            }
        }

        if (m_SelectedDisk != null)
        {
            var l_Force = m_SelectedDisk.UpdateSwipe();
            CmdAddForce(m_SelectedDisk.gameObject, l_Force);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (m_SelectedDisk != null)
            {
                m_IsCanTurn = false;
                CmdEndTurn();
            }
            m_SelectedDisk = null;
        }
    }

    [Command]
    private void CmdAddForce(GameObject diskGameObject, Vector3 force)
    {
        RpcAddForce(diskGameObject, force);
    }

    [ClientRpc]
    private void RpcAddForce(GameObject diskGameObject, Vector3 force)
    {
        diskGameObject.GetComponent<Rigidbody2D>().AddForce(force);
    }

    [ClientRpc]
    public void RpcSetTeamId(byte teamId)
    {
        m_TeamId = teamId;

        if (hasAuthority)
        {
            
        }
    }

    [ClientRpc]
    public void RpcStartTurn()
    {
        m_IsCanTurn = true;

        if (hasAuthority)
        {
            Debug.Log("MyTurn");
        }
    }

    [Command]
    private void CmdEndTurn()
    {
        GameSession.Instance.turnManager.NextTurn();
    }
}
