using System.Text;

namespace Petrauskas_U1_17
{
	/// <summary>
	/// Holds Question data
	/// </summary>
	public class Question
	{
		public string Topic { get; }
		public int Difficulty { get; }
		public string Author { get; }
		public string Text { get; }
		public string[] Variants { get; }
		public int Answer { get; }
		public int Points { get; }

		//Constructor for the Question Class
		public Question(string topic, int difficulty, string author, string text, string[] variants, int answer, int points)
		{
			Topic = topic;
			Difficulty = difficulty;
			Author = author;
			Text = text;
			Variants = variants;
			Answer = answer;
			Points = points;
		}

		//This constructor accepts string array which is then parsed
		public Question(string[] data)
		{
			Topic = data[0];
			Difficulty = int.Parse(data[1]); 
			Author = data[2];
			Text = data[3];
			Variants = new[] {data[4], data[5], data[6], data[7]};
			Answer = int.Parse(data[8]);
			Points = int.Parse(data[9]);
		}

        /// <summary>
        /// Information ready for printing into table
        /// </summary>
        /// <returns>Information about the object</returns>
        public override string ToString()
		{
			var sb = new StringBuilder();

			sb.Append($"| {Topic,-15} ");
			sb.Append($"| {Difficulty,10} ");
			sb.Append($"| {Author, -15} ");
			sb.Append($"| {Text, -40} ");
			foreach (string choice in Variants)
			{
				sb.Append($"| {choice, -15} ");
			}
			sb.Append($"| {Answer,6} ");
			sb.Append($"| {Points,6} ");
			sb.Append('|');

			return sb.ToString();
		}	
	}
}
