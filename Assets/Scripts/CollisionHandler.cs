using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //Explosion VFX/SFX
    [SerializeField] GameObject explosionFX;
    void OnTriggerEnter (Collider collider)
    {
        SaveScore();
        if (collider.tag.Equals("FinishCollider"))
        {
            Finish();
            return;
        }
        Crash();
    }

    void Crash()
    {
        PlayerController p = GetComponent<PlayerController>();
        p.enabled = false;
        p.SetLasersActive(false);
        GetComponent<MeshRenderer>().enabled = false;
        foreach (var col in GetComponentsInChildren<Collider>()) { col.enabled = false; }
        Destroy(GameObject.Find("Master Timeline").gameObject);

        Instantiate(explosionFX, transform.position, Quaternion.identity);
        Invoke("LoadMainMenu", 1.5f);
    }

    void Finish()
    {
        Invoke("LoadMainMenu", 2f);
    }
    void SaveScore()
    {
        int currentScore = FindObjectOfType<GameManager>().CurrentScore;
        if (currentScore > PlayerPrefs.GetInt("BestScore", 0)) 
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
        }
    }
    void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
