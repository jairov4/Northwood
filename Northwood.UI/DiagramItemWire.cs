using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Northwood.UI
{
	public class DiagramItemWire : DiagramItem
	{
		static DiagramItemWire()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagramItemWire), new FrameworkPropertyMetadata(typeof(DiagramItemWire)));
		}

		public double EndX
		{
			get { return (double)GetValue(EndXProperty); }
			set { SetValue(EndXProperty, value); }
		}

		// Using a DependencyProperty as the backing store for EndX.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty EndXProperty =
			DependencyProperty.Register("EndX", typeof(double), typeof(DiagramItemWire), new FrameworkPropertyMetadata(0.0));


		public double EndY
		{
			get { return (double)GetValue(EndYProperty); }
			set { SetValue(EndYProperty, value); }
		}

		// Using a DependencyProperty as the backing store for EndY.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty EndYProperty =
			DependencyProperty.Register("EndY", typeof(double), typeof(DiagramItemWire), new FrameworkPropertyMetadata(0.0));


		public WireType WireType
		{
			get { return (WireType)GetValue(WireTypeProperty); }
			set { SetValue(WireTypeProperty, value); }
		}

		// Using a DependencyProperty as the backing store for WireType.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty WireTypeProperty =
			DependencyProperty.Register("WireType", typeof(WireType), typeof(DiagramItemWire), new FrameworkPropertyMetadata(0.0));


		public Thickness WireThickness
		{
			get { return (Thickness)GetValue(WireThicknessProperty); }
			set { SetValue(WireThicknessProperty, value); }
		}

		// Using a DependencyProperty as the backing store for WireThickness.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty WireThicknessProperty =
			DependencyProperty.Register("WireThickness", typeof(Thickness), typeof(DiagramItemWire), new PropertyMetadata(0));

	}

	public enum WireType
	{
		Horizontal, Vertical, Diagonal, Free
	}
}
