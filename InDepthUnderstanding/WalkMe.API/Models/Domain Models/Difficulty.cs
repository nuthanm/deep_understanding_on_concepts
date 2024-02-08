namespace WalkMe.API.Models.Domain_Models;

public class Difficulty
{
    public Guid Id { get; set; }

    public string Name { get; set; }
}

// Nullable means allow null value to the property
// public string? Name {get; set;}
