using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitDisplayController : MonoBehaviour {

    private Text m_text;


    void Start()
    {
        m_text = GetComponent<Text>();
        PlayerController.m_instance.OnHitChanged += DisplayHit;
    }

    //void OnDestroy()
    //{
    //    PlayerController.m_instance.OnLifeChanged -= DisplayLife;
    //}

    void DisplayHit(uint hits)
    {
        m_text.text = hits.ToString();
    }
}
