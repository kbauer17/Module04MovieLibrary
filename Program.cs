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
        for(int i = 0; i < 10; i++)
        //while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            // parse the movieID from each line of data
            string movieID = line.Substring(0,line.IndexOf(','));

            Console.WriteLine($"MovieID is: {movieID}");
            
            //Console.WriteLine($"Week of {parsedDate:MMM, dd, yyyy}:");
            //Console.WriteLine("{0,3}{1,3}{2,3}{3,3}{4,3}{5,3}{6,3}","Su","Mo","Tu","We","Th","Fr","Sa");
            //Console.WriteLine("{0,3}{1,3}{2,3}{3,3}{4,3}{5,3}{6,3}","--","--","--","--","--","--","--");

            // parse the movie title from each line
            string title = line.Substring(line.IndexOf(',')+1);
            Console.WriteLine($"Title is: {title}");

            //String[] arr = hours.Split('|');
            //Console.WriteLine("{0,3}{1,3}{2,3}{3,3}{4,3}{5,3}{6,3}",arr[0],arr[1],arr[2],arr[3],arr[4],arr[5],arr[6]);
            System.Console.WriteLine();
            i++;
        }
    }
   
    
}



