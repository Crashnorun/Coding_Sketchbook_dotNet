using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Access;
using System.Data.OleDb;
using System.Reflection;
using System.Data;


/// <summary>
/// <references>
/// https://docs.microsoft.com/en-us/previous-versions/office/troubleshoot/office-developer/automate-access-using-visual-c
/// https://www.c-sharpcorner.com/article/read-microsoft-access-database-in-C-Sharp/
/// https://stackoverflow.com/questions/17086726/c-sharp-query-with-space-in-sqlt-table
/// </references>
/// </summary>



namespace AccessDB2JSON
{
    class Program
    {
        static void Main(string[] args)
        {
            string FilePath = @"C:\Users\cportelli\Documents\Professional\TT Projects\Glass Library\Glass Database.accdb";
            //string ConnectionStrng = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" | DataDirectory |\Glass Database.accdb"";
            string ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FilePath;

            AccessDB(FilePath, ConnectionString);

            Console.ReadKey();
        }


        static void AccessDB(string FilePath, string ConnectionString)
        {
            List<GlassSample> glassSamples = new List<GlassSample>();

            OleDbConnection conn = new OleDbConnection(ConnectionString);
            string str = "select * from [Glass Samples]";
            OleDbCommand cmd = new OleDbCommand(str, conn);
            conn.Open();



            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var table = reader.GetSchemaTable();
                    foreach (DataRow row in table.Rows) Console.WriteLine(row[0]);

                    /*Console.WriteLine("Company: " + reader["Company"] + " Name: " + reader["Name"] + " Label: " + reader["Label"]);
                    Console.WriteLine(reader.FieldCount);*/
                    string val = string.Empty;

                    GlassSample sample = new GlassSample();

                    try { sample.Label = reader[1].ToString(); } catch (Exception ex) { }
                    try { sample.Company = reader[2].ToString(); } catch (Exception ex) { }
                    try { sample.Name = reader[3].ToString(); } catch (Exception ex) { }
                    try { sample.Category = reader[4].ToString(); } catch (Exception ex) { }

                    try { sample.GlassBase = reader[5].ToString(); } catch (Exception ex) { }
                    try { sample.BaseSpec = reader[6].ToString(); } catch (Exception ex) { }
                    try { sample.GlassThick1 = reader[7].ToString(); } catch (Exception ex) { }

                    try { sample.GlassBase2 = reader[8].ToString(); } catch (Exception ex) { }
                    try { sample.BaseSpec2 = reader[9].ToString(); } catch (Exception ex) { }
                    try { sample.GlassThick2 = reader[10].ToString(); } catch (Exception ex) { }

                    try { sample.GlassThick3 = reader[11].ToString(); } catch (Exception ex) { }
                    try { sample.BaseSpec3 = reader[12].ToString(); } catch (Exception ex) { }
                    try { sample.GlassThick3 = reader[13].ToString(); } catch (Exception ex) { }

                    try { sample.Coating = reader[14].ToString(); } catch (Exception ex) { }
                    try { sample.CoatingSurface = reader[15].ToString(); } catch (Exception ex) { }
                    try { sample.CoatingSurface2 = reader[16].ToString(); } catch (Exception ex) { }
                    try { sample.SurfaceModif = reader[17].ToString(); } catch (Exception ex) { }
                    try { sample.Lamination = reader[18].ToString(); } catch (Exception ex) { }
                    try { sample.Transmittance = reader[19].ToString(); } catch (Exception ex) { }
                    try { sample.ReflectExt = reader[20].ToString(); } catch (Exception ex) { }
                    try { sample.ReflectInt = reader[21].ToString(); } catch (Exception ex) { }
                    try { sample.UValue = double.Parse(reader[22].ToString()); } catch (Exception ex) { }
                    try { sample.SHGC = double.Parse(reader[23].ToString()); } catch (Exception ex) { }
                    try { sample.LSG = double.Parse(reader[24].ToString()); } catch (Exception ex) { }
                    try { sample.SampleDimensions = reader[25].ToString(); } catch (Exception ex) { }
                    try { sample.CheckedOut = (bool)reader[26]; } catch (Exception ex) { }


                    glassSamples.Add(sample);


                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        val += reader[i].ToString() + " ";
                    }

                    Console.WriteLine(val);
                }

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(glassSamples, Newtonsoft.Json.Formatting.Indented);
                Assembly assembly = Assembly.GetExecutingAssembly();
                string path = System.IO.Path.GetDirectoryName(assembly.Location) + "GlassData.json";
                System.IO.File.WriteAllText(path, json);
            }
            conn.Close();
        }

    }
}

