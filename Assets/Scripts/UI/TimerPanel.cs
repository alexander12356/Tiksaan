using System;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TimerPanel : MonoBehaviour
{
    private Text _text;

    public UnityEvent OnTimerEnd;
    public float time;

    private void Awake()
    {
        _text = GetComponentInChildren<Text>();
    }

    private void OnEnable ()
    {
		_text.text = "GAME STARTING:\n" + time;
    }

    public void Init(int time)
    {
        this.time = time;
    }
	
	private void Update ()
    {
        time -= Time.deltaTime;
        if (time > 0)
        {
            _text.text = "GAME STARTING:\n" + time.ToString("0");
        }
        else
        {
            if (OnTimerEnd != null)
            {
                OnTimerEnd.Invoke();
            }
            gameObject.SetActive(false);
        }
	}
}
