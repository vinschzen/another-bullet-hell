using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
  public Image healthSegmentPrefab; 

  private List<Image> healthSegments; 

  private PlayerData data;
  private void Start()
  {
    try
    {
        this.data = SaveManager.Instance.CurrentSave;
    }
    catch (System.Exception)
    {
        string path = Application.persistentDataPath + "/playerData_1.json";
        string json = File.ReadAllText(path);
        this.data = JsonUtility.FromJson<PlayerData>(json);
    }
    int health = data.stats[2].value ;
    healthSegments = new List<Image>();
    float initialWidth = 300;
    float segmentWidth = (initialWidth / health) - 7.5f;


    for (int i = 0; i < health; i++)
    {
      Image segment = Instantiate(healthSegmentPrefab, transform);
      float xPos = (segmentWidth + 7.5f) * i - 209.5f;
      segment.rectTransform.anchoredPosition = new Vector2(xPos, 0);
      segment.rectTransform.sizeDelta = new Vector2(segmentWidth, healthSegmentPrefab.rectTransform.sizeDelta.y);
      healthSegments.Add(segment);
    }

    Destroy(healthSegmentPrefab);
  }

  public void DecrementHealth()
  {
    if (healthSegments.Count > 0)
    {
      Destroy(healthSegments[healthSegments.Count - 1].gameObject);
      healthSegments.RemoveAt(healthSegments.Count - 1);
    }
  }
}
