using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    enum SoundLength
    {
        SHORT,
        LONG
    }

    public GameObject m_PoopFab;
    public float m_MoveSpeed = 0.02f;

    public int m_MaxShots = 10;
    public float m_ShotInterval = 0.1f;
    public float m_ShotRechargeSpeed = 0.5f;
    private int m_ShotsRemain = 0;
    private float m_ShootTimer = 0;
    private float m_RechargeTimer = 0.0f;
    public GameObject m_ShotsBar;
    private Vector3 m_ShotsBarMaxScale;

    private Transform m_PoopSpawnPos;

    private RandomSoundPlayer m_PoopShortSound;
    private RandomSoundPlayer m_PoopLongSound;
    private int m_PlaySoundCounter = 0;
    private float m_PlaySoundCooldown = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_PoopSpawnPos = transform.Find("PoopSpawnPos");
        m_PoopShortSound = transform.Find("SoundShort").GetComponent<RandomSoundPlayer>();
        m_PoopLongSound = transform.Find("SoundLong").GetComponent<RandomSoundPlayer>();

        m_ShotsRemain = m_MaxShots;

        m_ShotsBarMaxScale = m_ShotsBar.transform.localScale;
        UpdateBar();
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
                if (m_PlaySoundCounter > 2)
                {
                    PlaySound(SoundLength.LONG);
                }
                ++m_PlaySoundCounter;
            }
        }
        else
        {
            RechargePoop();

            if (m_PlaySoundCounter > 0)
            {
                PlaySound(SoundLength.SHORT);
            }
            m_PlaySoundCounter = 0;
            m_PlaySoundCooldown -= Time.deltaTime;
        }
    }

    private void DoPoop(Vector2 add_v){
        m_ShootTimer = m_ShotInterval;
        if (m_ShotsRemain <= 0) { return; }
        m_ShotsRemain--;
        UpdateBar();

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
            UpdateBar();
        }
    }

    private void PlaySound(SoundLength length)
    {
        if (m_PlaySoundCooldown > 0.0f) { return; }
        m_PlaySoundCooldown = 0.3f;

        if (length == SoundLength.LONG)
        {
            m_PoopLongSound.PlayRandom();
        }
        else
        {
            m_PoopShortSound.PlayRandom();
        }
    }

    private void UpdateBar()
    {
        m_ShotsBar.transform.localScale = Mathf.Min(1.0f, (float)m_ShotsRemain / m_MaxShots) * m_ShotsBarMaxScale;

    }
}
