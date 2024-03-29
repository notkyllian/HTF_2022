﻿using System.Net.Http.Json;

namespace HTF2022;

public class A2Response
{
    public List<Wizard> TeamA { get; set; } = null!;
    public List<Wizard> TeamB { get; set; } = null!;

    public override string ToString()
    {
        return $"{nameof(TeamA)}: {TeamA.Count}, {nameof(TeamB)}: {TeamB.Count}";
    }
}

public class Wizard
{
    public int Strength { get; set; }
    public int Speed { get; set; }
    public int Health { get; set; }

    public override string ToString()
    {
        return $"{nameof(Strength)}: {Strength}, {nameof(Speed)}: {Speed}, {nameof(Health)}: {Health}";
    }
}

internal static class A2
{
    private const string TestUrl = "/api/path/1/medium/Sample";
    private const string ProductionUrl = "/api/path/1/medium/Puzzle";

    private static readonly HTTPInstance ClientInstance = new();

    internal static void LocalExecution()
    {
        Console.WriteLine("-Local Execution: \n");
    }

    internal static async Task TestExecution()
    {
        Console.WriteLine("-Test Execution: \n");
        var testData = await ClientInstance.Client.GetFromJsonAsync<A2Response>(TestUrl);
        Console.WriteLine($"Test endpoint data: {testData}");

        Console.WriteLine("Team A:");
        foreach (var wizard in testData.TeamA) Console.WriteLine(wizard);

        Console.WriteLine("Team B:");
        foreach (var wizard in testData.TeamB) Console.WriteLine(wizard);

        var result = Fight(testData.TeamA, testData.TeamB);
        Console.WriteLine($"Fight Result: {result}");

        var response = await ClientInstance.Client.PostAsJsonAsync(TestUrl, result);
        Console.WriteLine($"Response: {await response.Content.ReadAsStringAsync()}");
    }

    internal static async Task ProductionExecution()
    {
        Console.WriteLine("-Production Execution: \n");

        var inputData = await ClientInstance.Client.GetFromJsonAsync<A2Response>(ProductionUrl);
        Console.WriteLine($"Production endpoint data: {inputData}");

        var result = Fight(inputData.TeamA, inputData.TeamB);
        Console.WriteLine($"Fight Result: {result}");

        var response = await ClientInstance.Client.PostAsJsonAsync(ProductionUrl, result);
        Console.WriteLine($"Response: {await response.Content.ReadAsStringAsync()}");
    }


    internal static string Fight(List<Wizard> teamA, List<Wizard> teamB)
    {
        var finished = false;
        var nextAttack = "N";

        while (!finished)
        {
            var wizardA = teamA[0];
            var wizardB = teamB[0];

            switch (nextAttack)
            {
                case "N":
                    nextAttack = wizardA.Speed > wizardB.Speed ? "A" : "B";
                    break;
                case "A":
                {
                    wizardB.Health -= wizardA.Strength;
                    if (wizardB.Health <= 0)
                    {
                        teamB.RemoveAt(0);
                        nextAttack = "N";
                    }
                    else
                    {
                        nextAttack = "B";
                    }

                    break;
                }
                default:
                {
                    wizardA.Health -= wizardB.Strength;
                    if (wizardA.Health <= 0)
                    {
                        teamA.RemoveAt(0);
                        nextAttack = "N";
                    }
                    else
                    {
                        nextAttack = "A";
                    }
                    break;
                }
            }

            if (teamA.Count == 0 || teamB.Count == 0) finished = true;
        }

        return teamA.Count == 0 ? "TeamB" : "TeamA";
    }
}