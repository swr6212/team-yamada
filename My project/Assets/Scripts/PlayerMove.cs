using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float wallMaxValue;
    [SerializeField]private float wallMinValue;
    private Vector2 inputDirections;
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
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
        float displacement = inputDirections.x * moveSpeed * Time.fixedDeltaTime;
        float newX = rigidbody.position.x + displacement;
        float restrictions = Mathf.Clamp(newX,wallMinValue, wallMaxValue);
        Vector3 movedPos = new Vector3(restrictions,rigidbody.position.y,rigidbody.position.z);
        rigidbody.MovePosition(movedPos);
        

    }

}
