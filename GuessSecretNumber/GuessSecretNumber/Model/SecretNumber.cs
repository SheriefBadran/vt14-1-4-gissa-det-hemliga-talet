using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuessSecretNumber.Model
{
    public class SecretNumber
    {
        // Fields
        public const int MaxNumberOfGuesses = 7;

        private GuessedNumber[] _guessedNumbers;
        private int? _number = null;

        // Fields declared by me
        private int? _latestGuess;
        private Outcome _outCome;
        private Random _random;
        private int _count;


        // Constructor
        public SecretNumber()
        {
            _guessedNumbers = new GuessedNumber[MaxNumberOfGuesses];

            _outCome = new Outcome();
            _random = new Random();
            Initialize();
        }

        // Properties

        // Knows if user can guess or not
        public bool CanMakeGuess
        {
            get
            {
                // (corresponding to a foreach loop gn -> temporary element variable. gn can be named anything).
                return Count < MaxNumberOfGuesses && !_guessedNumbers.Any(gn => gn.Outcome == Outcome.Right);
            }

        }

        // Calculates amount of guesses
        // Or property that holds amount of guesses
        public int Count
        {
            get
            {
                return _count;
            }
            private set
            {
                _count = value;
            }
        }

        // Contains latest guess
        public int? Guess
        {
            get
            {
                return _latestGuess;
            }
            private set
            {
                if (_latestGuess != 0)
                {
                    _latestGuess = value;
                }
            }
        }

        // returns a reference to a copy of the privte field _guessedNumbers
        public GuessedNumber[] GuessedNumbers
        {
            get { return (GuessedNumber[])_guessedNumbers.Clone(); }
        }

        // Related to propery CanMakeGuess
        // Contains the secret number
        public int? Number
        {
            get
            {
                return CanMakeGuess ? null : _number;
            }
            private set
            {
                _number = value;
            }
        }

        // Related to Guess
        public Outcome Outcome
        {
            get
            {
                return _outCome;
            }
            private set
            {
                _outCome = value;
            }
        }


        public void Initialize()
        {
            // Recycle the array that _guessedNumbers refers to. Clear all guesses and initialize the elements with default values for the type GuessedNumber
            //_guessedNumbers.Initialize();

            for (int i = 0; i < _guessedNumbers.Length; i++)
            {
                _guessedNumbers[i].Number = null;
                _guessedNumbers[i].Outcome = Outcome.Indefinite;
            }

            // Generate secret number
            _number = _random.Next(1, 101);

            // Property that holds amount of guesses
            Count = 0;

            // Set _latestGuess to null
            Guess = null;

            // returns outcome of latest guess
            Outcome = Outcome.Indefinite;

        }

        public Outcome MakeGuess(int guess)
        {

            // Check if the guessed number is to high, low or if it equals the secret number
            if (guess < 1 || guess > 100)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (CanMakeGuess)
            {
                // Check if guessed number already is guessed.
                if (Count > 0 && _guessedNumbers.Any(gn => gn.Number == guess))
                {
                    Outcome = Outcome.OldGuess;
                    Guess = guess;
                }
                else
                {
                    if (guess > _number)
                    {
                        Outcome = Outcome.High;
                        Guess = guess;
                    }
                    else if (guess < _number)
                    {
                        Outcome = Outcome.Low;
                        Guess = guess;
                    }
                    else
                    {
                        Outcome = Outcome.Right;
                        Guess = guess;
                    }

                    _guessedNumbers[Count].Number = guess;
                    _guessedNumbers[Count].Outcome = Outcome;

                    Count++;
                }
            }
            else
            {
                Outcome = Outcome.NoMoreGuesses;
            }

            // Return Outcome instead of _outCome, important encapulation. It leaves the class user with options to call MakeGuess and then check values later on.
            return Outcome;
        }
    }
}