using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FinalScoreDisplayController : MonoBehaviour {

    public static FinalScoreDisplayController m_instance;

    public GameObject m_ui;
    public Text m_finalScore;
    public Text m_finalTime;

    private PlayerController m_playerRef;
    private bool m_end = false;

    void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        else if (m_instance != this)
            Destroy(this.gameObject);
    }

    void Start()
    {
        m_playerRef = PlayerController.m_instance;
        this.gameObject.SetActive(false);
        m_ui.SetActive(true);
    }

    void Update()
    {
        if (!m_end)
            return;

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DisplayFinalScore()
    {
        this.gameObject.SetActive(true);
        m_ui.SetActive(false);
        m_finalScore.text = "Final score: \t" + m_playerRef.GetFinalScore().ToString();
        m_finalTime.text = "Final time: \t" + m_playerRef.m_timer.ToString("0.00");

        m_end = true;
    }
}
