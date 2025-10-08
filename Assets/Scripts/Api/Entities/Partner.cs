using System;
using System.Collections.Generic;


[Serializable]
public class Gallery
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
public class Medium
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
public class ResultObject
{
    public int id;
    public string name;
    public string description;
    public string url;
    public string conditions;
    public string starts_on;
    public string ends_on;
    public int cost;
    public int stock;
    public DateTime created_at;
    public DateTime updated_at;
    public object deleted_at;
    public Thumbnail thumbnail;
    public Gallery[] gallery;
    public Medium[] media;
}

[Serializable]
public class Root
{
    public List<ResultObject> results;
    public int total;
    public string next;
    public object previous;
}

[Serializable]
public class Thumbnail
{
    public int id;
    public string type;
    public string context;
    public string url;
    public int related_id;
    public string related_type;
    public string absolute_url;
}