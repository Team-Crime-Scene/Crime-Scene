using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [SerializeField] float moveSpeed;
    [SerializeField] float RunSpeed;

    private Vector3 moveDir;
    private bool isRun;
    private void Update()
    {
        Move();
    }

    public void Move()
    {
        if ( isRun )
        {
            controller.Move(transform.forward * moveDir.z * RunSpeed * Time.deltaTime);
            controller.Move(transform.right * moveDir.x * RunSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(transform.forward * moveDir.z * moveSpeed * Time.deltaTime);
            controller.Move(transform.right * moveDir.x * moveSpeed * Time.deltaTime);
        }
    }

    public void OnRun( InputValue value )
    {

        if ( value.isPressed )
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }
    }

    public void OnMove( InputValue value )
    {
        Vector2 input = value.Get<Vector2>();
        moveDir.x = input.x;
        moveDir.z = input.y;
    }
}
