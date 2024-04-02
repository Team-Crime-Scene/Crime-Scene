using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    // 오브젝트의 쿼터니언
    Quaternion m_rotation;
    private void Awake()
    {
        // 오브젝트의 회전을 쿼터니언으로 변경후 저장
        m_rotation = transform.rotation;
    }

    // 마우스커서가 있을때는 오브젝트가 안돌아가게해주고 있을땐 돌아가게함
    public void OnClick( InputValue value )
    {
        
        if ( Cursor.lockState == CursorLockMode.None )
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    // 마우스를 보일때와 안보일때 컨트롤해줌 
    public void OnRotationCon( InputValue value )
    {
        Vector2 input = value.Get<Vector2>();

        m_rotation.x = input.x;
        m_rotation.y = input.y;

    }

    // 마우스 움직임에 따라 바꿔줌
    public void RotationCon()
    {
        transform.Rotate(m_rotation.x, 0, 0);
        transform.Rotate(0, m_rotation.y, 0);
    }

    // 커서가 안보일때만 회전되게함
    private void Update()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        RotationCon();
    }

    // R키를 누르면 읽기 ui가 나오게해줌
    public void OnRead(InputValue value )
    {
        Canvas readKey = gameObject.GetComponentInChildren<Canvas>(true);
        if(readKey.enabled == false) 
        readKey.enabled = true;
        else
            readKey.enabled = false;
    }

}
