using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    private static int m_Score = 0;
    private static Text m_ScoreText;
    private static string m_ScoreTextFormat;
    public GameObject m_Text;

    // Start is called before the first frame update
    void Start()
    {
        m_ScoreText = m_Text.GetComponent<Text>();
        m_ScoreTextFormat = m_ScoreText.text;
        AddScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddScore(int score)
    {
        m_Score += score;
        m_ScoreText.text = string.Format(m_ScoreTextFormat, m_Score);
    }
}
