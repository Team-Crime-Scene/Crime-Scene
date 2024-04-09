using UnityEngine.InputSystem;

public interface IRotatable
{
    public void Rotate();
    public void GetRotationInput( InputValue inputValue );
}
