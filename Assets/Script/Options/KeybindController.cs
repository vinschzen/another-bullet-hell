    using UnityEngine;
    using UnityEngine.InputSystem;
    using TMPro;
using UnityEngine.UI;

public class KeybindController : MonoBehaviour
    {
    [SerializeField] private Button rebindButton; // Reference to the rebind UI button
    [SerializeField] private TextMeshProUGUI keybindText; // Reference to the TextMeshProUGUI for displaying keybind
    [SerializeField] private InputActionAsset inputActions; // Reference to your InputActionAsset
    [SerializeField] private string actionToRebind; // Name of the action to rebind

    private InputAction RebindAction; 

    private void Awake()
    {
        rebindButton.onClick.AddListener(OnRebindButtonClicked); 
        RebindAction = inputActions.FindAction(actionToRebind); 
        if (RebindAction == null)
        {
            Debug.LogError("Action to rebind not found in InputActionAsset!");
            return;
        }
    }

    private void OnRebindButtonClicked()
    {
        keybindText.text = "Press any button";
        // RebindAction.PerformInteraction(InputActionPerformace.Rebind); 
    }

    private void OnDisable()
    {
        rebindButton.onClick.RemoveListener(OnRebindButtonClicked); 
    }

    private void OnActionRebound(InputAction.CallbackContext context)
    {
        keybindText.text = context.control.shortDisplayName;
    }
    }