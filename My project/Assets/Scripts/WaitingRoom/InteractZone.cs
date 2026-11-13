using UnityEngine;
using UnityEngine.InputSystem;

public class InteractZone : MonoBehaviour
{
    [SerializeField] private GameObject interactUI;
    private PlayerMove playerMove;
    private bool isPlayerInZone = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && isPlayerInZone)
        {
            SetPanel(!interactUI.activeSelf);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       PlayerMove player = other.GetComponentInParent<PlayerMove>();
        if(player != null)
        {
            isPlayerInZone = true;
            playerMove = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMove player = other.GetComponentInParent<PlayerMove>();
        if(player != null)
        {
            isPlayerInZone = false;
            SetPanel(false);
            playerMove = null;
        }
    }

    private void SetPanel(bool open)
    {
        interactUI.SetActive(open);
        playerMove.SetMove(!open);
    }
}
