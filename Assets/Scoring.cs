using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    private static int m_Score = 0;
    private static Text m_ScoreText;
    public GameObject m_Text;

    // Start is called before the first frame update
    void Start()
    {
        m_ScoreText = m_Text.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void AddScore(int score)
    {
        m_Score += score;
        m_ScoreText.text = m_Score.ToString();
    }
}
