using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public static PlayerController m_instance = null;

    [SerializeField]
    private int m_life = 100;
    [SerializeField]
    private int m_hits = 0;
    [SerializeField]
    private int m_score = 0;

    public float m_timer = 0.0f;
    public bool m_isTimerActive = false;

    // delegates
    public delegate void LifeChanged(uint life);
    public delegate void ScoreChanged(uint score);
    public delegate void HitChanged(uint hits);
    // events
    public event LifeChanged OnLifeChanged;
    public event ScoreChanged OnScoreChanged;
    public event HitChanged OnHitChanged;

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
        if (OnHitChanged != null)
            OnHitChanged((uint)m_hits);

        m_timer = 0.0f;
        m_isTimerActive = false;
    }

    void Update()
    {
        if(m_isTimerActive)
            m_timer += Time.deltaTime;
    }

    public void LoseLife(uint damage)
    {
        m_life -= (int)damage;

        // TODO: Player game over

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

    // TODO: internal cooldown for collision hit additions
    public void AddHit()
    {
        m_hits++;

        if (OnHitChanged != null)
            OnHitChanged((uint)m_hits);
    }

    public void StartTimer()
    {
        m_isTimerActive = true;
    }

    public void StopTimer()
    {
        m_isTimerActive = false;
    }

    private int CalculateFinalScore()
    {
        return (int)(m_score - 0.1f * m_timer - 25 * m_hits);
    }
}
