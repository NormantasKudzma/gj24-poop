using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public static float g_GlobalScrollSpeed = 1.2f;

    public List<GameObject> m_Tiles;
    private int nextTileIndex = 0;

    public float m_ScrollSpeed = 0.5f;
    public float m_SpawnDiffMin = 0.0f;
    public float m_SpawnDiffMax = 0.0f;
    public bool m_DoGrayshift = true;

    private float m_SpawnDist = 10.0f;
    private float m_DestroyAt = -20.0f;

    private List<GameObject> m_Spawned = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            m_Spawned.Add(transform.GetChild(i).gameObject);
        }

        m_Spawned.ForEach(ApplyGrayshift);

        m_Spawned.Sort((a, b) =>
        {
            return a.transform.localPosition.x > b.transform.localPosition.x ? 1 : -1;
        });

        m_ScrollSpeed /= 100;
        m_ScrollSpeed *= g_GlobalScrollSpeed;
        m_ScrollSpeed /= transform.localScale.x;

        m_SpawnDist /= transform.localScale.x;
        m_DestroyAt /= transform.localScale.x;

        ShuffleTiles();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = m_Spawned.Count - 1; i >= 0; --i)
        {
            var nextP = m_Spawned[i].transform.localPosition;
            nextP.x -= m_ScrollSpeed;
            m_Spawned[i].transform.localPosition = nextP;

            if (nextP.x <= m_DestroyAt)
            {
                Destroy(m_Spawned[i]);
                m_Spawned.RemoveAt(i);
            }
        }

        var last = m_Spawned[m_Spawned.Count - 1];
        if (last.transform.localPosition.x <= m_SpawnDist)
        {
            var r = last.GetComponent<SpriteRenderer>();
            var halfSize = r.bounds.extents;
            halfSize.y = 0;
            SpawnNew(last.transform.localPosition + halfSize);
        }
    }

    private void SpawnNew(Vector2 at)
    {
        var newTile = GameObject.Instantiate(m_Tiles[nextTileIndex], transform);
        if (++nextTileIndex >= m_Tiles.Count)
        {
            nextTileIndex = 0;
            ShuffleTiles();
        }

        Vector3 newPos = Vector3.zero;
        Vector2 off = newTile.GetComponent<SpriteRenderer>().bounds.extents;
        float rand = Random.Range(m_SpawnDiffMin, m_SpawnDiffMax);

        ApplyGrayshift(newTile);
        newPos.x = off.x + at.x + rand;
        newTile.transform.localPosition = newPos;
        m_Spawned.Add(newTile);
    }

    private void ApplyGrayshift(GameObject obj)
    {
        const float shiftStartAt = 10;
        if (m_DoGrayshift && transform.localPosition.z > shiftStartAt)
        {
            const float shiftCapAt = 60;
            const float maxGray = 0.36f;
            float grayshift = 1.0f - Mathf.Clamp(transform.localPosition.z, 0, shiftCapAt) / shiftCapAt * maxGray;
            obj.GetComponent<SpriteRenderer>().color = new Color(grayshift, grayshift, grayshift, 1.0f);
        }
    }

    private void ShuffleTiles()
    {
        if (m_Tiles.Count == 1) { return; }

        for (int i = 0; i < m_Tiles.Count; ++i)
        {
            int swapWith = Random.Range(0, m_Tiles.Count);
            var temp = m_Tiles[i];
            m_Tiles[i] = m_Tiles[swapWith];
            m_Tiles[swapWith] = temp;
        }
    }
}
