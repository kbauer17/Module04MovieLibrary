using NLog;
string path = Directory.GetCurrentDirectory() + "\\nlog.config";
// create instance of Logger
var logger = LogManager.LoadConfiguration(path).GetCurrentClassLogger();

// log sample messages
/*
logger.Trace("Sample trace message");
logger.Debug("Sample debug message");
logger.Info("Sample informational message");
logger.Warn("Sample warning message");
logger.Error("Sample error message");
logger.Fatal("Sample fatal error message");
*/

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
        for(int i = 0; i < 100; i++)
        //while (!sr.EndOfStream)
        {
            string line = sr.ReadLine();
            // parse the movieID from each line of data
            String[] arr = line.Split(',');//put the movieID, title and genres into an array
            //parse the genres by |
            String[] genreArray = arr[2].Split('|');

            //build part of the output string
            string myOutput = String.Format("{0,-10}{1,-80}   ",arr[0],arr[1]);
                        
            //loop for writing the genres to the output string
            for(int j = 0; j < genreArray.Length; j++){
            myOutput += genreArray[j];
                if(j < genreArray.Length-1) {
                    myOutput += ", ";
                }
            
            }
            Console.WriteLine(myOutput);
            
        }
    }
   
    
}



