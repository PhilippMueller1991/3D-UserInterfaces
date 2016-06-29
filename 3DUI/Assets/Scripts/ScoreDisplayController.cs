using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreDisplayController : MonoBehaviour
{
    private Text m_text;

        
    void Start ()
    {
        m_text = GetComponent<Text>();
        PlayerController.m_instance.OnScoreChanged += DisplayScore;	    
	}

    //void OnDestroy()
    //{
    //    PlayerController.m_instance.OnScoreChanged -= DisplayScore;
    //}

    void DisplayScore(uint score)
    {
        m_text.text = score.ToString();
    }
}
