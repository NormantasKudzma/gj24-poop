using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public List<AudioClip> m_Sounds;

    private int m_NextIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        Shuffle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayRandom()
    {
        var audioSource = GetComponent<AudioSource>();
        if (audioSource == null) { return; }

        if (m_Sounds.Count == 0) { return; }
        var play = m_Sounds[m_NextIndex];
        audioSource.clip = play;
        audioSource.Play();

        if (++m_NextIndex == m_Sounds.Count)
        {
            m_NextIndex = 0;
            Shuffle();
        }
    }

    private void Shuffle()
    {
        if (m_Sounds.Count <= 1) { return; }

        for (int i = 0; i < m_Sounds.Count; ++i)
        {
            int swapWith = Random.Range(0, m_Sounds.Count);
            var temp = m_Sounds[i];
            m_Sounds[i] = m_Sounds[swapWith];
            m_Sounds[swapWith] = temp;
        }
    }
}
