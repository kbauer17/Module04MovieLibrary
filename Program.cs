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
int myStringCompare, k = 0;
string[] arrTitles = new string[100];

// ask for input
Console.WriteLine("Enter 1 to append to the Movie Library.");
Console.WriteLine("Enter 2 to parse data.");
Console.WriteLine("Enter anything else to quit.");

// input response
string? resp = Console.ReadLine();
if (resp == "1")
{
    // append to data file:  StreamWriter sw = new StreamWriter(file,append:  true);

    //ask a question
    Console.WriteLine("What is the movie title?");
    // input the title
    string? newTitle = Console.ReadLine();
    
    //  check if new entry duplicates a current title
    using(StreamReader sr = new StreamReader(file))
    
    for (int i = 0; i < 1500; i++)
    //while (!sr.EndOfStream)
    {
        string? line = sr.ReadLine();
        //System.Console.WriteLine(line);

        // find boundaries within to compare strings
        int firstComma = line.IndexOf(",");
        int lastComma = line.LastIndexOf(",");

        //  examine existing title for inclusion of new title
        myStringCompare = line.ToUpper().IndexOf(newTitle.ToUpper(),firstComma+1,lastComma-firstComma-1);
            if(myStringCompare > 0) {
                arrTitles[k] = line.Substring(firstComma+1,lastComma-firstComma-1);
                k++;  // means the array has indexes used up through k-1;  this counter increments by may not come back around to fill that item
            } 
    }
    
    //  let the user know if possible matches were found
    if(k == 0){
        System.Console.WriteLine("No matches found");
    } else {
        System.Console.WriteLine("Possible matches found: ");
        for(int m = 0; m < k; m++){
            System.Console.Write("   ");
            System.Console.WriteLine(arrTitles[m]);
        }
        System.Console.WriteLine("Is your movie in the list of possible matches? (Y/N)");
            string? myMatch = Console.ReadLine();
            
            if(myMatch.ToUpper().IndexOf("N") < 0 && myMatch.ToUpper().IndexOf("Y") < 0) {
                System.Console.WriteLine("Invalid response, please select again");
            } else if (myMatch.ToUpper().IndexOf("N") == 0) {
                System.Console.WriteLine("The user does not see a match to their title");
            } else {
                System.Console.WriteLine("The user sees a match to their title");
            }
    }
    
        

}
else if (resp == "2")
{
    // parse data file
   
    using(StreamReader sr = new StreamReader(file))

    //for (int i = 0; i < 9000; i++)
    while (!sr.EndOfStream)
    {
        
        string? line = sr.ReadLine();
        
        // check for " in the movie title
        int foundQ1 = line.IndexOf("\"");
        int foundQ2 = line.IndexOf("\"", foundQ1 + 1);
        
        if (foundQ1 != foundQ2 && foundQ1 >= 0)
        {
            //The line contains at least 1 pr of quotes
            // within a line containing quotes, check for specific cases
            char ch = '"';
            int count = line.Count(f =>(f==ch)); //checking for more than one set of ""
            bool b1 = line.Contains("September 11 (2002)");
            bool b2 = line.Contains("Great Performances\"\" Cats");

            if(count > 2 && b1 == true){
                //The line contains > 1 pr of quotes & September 11
                line = line.Remove(foundQ1+1, line.IndexOf("September") - (foundQ1+1));
                String[] arr = line.Split(',');  //put the movieID, title and genres into an array
                CreateOutputString(arr);
                
            } else if (count > 2 && b2 == true){
                //The line contains > 1 pr of quotes & Great Performance
                char ch2 = '"';
                int count2 = line.Count(f =>(f==ch2));
                
                while(count2 > 2){
                    int foundExtraQ = line.IndexOf("\"", foundQ1 + 1);
                    line = line.Remove(foundExtraQ, 1);
                    count2--;
                }
                
                String[] arr = line.Split(',');  //put the movieID, title and genres into an array
                CreateOutputString(arr);
                
            } else { //this is the general case for a line with only 1 set of ""
        
                int foundC1 = line.IndexOf(",", foundQ1 + 1);  // position of the first comma inside the quotations
                int foundP1 = line.IndexOf("(", foundC1);      // position of first parenthesis inside quotations after the first comma

                    // the line has one set of "" and no parenthesis (i.e., no date given after the title)
                    if(foundP1 < 0){
                        //The line has only 1 pr of quotes but no () (i.e., no date)
                        String[] arr1 = {line.Substring(0,foundQ1-1),
                                        line.Substring(foundQ1+1, foundQ2-foundQ1-1),
                                        line.Substring(foundQ2+2)};
                        CreateOutputString(arr1);
                    } else {
                        //The line contains 1 pr quotes and no other issues
                        // rebuild 'line' with substrings
                        String portionOne = line.Substring(0,foundQ1-1);  //this is everything up to the first comma
                        String portionTwo = line.Substring(foundC1 +2, (foundP1-(foundC1+2)));  //inside the "", 4 chars after the inside comma, 
                                                                                                //starting 2 char after the comma, meant to capture "The "
                        String portionThree = line.Substring(foundQ1+1, foundC1 - foundQ1 - 1);
                        String portionFour = line.Substring(foundP1-1, foundQ2 - (foundP1-1));  //traps the year plus a leading space
                        String portionFive = line.Substring(foundQ2+2);  //this is everything after the final comma
                                                
                        String[] arr = {portionOne,string.Concat(portionTwo,portionThree,portionFour),portionFive};
                        CreateOutputString(arr);
                        }
                
            }

        } else {
            //The line is formatted without quotes
            // parse the line into an array and pass it to the output method
            String[] arr = line.Split(',');//put the movieID, title and genres into an array
            CreateOutputString(arr);
            
        }
                
    }
    
   
    
}

static void CreateOutputString(String[] arr){
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

