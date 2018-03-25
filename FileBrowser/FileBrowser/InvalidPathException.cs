using System;
using System.Runtime.Serialization;

namespace FileBrowser
{
	[Serializable]
	internal class InvalidPathException : Exception
	{
		public InvalidPathException()
		{
			Console.WriteLine("Invalid path!");
		}

		public InvalidPathException(string message) : base(message)
		{
			Console.WriteLine(message + " is not a valid path!");
		}

		public InvalidPathException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected InvalidPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}