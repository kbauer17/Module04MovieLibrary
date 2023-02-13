using NLog;

// See https://aka.ms/new-console-template for more information
string path = Directory.GetCurrentDirectory() + "\\nlog.config";

// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();

// log sample messages
logger.Trace("Sample trace message");
logger.Debug("Sample debug message");
logger.Info("Sample informational message");
logger.Warn("Sample warning message");
logger.Error("Sample error message");
logger.Fatal("Sample fatal error message");

//declare the file
string file = "D:/School/2023 Spring/DotNet/Module04/ml-latest-small/movies.csv";
// ask for input
Console.WriteLine("Enter 1 to append to the Movie Library.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");
// input response
string? resp = Console.ReadLine();
if (resp == "1")
{
    // append to data file:  StreamWriter sw = new StreamWriter(file,append:  true);

}
else if (resp == "2")
{
    // parse data file
    
    using (StreamReader sr = new StreamReader(file))
    {
        for(int i = 0; i < 5; i++)
        //while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            // parse the movieID from each line of data
            String[] arr = line.Split(',');//put the movieID, title and genres into an array
            Console.WriteLine("{0,-10}{1,-25}{2,-40}",arr[0],arr[1],arr[2]);
            String[] genreArray = arr[2].Split('|');

            string myOutput = $"{arr[0]} {arr[1]}";
            Console.WriteLine(myOutput);
            
            //loop for writing the genres
            for(int j = 0; j < genreArray.Length; j++)
            System.Console.WriteLine(genreArray[j]);
            
        }
    }
   
    
}



