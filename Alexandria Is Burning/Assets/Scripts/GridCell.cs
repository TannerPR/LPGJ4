using UnityEngine;
using System.Collections;

public class GridCell : MonoBehaviour
{
    public bool m_IsABook = false;
    public bool m_IsOnFire = false;
    public bool m_HasStartedSmoking = false;

    public bool m_Test = false;

    public float m_TimeToSwitchFromSmokeToFire = 5.0f;
    public float m_TimeToSwitchFromFireToInferno = 5.0f;

    private float m_SmokeTimer;
    private float m_FireTimer;

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

        if (m_Test)
        {
            ChangeBookState(BookState.Smoking);
            m_Test = false;
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
                GetComponent<SpriteRenderer>().color = Color.gray;
                break;
            case BookState.OnFire:
                m_IsOnFire = true;
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case BookState.Inferno:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
        }
    }

    void OnMouseDown()
    {
        ChangeBookState(BookState.Smoking);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!m_IsABook)
        {
            return;
        }
    }

    void Reset()
    {
        m_IsOnFire = false;
        m_HasStartedSmoking = false;
        m_SmokeTimer = m_TimeToSwitchFromSmokeToFire;
        m_FireTimer = m_TimeToSwitchFromFireToInferno;
        GetComponent<SpriteRenderer>().color = Color.white;
    }


}
