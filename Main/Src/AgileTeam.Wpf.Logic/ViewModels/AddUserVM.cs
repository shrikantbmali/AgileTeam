using System.Infrastructure.Services;
using AgileTeam.Core;
using AgileTeam.Core.Entities;
using AgileTeam.Wpf.Logic.ViewModels.IViewModels;
using System.IO;
using System.Mvvm;
using System.Windows.Input;

namespace AgileTeam.Wpf.Logic.ViewModels
{
	internal sealed class AddUserVM : AgileTeamVM, IAddUserViewModel
	{
		private readonly UserPropertiesEntity _userProperties;

		public bool IsUserAdded { get; private set; }

		public byte[] Image
		{
			get { return _userProperties.Image; }
			set
			{
				UpdateImageValue(value);
				RaisePropertyChangedEvent();
			}
		}

		public string FirstName
		{
			get { return _userProperties.FirstName; }
			set
			{
				_userProperties.FirstName = value;
				RaisePropertyChangedEvent();
			}
		}

		public string LastName
		{
			get { return _userProperties.LastName; }
			set
			{
				_userProperties.LastName = value;
				RaisePropertyChangedEvent();
			}
		}

		public string Username
		{
			get { return GetValue<string>(); }
			set { SetValue(value); }
		}

		public string Password
		{
			get { return GetValue<string>(); }
			set { SetValue(value); }
		}

		public string PasswordToCheck
		{
			get { return GetValue<string>(); }
			set { SetValue(value); }
		}

		public ICommand SelectImageCommand { get; private set; }

		public ICommand CreateUserCommand { get; private set; }

		public ICommand CancelCommand { get; private set; }

		public IUserPermissionViewModel UserPermissionViewModel { get; private set; }

		public AddUserVM(IMessengerService messengerService, bool createUserAsAdmin)
			: base(messengerService)
		{
			_userProperties = new UserPropertiesEntity { Title = "Mr." };

			UserPermissionViewModel = ViewModelFactory.Instance.GetUserPermissionViewModel(createUserAsAdmin);

			SelectImageCommand = new RelayCommand(o => true, SelectImage);
			CreateUserCommand = new RelayCommand(CanCreateUser, CreateUser);
			CancelCommand = new RelayCommand(o => true, Cancel);
		}

		private void UpdateImageValue(byte[] value)
		{
			_userProperties.Image = value == null || value.Length == 0 ? new byte[] { 0 } : value;
		}

		private void Cancel(object obj)
		{
			MessengerService.CloseView<IAddUserViewModel>();
		}

		private void CreateUser(object obj)
		{
			UpdateImageValue(_userProperties.Image);

			IsUserAdded = LoginManager.Instance.TryCreateFirstUser(new UserCredentials(Username, Password),
				_userProperties, UserPermissionViewModel.Permissions);

			if (IsUserAdded)
				Close<IAddUserViewModel>(System.Infrastructure.Dialogs.DialogResult.OK, this);
		}

		private bool CanCreateUser(object obj)
		{
			return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName) &&
				   (!string.IsNullOrEmpty(Password) && Password.Equals(PasswordToCheck));
		}

		private void SelectImage(object obj)
		{
			var imageFile = MessengerService.ShowSelectFileDialog<IAddUserViewModel>(false,
				new FileSelectionFilter("Image Files (*.png;*.jpg)") { "png", "jpg" });

			Image = ReadImageBytes(imageFile);
		}

		private static byte[] ReadImageBytes(string imageFile)
		{
			using (var fileStream = File.Open(imageFile, FileMode.Open))
			{
				var imageBytes = new byte[fileStream.Length];

				fileStream.Read(imageBytes, 0, imageBytes.Length);

				fileStream.Close();

				return imageBytes;
			}
		}
	}
}