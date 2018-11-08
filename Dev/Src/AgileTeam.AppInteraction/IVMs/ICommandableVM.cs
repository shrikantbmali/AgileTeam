using System;
using System.Mvvm;
using System.Mvvm.EventArgs;
using System.Windows.Input;

namespace AgileTeam.AppInteraction.IVMs
{
	public interface ICommandableVM : IBindable, IProcedure<ICommandableVM>
	{
		ICommand Command { get; }
	}

	internal class CommandableVM : BindableBase<ICommandableVM>, ICommandableVM
	{
		public ICommand Command { get; protected set; }

		protected CommandableVM()
		{
		}

		public CommandableVM(Predicate<object> canExecute, Action<object> execute)
		{
			Command = new RelayCommand(canExecute, o =>
			{
				RaiseStartedEvent(this);

				try
				{
					execute(o);
				}
				catch (Exception)
				{
					RaiseFailedEvent(this);
				}

				RaiseCompletedEvent(this);
			});
		}

		#region EVENTS

		public event EventHandler<AssociatableEventArgs<ICommandableVM>> Started;

		protected void RaiseStartedEvent(ICommandableVM e)
		{
			var handler = Started;
			if (handler != null)
				handler(this, new AssociatableEventArgs<ICommandableVM>(e));
		}

		public event EventHandler<AssociatableEventArgs<ICommandableVM>> Failed;

		protected void RaiseFailedEvent(ICommandableVM e)
		{
			var handler = Failed;
			if (handler != null)
				handler(this, new AssociatableEventArgs<ICommandableVM>(e));
		}

		public event EventHandler<AssociatableEventArgs<ICommandableVM>> Completed;

		protected void RaiseCompletedEvent(ICommandableVM e)
		{
			var handler = Completed;
			if (handler != null)
				handler(this, new AssociatableEventArgs<ICommandableVM>(e));
		}

		#endregion EVENTS
	}

	internal class CommandableVM<TType> : CommandableVM
	{
		public CommandableVM(Predicate<TType> canExecute, Action<TType> execute)
		{
			Command = new RelayCommand<TType, TType>(canExecute, execute);
		}
	}
}