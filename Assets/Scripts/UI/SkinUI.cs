using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinUI : MonoBehaviour
{
    public Image[] visualSkins;
    private int currentSkinId;

    private void Start()
    {
        currentSkinId = PlayerPrefs.GetInt(Constant.skinId, 0);
        SetThisSkin(currentSkinId);
    }

    public void SetThisSkin(int skinId)
    {
        PlayerPrefs.SetInt(Constant.skinId,skinId);
        visualSkins[currentSkinId].enabled = false;
        currentSkinId = skinId;
        visualSkins[currentSkinId].enabled = true;
    }
}
