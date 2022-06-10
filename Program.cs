using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proj301pt2
{

    public class Program
    {

        private static IMember currentUser = null;

        static void Main(string[] args) {
        MovieCollection mcn = new MovieCollection();
        MemberCollection mem = new MemberCollection(20);
        consinit(mcn,mem);
        }
        static void consinit(MovieCollection mcn, MemberCollection mem)
        {
            Console.Clear();
            Console.WriteLine("========================================================================");
            Console.WriteLine("Welcome to Community Library Movie DVD Management System");
            Console.WriteLine("========================================================================");
            Console.WriteLine("");
            Console.WriteLine("================================Main Menu===============================");
            Console.WriteLine("");
            Console.WriteLine("1. Staff Login");
            Console.WriteLine("2. Member Login");
            Console.WriteLine("0. Exit");
            Console.WriteLine();
            Console.Write("Enter your choice ==> (1/2/0)");

            string choice = Console.ReadLine();


            if (choice != null)
            {
                choice = choice.Trim();
                if (choice.Length > 0)
                {
                    if (choice[0] == '0')
                    {
                        Environment.Exit(0);
                    }
                    else if (choice[0] == '1')
                    {
                        staffpw(mcn,mem);
                        staff(mcn, mem);
                    }
                    else if (choice[0] == '2')
                    {
                        memberpw(mcn, mem);
                        member(mcn, mem);
                    }
                    else
                    {
                        consinit(mcn, mem);
                    }
                }
                else {
                    consinit(mcn, mem);
                }
            }
        }

        private static void staffpw(MovieCollection mcn, MemberCollection mem)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=========================Enter Staff Credentials===============================");
            Console.WriteLine();
            Console.Write("Enter username:");
            string username = Console.ReadLine();
            Console.Write("Enter password:");
            string password = Console.ReadLine();
            if (username == "staff")
            {
                if (password == "today123")
                {
                    Console.WriteLine("Access granted");
                    Console.WriteLine("Press any key to enter staff menu");
                    Console.ReadKey();
                    staff(mcn, mem);

                }
                else
                {
                    Console.WriteLine("Access denied");
                    Console.WriteLine("Press any key to return to main menu");
                    Console.ReadKey();
                    consinit(mcn, mem);
                }
            }
            else {
                Console.WriteLine("Access denied");
                Console.WriteLine("Press any key to return to main menu");
                Console.ReadKey();
                consinit(mcn, mem);
            }
        }

        static void staff(MovieCollection mcn, MemberCollection mem)
        {
            
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("================================Staff Menu===============================");
            Console.WriteLine();
            Console.WriteLine("1. Add new DVDs of a new movie to the system");
            Console.WriteLine("2. Remove DVDs of a movie from the system");
            Console.WriteLine("3. Register a new member from the system");
            Console.WriteLine("4. Remove a registered member from the system");
            Console.WriteLine("5. Display a member's contact phone number, given the member's name");
            Console.WriteLine("6. Display all members who are currently renting a particular movie");
            Console.WriteLine("0. Return to the main menu");
            Console.WriteLine();
            Console.Write("Enter your choice ==> (1/2/3/4/5/6/0)");
            string choice = Console.ReadLine();
            if (choice != null)
            {
                choice = choice.Trim();
                if (choice.Length > 0)
                {
                    if (choice[0] == '0')
                    {
                        Console.Clear();
                        consinit(mcn, mem);
                    }
                    else if (choice[0] == '1')
                    {
                        addDVD(mcn, mem);
                    }
                    else if (choice[0] == '2')
                    {
                        removeDVD(mcn, mem);
                    }
                    else if (choice[0] == '3')
                    {
                        registerMember(mcn, mem);
                    }
                    else if (choice[0] == '4')
                    {
                        removeMember(mcn, mem);
                    }
                    else if (choice[0] == '5')
                    {
                        displayPhoneNumber(mcn,mem);
                    }
                    else if (choice[0] == '6')
                    {
                        displayRenters(mcn,mem);
                    }
                    else
                    {
                        staff(mcn, mem);
                    }
                }
                else {
                    staff(mcn, mem);
                }
            }
        }

        private static void displayRenters(MovieCollection mcn, MemberCollection mem)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=======================Display Movie Renters===========================");
            Console.WriteLine();
            Console.Write("Enter movie title:");
            string mt = Console.ReadLine();
            Movie mv = (Movie)mcn.Search(mt);
            if (mv == null)
            {
                Console.WriteLine("Press any key to return to staff menu");
                Console.ReadKey();
                staff(mcn, mem);
            }
            else {
                Console.WriteLine("Borrowers:");
                MemberCollection mb = (MemberCollection)mv.Borrowers;
                string a = mb.ToString();
                Console.WriteLine(a);
                Console.WriteLine("Press any key to return to staff menu");
                Console.ReadKey();
                staff(mcn, mem);
            }
        }

        private static void displayPhoneNumber(MovieCollection mcn, MemberCollection mem)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("===================Display Member Phone Number===========================");
            Console.WriteLine();
            Console.Write("Enter member first name:");
            string fn = Console.ReadLine();
            Console.Write("Enter member last name:");
            string ln = Console.ReadLine();
            Member sch = (new Member(fn, ln));
            Member scht = (Member)mem.Find(sch);
            if (scht == null)
            {
                Console.WriteLine("Member does not exist in database");
                Console.WriteLine("Press any key to return to staff menu");
                Console.ReadKey();
                staff(mcn, mem);
            }
            else {

                string phone = scht.ContactNumber;
                Console.WriteLine("Phone number: " + phone);
                Console.WriteLine("Press any key to return to staff menu");
                Console.ReadKey();
                staff(mcn, mem);
            }
        }

        private static void removeMember(MovieCollection mcn, MemberCollection mem) // check if a member doesn't have dvd, then remove
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("===========================Remove Member===============================");
            Console.WriteLine();
            Console.Write("Enter member first name:");
            string fn = Console.ReadLine();
            Console.Write("Enter member last name:");
            string ln = Console.ReadLine();
            Member del = (Member)mem.Find(new Member(fn, ln));
            if (del == null)
            {
                Console.WriteLine("Member does not exist in database.");
                Console.WriteLine("Press any key to return to staff menu");
                Console.ReadKey();
                staff(mcn, mem);
            }
            else
            {
                if (del.Borrowed.Number != 0)
                { //member is borrowing currently
                    Console.WriteLine("Member cannot be removed because they are borrowing DVDs.");
                    Console.WriteLine("Press any key to return to staff menu");
                    Console.ReadKey();
                    staff(mcn, mem);
                }
                else
                { // member is not borrowing currently
                    mem.Delete(del);
                    Console.WriteLine("Member removed from database.");
                    Console.WriteLine("Press any key to return to staff menu");
                    Console.ReadKey();
                    staff(mcn, mem);
                }
            }
            
        }

        private static void registerMember(MovieCollection mcn, MemberCollection mem) 
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=============================Add Member===============================");
            Console.WriteLine();
            Console.Write("Enter member first name:");
            string fn = Console.ReadLine();
            Console.Write("Enter member last name:");
            string ln = Console.ReadLine();
            Console.Write("Enter member phone number:");
            string ph = Console.ReadLine();
            bool pht = IMember.IsValidContactNumber(ph);
            if (pht == false) {
                Console.WriteLine("Phone Number invalid");
                Console.WriteLine("Press any key to return to staff menu");
                Console.ReadKey();
                staff(mcn, mem);
            }
            Console.Write("Enter member pin number:");
            string pn = Console.ReadLine();
            bool pnt = IMember.IsValidPin(pn);
            if (pnt == false)
            {
                Console.WriteLine("Pin Number invalid");
                Console.WriteLine("Press any key to return to staff menu");
                Console.ReadKey();
                staff(mcn, mem);
            }
            IMovieCollection newmember = new MovieCollection();
            Member add = (new Member(fn, ln, ph, pn, newmember));
            mem.Add(add);
            Console.WriteLine("Member added to database.");
            Console.WriteLine("Press any key to return to staff menu");
            Console.ReadKey();
            staff(mcn, mem);
        }

        private static void removeDVD(MovieCollection mcn, MemberCollection mem) // Need to update totalcopies, not TotalCopies
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=============================Remove DVDs===============================");
            Console.WriteLine();
            Console.Write("Enter movie title:");
            string title = Console.ReadLine();
            IMovie delete = mcn.Search(title);
            if (delete != null)
            {
                Console.WriteLine("Total Copies: " + delete.TotalCopies);
                Console.WriteLine("How many copies to delete?");
                string copy = Console.ReadLine();
                int copies;
                Int32.TryParse(copy, out copies);
                if (copies == delete.TotalCopies)
                {
                    mcn.Delete(delete);
                    Console.WriteLine("Movie deleted.");
                    Console.WriteLine("Press any key to return to staff menu");
                    Console.ReadKey();
                    staff(mcn, mem);
                }
                else if (copies < delete.TotalCopies) {
                    delete.TotalCopies = delete.TotalCopies - copies;
                    delete.AvailableCopies = delete.AvailableCopies- copies;
                    Console.WriteLine("Total Copies: " + delete.TotalCopies);
                    mcn.Delete(delete);
                    mcn.Insert(delete);
                    Console.WriteLine(delete.ToString());
                    Console.WriteLine("Press any key to return to staff menu");
                    Console.ReadKey();
                    staff(mcn, mem);

                }
                
            }
            else {
                Console.WriteLine("Movie not found in database.");
                Console.WriteLine("Press any key to return to staff menu");
                Console.ReadKey();
                staff(mcn, mem);
            }
        }

        private static void addDVD(MovieCollection mcn, MemberCollection mem) // If same movie added, only update availablecopies
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("================================Add DVD===============================");
            Console.WriteLine();
            Console.Write("Enter movie title:");
            string title = Console.ReadLine();

            if (mcn.Search(title) != null)
            {
                Movie current = (Movie)mcn.Search(title);
                Console.WriteLine("Current copies: " + current.TotalCopies);
                Console.Write("Enter additional copies of DVD:");
                string copy = Console.ReadLine();
                int copies;
                Int32.TryParse(copy, out copies);
                current.TotalCopies = current.TotalCopies + copies;
                Console.WriteLine("Current copies: " + current.TotalCopies);
                Console.WriteLine("Is this correct?");
                Console.Write("Enter your choice ==> (Y/N)");
                int correct = Console.Read();
                Console.Read();
                if (correct == 89 || correct == 121) // correct
                {
                    Console.Read();
                    mcn.Delete(current);
                    mcn.Insert(current);
                    Console.WriteLine("DVD's added");
                    Console.WriteLine("Press any key to return to staff menu");
                    Console.ReadKey();
                    staff(mcn, mem);
                }
                else if (correct == 78 || correct == 110)
                { // not correct
                    Console.Read();
                    Console.WriteLine("DVD's not added");
                    Console.WriteLine("Press any key to return to staff menu");
                    Console.ReadKey();
                    staff(mcn, mem);
                }
                else
                {
                    Console.Read();
                    Console.WriteLine("Invalid selection. DVD's not added");
                    Console.WriteLine("Press any key to return to staff menu");
                    Console.ReadKey();
                    staff(mcn, mem);

                }

            }
            else // not in database
            {

                Console.WriteLine("Select movie genre:");
                Console.WriteLine("1 - Action");
                Console.WriteLine("2 - Comedy");
                Console.WriteLine("3 - History");
                Console.WriteLine("4 - Drama");
                Console.WriteLine("5 - Western");
                string genr = Console.ReadLine();
                int genre;
                Int32.TryParse(genr, out genre);
                MovieGenre add = new MovieGenre();
                if (genre > 0 && genre < 6)
                {
                    if (genre == 1)
                    {
                        add = MovieGenre.Action;
                    }
                    else if (genre == 2)
                    {
                        add = MovieGenre.Comedy;
                    }
                    else if (genre == 3)
                    {
                        add = MovieGenre.History;
                    }
                    else if (genre == 4)
                    {
                        add = MovieGenre.Drama;
                    }
                    else if (genre == 5)
                    {
                        add = MovieGenre.Western;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection.");
                }
                Console.WriteLine("Select movie classification:");
                Console.WriteLine("1 - G");
                Console.WriteLine("2 - PG");
                Console.WriteLine("3 - M");
                Console.WriteLine("4 - M15Plus");
                MovieClassification add2 = new MovieClassification();
                string classifs = Console.ReadLine();
                int classif;
                Int32.TryParse(classifs, out classif);
                if (classif > 0 && genre < 5)
                {
                    if (classif == 1)
                    {
                        add2 = MovieClassification.G;
                    }
                    else if (classif == 2)
                    {
                        add2 = MovieClassification.PG;
                    }
                    else if (classif == 3)
                    {
                        add2 = MovieClassification.M;
                    }
                    else if (classif == 4)
                    {
                        add2 = MovieClassification.M15Plus;
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection.");
                        Console.WriteLine("Press any key to return to staff menu");
                        Console.ReadKey();
                        staff(mcn, mem);
                    }
                }
                Console.Write("Enter movie duration in minutes:");
                string min = Console.ReadLine();
                int mins;
                Int32.TryParse(min, out mins);
                Console.WriteLine();
                Console.Write("Enter number of copies of DVD:");
                string copy = Console.ReadLine();
                int copies;
                Int32.TryParse(copy, out copies);
                Console.WriteLine();

                Console.WriteLine("Is the following correct?");
                Console.WriteLine("Title: " + title);
                Console.WriteLine("Movie Genre: " + add);
                Console.WriteLine("Movie Classification: " + add2);
                Console.WriteLine("Duration: " + mins);
                Console.WriteLine("Copies: " + copies);
                Console.Write("Enter your choice ==> (Y/N)");
                int correct = Console.Read();
                Console.Read();
                if (correct == 89 || correct == 121) // correct
                {
                    Console.Read();
                    mcn.Insert(new Movie(title, add, add2, mins, copies));
                    Console.WriteLine();
                    Console.WriteLine("Press any key to return to staff menu");
                    Console.ReadKey();
                    staff(mcn, mem);
                }
                else if (correct == 78 || correct == 110)
                { // not correct
                    Console.Read();
                    Console.WriteLine();
                    Console.WriteLine("Press any key to return to staff menu");
                    Console.ReadKey();
                    staff(mcn, mem);
                }
                else
                {


                }
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////Member Section///////////////////////////////////////////////////////////////////////////////////////

        private static void memberpw(MovieCollection mcn, MemberCollection mem) 
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=========================Enter Member Credentials===============================");
            Console.WriteLine();
            Console.Write("Enter First Name:");
            string userFirstname = Console.ReadLine();
            Console.Write("Enter last name:");
            string userLastname = Console.ReadLine();
            Console.Write("Enter password:");
            string password = Console.ReadLine();

            IMember store = new Member(userFirstname, userLastname);


            IMember name = mem.Find(store);
                
            if (name != null)//Find method for username
            {
                if (password == name.Pin) // Find method for password
                {
                    Console.WriteLine("Access granted");
                    Console.WriteLine("Press any key to enter staff menu");
                    Console.ReadKey();
                    currentUser = name;
                }
                else
                {
                    Console.WriteLine("Access denied");
                    Console.WriteLine("Press any key to return to main menu");
                    Console.ReadKey();
                    consinit(mcn, mem);
                }
            }
            else
            {
                Console.WriteLine("Access denied");
                Console.WriteLine("Press any key to return to main menu");
                Console.ReadKey();
                consinit(mcn, mem);
            }
        }

        static void member(MovieCollection mcn, MemberCollection mem)
        {
            
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("===============================Member Menu===============================");
            Console.WriteLine();
            Console.WriteLine("1. Browse all the movies");
            Console.WriteLine("2. Display all the information about a movie given the title of the movie");
            Console.WriteLine("3. Borrow a movie DVD");
            Console.WriteLine("4. Return a movie DVD");
            Console.WriteLine("5. List current borrowing movies");
            Console.WriteLine("6. Display top 3 movies rented by the members");
            Console.WriteLine("0. Return to the main menu");
            Console.WriteLine();
            Console.Write("Enter your choice ==> (1/2/3/4/5/6/0)");
            string choice = Console.ReadLine();
            if (choice != null)
            {
                choice = choice.Trim();
                if (choice.Length > 0)
                {
                    if (choice[0] == '0')
                    {
                        Console.Clear();
                        consinit(mcn, mem);
                    }
                    else if (choice[0] == '1')
                    {
                        browseMovies(mcn, mem);
                    }
                    else if (choice[0] == '2')
                    {
                        displayMovieFromTitle(mcn, mem);
                    }
                    else if (choice[0] == '3')
                    {
                        borrowMovie(mcn, mem);
                    }
                    else if (choice[0] == '4')
                    {
                        returnMovie(mcn, mem);
                    }
                    else if (choice[0] == '5')
                    {
                        currentlyBorrowed(mcn, mem);
                    }
                    else if (choice[0] == '6')
                    {
                        theTop3(mcn, mem);
                    }
                    else
                    {
                        member(mcn, mem);
                    }
                }
                else {
                    member(mcn, mem);
                }
            }
        }

        private static void theTop3(MovieCollection mcn, MemberCollection mem) // - 6
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=====Top 3 Movies=====");
            Console.WriteLine();
            int bor1 = 0;
            int bor2 = 0;
            int bor3 = 0;
            string no1 = "***No movie exists***";
            string no2 = "***No movie exists***";
            string no3 = "***No movie exists***";
            IMovie [] moviesbank = mcn.ToArray();
            int topborrows = 0;
            for (int i = 0; i < moviesbank.Length; i++)
            {
                
                
                if (moviesbank[i].NoBorrowings > topborrows) {
                    topborrows = moviesbank[i].NoBorrowings;
                    IMovie topmovie = moviesbank[i];
                    bor1 = topmovie.NoBorrowings;
                     no1 = topmovie.Title;   
                }
                if (topborrows == 0) {
                    IMovie topmovie = moviesbank[0];
                    bor1 = topmovie.NoBorrowings;
                    no1 = topmovie.Title;
                }

            }
            for (int i = 0; i < moviesbank.Length; i++)
            {
             
                if (moviesbank[i].NoBorrowings <= bor1 && moviesbank[i].Title != no1 && moviesbank[i].NoBorrowings >= bor2)
                {
                    
                    IMovie topmovie = moviesbank[i];
                    bor2 = topmovie.NoBorrowings;
                    no2 = topmovie.Title;

                }

            }
            for (int i = 0; i < moviesbank.Length; i++)
            {
                if (moviesbank[i].NoBorrowings <= bor2 && moviesbank[i].Title != no1 && moviesbank[i].Title != no2 && moviesbank[i].NoBorrowings >= bor3)
                {
                    IMovie topmovie = moviesbank[i];
                    bor3 = topmovie.NoBorrowings;
                    no3 = topmovie.Title;
                }

            }

            Console.WriteLine("1 - Movie: " + no1 + " No. of Borrowings: " + bor1);
            Console.WriteLine("2 - Movie: " + no2 + " No. of Borrowings: " + bor2);
            Console.WriteLine("3 - Movie: " + no3 + " No. of Borrowings: " + bor3);
            Console.WriteLine("Press any key to return to staff menu");
            Console.ReadKey();
            member(mcn, mem);
        }

        private static void currentlyBorrowed(MovieCollection mcn, MemberCollection mem) // -5 List of currently borrowing movies () // Add a field into the member class to store and access a collection of registered members whoe are holding the mvoie
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=====Currently Borrowed Movies=====");
            Console.WriteLine();
            Member current = (Member)mem.Find(currentUser);
            IMovie[] array = (current.Borrowed.ToArray());
            if (current.Borrowed.Number != 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    Console.WriteLine(array[i].ToString());
                }
            }
            else 
            {
                Console.WriteLine("No movie is currently borrowed by the member");
            }
            Console.WriteLine("Press any key to return to member menu");
            Console.ReadKey();
            member(mcn, mem);
        }

        private static void returnMovie(MovieCollection mcn, MemberCollection mem) // - 4 Return a borrow movie (Delete borrower) - Member credential, remove movie
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=====Return a movie DVD=====");
            Console.WriteLine();
            Console.Write("Enter movie title:");
            string movieName = Console.ReadLine();
            IMovie result = mcn.Search(movieName);
            if (result != null)
            {
                Console.WriteLine("The movie is returned.");
                result.RemoveBorrower(currentUser); // Borrowers position// Indicate the borrower name?
                Member current = (Member)mem.Find(currentUser);
                current.Borrowed.Delete(result);

                Console.WriteLine("Press any key to return to member menu");
                Console.ReadKey();
                member(mcn, mem);
            }
            else
            {
                Console.WriteLine("You have not borrowed the movie.");
                Console.WriteLine("Press any key to return to member menu");
                Console.ReadKey();
                member(mcn, mem);
            }
        }

        private static void borrowMovie(MovieCollection mcn, MemberCollection mem) // - 3 Borrow a Movie DVD (Add borrower) - Member credential, Add movie
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=====Borrow a movie DVD=====");
            Console.WriteLine();
            Console.Write("Enter movie title:");
            string movieName = Console.ReadLine();
            IMovie result = mcn.Search(movieName);
            if (result != null) {
                Console.WriteLine("");
                Console.WriteLine("The movie is borrowed.");
                result.AddBorrower(currentUser); //Borrowers position- (Indicate the borrower name?)
                Member current = (Member)mem.Find(currentUser);
                current.Borrowed.Insert(result);
                Console.WriteLine("Press any key to return to member menu");
                Console.ReadKey();
                member(mcn, mem);
            }
            else {
                Console.WriteLine("The movie list is empty.");
                Console.WriteLine("Press any key to return to member menu");
                Console.ReadKey();
                member(mcn, mem);
            }
        }

        private static void displayMovieFromTitle(MovieCollection mcn, MemberCollection mem) // Assign the correct variable
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=====Display all the information about a movie given the title of the movie=====");
            Console.WriteLine();
            Console.Write("Enter movie title:");
            string title = Console.ReadLine();
            IMovie searchThisMovie = mcn.Search(title);
            if (searchThisMovie != null)
            {
                Console.WriteLine(searchThisMovie.ToString());
                Console.WriteLine("Press any key to return to member menu");
                Console.ReadKey();
                member(mcn, mem);
            }
            else
            {
                Console.WriteLine("Movie not found in database.");
                Console.WriteLine("Press any key to return to member menu");
                Console.ReadKey();
                member(mcn, mem);
            }
        }

        private static void browseMovies(MovieCollection mcn, MemberCollection mem) // 1. Cannot have an empty list
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("=============Browse all the movies=============");
            Console.WriteLine();
            Console.WriteLine("{0}", mcn.Number);

            if (!mcn.IsEmpty())
            {
                IMovie[] store = mcn.ToArray();
                for (int i = 0; i < store.Length; i++)
                {
                    Console.WriteLine(store[i].ToString());
                }
                Console.WriteLine("Press any key to return to member menu");
                Console.ReadKey();
                member(mcn, mem);
            }
            else {
                Console.WriteLine("The movie list is empty.");
                Console.WriteLine("Press any key to return to member menu");
                Console.ReadKey();
                member(mcn, mem);
            }
        }
    }
}
