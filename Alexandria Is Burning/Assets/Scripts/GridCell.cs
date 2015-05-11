using UnityEngine;
using System.Collections;

public class GridCell : MonoBehaviour
{
    public bool m_IsABook = false;
    public bool m_IsOnFire = false;
    public bool m_HasStartedSmoking = false;

    public float m_TimeToSwitchFromSmokeToFire = 5.0f;
    public float m_TimeToSwitchFromFireToInferno = 5.0f;

    private float m_SmokeTimer;
    private float m_FireTimer;

    public GameObject m_SmokeEmitterObject;
    public GameObject m_SmallFireEmitterObject;
    public GameObject m_LargeFireEmitterObject;

    private GameObject m_SmokeEmitter;
    private GameObject m_SmallFireEmitter;
    private GameObject m_LargeFireEmitter;

    private GameObject m_Emitter;

    public enum BookState
    {
        Normal,
        Smoking,
        OnFire,
        Inferno
    }

    public BookState m_CurrentBookState = BookState.Normal;

    void Start()
    {
        m_SmokeTimer = m_TimeToSwitchFromSmokeToFire;
        m_FireTimer = m_TimeToSwitchFromFireToInferno;
    }

    void Update()
    {
        if (!m_IsABook)
        {
            return;
        }

        switch (m_CurrentBookState)
        {
            case BookState.Normal:
                break;
            case BookState.Smoking:
                if (m_SmokeTimer <= 0)
                {
                    ChangeBookState(BookState.OnFire);
                    m_SmokeTimer = m_TimeToSwitchFromSmokeToFire;
                }
                m_SmokeTimer -= Time.deltaTime;
                break;
            case BookState.OnFire:
                if (m_FireTimer <= 0)
                {
                    ChangeBookState(BookState.Inferno);
                    m_FireTimer = m_TimeToSwitchFromFireToInferno;
                }
                m_FireTimer -= Time.deltaTime;
                break;
            case BookState.Inferno:
                break;
        }
    }

    public void ChangeBookState(BookState state)
    {
        if (!m_IsABook)
        {
            return;
        }
        m_CurrentBookState = state;

        switch (state)
        {
            case BookState.Normal:
                Reset();
                break;
            case BookState.Smoking: ;
                m_IsOnFire = false;
                m_HasStartedSmoking = true;
                m_SmokeEmitter = (GameObject)Instantiate(m_SmokeEmitterObject, transform.position, Quaternion.identity);
                m_SmokeEmitter.transform.position = new Vector3(
                    m_SmokeEmitter.transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2,
                    m_SmokeEmitter.transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2,
                    -0.1f);
                break;
            case BookState.OnFire:
                m_IsOnFire = true;
                m_SmallFireEmitter = (GameObject)Instantiate(m_SmallFireEmitterObject, transform.position, Quaternion.identity);
                m_SmallFireEmitter.transform.position = new Vector3(
                    m_SmallFireEmitter.transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2,
                    m_SmallFireEmitter.transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2,
                    -0.2f);
                break;
            case BookState.Inferno:
                m_LargeFireEmitter = (GameObject)Instantiate(m_LargeFireEmitterObject, transform.position, Quaternion.identity);
                m_LargeFireEmitter.transform.position = new Vector3(
                    m_LargeFireEmitter.transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2,
                    m_LargeFireEmitter.transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2,
                    -0.2f);
                break;
        }
    }

    void OnMouseDown()
    {
        if (m_CurrentBookState == BookState.Normal) { ChangeBookState(BookState.Smoking); }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!m_IsABook)
        {
            return;
        }

        if(other.tag == "Breath")
        {
            switch(m_CurrentBookState)
            {
                case BookState.Smoking:
                    ChangeBookState(BookState.Normal);
                    Destroy(m_SmokeEmitter);
                    break;
                case BookState.OnFire:
                    ChangeBookState(BookState.Normal);
                    Destroy(m_SmallFireEmitter);
                    break;
                case BookState.Inferno:
                    ChangeBookState(BookState.Normal);
                    Destroy(m_LargeFireEmitter);
                    break;
            }
        }
    }

    void Reset()
    {
        m_IsOnFire = false;
        m_HasStartedSmoking = false;
        m_SmokeTimer = m_TimeToSwitchFromSmokeToFire;
        m_FireTimer = m_TimeToSwitchFromFireToInferno;

        if (m_SmokeEmitter != null) { Destroy(m_SmokeEmitter); }
        if (m_SmallFireEmitter != null) { Destroy(m_SmallFireEmitter); }
        if (m_LargeFireEmitter != null) { Destroy(m_LargeFireEmitter); }
    }


}
