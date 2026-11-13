using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float wallMaxX;
    [SerializeField]private float wallMinX;
    [SerializeField] private float wallMinZ;
    [SerializeField] private float wallMaxZ;
    private Vector2 inputDirections;
    private Rigidbody rb;
    private bool isMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //  ì¸óÕÇ™Ç»Ç≠Ç»Ç¡ÇΩÇÁÅAà⁄ìÆÇµÇ»Ç¢
        if (context.canceled)
        {
            inputDirections = Vector2.zero;
            return;
        }

        inputDirections = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    { 
        if (!isMove) return;

        float displacement = inputDirections.x * moveSpeed * Time.fixedDeltaTime;
        float disZ = inputDirections.y * moveSpeed * Time.fixedDeltaTime;
        float newX = rb.position.x + displacement;
        float newZ = rb.position.z + disZ;
        float restrictions = Mathf.Clamp(newX,wallMinX, wallMaxX);
        float resZ = Mathf.Clamp(newZ, wallMinZ, wallMaxZ);
        Vector3 movedPos = new Vector3(restrictions,rb.position.y,resZ);
        rb.MovePosition(movedPos);
    }

    public void SetMove(bool move)
    {
        isMove = move;
        if(!move)
        {
            inputDirections = Vector2.zero;
        }
    }
}
