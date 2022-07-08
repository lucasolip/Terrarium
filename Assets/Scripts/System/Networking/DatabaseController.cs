using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System;

public struct DatabaseUser
{
	public string _id;
	public string username;
	public string best_pet_birth_date;
	public string best_pet_death_date;
	public List<string> friends;
}

public struct DatabaseFriendRequest
{
	public string _id;
	public string from;
	public string to;
}

public struct DatabaseGift
{
	public string _id;
	public string from;
	public string to;
	public int gift_index;
}

public class DatabaseController : MonoBehaviour
{
	public string databaseURL;
	public string id;
	public int score;
	public string format = "dd/MM/yyyy HH:mm:ss";
	public Action<bool> receivedResponseEvent;
	public Action<List<DatabaseUser>> receivedUserListEvent;
	public Action<List<DatabaseGift>> receivedGiftListEvent;
	public Action<string> errorEvent;

	public void ValidName(string username)
    {
		StartCoroutine(ValidNameCoroutine(username));
    }

	public void InitializeUser(string username)
    {
		StartCoroutine(InitializeUserCoroutine(username));
	}

	public void UpdateUser(string username, DateTime? born, DateTime? died)
    {
		StartCoroutine(UpdateUserCoroutine(username, born == null? "null" : born.Value.ToString(format), died == null? "null" : died.Value.ToString(format)));
    }

	public void DeleteUser(string username)
    {
		StartCoroutine(DeleteUserCoroutine(username));
    }

	public void Search(string query)
    {
		StartCoroutine(SearchCoroutine(query));
    }

	public void RequestFriend(string from, string to)
	{
		StartCoroutine(RequestFriendCoroutine(from, to));
	}

	public void AcceptFriend(string from, string to)
	{
		StartCoroutine(AcceptFriendCoroutine(from, to));
	}

	public void GetPending(string username)
	{
		StartCoroutine(GetPendingCoroutine(username));
	}

	public void GetFriends(string username)
	{
		StartCoroutine(GetFriendsCoroutine(username));
	}

	public void SendGift(string from, string to, int gift)
    {
		StartCoroutine(SendGiftCoroutine(from, to, gift));
    }

	public void ReceiveGifts(string username)
    {
		StartCoroutine(ReceiveGiftsCoroutine(username));
    }

	private IEnumerator ValidNameCoroutine(string username)
    {
		using (UnityWebRequest request = UnityWebRequest.Get(databaseURL)) {
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
				Debug.Log(request.error);
				if (errorEvent != null) errorEvent(request.error);
            } else {
				List<DatabaseUser> users = JsonConvert.DeserializeObject<List<DatabaseUser>>(request.downloadHandler.text);
				foreach (DatabaseUser user in users) {
					if (username.Equals(user.username)) {
						if (receivedResponseEvent != null) receivedResponseEvent(false);
						yield break;
					}
				}
				if (receivedResponseEvent != null) receivedResponseEvent(true);
			}
		}
	}

	private IEnumerator InitializeUserCoroutine(string username)
	{
		WWWForm form = new WWWForm();
		form.AddField("username", username);
		form.AddField("bestBorn", "null");
		form.AddField("bestDied", "null");

		using (UnityWebRequest request = UnityWebRequest.Post(databaseURL, form))
		{
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
				Debug.Log(request.error);
				if (errorEvent != null) errorEvent(request.error);
			}
			else {
				Debug.Log("User initialized in database");
            }
		}
	}

	private IEnumerator UpdateUserCoroutine(string username, string born, string died)
	{
		WWWForm form = new WWWForm();
		form.AddField("bestBorn", born);
		form.AddField("bestDied", died);

		using (UnityWebRequest request = UnityWebRequest.Post(databaseURL + "/" + username, form))
		{
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.Log(request.error);
				if (errorEvent != null) errorEvent(request.error);
			}
			else
			{
				Debug.Log("User updated in database");
			}
		}
	}

	private IEnumerator DeleteUserCoroutine(string username)
	{
		WWWForm form = new WWWForm();

		using (UnityWebRequest request = UnityWebRequest.Delete(databaseURL + "/" + username))
		{
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.Log(request.error);
				if (errorEvent != null) errorEvent(request.error);
			}
			else
			{
				Debug.Log("User deleted in database");
				if (receivedResponseEvent != null) receivedResponseEvent(true);
			}
		}
	}

	private IEnumerator SearchCoroutine(string username)
	{
		using (UnityWebRequest request = UnityWebRequest.Get(databaseURL + "/" + username))
		{
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.Log(request.error);
				if (errorEvent != null) errorEvent(request.error);
			}
			else
			{
				List<DatabaseUser> users = JsonConvert.DeserializeObject<List<DatabaseUser>>(request.downloadHandler.text);
				if (receivedUserListEvent != null) receivedUserListEvent(users);
			}
		}
	}

	private IEnumerator GetPendingCoroutine(string username)
	{
		using (UnityWebRequest request = UnityWebRequest.Get(databaseURL + "/request/" + username)) {
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError) {
				Debug.Log(request.error);
				if (errorEvent != null) errorEvent(request.error);
			} else {
				List<DatabaseFriendRequest> pendingUsers = JsonConvert.DeserializeObject<List<DatabaseFriendRequest>>(request.downloadHandler.text);
				List<string> pendingUsersString = new List<string>();
				foreach (DatabaseFriendRequest user in pendingUsers) {
					pendingUsersString.Add(user.from);
                }

				using (UnityWebRequest newRequest = UnityWebRequest.Get(databaseURL)) {
					yield return newRequest.SendWebRequest();

					if (newRequest.result == UnityWebRequest.Result.ConnectionError || newRequest.result == UnityWebRequest.Result.ProtocolError) {
						Debug.Log(newRequest.error);
						if (errorEvent != null) errorEvent(newRequest.error);
					} else {
						List<DatabaseUser> allUsers = JsonConvert.DeserializeObject<List<DatabaseUser>>(newRequest.downloadHandler.text);
						List<DatabaseUser> users = new List<DatabaseUser>();
						foreach (DatabaseUser user in allUsers) {
							if (pendingUsersString.Contains(user.username)) {
								users.Add(user);
                            }
						}
						if (receivedUserListEvent != null) receivedUserListEvent(users);
					}
				}
			}
		}
	}

	private IEnumerator GetFriendsCoroutine(string username)
	{
		using (UnityWebRequest request = UnityWebRequest.Get(databaseURL + "/friends/" + username))
		{
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.Log(request.error);
				if (errorEvent != null) errorEvent(request.error);
			}
			else
			{
				List<DatabaseUser> users = new List<DatabaseUser>();
				Debug.Log(request.downloadHandler.text);
				DatabaseUser requestedUser = JsonConvert.DeserializeObject<DatabaseUser>(request.downloadHandler.text);
				List<string> friends = new List<string>();
				if (requestedUser.friends == null || requestedUser.friends.Count == 0)
                {
					if (receivedUserListEvent != null) receivedUserListEvent(users);
					yield break;
				}
				foreach (string friend in requestedUser.friends)
				{
					friends.Add(friend);
				}

				using (UnityWebRequest newRequest = UnityWebRequest.Get(databaseURL))
				{
					yield return newRequest.SendWebRequest();

					if (newRequest.result == UnityWebRequest.Result.ConnectionError || newRequest.result == UnityWebRequest.Result.ProtocolError)
					{
						Debug.Log(newRequest.error);
						if (errorEvent != null) errorEvent(newRequest.error);
					}
					else
					{
						List<DatabaseUser> allUsers = JsonConvert.DeserializeObject<List<DatabaseUser>>(newRequest.downloadHandler.text);
						foreach (DatabaseUser user in allUsers)
						{
							if (friends.Contains(user.username)) {
								users.Add(user);
                            }
						}
						if (receivedUserListEvent != null) receivedUserListEvent(users);
					}
				}
			}
		}
	}

	private IEnumerator RequestFriendCoroutine(string from, string to)
	{
		WWWForm form = new WWWForm();
		form.AddField("username", from);
		Debug.Log(from + ", " + to);

		using (UnityWebRequest request = UnityWebRequest.Post(databaseURL + "/request/" + to, form))
		{
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.Log(request.error);
				if (errorEvent != null) errorEvent(request.error);
			} else {
				Debug.Log("Sent request");
				if (receivedResponseEvent != null) receivedResponseEvent(true);
			}
		}
	}

	private IEnumerator AcceptFriendCoroutine(string from, string to)
	{
		WWWForm form = new WWWForm();
		form.AddField("username", from);

		using (UnityWebRequest request = UnityWebRequest.Post(databaseURL + "/accept/" + to, form))
		{
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.Log(request.error);
				if (errorEvent != null) errorEvent(request.error);
			}
			else
			{
				Debug.Log("Sent request");
				if (receivedResponseEvent != null) receivedResponseEvent(true);
			}
		}
	}

	private IEnumerator SendGiftCoroutine(string from, string to, int gift)
	{
		WWWForm form = new WWWForm();
		form.AddField("username", from);
		form.AddField("gift", gift);
		Debug.Log(from + ", " + to + ": " + gift);

		using (UnityWebRequest request = UnityWebRequest.Post(databaseURL + "/gifts/" + to, form))
		{
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.Log(request.error);
				if (errorEvent != null) errorEvent(request.error);
			}
			else
			{
				Debug.Log("Sent gift");
				if (receivedResponseEvent != null) receivedResponseEvent(true);
			}
		}
	}

	private IEnumerator ReceiveGiftsCoroutine(string username)
	{
		using (UnityWebRequest request = UnityWebRequest.Get(databaseURL + "/gifts/" + username))
		{
			yield return request.SendWebRequest();

			if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
			{
				Debug.Log(request.error);
				if (errorEvent != null) errorEvent(request.error);
			}
			else
			{
				List<DatabaseGift> gifts = JsonConvert.DeserializeObject<List<DatabaseGift>>(request.downloadHandler.text);
				if (receivedGiftListEvent != null) receivedGiftListEvent(gifts);
			}
		}
	}
}
