using System.Text.Json.Serialization;

namespace WorkWithCSVLib;

/// <summary>
/// Class for Attraction_TC.
/// </summary>
public class Attraction_TC
{
    [JsonInclude]
    public string? Name { get; private set; }
    [JsonInclude]
    public string? Photo { get; private set; }
    [JsonInclude]
    public string? AdmArea { get; private set; }
    [JsonInclude]
    public string? District { get; private set; }
    [JsonInclude]
    public string? Location { get; private set; }
    [JsonInclude]
    public string? RegistrationNumber { get; private set; }
    [JsonInclude]
    public string? State { get; private set; }
    [JsonInclude]
    public string? LocationType { get; private set; }
    [JsonInclude]
    public string? Global_id { get; private set; }
    [JsonInclude]
    public string? Geodata_center { get; private set; }
    [JsonInclude]
    public string? Geoarea { get; private set; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    public Attraction_TC() { }

    /// <summary>
    /// Constructor with parameters.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="photo"></param>
    /// <param name="admArea"></param>
    /// <param name="district"></param>
    /// <param name="location"></param>
    /// <param name="registrartionNumber"></param>
    /// <param name="state"></param>
    /// <param name="locationType"></param>
    /// <param name="global_id"></param>
    /// <param name="geodata_center"></param>
    /// <param name="geoarea"></param>
    public Attraction_TC(string? name, string? photo, string? admArea, string? district, string? location, string? registrartionNumber,
        string? state, string? locationType, string? global_id, string? geodata_center, string? geoarea)
    {
        Name = name;
        Photo = photo;
        AdmArea = admArea;
        District = district;
        Location = location;
        RegistrationNumber = registrartionNumber;
        State = state;
        LocationType = locationType;
        Global_id = global_id;
        Geodata_center = geodata_center;
        Geoarea = geoarea;
    }

    /// <summary>
    /// Method for converting to string.
    /// </summary>
    /// <returns>String.</returns>
    public string AttToString()
    {
        return $"\"{Name}\";\"{Photo}\";\"{AdmArea}\";\"{District}\";\"{Location}\";\"{RegistrationNumber}\";\"{State}\";\"{LocationType}\";\"{Global_id}\";\"{Geodata_center}\";\"{Geoarea}\"";
    }
}

