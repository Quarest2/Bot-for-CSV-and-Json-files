namespace WorkWithCSVLib;

public static class Selection
{
    /// <summary>
    /// Method that selecting attractions from list with param "example" in field "District".
    /// </summary>
    /// <param name="attractions"The list of Attraction_TC.</param>
    /// <param name="example">The param we are looking for in field "District".</param>
    /// <returns>Returning list of selected attractions.</returns>
    public static List<Attraction_TC> SelectionDistrict(List<Attraction_TC> attractions, string example)
    {
        try
        {
            var selection = new List<Attraction_TC>();
            foreach (Attraction_TC att in attractions)
            {
                if (att.District == example)
                {
                    selection.Add(att);
                }
            }
            if (selection.Count == 0)
            {
                throw new ArgumentNullException();
            }
            return selection;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Method that selecting attractions from list with param "example" in field "LocationType".
    /// </summary>
    /// <param name="attractions"The list of Attraction_TC.</param>
    /// <param name="example">The param we are looking for in field "LocationType".</param>
    /// <returns>Returning list of selected attractions.</returns>
    public static List<Attraction_TC> SelectionLocationType(List<Attraction_TC> attractions, string example)
    {
        try
        {
            var selection = new List<Attraction_TC>();
            foreach (Attraction_TC att in attractions)
            {
                if (att.LocationType == example)
                {
                    selection.Add(att);
                }
            }
            if (selection.Count == 0)
            {
                throw new ArgumentNullException();
            }
            return selection;
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Method that selecting rows with param "exampleAdm" in column "AdmArea" and with param "exampleLoc" in column "Location"".
    /// </summary>
    /// <param name="attractions"The list of Attraction_TC.</param>
    /// <param name="exampleAdm">The param we are looking for in column "AdmArea".</param>
    /// <param name="exampleLoc">The param we are looking for in column "Location".</param>
    /// <returns>Returning list of selected attractions.</returns>
    public static List<Attraction_TC> SelectionAdmAreaLocation(List<Attraction_TC> attractions, string exampleAdm, string exampleLoc)
    {
        try
        {
            var selection = new List<Attraction_TC>();
            foreach (Attraction_TC att in attractions)
            {
                if (att.AdmArea == exampleAdm && att.Location == exampleLoc)
                {
                    selection.Add(att);
                }
            }
            if (selection.Count == 0)
            {
                throw new ArgumentNullException();
            }
            return selection;
        }
        catch
        {
            throw;
        }
    }
}

