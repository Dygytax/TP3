using System;
using System.Linq;
using System.Threading;
using System.Xml.XPath;

namespace TP3
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      var collection = new MovieCollection().Movies;
      
      //Exercice 1
      
      //Count all movies.
      Console.WriteLine($"The number of movies are : {collection.Count}");

      //Count all movies with the letter e.
      Console.WriteLine($"The number of movies with the letter e are : {collection.Count(x => x.Title.Contains('e'))}");
      
      //Count how many time the letter f is in all the titles from this list.
      var counterF = 0;
      foreach (var itemF in collection)
      {
        counterF += itemF.Title.Count(x => x == 'f');
      }
      Console.WriteLine($"The number of the f letter is in the title of the movie are : {counterF}");
      
      //Display the title of the film with the higher budget.
      var highBudget = from item in collection orderby item.Budget descending select item.Title;
      Console.WriteLine($"The title of the movie with the higher budget is : {highBudget.First()}");
      
      //Display the title of the movie with the lowest box office.
      var lowestBox  = from item in collection orderby item.BoxOffice ascending select item.Title;
      Console.WriteLine($"The title of the movie with the lowest box office is : {lowestBox.First()}");
      
      //Order the movies by reversed alphabetical order and print the first 11 of the list.
      var reverse11 = (from item in collection orderby item.Title descending select item.Title).Take(11);
      foreach (var item in reverse11)
      {
        Console.WriteLine(item);
      }

      //Count all the movies made before 1980
      var movie1980 = from item in collection where item.ReleaseDate.Year < 1980 select item;
      Console.WriteLine($"The count of the movie made before 1980 is : {movie1980.Count()}");

      //Display the average running time of movies having a vowel as the first letter.
      var averageVowel  = from item in collection where "AEIOUY".IndexOf(item.Title.First()) >= 0 select item.RunningTime;
      Console.WriteLine($"The average running time of movies having a vowel as the first letter is : {averageVowel.Average()}");

      //Print all movies with the letter H or W in the title, but not the letter I or T.
      foreach (var waltDisneyMovies in from movie in collection where (movie.Title.ToUpper().Contains('H') || movie.Title.ToUpper().Contains('W')) && !(movie.Title.ToUpper().Contains('I') || movie.Title.ToUpper().Contains('T')) select movie) 
      {
        Console.WriteLine(waltDisneyMovies.Title);
      }
     
      //Calculate the mean of all Budget / Box Office of every movie ever
      var meanBudget  = (from item in collection select item.Budget).Average();
      var meanBoxOffice  = (from item in collection select item.BoxOffice).Average();
      var division = meanBudget / meanBoxOffice;
      Console.WriteLine($"The mean of all budget divided by box office of every movie ever is : {division}");
      Console.WriteLine($"The mean of all budget is : {meanBudget}");
      Console.WriteLine($"The mean of all budget is : {meanBoxOffice}");
      
    }
    
    //Exercice 2
    
    public static void CreateThread()
    {
      Thread t1 = new Thread(new ThreadStart(Thread1));
      Thread t2 = new Thread(new ThreadStart(Thread2));
      Thread t3 = new Thread(new ThreadStart(Thread3));
      t1.Start();
      t2.Start();
      t3.Start();

      t1.Join();
      t2.Join();
      t3.Join();
      Console.WriteLine("End of threads");
    }

    public static void Thread1()
    {
      var startTime = DateTime.UtcNow;
      while (DateTime.UtcNow - startTime < TimeSpan.FromSeconds(10))
      {
        print('_');
        Thread.Sleep(50);
      }
    }

    public static void Thread2()
    {
      var startTime = DateTime.UtcNow;
      while (DateTime.UtcNow - startTime < TimeSpan.FromSeconds(11))
      {
        print('*');
        Thread.Sleep(40);
      }
    }

    public static void Thread3()
    {
      var startTime = DateTime.UtcNow;
      while (DateTime.UtcNow - startTime < TimeSpan.FromSeconds(9))
      {
        print('°');
        Thread.Sleep(20);
      }
    }


    private static readonly Mutex m = new Mutex();

    public static void print(char c)
    {
      m.WaitOne();
      try
      {
        Console.WriteLine(c);
      }
      finally
      {
        m.ReleaseMutex();
      }
    }
  }
}