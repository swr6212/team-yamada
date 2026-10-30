using UnityEngine;

public class ScrollPlane : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float chunkLength = 10;
    [SerializeField] private int chunkCount = 3;
    [SerializeField] private float limitZ;

    void Update()
    {
        transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime);
        if(transform.position.z < limitZ)
        {
            transform.Translate(Vector3.forward * chunkCount * chunkLength);
        }
    }
}
