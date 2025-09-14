using System;
using System.Collections.Generic;

[Serializable]
public class Place
{
    public int id;
    public string name;
    public string description;
    public string image;
    public string image_url;
    public string website;
    public SocialMedia social_media;
    public string dress_code;
    public List<string> schedule;
    public List<string> payment_options;
    public string address_text;
    public string address_link;
}

[Serializable]
public class PlacesResponse
{
    public List<Place> places;
    public string prev;
    public string next;
}

