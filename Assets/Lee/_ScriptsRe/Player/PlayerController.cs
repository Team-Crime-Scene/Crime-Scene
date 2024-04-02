using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMove mover;
    [SerializeField] CamaraController camaraController;
    [SerializeField] InteractController interactController;

    private void Update()
    {
        if(interactController.IsZoomed == true)
        {
            mover.enabled = false;
            camaraController.enabled = false;
        }
        else if(interactController.IsZoomed == false)
        {
            mover.enabled =true;
            camaraController.enabled = true;
        }
    }
}
