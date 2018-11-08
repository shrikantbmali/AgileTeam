using System.Infrastructure.Services;
using AgileTeam.Core;
using AgileTeam.Core.Tasks;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;
using System;
using System.Collections.ObjectModel;
using System.Mvvm;
using System.Windows.Input;

namespace AgileTeam.Wpf.Logic.ViewModels
{
	internal class StartVM : AgileTeamVM, IStartViewModel
	{
		private Project _project;

		public ICommand NewProjectCommand { get; private set; }

		public ICommand SelectProjectCommand { get; private set; }

		public ICommand SelectRecentProjectCommand { get; private set; }

		public ObservableCollection<Project> RecentProjects { get; private set; }

		public Project Project
		{
			get
			{
				return _project;
			}
			private set
			{
				_project = value;
				RaiseProjectSelectedEvent();
			}
		}

		public event EventHandler ProjectSelected;

		public StartVM(IMessengerService messengerService)
			: base(messengerService)
		{
			NewProjectCommand = new RelayCommand(null, CreateNewProject);
			SelectProjectCommand = new RelayCommand(null, SelectProject);
			SelectRecentProjectCommand = new RelayCommand(CanSelectRecentProject, SelectRecentProject);

			RecentProjects = new ObservableCollection<Project>();
			foreach (var project in Project.GetAllProjects())
				RecentProjects.Add(project);
		}

		private void SelectRecentProject(object obj)
		{
			var project = obj as Project;

			if (project == null)
				return;

			Project = project;
		}

		private static bool CanSelectRecentProject(object obj)
		{
			return obj != null;
		}

		private void SelectProject(object obj)
		{
			var selectProjectViewModel = ViewModelFactory.Instance.GetSelectProjectViewModel();

			MessengerService.ShowViewAsChildDialog<ISelectProjectViewModel, IMainViewModel>(
				selectProjectViewModel);

			if (selectProjectViewModel.Project == null)
				return;

			Project = selectProjectViewModel.Project;
		}

		private void CreateNewProject(object obj)
		{
			if (!AgileTeamCoreFactory.PermissionResolver.CanCreateNewProject(LoginManager.Instance.CurrentUser))
				return;

			var newProjectViewModel = ViewModelFactory.Instance.GetCreateNewProjectViewModel();

			MessengerService.ShowViewAsChildDialog<ICreateNewProjectViewModel, IMainViewModel>(
				newProjectViewModel);

			if (newProjectViewModel.Project == null)
				return;

			Project = newProjectViewModel.Project;
		}

		private void RaiseProjectSelectedEvent()
		{
			var handler = ProjectSelected;
			if (handler != null) handler(this, EventArgs.Empty);
		}
	}
}