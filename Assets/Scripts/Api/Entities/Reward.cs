using System;
using System.Collections.Generic;

[Serializable]
public class Reward
{
    public int id;
    public string name;
    public string image;
    public string image_url;
    public string description;
    public int price;
    public DateTime starts_on;
    public DateTime ends_on;
    public int stock;
    public string conditions;
    public int max_redemptions;
    public string transaction_text;
    public List<Transaction> transactions;
    public List<Transaction> user_transactions;
    public int total_transactions;
    public int total_user_transactions;
    public int total_user_transactions_left;
    public string status;
}

[Serializable]
public class RewardsResponse
{
    public List<Reward> rewards;
    public string prev;
    public string next;
}

