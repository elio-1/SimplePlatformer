using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextCollectable : MonoBehaviour
{
[SerializeField] private IntVariable collectable;
    [SerializeField] private TextMeshProUGUI text;

    private void Update()
    {
        text.text = collectable.variable.ToString();
    }
}
