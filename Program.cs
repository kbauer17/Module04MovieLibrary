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
int widthColumn0 = 0;
int widthColumn1 = 0;
int widthColumn2 = 0;

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
   
    using(StreamReader sr = new StreamReader(file))
    //  determine width needed for each column
    //for(int i = 0; i < 100; i++)
    while (!sr.EndOfStream)
    {
        string line = sr.ReadLine();
        // parse the movieID from each line of data
        String[] arr = line.Split(',');//put the movieID, title and genres into an array
        if(arr[0].Length > widthColumn0)
            widthColumn0 = arr[0].Length;
        if(arr[1].Length > widthColumn1)
        widthColumn1 = arr[1].Length;
        if(arr[2].Length > widthColumn2)
        widthColumn2 = arr[2].Length;
        
    }
    //Console.WriteLine("The widths are:  " + widthColumn0 + " " + widthColumn1 + " " +widthColumn2);

    {
        using(StreamReader sr = new StreamReader(file))
        for(int i = 0; i < 150; i++)
        //while (!sr.EndOfStream)
        {
            
            string line = sr.ReadLine();
            
                // check for " in the movie title
                int foundQ1 = line.IndexOf("\"");
                int foundQ2 = line.IndexOf("\"", foundQ1 + 1);

                if (foundQ1 != foundQ2 && foundQ1 >= 0)
                {
                    int foundC1 = line.IndexOf(",", foundQ1 + 1);// position of a comma
                    int foundP1 = foundQ2 - 6;// position of first parenthesis

                    // rebuild 'line' with substrings
                    String portionOne = line.Substring(foundQ1+1, foundC1 - foundQ1 - 1);
                    String portionTwo = line.Substring(foundC1 +2, 4);// length was foundP1 - (foundC1 + 2)
                    String portionThree = line.Substring(foundC1 +6, foundQ2 - (foundC1+6));//start was p1, length was foundQ2 - foundP1
                    String portionFour = line.Substring(0,foundQ1);
                    String portionFive = line.Substring(foundQ2+1);
                    line = $"{portionFour}{portionTwo}{portionOne}{portionThree}{portionFive}";

                    //Console.WriteLine(line);
                }

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



