public class Ratings
{
    public string Artist { get; set; }
    public Dictionary<string, double> UserRatings { get; set; }
}

public class UserData
{
    public List<Ratings> RatingsData { get; set; }
}