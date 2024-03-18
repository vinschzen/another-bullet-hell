using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindMenuManager : MonoBehaviour
{
    public List<InputActionReference> listRef;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void onEnable()
    {
        foreach (InputActionReference item in listRef)
        {
            item.action.Disable();
        }
    }

    private void onDisable()
    {
        foreach (InputActionReference item in listRef)
        {
            item.action.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
