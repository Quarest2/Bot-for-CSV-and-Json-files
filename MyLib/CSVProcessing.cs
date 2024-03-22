using System;
using System.IO;
using System.Text;

namespace WorkWithCSVLib;

/// <summary>
/// Class that working with the CSV-file.
/// </summary>
public class CSVProcessing
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public CSVProcessing() { }

    /// <summary>
    /// Method that reads CSV-file.
    /// </summary>
    /// <param name="streamCSV">Stream from CSV-file.</param>
    /// <returns>List of Attraction_TC.</returns>
    /// <exception cref="ArgumentException">Exception if something wrong with file.</exception>
    public List<Attraction_TC> Read(Stream streamCSV)
    {
       
        string? line;
        List<Attraction_TC> result = new List<Attraction_TC>();
        try
        {
            using (StreamReader sr = new StreamReader(streamCSV))
            {
                line = sr.ReadLine();
                while (line != null)
                {
                    var fields = line.Split(';');
                    if (fields.Length < 11)
                    {
                        throw new ArgumentException();
                    }
                    result.Add(new Attraction_TC(fields[0].Trim(new char[]{'"', '"'}), fields[1].Trim(new char[] { '"', '"' }),
                        fields[2].Trim(new char[] { '"', '"' }), fields[3].Trim(new char[] { '"', '"' }), fields[4].Trim(new char[] { '"', '"' }),
                        fields[5].Trim(new char[] { '"', '"' }), fields[6].Trim(new char[] { '"', '"' }), fields[7].Trim(new char[] { '"', '"' }),
                        fields[8].Trim(new char[] { '"', '"' }), fields[9].Trim(new char[] { '"', '"' }), fields[10].Trim(new char[] { '"', '"' }))) ;
                    line = sr.ReadLine();
                }
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
            string path = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/result.csv";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            
            using (StreamWriter sw = new StreamWriter(File.Create(path)))
            {
                sw.Write("\"Name\"; \"Photo\"; \"AdmArea\"; \"District\"; \"Location\"; \"RegistrationNumber\"; \"State\"; \"LocationType\"; " +
                    "\"global_id\"; \"geodata_center\"; \"geoarea\";\n\"Название объекта\"; \"Фотография\"; \"Административный округ по адресу\"; " +
                    "\"Район\"; \"Месторасположение\"; \"Государственный регистрационный знак\"; \"Состояние регистрации\"; \"Тип места расположения\"; " +
                    "\"global_id\"; \"geodata_center\"; \"geoarea\";\n");
                foreach (Attraction_TC att in attractions)
                {
                    sw.WriteLine(att.AttToString());
                }
            }

            return File.Open(path, FileMode.Open);
        }
        catch
        {
            throw;
        }
    }
}
