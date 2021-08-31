using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CustomUserData
{
    //[Preserve]
    [BsonElement("_id")]
    public string Username { get; private set; }
    //[Preserve]
    public string Development { get; set; }
    //[Preserve]
    public int Age { get; set; }
    //[Preserve]
    public string Gender { get; set; }
    //[Preserve]
    public string Language { get; set; }

    public CustomUserData(string username)
    {
        Username = username;
    }

    public override string ToString()
    {
        return $"\nUsername: {Username} - Age: {Age}\nGender: {Gender} - Language: {Language}";
    }
}
