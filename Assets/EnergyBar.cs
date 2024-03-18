using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public Image energyBarImage; // Reference to the energy bar image prefab
    public float fillSpeed = 30f; // Speed (units per second) at which the bar fills
    public Color emptyColor = Color.red; // Color displayed when the bar is empty

    private float targetWidth; // Target width of the full bar
    private float currentWidth; // Current width of the bar
    private bool isFilling; // Flag to indicate if the bar is filling
    private Color initialColor; // Initial color of the energy bar image

    private void Start()
    {
        targetWidth =  energyBarImage.rectTransform.sizeDelta.x;
        currentWidth = 0f;
        initialColor = energyBarImage.color;
    }

    private void Update()
    {
        if (isFilling)
        {
            currentWidth = Mathf.Clamp(currentWidth + fillSpeed * Time.deltaTime, 0f, targetWidth);

            energyBarImage.rectTransform.sizeDelta = new Vector2(currentWidth, energyBarImage.rectTransform.sizeDelta.y);

            if (currentWidth == targetWidth)
            {
                isFilling = false;
            }
        }

        if (currentWidth == 0f)
        {
            isFilling = true;
        }
    }

    public void EmptyBar()
    {
        currentWidth = 0f;
        isFilling = true;
    }

    public bool CheckEnergy()
    {
        if (currentWidth == targetWidth)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void flashRed() 
    {    
        energyBarImage.color = emptyColor;
        StartCoroutine(FlashColor());
    }

    IEnumerator FlashColor()
    {
        yield return new WaitForSeconds(0.1f); 
        energyBarImage.color = initialColor;
    }
}
