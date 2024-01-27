using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Poop : MonoBehaviour
{
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
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
