using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class IconHandler : MonoBehaviour
{
    [SerializeField] private Image[] _icon;
    [SerializeField] private Color _usedColor;

    public void UseShot(int shotNumber)
    {
        for (int i = 0; i < _icon.Length; i++)
        {
            if (shotNumber == i + 1)
            {
                _icon[i].color = _usedColor;
                return;
            }
        }
    }
} 
