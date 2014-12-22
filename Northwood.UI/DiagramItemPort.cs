using System.Windows;
using System.Windows.Controls;

namespace Northwood.UI
{
	public class DiagramItemPort : HeaderedContentControl
	{
		static DiagramItemPort()
		{
			DefaultStyleKeyProperty.OverrideMetadata(typeof(DiagramItemPort), new FrameworkPropertyMetadata(typeof(DiagramItemPort)));
		}
	}
}