using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserListElementUI : MonoBehaviour
{
    public string subtitleString = "Récord: ";
    Text title, subtitle;
    UserListUI userList;

    void Awake()
    {
        title = transform.Find("UserInfo").Find("Username").GetComponent<Text>();
        subtitle = transform.Find("UserInfo").Find("UserRecord").GetComponent<Text>();
    }

    public void SetTitle(string title)
    {
        this.title.text = title;
    }

    public void SetUserList(UserListUI userList)
    {
        this.userList = userList;
    } 

    public void SetSubtitle(string subtitle)
    {
        this.subtitle.text = subtitleString + subtitle;
    }

    public string GetTitle()
    {
        return title.text;
    }

    public void RequestFriend()
    {
        userList.RequestFriend(this.title.text, this);
    }

    public void AcceptFriend()
    {
        userList.AcceptFriend(this.title.text, this);
    }

    public void SendGift()
    {
        userList.SendGift(this.title.text, this);
    }

    public void DestroyElement()
    {
        Destroy(gameObject);
    }
}
