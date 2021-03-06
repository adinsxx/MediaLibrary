﻿using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace MediaLibrary
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {

            logger.Info("Program started");

            // Movie movie = new Movie
            // {
            //     mediaId = 123,
            //     title = "Greatest Movie Ever, The (2020)",
            //     director = "Jeff Grissom",
            //     // timespan (hours, minutes, seconds)
            //     runningTime = new TimeSpan(2, 21, 23),
            //     genres = { "Comedy", "Romance" }
            // };

            // Console.WriteLine(movie.Display());

            // Album album = new Album
            // {
            //     mediaId = 321,
            //     title = "Greatest Album Ever, The (2020)",
            //     artist = "Jeff's Awesome Band",
            //     recordLabel = "Universal Music Group",
            //     genres = { "Rock" }
            // };
            // Console.WriteLine(album.Display());

            // Book book = new Book
            // {
            //     mediaId = 111,
            //     title = "Super Cool Book",
            //     author = "Jeff Grissom",
            //     pageCount = 101,
            //     publisher = "",
            //     genres = { "Suspense", "Mystery" }
            // };
            // Console.WriteLine(book.Display());

            string scrubbedFile = FileScrubber.ScrubMovies("movies.csv");
            logger.Info(scrubbedFile);

            string movieFilePath = Directory.GetCurrentDirectory() + "\\movies.scrubbed.csv";

            MovieFile movieFile = new MovieFile(movieFilePath);
            string choice = "";
            do
            {
                // display choices to user
                Console.WriteLine("1) Add Movie");
                Console.WriteLine("2) Display All Movies");
                Console.WriteLine("3) Find movie");
                Console.WriteLine("Enter to quit");
                // input selection
                choice = Console.ReadLine();
                logger.Info("User choice: {Choice}", choice);
                
                if (choice == "1")
                {
                    // Add movie
                    Movie movie = new Movie();
                    // ask user to input movie title
                    Console.WriteLine("Enter movie title");
                    // input title
                    movie.title = Console.ReadLine();

                    // verify title is unique
                    if ( movieFile.isUniqueTitle(movie.title)){
                        Console.WriteLine("Movie title is unique\n");
                        // input genres
                        string input;
                        do
                        {
                            // ask user to enter genre
                            Console.WriteLine("Enter genre (or done to quit)");
                            // input genre
                            input = Console.ReadLine();
                            // if user enters "done"
                            // or does not enter a genre do not add it to list
                            if (input != "done" && input.Length > 0)
                            {
                                movie.genres.Add(input);
                            }
                        } while (input != "done");
                        // specify if no genres are entered
                        if (movie.genres.Count == 0)
                        {
                            movie.genres.Add("(no genres listed)");
                        }
                        
                        Console.WriteLine("Enter Director name");
                        movie.director = Console.ReadLine();

                        Console.WriteLine("Enter running time (h:m:s)");
                        string runTime = null;
                        runTime = movie.runningTime.ToString(@"hh\:mm\:ss");
                        Console.WriteLine(runTime);

                        
                        // add movie
                        movieFile.AddMovie(movie);
                    }
                } else if (choice == "2")
                {
                    // Display All Movies
                    foreach(Movie m in movieFile.Movies)
                    {
                        Console.WriteLine(m.Display());
                    }
                }

                else if (choice == "3"){
                    Console.WriteLine("Please enter the title of the movie");
                    string userInput = Console.ReadLine();
                    var movieSearch = movieFile.Movies.Where(m => m.title.Contains(userInput)).Select(m => m.title);
                    Console.WriteLine($"There are {movieSearch.Count()} movies that match your entry:");
                    foreach(string s in movieSearch){
                        Console.WriteLine($"  {s}");
                    }
                }
            } while (choice == "1" || choice == "2" || choice == "3");

            logger.Info("Program ended");


        }
    }
}