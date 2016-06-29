using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LifeDisplayController : MonoBehaviour
{
    private Text m_text;


    void Start()
    {
        m_text = GetComponent<Text>();
        PlayerController.m_instance.OnLifeChanged += DisplayLife;
    }

    //void OnDestroy()
    //{
    //    PlayerController.m_instance.OnLifeChanged -= DisplayLife;
    //}

    void DisplayLife(uint life)
    {
        m_text.text = life.ToString();
    }
}
