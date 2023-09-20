// See https://aka.ms/new-console-template for more information
using System.Collections.Specialized;
using System.Configuration;

Console.WriteLine("Hello, World!");


// get the named section from the config file
NameValueCollection? section = ConfigurationManager.GetSection("categories") as NameValueCollection;


// check that it's not null
bool isNull = section == null;
Console.WriteLine("Is section null: " + isNull);
if (isNull) return;


// print the number of keys
Console.WriteLine("Number of keys in section: " + section.Keys.Count);
foreach (string k in section.Keys)
    Console.WriteLine("\t" + k);


// get the values
string[] values = section.GetValues(0);

foreach (string val in values)
    Console.WriteLine(val);


// split the value by the comma and trim any start / end white spaces
string[] vals = Array.ConvertAll(values[0].Split(','), s => s.Trim());

foreach (string val in vals)
    Console.WriteLine("\t" + val);

Console.ReadKey();



/* references:
    https://stackoverflow.com/questions/20195198/how-to-use-configurationmanager-appsettings-with-a-custom-section
    https://stackoverflow.com/questions/1977340/perform-trim-while-using-split

*/

