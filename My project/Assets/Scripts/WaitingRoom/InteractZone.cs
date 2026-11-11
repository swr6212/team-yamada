using UnityEngine;
using UnityEngine.InputSystem;

public class InteractZone : MonoBehaviour
{
    [SerializeField] private GameObject interactUI;
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
            interactUI.SetActive(!interactUI.activeSelf);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       PlayerMove player = other.GetComponentInParent<PlayerMove>();
        if(player != null)
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMove player = other.GetComponentInParent<PlayerMove>();
        if(player != null)
        {
            isPlayerInZone = false;
            interactUI.SetActive(false);
        }
    }
}
