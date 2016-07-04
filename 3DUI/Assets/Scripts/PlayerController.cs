﻿using UnityEngine;
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
    [SerializeField]
    private int m_scoreMultiplier = 1;
    [SerializeField]
    private float m_timer = 0.0f;

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

    public void ResetMultiplier()
    {
        // TODO write accumulated score with old score multiplier
        m_scoreMultiplier = 1;
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
        m_timer = Time.time;
    }

    public void StopTimer()
    {
        m_timer = m_timer - Time.time;
    }
}
