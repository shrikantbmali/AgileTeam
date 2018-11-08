using System;

namespace AgileTeam.Wpf.Logic.AppExecution
{
	public abstract class AppExecutionFlow
	{
		private static readonly Lazy<AppExecutionFlow> _lazyAppExecutionFlow = new Lazy<AppExecutionFlow>(Initialize);
		private readonly AppExecutionFlow _successor;

		public static AppExecutionFlow Instance
		{
			get { return _lazyAppExecutionFlow.Value; }
		}

		protected abstract ExectionFlowType ExectionFlowType { get; }

		public static event EventHandler ApplicationShutdownRequested;

		public static event EventHandler FatalErrorOccured;

		public static event EventHandler<ExecutionFlowChangedEvenArgs> ExecutionFlowChanged;

		protected AppExecutionFlow(AppExecutionFlow successor)
		{
			_successor = successor;
		}

		private static AppExecutionFlow Initialize()
		{
			return
				new SqlServerSetupExecutionFlow(
					new UserAccountSetupCheckExecutionFlow(
						new MainExecutionFlow(null)));
		}

		protected abstract void Execute();

		private void RaiseFatalErrorOccuredEvent()
		{
			var handler = FatalErrorOccured;
			if (handler != null)
				handler(Instance, EventArgs.Empty);
		}

		private static void RiaseExecutionFlowChanged(ExecutionFlowChangedEvenArgs e)
		{
			var handler = ExecutionFlowChanged;
			if (handler != null)
				handler(Instance, e);
		}

		public static void ShutDownApplication()
		{
			var handler = ApplicationShutdownRequested;
			if (handler != null)
				handler(Instance, EventArgs.Empty);
		}

		protected void ToSuccessor()
		{
			RiaseExecutionFlowChanged(new ExecutionFlowChangedEvenArgs(_successor.ExectionFlowType));
			_successor.Execute();
		}

		public void Start()
		{
			RiaseExecutionFlowChanged(new ExecutionFlowChangedEvenArgs(ExectionFlowType));
			Execute();
		}
	}
}