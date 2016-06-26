using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController m_instance = null;

    [SerializeField]
    private int m_life = 100;
    [SerializeField]
    private int m_score = 0;
    [SerializeField]
    private int m_scoreMultiplier = 1;

    // delegates
    public delegate void LifeChanged(uint life);
    public delegate void ScoreChanged(uint score);
    // events
    public event LifeChanged OnLifeChanged;
    public event ScoreChanged OnScoreChanged;


    void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        else if (m_instance != this)
            Destroy(this);
    }

    void Start()
    {
        if (OnLifeChanged != null)
            OnLifeChanged((uint)m_life);
        if (OnScoreChanged != null)
            OnScoreChanged((uint)m_score);
    }

    public void LoseLife(uint damage)
    {
        m_life -= (int)damage;

        if (OnLifeChanged != null)
            OnLifeChanged((uint)m_life);
    }

    public void Heal(uint heal)
    {
        m_life += (int)heal;

        if (OnLifeChanged != null)
            OnLifeChanged((uint)m_life);
    }

    public void AddScore(uint points)
    {
        m_score += (int)points;

        if (OnScoreChanged != null)
            OnScoreChanged((uint)m_score);
    }

    public void ResetMultiplier()
    {
        // TODO write accumulated score with old score multiplier
        m_scoreMultiplier = 1;
    }
}
