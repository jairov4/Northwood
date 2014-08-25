using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwood
{
	public class LoggerTrace : LoggerBase
	{
		protected override void Log(string str)
		{
			Trace.WriteLine(str);
		}
	}
}
