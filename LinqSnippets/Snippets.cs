using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            //TODO:


            //Variables


            //ZIP


            //Repeat


            //ALL


            //Agregate


            //Disctinct


            //GroupBy
        }


    }
}