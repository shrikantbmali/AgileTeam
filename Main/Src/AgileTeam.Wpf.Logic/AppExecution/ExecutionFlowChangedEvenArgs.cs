using System;

namespace AgileTeam.Wpf.Logic.AppExecution
{
	public class ExecutionFlowChangedEvenArgs : EventArgs
	{
		public ExectionFlowType CurrentExectionFlow { get; private set; }

		public ExecutionFlowChangedEvenArgs(ExectionFlowType currentExectionFlow)
		{
			CurrentExectionFlow = currentExectionFlow;
		}
	}
}