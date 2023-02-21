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


//declare the file and variables
string file = "D:/School/2023 Spring/DotNet/Module04/Module04MovieLibrary/movies.csv";

string? resp;
int myStringCompare = 0;
int k = 0;
int myMovieIndex = 0;
string[] arrTitles = new string[100];

do
{

    // ask for input
    Console.WriteLine("Enter 1 to append a new movie to the Movie Library.");
    Console.WriteLine("Enter 2 to list the current contents of the Movie Library.");
    Console.WriteLine("Enter anything else to quit.");

    // input response
    resp = Console.ReadLine();//string? 
    if (resp == "1")
    {
        //ask the user for the movie title they wish to add to the file
        Console.WriteLine("What is the movie title?");
        string? newTitle = Console.ReadLine();
        
        //  check if new entry duplicates a current title
        using(StreamReader sr = new StreamReader(file))
        
        //for (int i = 0; i < 1500; i++)
        while (!sr.EndOfStream)
        {
            string? line = sr.ReadLine();
            
            // find boundaries within current movie title to compare strings
            int firstComma = line!.IndexOf(",");
            int lastComma = line.LastIndexOf(",");
    
            // capture the movieIndex while checking for potential duplicate titles
            bool isParsable = Int32.TryParse((line.Substring(0,firstComma)), out int currentLineIndex);
                if(currentLineIndex > myMovieIndex)
                    myMovieIndex = currentLineIndex;

            // create a no punctuation string from the current movie title
            string? myNoPuncLine = line.Substring(firstComma+1, lastComma-firstComma-1);
            int lastParen = myNoPuncLine.LastIndexOf(")");
            
            // first, remove the year from the current movie title
            if(lastParen > 0)
                myNoPuncLine = myNoPuncLine.Remove(lastParen-5,6);
                
            // remove punctuation...
            string? myNewTitle = newTitle;
            char[] ch = {'"', ',', ' '};
            foreach(char x in ch){
                // ...from the current movie title
                int count = myNoPuncLine.Count(f=>(f==x));
                while(count > 0){
                    int foundPunc = myNoPuncLine.IndexOf(x);
                    myNoPuncLine=myNoPuncLine.Remove(foundPunc,1);
                    count--;
                }
                // ...from the user entry
                int count2 = myNewTitle!.Count(f=>(f==x));
                while(count2 > 0){
                    int foundPunc2 = myNewTitle!.IndexOf(x);
                    myNewTitle=myNewTitle.Remove(foundPunc2,1);
                    count2--;
                }
                
            }

            //  examine existing title from file for inclusion of new title
            myStringCompare = myNoPuncLine.ToUpper().IndexOf(newTitle!.ToUpper());
                if(myStringCompare >= 0) {
                    arrTitles[k] = line.Substring(firstComma+1,lastComma-firstComma-1);
                    k++;  // means the array has indexes used up through k-1;  this counter increments but may not come back around to fill that item
                } 
        }
        
        //  let the user know if possible matches were found
        if(k == 0){
            //System.Console.WriteLine("No matches found");
            AppendMovieTitle(myMovieIndex, newTitle!,file);  
        } else {
            System.Console.WriteLine("Possible matches found: ");
            for(int m = 0; m < k; m++){
                System.Console.Write("   ");
                System.Console.WriteLine(arrTitles[m]);
            }
            System.Console.WriteLine("Is your movie in the list of possible matches? (Y/N)");
                string? myMatch = Console.ReadLine();
                
                if(myMatch!.ToUpper().IndexOf("N") < 0 && myMatch.ToUpper().IndexOf("Y") < 0) {
                    System.Console.WriteLine("Invalid response, please select again");

                } else if (myMatch.ToUpper().IndexOf("N") == 0) {
                    //System.Console.WriteLine("     The user does not see a match to their title");
                    AppendMovieTitle(myMovieIndex, newTitle!,file);

                } else {
                    System.Console.WriteLine("\nOk, that title is already in the Movie Library.");
                    System.Console.WriteLine("Please select from the following:\n");
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
            int foundQ1 = line!.IndexOf("\"");
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
}while(resp == "1" || resp == "2");

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

static void AppendMovieTitle(int myMovieIndex, string newTitle, string file){
    string[] arrGenres = {"Action","Adventure","Animation","Children","Comedy","Crime",
                        "Documentary","Drama","Fantasy","Film-Noir","Horror","IMAX",
                        "Musical","Mystery","Romance","Sci-Fi","Thriller","War",
                        "Western","Other"};
    List<string> arrNewTitleGenres = new List<string>();

    System.Console.WriteLine("What year was the movie released?  (YYYY)");
                string newTitleYear = " (" + Console.ReadLine() + ")";
                
                System.Console.WriteLine("Please select up to five genres from this list by entering the number: (x to exit)");
                System.Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}","1. "+arrGenres[0],"2. "+arrGenres[1],
                                        "3. "+arrGenres[2],"4. "+arrGenres[3],"5. "+arrGenres[4]);
                System.Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}","6. "+arrGenres[5],"7. "+arrGenres[6],
                                        "8. "+arrGenres[7],"9. "+arrGenres[8],"10. "+arrGenres[9]);
                System.Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}","11. "+arrGenres[10],"12. "+arrGenres[11],
                                        "13. "+arrGenres[12],"14. "+arrGenres[13],"15. "+arrGenres[14]);
                System.Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}","16. "+arrGenres[15],"17. "+arrGenres[16],
                                        "18. "+arrGenres[17],"19. "+arrGenres[18],"20. "+arrGenres[19]);
                
                for(int i = 0; i < 5; i++){
                    bool isParsable = Int32.TryParse(Console.ReadLine(), out int userInputGenre);
                    if(isParsable == false) {
                        break;
                    }else{
                        arrNewTitleGenres.Add(arrGenres[userInputGenre]);
                    }
                }
                
                // now build the string and append to the file
                    //  build part of the output string
                string myOutputToAppend = String.Concat(myMovieIndex + 1,",",newTitle,newTitleYear,",");

                //loop for writing the genres to the output string
                for(int j = 0; j < arrNewTitleGenres.Count; j++){
                    myOutputToAppend += arrNewTitleGenres[j];
                    if(j < arrNewTitleGenres.Count-1){
                        myOutputToAppend += "|";
                    }
                }
                System.Console.WriteLine(myOutputToAppend);
                //add the line to the end of the file
                //StreamWriter sw = new StreamWriter(file,append:  true);
                using(StreamWriter sw = new StreamWriter(file,append: true))
                //"D:/School/2023 Spring/DotNet/Module04/ml-latest-small/movies.csv"
                sw.WriteLine(myOutputToAppend);
}

