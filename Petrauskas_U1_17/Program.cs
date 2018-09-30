using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Petrauskas_U1_17
{
    public class Program
    {
        //Constant variables for Question sets
        private const int SetCount = 2;
        private const int SetSize = 4;

        //Random class for creating Questions sets
        private static Random _rnd = new Random();

        private static void Main(string[] args)
        {
            var questions = ReadQuestionData();
            PrintDefaultData(questions);
            PrintDifficulty(FindDifficulty(questions));
            PrintTopics(FindTopics(questions));

            var sets = new List<List<Question>>();
            for (int i = 0; i < SetCount; i++)
            {
                sets.Add(CreateSet(questions));
            }

            PrintSetsData(sets);

            Console.ReadKey();
        }

        /// <summary>
        /// Reads data from file
        /// </summary>
        /// <returns>List of Questions</returns>
        private static List<Question> ReadQuestionData()
        {
            var questions = new List<Question>();

            var lines = File.ReadAllLines("Duomenys.csv");
            foreach (string line in lines)
            {
                var question = line.Split(',');
                questions.Add(new Question(question));
            }

            return questions;
        }

		/// <summary>
		/// Prints initial data
		/// </summary>
		/// <param name="questions">List of Questions</param>
		private static void PrintDefaultData(List<Question> questions)
        {
            using (var file = new StreamWriter("L1duom.txt"))
            {
				file.WriteLine(PrintLineSep());
				file.WriteLine("|      Topic      | Difficulty |      Author     |                   Text                   |                                Variants                               | Answer | Points |");
	            file.WriteLine(PrintLineSep());
				foreach (var question in questions)
                {
                    file.WriteLine(question.ToString());
	                file.WriteLine(PrintLineSep());
				}
            }
        }

		/// <summary>
		/// Creates Line separator string
		/// </summary>
		/// <returns>Formatted Line separated string</returns>
	    private static string PrintLineSep()
	    {
		    int[] lenghts = { 17, 12, 17, 42, 17, 17, 17, 17, 8, 8 };
			var sb = new StringBuilder();

		    sb.Append('+');
		    foreach (int amount in lenghts)
		    {
			    for (int i = 0; i < amount; i++)
			    {
				    sb.Append('-');
			    }
			    sb.Append('+');
		    }

			return sb.ToString();
	    }

	    /// <summary>
		/// Finds I, II, II difficulty level questions
		/// </summary>
		/// <param name="questions">List of Questions</param>
		/// <returns>Array with I, II, III level question counts</returns>
		private static int[] FindDifficulty(List<Question> questions)
        {
            int[] questionDifficulty = { 0, 0, 0 };
            foreach (var question in questions)
            {
                if(question.Difficulty <= 3)
                { 
                    questionDifficulty[question.Difficulty - 1]++;
                }
			}
            return questionDifficulty;
        }

		/// <summary>
		/// Prints an array with I, II, III level question counts to console
		/// </summary>
		/// <param name="questionDifficulty">Array with I, II, III level question counts</param>
		private static void PrintDifficulty(int[] questionDifficulty)
		{
			Console.WriteLine("I: {0}, II: {1}, III: {2}", questionDifficulty[0], questionDifficulty[1], questionDifficulty[2]);
        }

		/// <summary>
		/// Find all question topics
		/// </summary>
		/// <param name="questions">List of Questions</param>
		/// <returns>All question topics</returns>
		private static List<string> FindTopics(List<Question> questions)
		{
			var topics = new List<string>();
			foreach (var question in questions)
			{
				if (!topics.Contains(question.Topic))
				{
					topics.Add(question.Topic);
				}
			}
			return topics;
		}

		/// <summary>
		/// Prints question topics to file
		/// </summary>
		/// <param name="topics">All question topics</param>
		private static void PrintTopics(List<string> topics)
		{
			using (var file = new StreamWriter("Temos.csv"))
			{
				for (int i = 0; i < topics.Count; i++)
				{
					string topic = topics[i];
					file.Write(topic);
					if (i != topics.Count - 1)
					{
						file.Write(',');
					}
				}
			}
		}

		/// <summary>
		/// Creates a question set in which topics are unique
		/// </summary>
		/// <param name="questions">List of Questions</param>
		/// <returns>Question set in which topics are unique</returns>
		private static List<Question> CreateSet(List<Question> questions)
		{
			var set = new List<Question>();

			int size;
			if (questions.Count < SetSize)
			{
				size = questions.Count;
				//Console.WriteLine("Warning! There are less available questions left than the set requires!");
			}
			else
			{
				size = SetSize;
			}

			int attempts = questions.Count * 2;
			while (set.Count < size && attempts > 0)
			{
				attempts--;

				if (set.Count == 0)
				{
					set.Add(questions[_rnd.Next(0, questions.Count)]);
				}

				bool isValid = true;
				var candidateQuestion = questions[_rnd.Next(0, questions.Count)];

				foreach (var setQuestion in set)
				{
					if (candidateQuestion.Topic.Equals(setQuestion.Topic))
					{
						isValid = false;
						break;
					}
				}

				if (isValid)
				{
					set.Add(candidateQuestion);
				}	
			}

			if (attempts == 0)
			{
				//Console.WriteLine("ERROR! No suitable question found! Returning incomplete set.");
			}

			return set;
		}

		/// <summary>
		/// Prints question set in which topics are unique to file
		/// </summary>
		/// <param name="sets">List of question sets in which topics are unique</param>
		private static void PrintSetsData(List<List<Question>> sets)
		{
			using (var file = new StreamWriter("Klausimai.csv"))
			{
				foreach (var set in sets)
				{
					foreach (var question in set)
					{
						var sb = new StringBuilder();

						sb.Append(question.Topic);
						sb.Append(',');
						sb.Append(question.Text);
						sb.Append(',');
						sb.Append(question.Points);

						file.WriteLine(sb.ToString());
					}
				}
			}
		}
	}
}