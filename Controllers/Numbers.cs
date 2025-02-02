using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace task1.Controllers;

[ApiController]
[Route("api/classify-number")]
public class NumbersController : ControllerBase
{
    private static readonly HttpClient httpClient = new HttpClient();

    [HttpGet]
    public async Task<IActionResult> GetNumber([FromQuery] string number)
    {
        if (!int.TryParse(number, out int parsedNumber))
        {
            return BadRequest(new
            {
                number = "alphabet",
                error = true
            });
        }
        else
        {
            string isOdd = (parsedNumber % 2 != 0) ? "odd" : "even";
            var isArmstrong = IsArmstrongNumber(parsedNumber);
            string[] properties = isArmstrong ? new[] { "armstrong", isOdd } : new[] { isOdd };

            string funFact = await GetFunFactAsync(parsedNumber);

            return Ok(new
            {
                number,
                is_prime = IsPrime(parsedNumber),
                is_perfect = IsPerfectNumber(parsedNumber),
                properties,
                digit_sum = SumOfDigits(parsedNumber),
                fun_fact = funFact
            });
        }
    }

    private static bool IsPrime(int number)
    {
        if (number < 2) return false;

        if (number == 2) return true;

        if (number % 2 == 0) return false;

        for (int i = 3; i * i <= number; i += 2)
        {
            if (number % i == 0)
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsPerfectNumber(int number)
    {
        if (number < 2) return false;
        int sumOfDivisors = 1;

        for (int i = 2; i * i <= number; i++)
        {
            if (number % i == 0)
            {
                sumOfDivisors += i;
                if (i != number / i)
                {
                    sumOfDivisors += number / i;
                }
            }
        }
        return sumOfDivisors == number;
    }

    public static bool IsArmstrongNumber(int number)
    {
        if (number < 0) return false;

        int originalNumber = number;
        int numberOfDigits = GetNumberOfDigits(number);
        int sum = 0;

        while (number > 0)
        {
            int digit = number % 10;
            sum += (int)Math.Pow(digit, numberOfDigits);
            number /= 10;
        }

        return sum == originalNumber;
    }

    private static int GetNumberOfDigits(int number)
    {
        if (number == 0)
        {
            return 1;
        }

        int count = 0;
        while (number != 0)
        {
            number /= 10;
            count++;
        }
        return count;
    }

    public static int SumOfDigits(int number)
{
    int sum = 0;
    int sign = number < 0 ? -1 : 1; 
    number = Math.Abs(number);  

    while (number > 0)
    {
        int digit = number % 10;
        sum += digit;
        number /= 10;
    }

    return sum * sign; 
}

    private static async Task<string> GetFunFactAsync(int number)
    {
        string apiUrl = $"http://numbersapi.com/{number}/math?json";

        try
        {

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync();

            var jsonObject = JsonDocument.Parse(jsonResponse);
            string funFact = jsonObject.RootElement.GetProperty("text").GetString()!;

            return funFact;
        }
        catch (HttpRequestException ex)
        {
            return $"Error fetching fun fact: {ex.Message}";
        }
    }
}
