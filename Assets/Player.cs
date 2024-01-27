using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject m_PoopFab;
    public float m_MoveSpeed = 0.02f;

    private Transform m_PoopSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        m_PoopSpawnPos = transform.Find("PoopSpawnPos");
    }

    // Update is called once per frame
    void Update()
    {
        var nextP = Vector3.zero;
        if (Input.GetKey("down")){
            nextP.y -= m_MoveSpeed;
        }
        else if (Input.GetKey("up")){
            nextP.y += m_MoveSpeed;
        }
        
        if (Input.GetKey("left")){
            nextP.x -= m_MoveSpeed;
        }
        else if (Input.GetKey("right")){
            nextP.x += m_MoveSpeed;
        }
        transform.position = transform.position + nextP;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DoPoop(nextP);
        }
    }

    private void DoPoop(Vector2 add_v){
        var poop = GameObject.Instantiate(m_PoopFab);
        poop.transform.position = m_PoopSpawnPos.transform.position;

        add_v.y = Math.Min(0, add_v.y * 10000);
        add_v.x *= 13000;
        poop.GetComponent<Rigidbody2D>().AddForce(add_v);

        GetComponent<Animator>().Play("scream");
    }
}
