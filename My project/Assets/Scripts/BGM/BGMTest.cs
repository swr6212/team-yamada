using UnityEngine;

public class BGMTest : MonoBehaviour
{
    [SerializeField] BGMManager bgmManager;
    [SerializeField] AudioClip bgmClip;
    
    public void OnClick()
    {
        bgmManager.Play(bgmClip,1f);
    }
}
