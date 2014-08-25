using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Northwood
{
	public abstract class LoggerBase : ILogger
	{
		public virtual void Info(string msg, object[] p = null, [CallerFilePath] string filename = null, [CallerMemberName] string member = null, [CallerLineNumber] int lineNumber = -1)
		{
			if (p == null) p = new object[0];
			var msgF = string.Format(msg, p);
			var prt = string.Format("INFO  {1}({2}): {3} - {0}", msg, Path.GetFileName(filename), lineNumber, member);
			Log(prt);
		}

		public virtual void Error(Exception e, string msg, object[] p = null, [CallerFilePath] string filename = null, [CallerMemberName] string member = null, [CallerLineNumber] int lineNumber = -1)
		{
			if (p == null) p = new object[0];
			var msgF = string.Format(msg, p);
			var prt = string.Format("ERROR {1}({2}): {3} - {0}", msg, Path.GetFileName(filename), lineNumber, member);
			Log(prt);
		}

		public virtual void Error(string msg, object[] p = null, [CallerFilePath] string filename = null, [CallerMemberName] string member = null, [CallerLineNumber] int lineNumber = -1)
		{
			if (p == null) p = new object[0];
			var msgF = string.Format(msg, p);
			var prt = string.Format("ERROR {1}({2}): {3} - {0}", msg, Path.GetFileName(filename), lineNumber, member);
			Log(prt);
		}

		public virtual void Warning(Exception e, string msg, object[] p = null, [CallerFilePath] string filename = null, [CallerMemberName] string member = null, [CallerLineNumber] int lineNumber = -1)
		{
			if (p == null) p = new object[0];
			var msgF = string.Format(msg, p);
			var prt = string.Format("WARN  {1}({2}): {3} - {0}", msg, Path.GetFileName(filename), lineNumber, member);
			Log(prt);
		}

		public virtual void Warning(string msg, object[] p = null, [CallerFilePath] string filename = null, [CallerMemberName] string member = null, [CallerLineNumber] int lineNumber = -1)
		{
			if (p == null) p = new object[0];
			var msgF = string.Format(msg, p);
			var prt = string.Format("WARN  {1}({2}): {3} - {0}", msg, Path.GetFileName(filename), lineNumber, member);
			Log(prt);
		}

		public virtual void Debug(string msg, object[] p = null, [CallerFilePath] string filename = null, [CallerMemberName] string member = null, [CallerLineNumber] int lineNumber = -1)
		{
			if (p == null) p = new object[0];
			var msgF = string.Format(msg, p);
			var prt = string.Format("DEBUG {1}({2}): {3} - {0}", msg, Path.GetFileName(filename), lineNumber, member);
			Log(prt);
		}

		protected string FormatException(Exception e)
		{
			return e.ToString();
		}

		protected abstract void Log(string str);
	}
}