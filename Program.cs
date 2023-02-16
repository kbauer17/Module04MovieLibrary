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
string myOutput;
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
        //for (int i = 0; i < 150; i++)
        while (!sr.EndOfStream)
        {
            
            string line = sr.ReadLine();
            
            // check for " in the movie title
            int foundQ1 = line.IndexOf("\"");
            int foundQ2 = line.IndexOf("\"", foundQ1 + 1);
            
            if (foundQ1 != foundQ2 && foundQ1 >= 0)
            {
                System.Console.WriteLine("The line contains at least 1 pr of quotes");
                // within a line containing quotes, check for a specific case
                char ch = '"';
                int count = line.Count(f =>(f==ch)); //checking for more than one set of ""
                bool b1 = line.Contains("September 11 (2002)");
                bool b2 = line.Contains("Great Performances\"\" Cats");

                if(count > 2 && b1 == true){
                    System.Console.WriteLine("The line contains > 1 pr of quotes & September 11");
                    line = line.Remove(foundQ1+1, line.IndexOf("September") - (foundQ1+1));
                    System.Console.WriteLine(line);

                    String[] arr = line.Split(',');//put the movieID, title and genres into an array
                    //parse the genres by |
                    String[] genreArray = arr[2].Split('|');

                    //build part of the output string
                    myOutput = String.Format("{0,-10}{1,-80}   ",arr[0],arr[1]);
                                
                    //loop for writing the genres to the output string
                    for(int j = 0; j < genreArray.Length; j++){
                    myOutput += genreArray[j];
                        if(j < genreArray.Length-1) {
                            myOutput += ", ";
                        }
                    }
                } else if (count > 2 && b2 == true){
                    System.Console.WriteLine("The line contains > 1 pr of quotes & Great Performance");
                    char ch2 = '"';
                    int count2 = line.Count(f =>(f==ch2));
                    System.Console.WriteLine("Count = " + count2);
                    while(count2 > 2){
                        int foundExtraQ = line.IndexOf("\"", foundQ1 + 1);
                        System.Console.WriteLine("Index of first extra quote is "+ foundExtraQ);
                        line = line.Remove(foundExtraQ, 1);
                        System.Console.WriteLine("Line after first extra q is removed = " +line);
                        count--;
                    }
                    System.Console.WriteLine(line);
                    String[] arr = line.Split(',');//put the movieID, title and genres into an array
                    //parse the genres by |
                    String[] genreArray = arr[2].Split('|');

                    //build part of the output string
                    myOutput = String.Format("{0,-10}{1,-80}   ",arr[0],arr[1]);
                                
                    //loop for writing the genres to the output string
                    for(int j = 0; j < genreArray.Length; j++){
                    myOutput += genreArray[j];
                        if(j < genreArray.Length-1) {
                            myOutput += ", ";
                        }
                    }



                } else { //this is the general case for a line with ""
            

                
                
                    /*  this block of code works but isn't what I need for the current issue
                    // check for " inside the ""
                    if (foundC1 > foundQ2){
                        char ch = '"';
                        int count = line.Count(f =>(f==ch));
                        System.Console.WriteLine("Count = " + count);
                        while(count > 2){
                            int foundExtraQ = line.IndexOf("\"", foundQ1 + 1);
                            System.Console.WriteLine("Index of first extra quote is "+ foundExtraQ);
                            line = line.Remove(foundExtraQ, 1);
                            System.Console.WriteLine("Line after first extra q is removed = " +line);
                            count--;
                        }
                        System.Console.WriteLine(line);
                    } */
                    System.Console.WriteLine("The line contains only 1 pair of quotes");
                    int foundC1 = line.IndexOf(",", foundQ1 + 1);// position of the first comma inside the quotations
                    int foundP1 = line.IndexOf("(", foundC1);// position of first parenthesis inside quotations after the first comma
                    System.Console.WriteLine("Q1 = "+foundQ1);
                    System.Console.WriteLine("Q2 = "+foundQ2);
                    System.Console.WriteLine("C1 = "+foundC1);
                    System.Console.WriteLine("P1 = "+foundP1);

                    

                    // rebuild 'line' with substrings
                    String portionOne = line.Substring(0,foundQ1-1);//this is everything up to the first comma
                    String portionTwo = line.Substring(foundC1 +2, (foundP1-(foundC1+2)));//inside the "", 4 chars after the inside comma, starting 2 char after the comma, meant to capture "The "
                    String portionThree = line.Substring(foundQ1+1, foundC1 - foundQ1 - 1);
                    String portionFour = line.Substring(foundP1-1, foundQ2 - (foundP1-1));//traps the year plus a leading space
                    String portionFive = line.Substring(foundQ2+2);//this is everything after the final comma
                    //line = $"{portionOne}{portionTwo}{portionThree}{portionFour}{portionFive}";
                    /*
                    System.Console.WriteLine("PortionOne is: "+portionOne);
                    System.Console.WriteLine("PortionTwo is: "+portionTwo);
                    System.Console.WriteLine("PortionThree is: "+portionThree);
                    System.Console.WriteLine("PortionFour is: "+portionFour);
                    System.Console.WriteLine("PortionFive is: "+portionFive);
                    */
                    //Console.WriteLine(line);
                    
                    String[] arr = {portionOne,string.Concat(portionTwo,portionThree,portionFour),portionFive};
                    String[] genreArray = arr[2].Split('|');

                    //build part of the output string
                    myOutput = String.Format("{0,-10}{1,-80}   ",arr[0],arr[1]);
                                
                    //loop for writing the genres to the output string
                    for(int j = 0; j < genreArray.Length; j++){
                        myOutput += genreArray[j];
                        if(j < genreArray.Length-1) {
                            myOutput += ", ";
                        }
                    }
                }

            } else {
                System.Console.WriteLine("The line is formatted without quotes");
                String[] arr = line.Split(',');//put the movieID, title and genres into an array
                //parse the genres by |
                String[] genreArray = arr[2].Split('|');

                //build part of the output string
                myOutput = String.Format("{0,-10}{1,-80}   ",arr[0],arr[1]);
                            
                //loop for writing the genres to the output string
                for(int j = 0; j < genreArray.Length; j++){
                myOutput += genreArray[j];
                    if(j < genreArray.Length-1) {
                        myOutput += ", ";
                    }
                }
                
            }

            Console.WriteLine(myOutput);
            
        }
    }
   
    
}



