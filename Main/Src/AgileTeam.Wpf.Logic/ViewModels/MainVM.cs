using System.Infrastructure.Services;
using AgileTeam.Core;
using AgileTeam.Wpf.Logic.AppExecution;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;
using System.Mvvm;
using System.Windows.Input;

namespace AgileTeam.Wpf.Logic.ViewModels
{
	internal class MainVM : AgileTeamVM, IMainViewModel
	{
		public ICommand SettingsCommand { get; private set; }

		public ICommand ClosedCommand { get; private set; }

		public string Title
		{
			get { return UserName + " : Agile Team"; }
		}

		public string UserName
		{
			get { return LoginManager.Instance.CurrentUser.FirstName; }
		}

		public IStartViewModel StartViewModel { get; private set; }

		public VMVisibility StartViewVisibility
		{
			get { return GetValue<VMVisibility>(); }
			set { SetValue(value); }
		}

		public string HeaderText
		{
			get { return GetValue<string>(); }
			private set { SetValue(value); }
		}

		public MainVM(IMessengerService messengerService)
			: base(messengerService)
		{
			SettingsCommand = new RelayCommand(null, ShowSettingsView);
			ClosedCommand = new RelayCommand(null, MainWindowClosed);

			StartViewModel = ViewModelFactory.Instance.GetStartViewModel();

			StartViewModel.ProjectSelected += StartViewModel_ProjectSelected;

			HeaderText = "Agile Team";
		}

		private void StartViewModel_ProjectSelected(object sender, System.EventArgs e)
		{
			StartViewVisibility = VMVisibility.Hidden;
			HeaderText += " | " + StartViewModel.Project.Title;
		}

		private static void MainWindowClosed(object obj)
		{
			AppExecutionFlow.ShutDownApplication();
		}

		private void ShowSettingsView(object obj)
		{
			var settingsViewModel = ViewModelFactory.Instance.GetSettingsViewModel();

			MessengerService.ShowViewAsChildDialog<ISettingsViewModel, IMainViewModel>(
				settingsViewModel);
		}
	}
}