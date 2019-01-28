/* Tyler Shaw
 * 1/12/18
 * CS 2450
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace A01Hangman
{
    class Program
    {
        //Declare fields
        static string word = "snorlax"; //Hardcoded, but change as you want
        static char[] guessedWord, goalWord;
        static List<Char> badLetters = new List<Char>();
        static int lives = 6;
        static char guess = '1';
        static bool hasWon = false;
        static bool hasLost = false;
        static Regex reg = new Regex("[a-z]");


        static void Main(string[] args)
        {
            //Set the words
            goalWord = word.ToLower().ToCharArray();
            guessedWord = new char[goalWord.Length];
            for(int i = 0; i < goalWord.Length; i++) //Fill guessedWord with blanks (underscores)
            {
                guessedWord[i] = '_';
            }

            //while loop goes here
            while(!hasWon && !hasLost) //while player has not won and has not lost
            {
                PrintGame();
                GetGuess();
            }

            if(hasWon == true)
            {
                Console.WriteLine("You've won! You successfully guessed " + word + ". Please play again later!");
            }
            if (hasLost == true)
            {
                Console.WriteLine("You lost :( No more lives remain. The word was " + word + ". Please play again later!");
            }


            Console.Read(); //Keeps the console open

        }

        static void PrintGame()
        {
            //will use switch to print differently for the situation of the hangman, based on lives remaining

            //print letters tried
            Console.Write("Letters tried: ");
            for(int i = 0; i < badLetters.Count; i++)
            {
                Console.Write(" " + badLetters[i]);
            }
            Console.WriteLine(); //return cursor to new line

            

            //print guessedWord
            for (int i = 0; i < guessedWord.Length; i++)
            {
                if (guessedWord[i].Equals('_'))
                {
                    Console.Write("_ ");
                }
                else
                {
                    Console.Write(guessedWord[i] + " ");
                }
            }
            Console.WriteLine();

            //print lives remaining
            Console.WriteLine("Lives remaining: " + lives);

            switch (lives)
            {
                case 6:
                    Console.WriteLine("  ; --,");
                    Console.WriteLine("  |   ");
                    Console.WriteLine("  |   ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("_ | _____");
                    break;
                case 5:
                    Console.WriteLine("  ; --,");
                    Console.WriteLine("  |   O");
                    Console.WriteLine("  |   ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("_ | _____");
                    break;
                case 4:
                    Console.WriteLine("  ; --,");
                    Console.WriteLine("  |   O");
                    Console.WriteLine("  |   | ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("_ | _____");
                    break;

                case 3:
                    Console.WriteLine("  ; --,");
                    Console.WriteLine("  |   O");
                    Console.WriteLine("  |  /| ");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("_ | _____");
                    break;
                case 2:
                    Console.WriteLine("  ; --,");
                    Console.WriteLine("  |   O");
                    Console.WriteLine("  |  /|\\ ASK NOT FOR WHOM THE BELL TOLLS");
                    Console.WriteLine("  |  ");
                    Console.WriteLine("_ | _____");
                    break;
                case 1:
                    Console.WriteLine("  ; --,");
                    Console.WriteLine("  |   O");
                    Console.WriteLine("  |  /|\\ ");
                    Console.WriteLine("  |  / ");
                    Console.WriteLine("_ | _____");
                    break;
                case 0:
                    Console.WriteLine("  ; --,");
                    Console.WriteLine("  |   O");
                    Console.WriteLine("  |  /|\\ You're DEAD");
                    Console.WriteLine("  |  / \\");
                    Console.WriteLine("_ | _____");
                    break;
                default:
                    Console.WriteLine("Something wrong happened when printing the hangman picture!");
                    break;
            }

        }

        static void GetGuess()
        {
            //Initial check that game isn't already lost. Don't prompt again if lives gone.
            if (lives <= 0) //if lives get to zero, you've lost
            {
                hasLost = true;
                return;
            }
            
            //Prompt for guess

            Console.Write("Enter your guess (a-z): ");
            guess = Console.ReadKey().KeyChar;
            guess = guess.ToString().ToLower().ToCharArray()[0]; //sends to lowercase
            Console.WriteLine("\n\n");

            if(!reg.Match(guess.ToString()).Success) //if guess is not a-z
            {
                Console.WriteLine(guess + " is not a valid letter. Please enter a letter from a - z");
            }
            else if(!goalWord.Contains(guess) && !badLetters.Contains(guess)) //new bad letter
            {
                badLetters.Add(guess);
                
                if (lives >= 0) //if lives go below zero, you've lost
                {
                    lives--;
                }
                else
                {
                    hasLost = true;
                }

            }
            else if (goalWord.Contains(guess) && guessedWord.Contains(guess)) //letter is good, but is already used.
            {
                Console.WriteLine("You've already used that letter! Try a new one.");
            }
            else if (badLetters.Contains(guess)) //letter is bad and has been used before
            {
                Console.WriteLine("You've already used that letter! Try a new one.");
            }
            else if (goalWord.Contains(guess) && !guessedWord.Contains(guess)) //letter is good and new guessed
            {
                for(int i = 0; i < guessedWord.Length; i++) //set the good letter for all instances
                {
                    if (goalWord[i].Equals(guess))
                    {
                        guessedWord[i] = guess;
                    }
                }

                if(!guessedWord.Contains('_')) //if game is won
                {
                    hasWon = true;
                }
            }
            else //error control
            {
                Console.WriteLine("Something bad has occured!");
            }
        }
    }
}
