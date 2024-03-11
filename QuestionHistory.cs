using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ARRL_Tech_Practice
{
    /// <summary>
    /// This class collects the results of a practice session.  Questions answered correctly/incorrectly or skipped will be saved for future, focused sessions.
    /// </summary>
    class QuestionHistory
    {
        string path = @"C:\Users\carmodj\source\repos\ARRL Tech Practice\Answered.txt";

        private List<string> correctAnswers = new List<string>();
        private List<string> inCorrectAnswers = new List<string>();
        private List<string> skippedQuestions = new List<string>();

        public void AddCorrect(Question question)
        {
            correctAnswers.Add(question.questionRef);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("correct");
            Console.ResetColor();
        }
        public void AddCorrect(string question)
        {
            correctAnswers.Add(question);
        }
        public void AddInCorrect(Question question)
        {
            inCorrectAnswers.Add(question.questionRef);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"incorrect ({question.answer})");
            Console.ResetColor();
        }
        public void AddInCorrect(string question)
        {
            inCorrectAnswers.Add(question);
        }
        public void AddSkipped(Question question)
        {
            skippedQuestions.Add(question.questionRef);
        }
        public void AddSkipped(string question)
        {
            skippedQuestions.Add(question);
        }



        public void SaveHistory()
        {
            List<string> fileData = new List<string>();
            fileData.Add("Correct Answers");
            fileData.AddRange(correctAnswers);
            fileData.Add("Incorrect Answers");
            fileData.AddRange(inCorrectAnswers);
            fileData.Add("Skipped Questions");
            fileData.AddRange(skippedQuestions);

            File.WriteAllLines(path, fileData.ToArray());
        }
        public void LoadHistory()
        {
            string[] historyQuestions = File.ReadAllLines(path);
            string typeOfAnswer = "";

            foreach (string histQ in historyQuestions)
            {
                switch(histQ)
                {
                    case "Correct Answers":
                        typeOfAnswer = "correct";
                        continue;
                    case "Incorrect Answers":
                        typeOfAnswer = "incorrect";
                        continue;
                    case "Skipped Questions":
                        typeOfAnswer = "skipped";
                        continue;
                }
                    
                switch(typeOfAnswer)
                {
                    case ("correct"):
                        AddCorrect(histQ);
                        break;
                    case ("incorrect"):
                        AddInCorrect(histQ);
                        break;
                    case ("skipped"):
                        AddSkipped(histQ);
                        break;
                }
            }
        }
        public void DisplayHistory()
        {
            Console.WriteLine("Correct Answers");
            foreach (string qRef in correctAnswers)
            {
                Console.WriteLine(qRef);
            }
            Console.WriteLine("\nIncorrect Answers");
            foreach (string qRef in inCorrectAnswers)
            {
                Console.WriteLine(qRef);
            }
            Console.WriteLine("\nSkipped Questions");
            foreach (string qRef in skippedQuestions)
            {
                Console.WriteLine(qRef);
            }
        }
        
    }

    /// <summary>
    /// This class is the structure of a practice question
    /// </summary>
    class Question
    {
        public string questionRef { get; set; }
        public string question { get; set; }
        public string ansA { get; set; }
        public string ansB { get; set; }
        public string ansC { get; set; }
        public string ansD { get; set; }
        public string answer { get; set; }
    }
}
