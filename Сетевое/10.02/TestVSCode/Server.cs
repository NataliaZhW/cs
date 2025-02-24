﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace TestVSCode
{
    class Program
    {     
        static async Task Main(string[] args)
        {
            string url = "https://www.gutenberg.org/";
            string searchText = "book";
            int maxTimeSeconds = 10;
            int maxConcurrency = 5;

            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(maxTimeSeconds));
            var semaphore = new SemaphoreSlim(maxConcurrency);
            var urlsToParse = new List<string> { url };
            var results = new List<string>();
            Console.WriteLine(" + для добавления, - для удаления, c - cansel");
            var tasks = new List<Task>();
            while (urlsToParse.Count > 0 || tasks.Count > 0)
            {
                if (urlsToParse.Count > 0  && semaphore.CurrentCount > 0)
                {
                    var currentUrl = urlsToParse[0];
                    urlsToParse.RemoveAt(0);
                    tasks.Add(ParseUrlAsync(currentUrl, semaphore, cts.Token, results));
                    Console.WriteLine($"Парсинг {currentUrl}");
                }
                var key = Console.ReadKey(true);
                switch (key.KeyChar.ToString().ToLower())
                {
                    case "+":
                        if (maxConcurrency < 10)
                        {
                            maxConcurrency++;
                            semaphore.Release();
                            Console.WriteLine($"Max concurrency increased to {maxConcurrency}");
                        }
                        break;
                    case "-":
                        if (maxConcurrency > 1)
                        {
                            maxConcurrency--;
                            Console.WriteLine($"concurrency decreased to {maxConcurrency}");
                        }
                        break;
                    case "c":
                        cts.Cancel();
                        Console.WriteLine("Cancelling.");
                        return;
                    default:
                        break;
                }
                await Task.Delay(100);
            }

            foreach (var result in results)
            {
                if (result.Contains(searchText))
                {
                    Console.WriteLine("Text found on the page.");
                    int index = result.IndexOf(searchText);
                    int fragmentStart = Math.Max(0, index - 50);
                    int fragmentEnd = Math.Min(100, result.Length - fragmentStart);
                    string fragment = result.Substring(fragmentStart, fragmentEnd);
                    Console.WriteLine($"Fragment: {fragment}");
                }
                else
                {
                    Console.WriteLine("Text not found on the page.");
                }
            }
        }
        
        static async Task ParseUrlAsync(string url, SemaphoreSlim semaphore, CancellationToken cancellationToken, List<string> results)
        {
            await semaphore.WaitAsync(cancellationToken);
            try
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url, cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    var html = await response.Content.ReadAsByteArrayAsync(cancellationToken);
                    var htmlString = Encoding.UTF8.GetString(html);
                    results.Add(htmlString);
                }
                else
                {
                    Console.WriteLine($"Failed to get page: {url} - {response.StatusCode}");
                }
            }
            catch (OperationCanceledException ex)
            {
                Console.WriteLine($"Task cancelled: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
