using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CustomUserData
{
    [BsonElement("_id")]
    public string Username { get; private set; }
    public string Development { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
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
