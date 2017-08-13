using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class StartGamePanel : NetworkBehaviour
{
    private Text _text;
    
    public float time;
    public GameSession gameSession;

    private void Awake()
    {
        _text = GetComponentInChildren<Text>();
    }

    private void OnEnable ()
    {
		_text.text = "GAME STARTING:\n" + time;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();

        Debug.Log("Start on server");
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
            gameObject.SetActive(false);
        }
	}
}
