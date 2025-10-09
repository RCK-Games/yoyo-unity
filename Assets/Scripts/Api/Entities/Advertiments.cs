using System;
using System.Collections.Generic;

[Serializable]
public class Main
{
    public int id;
    public string type;
    public string context;
    public string url;
    public int related_id;
    public string related_type;
    public string absolute_url;
}
[Serializable]
public class Result
{
    public int id;
    public int order;
    public DateTime created_at;
    public DateTime updated_at;
    public Main main;
    public Medium[] media;
}
[Serializable]
public class advertisement
{
    public Result[] results;
}