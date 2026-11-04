using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private string titleSceneName; // タイトルシーン名（Tキーで移動）
    private bool isGameOver = false;

    void Start()
    {
        Time.timeScale = 1f; // ゲームを開始する
    }

    public void GameOver()
    {
        if(isGameOver) return; // すでにゲームオーバーの場合は処理を行わない
        isGameOver = true;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // ゲームを止める
    }

    void Update()
    {
        if(isGameOver)
        {
            if(Keyboard.current.rKey.wasPressedThisFrame)
            {
                // ゲームオーバーUIが表示されている状態でRキーが押された場合、ゲームを再起動する
                Time.timeScale = 1f; // ゲームを再開する
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if(Keyboard.current.tKey.wasPressedThisFrame)
            {
                Time.timeScale = 1f; // ゲームを再開する
                SceneManager.LoadScene(titleSceneName);
            }
        }
#if UNITY_EDITOR
        if(Keyboard.current.f1Key.wasPressedThisFrame)
        {
            GameOver();
        }
        
        if(Keyboard.current.f2Key.wasPressedThisFrame)
        {
            Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
            foreach(Enemy e in enemies)
            {
                e.TakeDamage(999);
            }
        }
#endif

    }
}
