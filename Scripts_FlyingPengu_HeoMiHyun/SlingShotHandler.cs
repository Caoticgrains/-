using UnityEngine;
using UnityEngine.InputSystem;


public class SlingShotHandler : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _centerPosition;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private holyCrab _holycrabPrefab;
    [SerializeField] private float _shotForce = 10f;

    private holyCrab _spawnedHolyCrab;
    private Vector2 _direction;
    private bool _isDragging;

    private void Awake()
    {
        _lineRenderer.enabled = false; // 선 숨기기
    }

    void Update()
    {
        if (_isDragging)
        {
            DrawSlingshotLine();
            UpdateCrabPosition();
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            StartDragging();
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (GameManager.instance.HasEnoughShots())
            {
                ReleaseCrab();
                GameManager.instance.UseShot();
            }
        }
    }

    private void StartDragging()
    {
        _isDragging = true;
        _spawnedHolyCrab = Instantiate(_holycrabPrefab, _startPosition.position, Quaternion.identity);
        _lineRenderer.enabled = true;
    }

    private void DrawSlingshotLine()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, Camera.main.nearClipPlane));
        mousePosition.z = 0;

        _direction = (_centerPosition.position - mousePosition);
        _lineRenderer.SetPosition(0, _startPosition.position);
        _lineRenderer.SetPosition(1, mousePosition);
    }

    private void UpdateCrabPosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, Camera.main.nearClipPlane));
        mousePosition.z = 0;
        _spawnedHolyCrab.transform.position = mousePosition;
    }

    private void ReleaseCrab()
    {
        _isDragging = false;
        _lineRenderer.enabled = false;

        // 던지는 힘 계산
        Vector2 releaseDirection = _direction.normalized;
        _spawnedHolyCrab.LaunchCrab(releaseDirection, _shotForce);
    }
}