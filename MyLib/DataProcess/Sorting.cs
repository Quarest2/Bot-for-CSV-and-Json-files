namespace WorkWithCSVLib;

public static class Sorting
{
    /// <summary>
    /// Method that sorting the list of Attraction_TC by field "AdmArea" in alphabetical order.
    /// </summary>
    /// <param name="attractions"The list of Attraction_TC.</param>
    /// <returns>Returns sorted list.</returns>
    public static List<Attraction_TC> SortAdmAreaAlphabet(List<Attraction_TC> attractions)
    {
        try
        {
            return attractions.OrderBy(at => at.AdmArea).ToList();
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// Method that sorting the list of Attraction_TC by field "AdmArea" in descending alphabetical order.
    /// </summary>
    /// <param name="attractions"The list of Attraction_TC.</param>
    /// <returns>Returns sorted list.</returns>
    public static List<Attraction_TC> SortAdmAreaDescendingAlphabet(List<Attraction_TC> attractions)
    {
        try
        {
            return attractions.OrderByDescending(at => at.AdmArea).ToList();
        }
        catch
        {
            throw;
        }
    }
}

