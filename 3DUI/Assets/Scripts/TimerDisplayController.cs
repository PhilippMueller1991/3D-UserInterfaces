using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerDisplayController : MonoBehaviour {

    private Text m_text;
    private PlayerController m_playerRef;

    void Start()
    {
        m_text = GetComponent<Text>();
        m_playerRef = PlayerController.m_instance;
    }

    void Update()
    {
        m_text.text = m_playerRef.m_timer.ToString("0.00");
    }
}
