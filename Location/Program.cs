using System;
using System.Diagnostics;

public class Location
{
	public static readonly Dictionary<string, string> pages = new Dictionary<string, string>()
	{
		{ "google", "Welcome to google.com!" },
		{ "youtube", "Welcome to youtube.com!" },
		{ "home", "Welcome to home page." },
		{ "error", "Page not found :(" }
	};

	private List<string> history = new List<string>(), forwardHistory = new List<string>();

	public static void Main(string[] args)
	{
		Location location = new Location();

		location.VisitPage("home");

		while (true)
		{
			string? text = Console.ReadLine();

			if (text != null)
			{
				location.Interpret(text);
			}
		}
	}

	private void Interpret(string text)
	{
		string[] split = text.Split(' ');
		string args = (split.Length > 1) ? split[1] : "";
		switch (split[0])
		{
			case "visit":
				forwardHistory.Clear();
				history.Add(split[1]);
				VisitPage(args);

				break;
			case "back":
				if (history.Count > 0)
				{
					forwardHistory.Add(history[history.Count - 1]);
					history.RemoveAt(history.Count - 1);
				}
				VisitPage(history.Count > 0 ? history[history.Count - 1] : "home");

				break;
			case "forward":
				if (forwardHistory.Count > 0)
				{
					history.Add(forwardHistory[0]);

					VisitPage(forwardHistory.Count > 0 ? forwardHistory[0] : "home");

					forwardHistory.RemoveAt(0);
				}

				break;
			default:
				Console.WriteLine("Command not recognized!");

				break;
		}
	}

	private void VisitPage(string url)
	{
		if (pages.ContainsKey(url))
		{
			Console.WriteLine(pages[url]);
		} else
		{
			Console.WriteLine(pages["error"]);
		}
	}
}