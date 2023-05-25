using UnityEngine;

public class SelectableFinder : MonoBehaviour
{
    [Header("Interaction Detection Settings")]
    [SerializeField] private float interactionCheckRayLength = 5f;
    [SerializeField] private LayerMask interactionCheckLayerMask = 1 << 3;

    private IHoverable currentHoverable = null;
    private ISelectable currentSelectable = null;

    private Camera mainCamera = null;
    private Vector2 currentMousePosition = Vector2.zero;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        currentMousePosition = Input.mousePosition;

        Ray _ray = mainCamera.ScreenPointToRay(currentMousePosition);
        bool _wasHit = Physics.Raycast(_ray.origin, _ray.direction, out RaycastHit _hit, interactionCheckRayLength, interactionCheckLayerMask);

        handleHoverable(_wasHit, _hit);
        handleSelectable(_wasHit, _hit);
    }

    private void handleHoverable(bool _wasHit, RaycastHit _hit)
    {
        if (_wasHit == false || _hit.collider.TryGetComponent(out IHoverable _foundHoverable) == false)
        {
            if (currentHoverable != null)
            {
                currentHoverable.OnPointerExit();
                currentHoverable = null;
            }

            return;
        }

        currentHoverable = _foundHoverable;
        currentHoverable.OnPointerEnter();
    }

    private void handleSelectable(bool _wasHit, RaycastHit _hit)
    {
        if (_wasHit == false || _hit.collider.TryGetComponent(out ISelectable _foundHoverable) == false)
        {
            if (Input.GetMouseButtonDown(0) == false)
            {
                return;
            }

            if (currentSelectable != null)
            {
                currentSelectable.OnDeselected();
                currentSelectable = null;
            }

            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (currentSelectable != null && currentSelectable != _foundHoverable)
            {
                currentSelectable.OnDeselected();
            }

            currentSelectable = _foundHoverable;
            currentSelectable.OnSelected(); 
        }
    }
}
