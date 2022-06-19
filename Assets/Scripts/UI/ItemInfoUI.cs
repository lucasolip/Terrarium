using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    RawImage icon;
    Text title, description;
    void Start()
    {
        icon = transform.Find("ItemIcon").GetComponent<RawImage>();
        title = transform.Find("ItemInfo").Find("ItemName").GetComponent<Text>();
        description = transform.Find("ItemInfo").Find("ItemDescription").GetComponent<Text>();
        icon.texture = TextureLocator.nullTexture;
        title.text = "";
        description.text = "";
    }

    public void SetInfo(string title, string description, Texture texture)
    {
        icon.texture = texture;
        this.title.text = title;
        this.description.text = description;
    }
}
