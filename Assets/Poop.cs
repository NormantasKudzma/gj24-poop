using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Poop : MonoBehaviour
{
    public GameObject m_ExclamationMark;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pedestrian"))
        {
            Debug.Log("Hit person!");
            col.collider.enabled = false;
            Scoring.AddScore(10);

            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
            transform.parent = col.gameObject.transform;
            Vector3 newPoopPos = col.collider.offset;
            newPoopPos.z -= 0.1f;
            transform.localPosition = newPoopPos;

            transform.localScale = Vector3.Scale(transform.localScale, col.gameObject.transform.localScale);

            var unhappy = col.gameObject.GetComponent<RandomSoundPlayer>();
            if (unhappy != null) { unhappy.PlayRandom(); }

            var exclamation = GameObject.Instantiate(m_ExclamationMark, col.gameObject.transform);
            Vector3 exs = exclamation.transform.localScale;
            exs.x /= col.gameObject.transform.localScale.y;
            exs.y /= col.gameObject.transform.localScale.y;
            exs.x = Mathf.Abs(exs.x);
            exclamation.transform.localScale = exs;
            exclamation.transform.localPosition = newPoopPos;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
