using System;
using System.Collections.Generic;

[Serializable]
public class Place
{
    public int id;
    public string name;
    public string description;
    public string url;
    public string schedule;
    public string music_genre;
    public string music_lineup;
    public string dresscode;
    public string payment_options;
    public string address;
    public string gmaps;
    public string website_url;
    public string instagram_url;
    public string facebook_url;
    public string cost_rate;
    public DateTime created_at;
    public DateTime updated_at;
    public object deleted_at;
    public List<string> music_genre_list;
    public List<string> payment_options_list;
    public List<string> schedule_list;
    public List<MediaObject> media;
    public List<GalleryObject> gallery;
}

[Serializable]
public class PlacesResponse
{
    public List<Place> results;
    public string prev;
    public int total;
    public string next;
}
[Serializable]
public class MediaObject
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
public class GalleryObject
{
    public int id;
    public string type;
    public string context;
    public string url;
    public int related_id;
    public string related_type;
    public string absolute_url;
}

