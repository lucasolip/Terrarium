using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UserListType
{
    FriendList,
    SearchList,
    PendingList,
    SendFoodList,
    ReceiveFoodList
}

public class UserListUI : MonoBehaviour
{
    public GameObject userElementPrefab;
    public UserListType listType;
    public DatabaseController database;
    public UserManager user;
    public InventoryController userInventory;
    public GameObject userList;
    UIAudioPlayer player;
    InputField inputField;
    ItemUI itemUI;
    VerticalAutoHeight autoHeight;
    LinkedList<UserListElementUI> selected;

    void Start()
    {
        selected = new LinkedList<UserListElementUI>();
        autoHeight = userList.GetComponent<VerticalAutoHeight>();
        switch (listType) {
            case UserListType.SearchList:
                inputField = transform.Find("InputPanel").Find("InputField").GetComponent<InputField>();
                break;
            case UserListType.SendFoodList:
                itemUI = transform.Find("ItemPanel").GetComponent<ItemUI>();
                player = GetComponent<UIAudioPlayer>();
                break;
        }
    }

    public void Search()
    {
        database.receivedUserListEvent += OnReceivedUserList;
        database.Search(inputField.text);
    }

    public void SendGift(string username, UserListElementUI userListElement)
    {
        if (itemUI.IsEmpty()) {
            player.Play(1);
            return;
        }
        database.receivedResponseEvent += OnReceivedResponse;
        selected.AddFirst(userListElement);
        database.SendGift(this.user.username, username, ScriptableObjectLocator.GetIndex(itemUI.GetCurrentItem()));
    }

    public void RequestFriend(string username, UserListElementUI userListElement) 
    {
        database.receivedResponseEvent += OnReceivedResponse;
        selected.AddFirst(userListElement);
        database.RequestFriend(this.user.username, username);
    }

    public void AcceptFriend(string username, UserListElementUI userListElement)
    {
        database.receivedResponseEvent += OnReceivedResponse;
        selected.AddFirst(userListElement);
        database.AcceptFriend(username, this.user.username);
    }

    public void OnReceivedResponse(bool acknowledged)
    {
        database.receivedResponseEvent -= OnReceivedResponse;
        switch (listType)
        {
            case UserListType.SendFoodList:
                player.Play(0);
                itemUI.PickItem();
                break;
            case UserListType.ReceiveFoodList:
                break;
            default:
                Destroy(selected.Last.Value.gameObject);
                selected.RemoveLast();
                break;
        }
    }

    public void OnReceivedUserList(List<DatabaseUser> response)
    {
        database.receivedUserListEvent -= OnReceivedUserList;
        if (response == null || response.Count == 0) return;
        ClearList();
        UserListElementUI userElement;
        foreach (DatabaseUser user in response) {
            if (!this.user.username.Equals(user.username)) {
                userElement = Instantiate(userElementPrefab, userList.transform).GetComponent<UserListElementUI>();
                userElement.SetUserList(this);
                userElement.SetTitle(user.username);
                userElement.SetSubtitle(TimeFormatter.Format(user.best_pet_birth_date, user.best_pet_death_date, database.format));
            }
        }
        autoHeight.Resize();
    }

    public void OnReceivedGiftList(List<DatabaseGift> response)
    {
        database.receivedGiftListEvent -= OnReceivedGiftList;
        if (response.Count == 0) return;
        UserListElementUI userElement;
        foreach (DatabaseGift user in response)
        {
            userElement = Instantiate(userElementPrefab, userList.transform).GetComponent<UserListElementUI>();
            userElement.SetUserList(this);
            userElement.SetTitle(user.from);
            ItemModel item = (ItemModel)ScriptableObjectLocator.Get(user.gift_index);
            userElement.SetSubtitle(item.itemName);
            userInventory.AddItem(item);
        }
        autoHeight.Resize();
    }

    private void OnEnable()
    {
        if (!this.user.username.Equals("")) {
            switch (listType) {
                case UserListType.PendingList:
                    database.receivedUserListEvent += OnReceivedUserList;
                    database.GetPending(this.user.username);
                    break;
                case UserListType.FriendList:
                    database.receivedUserListEvent += OnReceivedUserList;
                    database.GetFriends(this.user.username);
                    break;
                case UserListType.SendFoodList:
                    database.receivedUserListEvent += OnReceivedUserList;
                    database.GetFriends(this.user.username);
                    break;
                case UserListType.ReceiveFoodList:
                    database.receivedGiftListEvent += OnReceivedGiftList;
                    database.ReceiveGifts(this.user.username);
                    break;
            }
        }
    }

    public void ClearList()
    {
        for (int i = 0; i < userList.transform.childCount; i++) {
            Destroy(userList.transform.GetChild(i).gameObject);
        }
    }
}
