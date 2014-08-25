using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Northwood
{
	public abstract class ProjectDocument : ObservableObject
	{
		private string _Name;

		public string Name
		{
			get { return _Name; }
			set { SetValue(ref _Name, value); }
		}

		public ProjectDocument()
		{
			Name = "Sin_titulo";
		}
	}

	public enum PortMode
	{
		Input, Output, Bidirectional
	}

	public enum SignalType
	{
		Bus, Wire
	}

	public class SignalDeclaration : ObservableObject
	{
		int _Size;
		public int Size
		{
			get { return _Size; }
			set { SetValue(ref _Size, value); }
		}
	}

	public class PortSignalDeclaration : SignalDeclaration
	{
		public PortMode Mode { get; set; }
	}

	public enum PortSide
	{
		Left, Right
	}

	public class BlockPortMapping : PortSignalDeclaration
	{
		// Distance to top
		public decimal Location { get; set; }

		public PortSide Side { get; set; }

		public string Signal { get; set; }
	}

	public class Block
	{
		public string DesignUnit { get; set; }

		public decimal Width { get; set; }

		public decimal Height { get; set; }

		public Dictionary<string, BlockPortMapping> Ports { get; set; }
	}

	public struct DecimalVector
	{
		public decimal X { get; set; }
		public decimal Y { get; set; }
	}


	public class WireSegment
	{
		public DecimalVector P { get; set; }
		public DecimalVector Q { get; set; }

		public string Signal { get; set; }
		public SignalType Type { get; set; }
	}

	public class ProjectBlockDiagram : ProjectDocument
	{
		public ProjectBlockDiagram()
		{
			Ports = new ObservableDictionary<string, PortSignalDeclaration>();
			Signals = new ObservableDictionary<string, SignalDeclaration>();
			Components = new ObservableDictionary<string, Block>();
			Wiring = new ObservableCollection<WireSegment>();
		}

		public ObservableDictionary<string, PortSignalDeclaration> Ports { get; private set; }

		public ObservableDictionary<string, SignalDeclaration> Signals { get; private set; }

		public ObservableDictionary<string, Block> Components { get; private set; }

		public ObservableCollection<WireSegment> Wiring { get; private set; }
	}

	public class ProjectFSM : ProjectDocument
	{
		public ProjectFSM()
		{			
		}
	}
}
