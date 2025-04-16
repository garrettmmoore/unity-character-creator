using UnityEngine;

namespace CharacterCreator
{
    /// <summary>
    /// Allows rotating a character by clicking and dragging, and optionally auto-rotating when not being manipulated.
    /// Attach this script to the character game object that should be rotated.
    /// </summary>
    public class CharacterRotator : MonoBehaviour
    {
        [Header("Manual Rotation Settings")]
        [Tooltip("Speed multiplier for manual rotation")]
        [SerializeField] private float manualRotationSpeed = 10f;
        
        [Tooltip("The layers that should be considered for character detection")]
        [SerializeField] private LayerMask characterLayers = -1; // Default to "Everything"
        
        [Tooltip("Optional reference to a specific collider to use for detection")]
        [SerializeField] private Collider characterCollider;
        
        [Header("Auto Rotation Settings")]
        [Tooltip("Enable/disable automatic rotation when not manually rotating")]
        [SerializeField] private bool autoRotateEnabled = true;
        
        [Tooltip("Speed of automatic rotation in degrees per second")]
        [SerializeField] private float autoRotationSpeed = 30f;
        
        [Tooltip("Direction of auto-rotation: positive = clockwise, negative = counter-clockwise")]
        [SerializeField] private float autoRotationDirection = 1f;
        
        // Internal tracking variables
        private bool isDragging;
        private float previousMouseX;
        private Camera mainCamera;
        
        // Timer for resuming auto-rotation after manual interaction
        private float autoRotationResumeTimer;
        [SerializeField] private float autoRotationResumeDelay = 0.5f;
        
        private void Awake()
        {
            mainCamera = Camera.main;
            
            // If no specific collider was assigned, check if there's a collider on this object
            if (!characterCollider)
            {
                characterCollider = GetComponent<Collider>();
            }
        }
        
        private void Update()
        {
            // Ensure we have a camera
            if (!mainCamera)
            {
                mainCamera = Camera.main;
                if (!mainCamera) return; // Can't do anything without a camera
            }
            
            // Handle initial click
            if (Input.GetMouseButtonDown(0))
            {
                TryStartDrag();
            }
            
            // Handle release
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                autoRotationResumeTimer = 0f; // Reset timer to resume auto-rotation after delay
            }
            
            // Handle dragging for manual rotation
            if (isDragging)
            {
                RotateCharacterManually();
            }
            // Handle auto-rotation when not dragging
            else if (autoRotateEnabled)
            {
                // Only auto-rotate after a delay from manual rotation
                if (autoRotationResumeTimer >= autoRotationResumeDelay)
                {
                    RotateCharacterAutomatically();
                }
                else
                {
                    autoRotationResumeTimer += Time.deltaTime;
                }
            }
        }
        
        private void TryStartDrag()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, characterLayers))
            {
                // Check if we hit this object or any of its children
                Transform hitTransform = hit.transform;
                
                
                // Check if the hit object is part of this character
                if (hitTransform == transform || 
                    hitTransform.IsChildOf(transform) || 
                    (characterCollider != null && hit.collider == characterCollider))
                {
                    isDragging = true;
                    previousMouseX = Input.mousePosition.x;
                }
            }
        }
        
        private void RotateCharacterManually()
        {
            // Calculate mouse movement delta
            float deltaX = Input.mousePosition.x - previousMouseX;
            
            // Rotate the character around its Y-axis (left/right)
            // Negative deltaX means rotating clockwise when moving right
            transform.Rotate(Vector3.up, -deltaX * manualRotationSpeed * Time.deltaTime);
            
            // Update previous mouse position
            previousMouseX = Input.mousePosition.x;
        }
        
        private void RotateCharacterAutomatically()
        {
            // Simple continuous rotation around the Y-axis
            transform.Rotate(Vector3.up, autoRotationSpeed * autoRotationDirection * Time.deltaTime);
        }
    }
}