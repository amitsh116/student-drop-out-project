using System;

namespace RecommendationGUI.BackEnd;

public class BackEndException : Exception
{
	public BackEndException() { }
	public BackEndException(string message) : base(message) { }
	public BackEndException(string message, Exception inner) : base(message, inner) { }
}
