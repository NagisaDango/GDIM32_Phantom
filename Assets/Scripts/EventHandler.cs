using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action<int> StartGameEvent;
    public static void CallStartGameEvent(int playerId)
    {
        StartGameEvent?.Invoke(playerId);
    }

    public static event Action<Player> PlayerSpawnEvent;
    public static void CallPlayerSpawnEvent(Player player)
    {
        PlayerSpawnEvent?.Invoke(player);
    }

    public static event Action<FarmManager> FeedClickedEvent;
    public static void CallFeedClickedEvent(FarmManager manager)
    {
        FeedClickedEvent?.Invoke(manager);
    }

    public static event Action<FarmManager> SellClickedEvent;
    public static void CallSellClickedEvent(FarmManager manager)
    {
        SellClickedEvent?.Invoke(manager);
    }

}
