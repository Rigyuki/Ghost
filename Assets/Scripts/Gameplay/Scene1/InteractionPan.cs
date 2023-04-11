using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionPan : MonoBehaviour
{
    public Button InteractionButton;
    public Text InteractionText;
    private void Start()
    {
        
    }

    public void PanelText(string text) 
    {
        InteractionText.text = text;
    }

    public void PanelButton()
    {
        this.gameObject.SetActive(false);
    }
}
