using UnityEngine;

public class YourTurnPanel : MonoBehaviour
{
    private Animator m_Animator;

    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        m_Animator.SetTrigger("Show");
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
