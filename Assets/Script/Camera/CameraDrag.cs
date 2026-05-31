using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraDrag : MonoBehaviour
{
    private Vector3 orgin;
    private Vector3 difference;

    private Camera _mainCamera;

    private bool isDraging;


    private void Awake()
    {
        _mainCamera = Camera.main;
    }


    public void OnDrag(InputAction.CallbackContext ctx)
    {
        if(ctx.started) orgin = GetMousePos;
        isDraging = ctx.started  || ctx.performed;


    }

    private void LateUpdate()
    {
        if (!isDraging) return;

        difference = GetMousePos - transform.position;
        transform.position = orgin - difference;

    }

    private Vector3 GetMousePos => _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
}
