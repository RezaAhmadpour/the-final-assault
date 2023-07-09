using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Enemy Explosion VFX/SFX
    [SerializeField] GameObject ExplosionFX;
    //Hitting Enemy VFX/SFX
    [SerializeField] GameObject HitFX;

    [SerializeField] int hitPoint;
    [SerializeField] int scorePerKill;


    void Start()
    {
        // Add Rigidbody So That Children's Colliders Are Also affected
        gameObject.AddComponent<Rigidbody>().useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        HitEnemy();
    }
    void HitEnemy()
    {

        if (--hitPoint > 0)
        {
            Instantiate(HitFX, transform.position, Quaternion.identity);
            return;
        }

        KillEnemy();
    }

    void KillEnemy()
    {
        AddScore();
        Instantiate(ExplosionFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void AddScore()
    {
        FindObjectOfType<GameManager>().CurrentScore += scorePerKill;
    }
}
