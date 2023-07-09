using UnityEngine;

public class DestroyDelay : MonoBehaviour
{
    [SerializeField] float delay;
    void Start()
    {
        Destroy(gameObject, delay);
    }
}
