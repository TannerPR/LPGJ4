using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
    public static LevelManager s_Instance = null;
    public static LevelManager instance
    {
        get { return s_Instance; }
    }

    public int m_CurrentLevel = 0;

    [SerializeField]
    private int m_FiresPerBookcase = 0;

    [SerializeField]
    private int m_NumberOfBookcases = 0;

    void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void AdvanceToNextLevel()
    {

    }
}
