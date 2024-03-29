﻿using System.Net.Http.Json;

namespace HTF2022;

internal static class B2
{
    private static readonly string testUrl = "/api/path/2/medium/Sample";
    private static readonly string productionUrl = "/api/path/2/medium/Puzzle";

    private static readonly HTTPInstance clientInstance = new();

    internal static void LocalExecution()
    {
        var phrases = new List<string> { "testing zin 1", "test zin 2" };
        var combos = GetAllCombinations(phrases);
        //combos.ForEach(Console.WriteLine);
        Console.WriteLine("-Local Execution: \n");
    }

    internal static async Task TestExecution()
    {
        Console.WriteLine("-Test Execution: \n");
        var testData = await clientInstance.Client.GetFromJsonAsync<List<string>>(testUrl);
        Console.WriteLine($"Test endpoint data: {string.Join("; ", testData)}");
        var combos = GetAllCombinations(testData);
        var testSolution = string.Join("", combos);
        Console.WriteLine(testSolution);
        var testPostResponse = await clientInstance.Client.PostAsJsonAsync(testUrl, testSolution);
        var testPostResponseValue = await testPostResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Test endpoint response: {testPostResponseValue}");
    }

    internal static async Task ProductionExecution()
    {
        Console.WriteLine("-Production Execution: \n");
        var productionData = await clientInstance.Client.GetFromJsonAsync<List<string>>(productionUrl);
        Console.WriteLine($"Test endpoint data: {string.Join("; ", productionData)}");
        var combos = GetAllCombinations(productionData).Where(x => x.Distinct().Count() == 1 && !x.Contains(" "))
            .Select(x => new string(x.Distinct().ToArray()));
        var productionSolution = string.Join("", combos);
        Console.WriteLine(productionSolution);
        var productionPostResponse = await clientInstance.Client.PostAsJsonAsync(productionUrl, productionSolution);
        var productionPostResponseValue = await productionPostResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Production endpoint response: {productionPostResponseValue}");
    }

    private static IEnumerable<string> GetAllCombinations(List<string> phrases)
    {
        var smallest = phrases.Min(x => x.Length);
        var possibleOutput = new List<string>();
        for (var i = 0; i < smallest; i++)
        {
            var z = phrases.Select(x => x[i]);
            possibleOutput.Add(new string(phrases.Select(x => x[i]).ToArray()));
        }

        return possibleOutput.Where(x => x.Distinct().Count() == 1 && !x.Contains(" "))
            .Select(x => new string(x.Distinct().ToArray()));
    }
}