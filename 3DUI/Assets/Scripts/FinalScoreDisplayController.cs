using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FinalScoreDisplayController : MonoBehaviour {

    public static FinalScoreDisplayController m_instance;

    private Text m_text;
    private PlayerController m_playerRef;

    void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        else if (m_instance != this)
            Destroy(this.gameObject);
    }

    void Start()
    {
        m_text = GetComponent<Text>();
        m_playerRef = PlayerController.m_instance;

        this.gameObject.SetActive(false);
    }

    void Update()
    {
        m_text.text = m_playerRef.m_timer.ToString();
    }

    public void DisplayFinalScore(int score)
    {

    }
}
