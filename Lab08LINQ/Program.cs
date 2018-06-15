﻿using System;
using Lab08LINQ.Classes;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Lab08LINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string path = "../../../data.json";
            string json = File.ReadAllText(path);
            FeatureCollection deserialized = JsonConvert.DeserializeObject<FeatureCollection>(json);

            PrintAllNeighborhoods(deserialized);
            FilterEmptyNeighborhoodNames(deserialized);
            RemoveDuplicateNames(deserialized);
            OneBigLINQ(deserialized);
            LambdaStyle(deserialized);

        }
        static void PrintAllNeighborhoods(FeatureCollection obj)
        {
            Console.Clear();
            var result = from i in obj.Features
                         select i.Properties.Neighborhood;

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----");
            Console.WriteLine("All the data from json object");
            Console.ReadLine();
        }
        static void FilterEmptyNeighborhoodNames(FeatureCollection obj)
        {
            Console.Clear();
            var result = from i in obj.Features
                         where i.Properties.Neighborhood != ""
                         select i.Properties.Neighborhood;

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----");
            Console.WriteLine("Removed Blank Names");
            Console.ReadLine();
        }
        static void RemoveDuplicateNames(FeatureCollection obj)
        {
            Console.Clear();
            var result = from i in obj.Features
                         where i.Properties.Neighborhood != ""
                         select i.Properties.Neighborhood;
            var filter = from i in result.Distinct()
                         select i;
            foreach(var item in filter)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----");
            Console.WriteLine("Filtered Duplicated Names");
            Console.ReadLine();
        }
        static void OneBigLINQ(FeatureCollection obj)
        {
            Console.Clear();

            var result = from i in obj.Features
                         where i.Properties.Neighborhood != ""
                         group i.Properties.Neighborhood by i.Properties.Neighborhood 
                         into myNeighborhood
                         select myNeighborhood.Key;
          
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----");
            Console.WriteLine("ONE BIG LINQ CALL");
            Console.ReadLine();
        }
        static void LambdaStyle(FeatureCollection obj)
        {
            Console.Clear();

            var result = obj.Features.Where(i => i.Properties.Neighborhood != "")
                                     .GroupBy(i => i.Properties.Neighborhood)
                                     .Select(i => i.Key);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("----");
            Console.WriteLine("Lambda Expresssion");
            Console.ReadLine();
        }
    }
}
