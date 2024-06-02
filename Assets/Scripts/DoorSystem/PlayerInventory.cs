using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField]
    private List<string> keys = new List<string>();

    [SerializeField]
    private Image redKeyImage, blueKeyImage;

    private void Start()
    {
        UpdateUI();
    }

    public void CollectKey(string keyColor)
    {
        keys.Add(keyColor);
        Debug.Log("Collected Key: " + keyColor);
        UpdateUI();
    }

    public void RemoveKey(string keyColor)
    {
        keys.Remove(keyColor);
        UpdateUI();
    }

    public bool HasKey(string keyColor)
    {
        return keys.Contains(keyColor);
    }

    private void UpdateUI()
    {
        if (HasKey("Red"))
        {
            redKeyImage.gameObject.SetActive(true);
        }
        else
        {
            redKeyImage.gameObject.SetActive(false);
        }
        if (HasKey("Blue"))
        {
            blueKeyImage.gameObject.SetActive(true);
        }
        else
        {
            blueKeyImage.gameObject.SetActive(false);
        }
    }
}
