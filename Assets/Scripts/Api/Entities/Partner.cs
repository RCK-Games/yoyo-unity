using System;
using System.Collections.Generic;

public class Gallery
{
    public int id { get; set; }
    public string type { get; set; }
    public string context { get; set; }
    public string url { get; set; }
    public int related_id { get; set; }
    public string related_type { get; set; }
    public string absolute_url { get; set; }
}
[Serializable]
    public class Medium
    {
        public int id { get; set; }
        public string type { get; set; }
        public string context { get; set; }
        public string url { get; set; }
        public int related_id { get; set; }
        public string related_type { get; set; }
        public string absolute_url { get; set; }
    }
[Serializable]
    public class Result
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string conditions { get; set; }
        public string starts_on { get; set; }
        public string ends_on { get; set; }
        public int cost { get; set; }
        public int stock { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object deleted_at { get; set; }
        public Thumbnail thumbnail { get; set; }
        public List<Gallery> gallery { get; set; }
        public List<Medium> media { get; set; }
    }
[Serializable]
    public class Root
    {
        public List<Result> results { get; set; }
        public int total { get; set; }
        public string next { get; set; }
        public object previous { get; set; }
    }
[Serializable]
    public class Thumbnail
    {
        public int id { get; set; }
        public string type { get; set; }
        public string context { get; set; }
        public string url { get; set; }
        public int related_id { get; set; }
        public string related_type { get; set; }
        public string absolute_url { get; set; }
    }