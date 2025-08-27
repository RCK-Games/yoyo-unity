#region Entities
using NUnit.Framework;
using System.Collections.Generic;

public class ClientUser
{ 
    public int id { get; set; }
    public string name { get; set; }
    public string email { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
    public string phone { get; set; }
    public int points { get; set; }
}

public class Reward
{
    public int id { get; set; }
    public string name { get; set; }
    public string image { get; set; }
    public string image_url { get; set; }
    public string description { get; set; }
    public int price { get; set; }
    public string starts_on { get; set; }
    public string ends_on { get; set; }
    public int stock {  get; set; }
    public string conditions { get; set; }
    public int max_redemptions { get; set; }
    public string transaction_text { get; set; }
    public Transaction[] transactions { get; set; }
    public Transaction[] user_transactions { get; set; }
    public int total_transactions { get; set; }
    public int total_user_transactions { get; set; }
    public int total_user_transactions_left { get; set; }
    public string status { get; set; }
}

public class Transaction
{
    public int id { get; set; }
    public int app_user_id {  get; set; }
    public int transactionable_id { get; set; }
    public string transactionable_type { get; set; }
    public string status { get; set; }
    public string description {  get; set; }
    public string url { get; set; }
    public string created_at { get; set; }
    public int points { get; set; }
}

public class SocialMedia
{
    public int id { get; set; }
    public string facebook { get; set; }
    public string instagram { get; set; }
    public string tiktok { get; set; }
    public string x { get; set; }
    public string youtube { get; set; }
}

public class Code
{
    public int id { get; set; }
    public string code { get; set; }
    public int stock { get; set; }
    public int max_redemptions { get; set; }
    public Transaction[] redemptions { get; set; }
}

public class Place
{
    public int id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string image {  get; set; }
    public string image_url { get; set; }
    public string website { get; set; }
    public SocialMedia social_media { get; set; }
    public string dress_code { get; set; }
    public string[] schedule { get; set; }
    public string[] payment_options { get; set; }
    public string address_text { get; set; }
    public string address_link { get; set; }
}

public class Media
{

}
#endregion

#region JSON Body Entities
public class ErrorMessageResponseJSON
{
    public string message { get; set; }
}

public class SignInJSONBody
{
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
    public string phone { get; set; }
    public int points { get; set; }
    public string access_code { get; set; }
    public bool related { get; set; }
}

public class LogInJSONBody
{
    public string name { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public int age { get; set; }
    public string gender { get; set; }
    public string phone { get; set; }
    public int points { get; set; }
    public string access_code { get; set; }
    public bool related { get; set; }
}

public class LogInResponseJSON
{
    public User user { get; set; }
    public string token { get; set; }
}

public class ResetPasswordJSONBody
{
    public string email { get; set; }
}

public class CheckAccessCodeJSONBody
{
    public string code { get; set; }
}

public class CheckAccessCodeResponseJSON
{
    public bool valid { get; set; }
}

public class GetRewardsResponseJSON
{
    public List<Reward> rewards { get; set; }
    public string prev {  get; set; }
    public string next { get; set; }
}

public class GetPlacesResponseJSON
{
    public List<Place> places { get; set; }
    public string prev { get; set; }
    public string next { get; set; }
}
#endregion