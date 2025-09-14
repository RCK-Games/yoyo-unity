using System;

[Serializable]
public class Transaction
{
    public int id;
    public int app_user_id;
    public int transactionable_id;
    public string transactionable_type;
    public string status;
    public string description;
    public string url;
    public DateTime created_at;
    public int points;
}

