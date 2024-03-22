using System.Text.Json;

namespace WorkWithCSVLib;

public class JSONProcessing
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public JSONProcessing() { }

    /// <summary>
    /// Method that reads JSON-file.
    /// </summary>
    /// <param name="streamJSON">Stream from JSON-file.</param>
    /// <returns>List of Attraction_TC.</returns>
    /// <exception cref="ArgumentException">Exception if something wrong with file.</exception>
    public List<Attraction_TC> Read(Stream streamJSON)
    {
        try
        {
            var result = JsonSerializer.Deserialize<List<Attraction_TC>>(streamJSON);
            if (result == null)
            {
                throw new ArgumentException();
            }
            return result;
        }
        catch
        {
            throw new ArgumentException();
        }
    }

    /// <summary>
    /// Method that converts list of Attraction_TC to FileStream.
    /// </summary>
    /// <param name="attractions">List of Attraction_TC.</param>
    /// <returns>FileStream of file with data.</returns>
    public Stream Write(List<Attraction_TC> attractions)
    {
        try
        {
            string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/result.json";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                JsonSerializer.Serialize(fs, attractions);
            }

            return File.Open(path, FileMode.Open);
        }
        catch
        {
            throw;
        }
    }
}
