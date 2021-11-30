using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticle : MonoBehaviour
{
    public static PlayerParticle instance;

    [SerializeField]
    GameObject levelupVFX;
    [SerializeField]
    GameObject pickupVFX_Emoji;
    
    private void Awake()
    {
        instance = this;
    }
    public void PlaylevelUpVFX()
    {
        levelupVFX.SetActive(true);
    }
    public void PlayPickUpVFX()
    {
        pickupVFX_Emoji.SetActive(true);
    }

}
