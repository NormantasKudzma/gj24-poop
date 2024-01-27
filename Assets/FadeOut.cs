using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    public float m_Duration = 0.3f;
    public float m_ScaleBy = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        m_ScaleBy /= m_Duration;
    }

    // Update is called once per frame
    void Update()
    {
        var nextS = transform.localScale;
        nextS.x += m_ScaleBy * Time.deltaTime;
        nextS.y += m_ScaleBy * Time.deltaTime;
        transform.localScale = nextS;

        m_Duration -= Time.deltaTime;
        if (m_Duration <= 0)
        {
            Destroy(gameObject);
        }
    }
}
