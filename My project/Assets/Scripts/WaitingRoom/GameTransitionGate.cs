using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTransitionGate : MonoBehaviour
{
    [SerializeField] private string nextSceneName; // Ÿ‚ÌƒV[ƒ“–¼‚ğw’è‚·‚é‚½‚ß‚Ì•Ï”
   
    void OnTriggerEnter(Collider other)
    {
        PlayerMove player = other.GetComponentInParent<PlayerMove>();
       if(player != null)
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
