using UnityEngine;

public class ThemeMusic : MonoBehaviour
{
    void Awake()
    {
        if(FindObjectsOfType<ThemeMusic>().Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }
}
