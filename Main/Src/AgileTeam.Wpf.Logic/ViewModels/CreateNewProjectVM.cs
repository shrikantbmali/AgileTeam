using System.Infrastructure.Services;
using AgileTeam.Core;
using AgileTeam.Core.Tasks;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;
using System.Mvvm;
using System.Windows.Input;

namespace AgileTeam.Wpf.Logic.ViewModels
{
	internal class CreateNewProjectVM : AgileTeamVM, ICreateNewProjectViewModel
	{
		public string ProjectTitle { get; set; }

		public string ProjectDescription { get; set; }

		public ICommand CancelCommand { get; private set; }

		public ICommand CreateProjectCommand { get; private set; }

		public Project Project { get; private set; }

		public CreateNewProjectVM(IMessengerService messengerService)
			: base(messengerService)
		{
			CancelCommand = new RelayCommand(null, Cancel);
			CreateProjectCommand = new RelayCommand(CanCreateProject, CreateNewProject);
		}

		private bool CanCreateProject(object obj)
		{
			return !string.IsNullOrEmpty(ProjectTitle) && !string.IsNullOrEmpty(ProjectDescription);
		}

		private void CreateNewProject(object obj)
		{
			Project = Project.New(LoginManager.Instance.CurrentUser, ProjectTitle, ProjectDescription);
			Close<ICreateNewProjectViewModel>(System.Infrastructure.Dialogs.DialogResult.OK, this);
		}

		private void Cancel(object obj)
		{
			Close<ICreateNewProjectViewModel>(System.Infrastructure.Dialogs.DialogResult.Cancel, this);
		}
	}
}