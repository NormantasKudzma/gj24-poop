using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject m_PoopFab;
    public float m_MoveSpeed = 0.02f;

    public int m_MaxShots = 10;
    public float m_ShotInterval = 0.1f;
    public float m_ShotRechargeSpeed = 0.5f;
    public GameObject m_ShotsText;
    private int m_ShotsRemain = 0;
    private float m_ShootTimer = 0;
    private float m_RechargeTimer = 0.0f;
    private string m_ShotsTextFormat;

    private Transform m_PoopSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        m_PoopSpawnPos = transform.Find("PoopSpawnPos");

        m_ShotsRemain = m_MaxShots;

        m_ShotsTextFormat = m_ShotsText.GetComponent<Text>().text;
        m_ShotsText.GetComponent<Text>().text = string.Format(m_ShotsTextFormat, m_ShotsRemain);
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

        m_ShootTimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            m_RechargeTimer = 0.0f;
            if (m_ShootTimer <= 0.0f)
            {
                DoPoop(nextP);
            }
        }
        else
        {
            RechargePoop();
        }
    }

    private void DoPoop(Vector2 add_v){
        m_ShootTimer = m_ShotInterval;
        if (m_ShotsRemain <= 0) { return; }
        m_ShotsRemain--;
        m_ShotsText.GetComponent<Text>().text = string.Format(m_ShotsTextFormat, m_ShotsRemain);

        var poop = Instantiate(m_PoopFab);
        poop.transform.position = m_PoopSpawnPos.transform.position;

        add_v.y = Math.Min(0, add_v.y * 10000);
        add_v.x *= 13000;
        poop.GetComponent<Rigidbody2D>().AddForce(add_v);

        GetComponent<Animator>().Play("scream");
    }

    private void RechargePoop()
    {
        if (m_ShotsRemain >= m_MaxShots) { return; }

        m_RechargeTimer += Time.deltaTime;
        if (m_RechargeTimer > m_ShotRechargeSpeed)
        {
            m_RechargeTimer = 0.0f;
            m_ShotsRemain++;
            m_ShotsText.GetComponent<Text>().text = string.Format(m_ShotsTextFormat, m_ShotsRemain);
        }
    }
}
