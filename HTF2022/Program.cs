﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace HTF2022 // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("#A-1 \n Done \n");
            //A1.LocalExecution();
            //await A1.TestExecution();
            //await A1.ProductionExecution();
            

            Console.WriteLine("#A-2 \n Done \n");
            A2.LocalExecution();
            //await A2.TestExecution();
            //await A2.ProductionExecution();


            Console.WriteLine("#A-3 \n");
            A3.LocalExecution();
            //await A3.TestExecution();
            //await A3.ProductionExecution();

        
            Console.WriteLine("#B-1 \n");
            //B1.LocalExecution();
            //await B1.TestExecution();
            //await B1.ProductionExecution();


            Console.WriteLine("#B-2 \n");
            //B2.LocalExecution();
            //await B2.TestExecution();
            //await B2.ProductionExecution();


            Console.WriteLine("#B-3 \n");
            B3.LocalExecution();
            await B3.TestExecution();
            //B3.ProductionExecution();
        }
    }
}