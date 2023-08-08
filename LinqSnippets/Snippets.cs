using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace LinqSnippets
{
    public class Snippets
    {
        public static void BasicLinQ()
        {
            string[] cars =
            {
                "VW Golf",
                "VW California",
                "Audi A3",
                "Audi A5",
                "Fiat Punto",
                "Seat Ibiza",
                "Seat Leon"
            };

            // 1. Select * from cars
            var carList = from car in cars select car;

            foreach(var car in carList)
            {
                Console.WriteLine(car);
            }
            
            //2 select where name is audi
            var audiList = from car in cars where car.Contains("Audi") select car; ;

            foreach (var car in audiList)
            {
                Console.WriteLine(car);
            }

            
        }

        // example with numbers

        public static void LinQNumbers()
        {
            List<int> numbers = new List<int> { 1,2,3,4,5,6,7,8,9 };

            // each number multiplied by 3
            // take all numbers, but 9
            // order numbers by ascending value

            var processedNumberList =
                numbers
                .Select(x => x * 3)
                .Where(x => x != 9)
                .OrderBy(x => x);

        }

        public static void SearchExamples()
        {
            List<string> textList = new List<string>
            {
                "a",
                "bx",
                "c",
                "d",
                "e",
                "cj",
                "f",
                "c",
            };

            //1. find first element
            var first = textList.First();

            //2. find first element that is "c"
            var cText = textList.First(text => text.Equals("c"));

            //3. first element that contains letter j
            var jText = textList.First(jtext => jtext.Contains("j"));

            //4. first element that contains letter z or default
            var firstOfdefaulText = textList.FirstOrDefault(text => text.Contains("z")); // "" or element that contains "z"

            //5. last element that contains letter z or default
            var lastOfdefaulText = textList.LastOrDefault(text => text.Contains("z")); // "" or element that contains "z"

            //6. single values
            var uniqueTexts = textList.Single();
            var uniqueText = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherEvenNumbers = { 0, 2, 6 };

            //obtain {4,8}
            var myEvenNumbers = evenNumbers.Except(otherEvenNumbers); // {4,8}


        }

        public static void MultipleSelects() 
        {
            // select many

            string[] myOpinions =
            {
                "Opinion 1, text 1",
                "Opinion 2, text 2",
                "Opinion 3, text 3",
            };

            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            var enterprises = new[]
            {
                new Enterprise()
                {
                    Id= 1,
                    Name ="Enterprise 1",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id= 1,
                            Name="Wilden",
                            Email="wilden@gmail.com",
                            Salary= 40000                          
                        },
                        new Employee()
                        {
                            Id= 2,
                            Name="Betsy",
                            Email="betsy@gmail.com",
                            Salary= 500
                        },
                        new Employee()
                        {
                            Id= 3,
                            Name="Marino",
                            Email="marino@gmail.com",
                            Salary= 50000
                        },

                    }
                },
                new Enterprise()
                {
                    Id= 2,
                    Name ="Enterprise 2",
                    Employees = new[]
                    {
                        new Employee()
                        {
                            Id= 4,
                            Name="Ana",
                            Email="ana@gmail.com",
                            Salary= 500
                        },
                        new Employee()
                        {
                            Id= 5,
                            Name="Maria",
                            Email="maria@gmail.com",
                            Salary= 21000
                        },
                        new Employee()
                        {
                            Id= 6,
                            Name="Christy",
                            Email="christy@gmail.com",
                            Salary= 60000
                        },

                    }
                }
            };

            //Obtain all the employees for all enterprises
            var employeeList = enterprises.SelectMany(enterprises => enterprises.Employees);

            //know if ana list is empty
            bool hasEnterprises = enterprises.Any();

            //know if the enterprises has any employee or data employee
            bool hasEmployees = enterprises.Any(enterprises => enterprises.Employees.Any());

            // all enterprises at least has an employee with more than 1000 of salary
            bool hasEmployeeWithSalaryMoreThanorEqual1000 = 
                enterprises.Any(enterprises => 
                enterprises.Employees.Any(employee => employee.Salary >= 1000));



        }

        public static void linQCollections()
        {
            var firstList = new List<string>(){"a","b","c"};
            var secondList = new List<string>(){ "a","c","d"};

            //inner join 
            var commonResult = from element in firstList
                               join element2 in secondList
                               on element equals element2
                               select new { element, element2 };

            var commonResult2 = firstList.Join(
                secondList,
                element => element,
                element2 => element2,
                (element, element2) => new { element, element2 }
                );

            // agarra y busca los elementos comunes, y toma los comunes de las dos listas y devuelve la lista de la izquierda sin los elementos comunes dentre las dos
            // outer join - left

            var leftOuterJoin = from element in firstList
                                join element2 in secondList
                                on element equals element2
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };

            var leftOuterJoin2 = from element in firstList
                                 from element2 in secondList.Where(s => s == element).DefaultIfEmpty()
                                 select new { Element = element, Element2 = element2 };

            // agarra y busca los elementos comunes, y toma los comunes de las dos listas y devuelve la lista de la derecha sin los elementos comunes dentre las dos
            // outer join - right
            var rightOuterJoin = from element2 in secondList
                                 join element in firstList
                                 on element2 equals element
                                 into temporalList
                                 from temporalElement in temporalList.DefaultIfEmpty()
                                 where element2 != temporalElement
                                 select new { Element = element2 };

            //trae todos los elementos de las dos listas, menos los comunes
            //union
            var unionList = leftOuterJoin.Union(rightOuterJoin);

        }

        public static void SkipTakeLinQ()
        {
            var myList = new[]
            {
                1,2,3,4,5,6,7,8,9,10
            };

            //SKIP
            var skipTwoFirstValues = myList.Skip(2); //{3,4,5,6,7,8,9,10}

            var skipLastTwoValues = myList.SkipLast(2); //{1,2,3,4,5,6,7,8}

            var skipWhileSmallerThan4 = myList.SkipWhile(num => num < 4); //{4,5,6,7,8}


            //TAKE
            var takeTwoFirstValues = myList.Take(2); //{1,2}

            var takeLastTwoValues = myList.TakeLast(2); //{9,10}

            var takeWhileSmallerThan4 = myList.TakeWhile(num => num < 4); //{1,2,3}

        }
        //TODO:


        //Paging with Skip y Take
        public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);
        }

        //Variables
        public static void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nSquare = Math.Pow(number, 2)
                               where nSquare > average
                               select number;

            Console.WriteLine("Average: {0}", numbers.Average());

            foreach (int number in aboveAverage)
            {
                Console.WriteLine("Number: {0} Square: {1}", number, Math.Pow(number,2));
            }
        }

        //ZIP
        public static void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + "=" + word); // {"1 = one", "2 = two", .....}
        }

        //Repeat & Range

        public static void RepeatRangeLinq()
        {
            IEnumerable<int> first1000 = Enumerable.Range(1,1000);

            IEnumerable<string> fiveXs = Enumerable.Repeat("X", 5); //{"X", "X", "X", "X", "X"}
        }

        public static void StudentsLinq()
        {
            var classRoom = new[]
            {
                new StudentLinq
                {
                    Id = 1,
                    Name = "Wilden",
                    Grade = 90,
                    Certified = true

                },
                new StudentLinq
                {
                    Id = 2,
                    Name = "Marino",
                    Grade = 90,
                    Certified = true

                },
                new StudentLinq
                {
                    Id = 3,
                    Name = "Hector",
                    Grade = 70,
                    Certified = false

                },
                new StudentLinq
                {
                    Id = 4,
                    Name = "Elias",
                    Grade = 85,
                    Certified = false

                },
                new StudentLinq
                {
                    Id = 5,
                    Name = "Mon",
                    Grade = 90,
                    Certified = false

                }
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertifiesStudents = from student in classRoom
                                       where student.Certified == false
                                       select student;

            var approvedStudentsNames = from student in classRoom
                                   where student.Grade > 50 && student.Certified == true
                                   select student.Name;
        }

        //ALL

        public static void AllLinq()
        {
            var numbers = new List<int>() { 1,2,3,4,5 };

            bool AllAreSmallerThanTen = numbers.All(number=> number < 10); //true

            bool AreAreBiggerOrEqualThanTwo = numbers.All(number => number >= 2); //false

            var EmptyList = new List<int>();

            bool AllNumbersAreGreaterThan0 = numbers.All(number=> number >= 0);
        }

        //Agregate
        public static void AggregateQueries()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            int sum = numbers.Aggregate((prevSum, current) => prevSum + current);
            // 0 + 1 = 1
            // 1 + 2 = 3
            // 3 + 3 = 6
            //....

            string[] word = { "hello,", "my", "name", "is", "Wilden" };
            string greeting = word.Aggregate((prevGreeting, current) => prevGreeting + current); // hello, my name is wilden
        }
        //Disctinct
        public static void DistinctValues()
        {
            //Valores distintos
            int[] numbers = { 1,2,3,4,5,5,4,3,2,1 };

            IEnumerable<int> distinctValues = numbers.Distinct();
        }
         

        //GroupBy

        public static void GroupByExample()
        {
            List<int> numbers = new List<int>() { 1,2,3,4,5,6,7,8,9 };    

            //Obtain only even numbers and generate to groups

            var grouped = numbers.GroupBy(x => x % 2 == 0);

            //we will have two groups
            //first group: the group that doesnt fit the condition(odd numbers)
            //second group: the group that fits the condition(even numbers)

            foreach (var group in grouped)
            {
                foreach(var value in group)
                {
                    Console.WriteLine(value); //1,3,5,7,9 .... 2,4,6,8 (first the odds an then the evens)
                }
            }

            //another example
            var classRoom = new[]
            {
                new StudentLinq
                {
                    Id = 1,
                    Name = "Wilden",
                    Grade = 90,
                    Certified = true

                },
                new StudentLinq
                {
                    Id = 2,
                    Name = "Marino",
                    Grade = 90,
                    Certified = true

                },
                new StudentLinq
                {
                    Id = 3,
                    Name = "Hector",
                    Grade = 70,
                    Certified = false

                },
                new StudentLinq
                {
                    Id = 4,
                    Name = "Elias",
                    Grade = 85,
                    Certified = false

                },
                new StudentLinq
                {
                    Id = 5,
                    Name = "Mon",
                    Grade = 90,
                    Certified = false

                }
            };

            var certifiedQuery = classRoom.GroupBy(student => student.Certified);

            //we obtain two groups
            //first: not ceritified
            //second: certified

            foreach (var group in certifiedQuery)
            {
                Console.WriteLine("-----------------{}---------------", group.Key);
                foreach (var certified in group)
                {
                    Console.WriteLine(certified.Name);
                }
            }
        }



        public static void RelationLinq()
        {
            List<Post> posts = new List<Post>()
            {
                new Post()
                {
                    Id = 1,
                    Title = "My first post",
                    Content = "My first content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment
                        {
                            Id = 1,
                            Title = "My title",
                            Content = "My first content",
                            Created = DateTime.Now
                        },
                        new Comment
                        {
                            Id = 2,
                            Title = "My second title",
                            Content = "My second content",
                            Created = DateTime.Now
                        }
                    }

                },
                new Post()
                {
                    Id = 2,
                    Title = "My second post",
                    Content = "My second content",
                    Created = DateTime.Now,
                    Comments = new List<Comment>()
                    {
                        new Comment
                        {
                            Id = 3,
                            Title = "My title lglg",
                            Content = "My third content",
                            Created = DateTime.Now
                        },
                        new Comment
                        {
                            Id = 4,
                            Title = "My 4 title",
                            Content = "My 4 content",
                            Created = DateTime.Now
                        }
                    }

                }
            };

            var commentsWithContent = posts.SelectMany(post => post.Comments, (post, comment) => new { PostId = post.Id, CommentContent = comment.Content });
            //devolver id del post y cada comentario del post


        }
    }
}