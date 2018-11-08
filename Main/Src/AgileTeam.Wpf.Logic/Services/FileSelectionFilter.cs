using System.Collections.Generic;

namespace AgileTeam.Wpf.Logic.Services
{
	public class FileSelectionFilter
	{
		private readonly List<string> _extensions;

		public string Title { get; private set; }

		public IEnumerable<string> Extensions { get { return _extensions; } }

		public FileSelectionFilter(string title)
		{
			_extensions = new List<string>();

			Title = title;
		}

		public void AddExtension(string extension)
		{
			_extensions.Add(extension);
		}
	}
}