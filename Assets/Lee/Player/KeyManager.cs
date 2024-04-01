using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyManager : MonoBehaviour
{
    [SerializeField] Key [] keys;
    InputActionAsset inputSystem;

    /// <summary>
    /// 플레이어의 PlayerInput에서 인풋시스템을 가져와 키를 추가합니다.
    /// </summary>
    public void Awake()
    {
        inputSystem = GetComponent<PlayerInput>().actions;
        if ( inputSystem != null )
            MakeKeyAction();
        else
            Debug.LogError("InputActionAsset이 할당되지 않았습니다.");
    }

    /// <summary>
    /// KeyManager 액션 맵을 생성하고 키를 생성합니다.
    /// </summary>
    void MakeKeyAction()
    {
        //인풋시스템을 비활성화합니다.
        inputSystem.Disable();

        //액션 맵을 생성합니다. 
        InputActionMap actionMap = inputSystem.AddActionMap("KeyManager");

        if ( actionMap != null )
        {
            for ( int i = 0; i < keys.Length; i++ )
            {
                //키에 대한 이벤트를 받을 액션을 생성합니다.
                AddAction(keys [i], actionMap);
            }
        }
        else
        {
            Debug.LogError("Player 액션 맵을 찾을 수 없습니다.");
        }
        //인풋시스템을 활성화합니다.
        inputSystem.Enable();
    }

    /// <summary>
    /// 키를 추가하는 함수입니다.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="actionMap"></param>
    void AddAction( Key name, InputActionMap actionMap )
    {
        string inputActionName = name.ToString();
        //해당 키의 액션이 있는지 확인합니다.
        InputAction action = actionMap.FindAction(inputActionName);

        //해당 키의 액션이 없을 경우 액션을 생성합니다.
        if ( action == null )
            action = actionMap.AddAction(inputActionName, InputActionType.Button, binding: $"<Keyboard>/{inputActionName}");
        //액션에 이벤트를 추가합니다.
        action.started += ( InputAction.CallbackContext context ) => OnKeyLayer(name);
    }

    public long KeyLayer { get; private set; }

    /// <summary>
    /// 모든 키의 플래그를 초기화합니다.
    /// </summary>
    public void ResetKeyLayer() => KeyLayer = 0;
    /// <summary>
    /// 특정 키의 플래그를 올립니다.
    /// </summary>
    /// <param name="inputKey"></param>
    public void OnKeyLayer( Key onKey ) => KeyLayer |= ( ( long )1 << ( int )onKey );
    /// <summary>
    /// 매개변수에 입력된 키의 플래그가 올라와있는지 확인합니다.
    /// </summary>
    /// <param name="checkKey"></param>
    /// <returns></returns>
    public bool GetKeyCheck( Key checkKey )
    {
        long temp = ( ( long )1 << ( int )checkKey );
        return 0 < ( temp & KeyLayer );
    }

}
