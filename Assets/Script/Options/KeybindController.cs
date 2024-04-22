    using UnityEngine;
    using UnityEngine.InputSystem;
    using TMPro;
using UnityEngine.UI;

public class KeybindController : MonoBehaviour
    {
    [SerializeField] private Button rebindButton; 
    [SerializeField] private TextMeshProUGUI keybindText; 
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private string actionToRebind;

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