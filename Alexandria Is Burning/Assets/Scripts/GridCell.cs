using UnityEngine;
using System.Collections;

public class GridCell : MonoBehaviour 
{
    public bool m_IsABook = false;
    public bool m_IsOnFire = false;

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

	void Start () 
    {
        m_SmokeTimer = m_TimeToSwitchFromSmokeToFire;
        m_FireTimer = m_TimeToSwitchFromFireToInferno;
	}
	
	void Update () 
    {
	    switch(m_CurrentBookState)
        {
            case BookState.Normal:
                break;
            case BookState.Smoking:
                if(m_SmokeTimer <= 0)
                {
                    ChangeBookState(BookState.OnFire);
                    m_SmokeTimer = m_TimeToSwitchFromSmokeToFire;
                }
                m_SmokeTimer -= Time.deltaTime;
                break;
            case BookState.OnFire:
                if(m_FireTimer <= 0)
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

    void ChangeBookState(BookState state)
    {
        m_CurrentBookState = state;

        switch (state)
        {
            case BookState.Normal:
                m_IsOnFire = false;
                break;
            case BookState.Smoking:;
                m_IsOnFire = false;
                break;
            case BookState.OnFire:
                m_IsOnFire = true;
                break;
            case BookState.Inferno:
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {

    }


}
