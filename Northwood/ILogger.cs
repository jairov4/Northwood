using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Northwood
{
	public interface ILogger
	{
		void Info(string msg, object[] p = null, [CallerFilePath] string filename = null, [CallerMemberName] string member = null, [CallerLineNumber] int lineNumber = -1);
		void Error(Exception e, string msg, object[] p = null, [CallerFilePath] string filename = null, [CallerMemberName] string member = null, [CallerLineNumber] int lineNumber = -1);
		void Error(string msg, object[] p = null, [CallerFilePath] string filename = null, [CallerMemberName] string member = null, [CallerLineNumber] int lineNumber = -1);
		void Warning(Exception e, string msg, object[] p = null, [CallerFilePath] string filename = null, [CallerMemberName] string member = null, [CallerLineNumber] int lineNumber = -1);
		void Warning(string msg, object[] p = null, [CallerFilePath] string filename = null, [CallerMemberName] string member = null, [CallerLineNumber] int lineNumber = -1);
		void Debug(string msg, object[] p = null, [CallerFilePath] string filename = null, [CallerMemberName] string member = null, [CallerLineNumber] int lineNumber = -1);
	}
}
