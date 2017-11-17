//Experimenting with c# and connecting to databases with it. Hope to eventually extend this to work for web pages.
//Program is of extremely bad design in one big nasty main and made with little testing because the unit testing wouldn't let me ref this and was i was getting furious so i went without
//Program also has almost no error handling so be perfect :))))
//Program by Ammon Riley last edited 11/16/2017 around 10:40 pm
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace data2Web
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get user id for database connection
            Console.WriteLine("Input User Id: ");
            String userID = Console.ReadLine();
            Console.Write("\n");

            //Get user password and make it invisible
            String userPass = null;
            Console.WriteLine("Input User Password: ");
            bool passLoop = true;
            //Get one char of the password at a time and make it invisible
                while (passLoop == true)
                {
                     var key = System.Console.ReadKey(true);
                     if (key.Key == ConsoleKey.Enter)
                        passLoop = false;
                     userPass += key.KeyChar;
                }
            Console.Write("\n");

            //Get name/ip of database server
            Console.WriteLine("Input Server Name: ");
            String serverName = Console.ReadLine();
            Console.Write("\n");

            //Get database you want to connect to
            Console.WriteLine("Input Database Name: ");
            String databaseName = Console.ReadLine();
            Console.Write("\n");

            //Connect to the ammonRiley database with above parameters
            SqlConnection newConnection = new
                SqlConnection("user id=" + userID +
                              ";password=" + userPass +
                              ";address=" + serverName +
                              ";database=" + databaseName + 
                              "; connection timeout=30;");


            //Attempt to open the connection, if successful continue
            try
            {
                newConnection.Open();
                Console.WriteLine("Connection opened to database.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            /*
            //Test to see if we can run some sql commands mang
            SqlCommand makeTable = new SqlCommand("CREATE TABLE testSchema.testTable (PersonID int identity(1,1) not null, firstName varchar(255));", newConnection);
            int i = makeTable.ExecuteNonQuery();
            Console.Write(i);

            TEST SUCCESS
            */

            //Loop for commands user wants to do
            while (true)
            {
                //Get the command or exit prompt
                Console.WriteLine("Input SQL command (in one line and perfectly) or input EXIT to close\nNote that only insert/create/alter statements supported no queries");
                String userWant = Console.ReadLine();

                //If prompt is EXIT then exit the loop and close program
                if (userWant == "EXIT")
                    break;

                //Make the command
                SqlCommand userCommand = new SqlCommand(userWant, newConnection);
                userCommand.ExecuteNonQuery();

                //FOR WHEN I'M MOTIVATED ENOUGH TO SUPPORT QUERIES AYY :)))))
                //SqlDataReader userReader = null;
                //userReader = userCommand.ExecuteReader();

            }

            //Attempt to close connection when done
            try
            {
                newConnection.Close();
                Console.WriteLine("Connection closed to database.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            //Wait for a keypress to exit the console
            Console.Write("\n");
            Console.WriteLine("Press any character to exit the program.");
            Console.ReadKey();
        }

    }
}
