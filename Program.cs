using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ARRL_Tech_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            QuestionHistory history = new QuestionHistory();
            history.LoadHistory();

            string path = @"C:\Users\carmodj\source\repos\ARRL Tech Practice\Questions.txt";

            // Read the entire JSON file into a stream
            using (var streamReader = new StreamReader(path))
            {
                var jsonDocument = JsonDocument.Parse(streamReader.ReadToEnd());

                // Deserialize the JSON document into a list of MyJsonObject
                List<Question> myObjects = new List<Question>();
                foreach (var element in jsonDocument.RootElement.EnumerateArray())
                {
                    var myObject = new Question
                    {
                        questionRef = element.GetProperty("QuestionRef").GetString(),
                        question = element.GetProperty("Question").GetString(),
                        ansA = element.GetProperty("Answer_A").GetString(),
                        ansB = element.GetProperty("Answer_B").GetString(),
                        ansC = element.GetProperty("Answer_C").GetString(),
                        ansD = element.GetProperty("Answer_D").GetString(),
                        answer = element.GetProperty("Answer").GetString()
                    };
                    myObjects.Add(myObject);
                }

                int lenQuestions = myObjects.Count;
                string myAnswer = "";
                List<string> correctAnswers = new List<string>();
                List<string> inCorrectAnswers = new List<string>();
                do
                {
                    int questionIndex = random.Next(lenQuestions);
                    Question obj = myObjects[questionIndex];
                    Console.WriteLine(obj.questionRef);
                    Console.WriteLine(obj.question);
                    Console.WriteLine(obj.ansA);
                    Console.WriteLine(obj.ansB);
                    Console.WriteLine(obj.ansC);
                    Console.WriteLine(obj.ansD);
                    Console.WriteLine();
                    Console.WriteLine("Enter \"s\" to skip this question.");

                    myAnswer = Console.ReadLine().ToUpper();
                    if (myAnswer != "S")
                    {
                        if (myAnswer == obj.answer)
                        {
                            history.AddCorrect(obj);
                        }
                        else
                        {
                            history.AddInCorrect(obj);
                        }
                        myObjects.RemoveAt(questionIndex); // so we don't see it again this session

                        Console.WriteLine("\nHit <enter> to continue... ");
                        Console.WriteLine("Enter \"e\" to end this practice session.");
                        myAnswer = Console.ReadLine().ToUpper();
                    }
                    else
                    {
                        history.AddSkipped(obj);
                    }

                    Console.Clear();
                } while (myAnswer != "E");

                history.DisplayHistory();
                history.SaveHistory();
            }
        }
    }
}
