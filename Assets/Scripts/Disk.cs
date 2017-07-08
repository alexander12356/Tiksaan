using System;
using UnityEngine;
using UnityEngine.Networking;

public class Disk : NetworkBehaviour
{
    [SerializeField]
    private float m_SpeedCoeff = 2.0f;
    [SerializeField]
    private int m_TeamId;
    private Vector3 m_PrevPosition;
    private Rigidbody2D m_RigidBody;


    public Action<Disk> OnRemove;
    
    public int teamId
    {
        get { return m_TeamId; }
    }

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        m_PrevPosition = transform.position;
    }
    
    public Vector3 UpdateSwipe()
    {
        var l_MouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var l_Direction = l_MouseWorldPosition - m_PrevPosition;
        var l_Distance = Vector2.Distance(l_MouseWorldPosition, m_PrevPosition);

        m_PrevPosition = transform.position;

        return l_Direction.normalized * l_Distance * m_SpeedCoeff;
    }

    [Command]
    public void CmdSetColor(Color p_NewColor)
    {
        RpcSetColor(p_NewColor);
    }

    [ClientRpc]
    private void RpcSetColor(Color p_NewColor)
    {
        GetComponent<SpriteRenderer>().color = p_NewColor;
    }

    public bool IsIdle()
    {
        return m_RigidBody.velocity == Vector2.zero;
    }

    public void Remove()
    {
        CmdRemove();
    }

    [Command]
    private void CmdRemove()
    {
        if (OnRemove != null)
        {
            OnRemove.Invoke(this);
        }
    }
}
