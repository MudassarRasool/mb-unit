using System;
using System.Collections.Generic;
using MbUnit.Framework.Kernel.Events;
using MbUnit.Framework.Kernel.Actions;

namespace MbUnit.Framework.Kernel.Model
{
    /// <summary>
    /// Base class for MbUnit-derived tests.
    /// </summary>
    public class MbUnitTest : BaseTest
    {
        private readonly ActionChain<MbUnitTestState> setUpChain;
        private readonly ActionChain<MbUnitTestState> executeChain;
        private readonly ActionChain<MbUnitTestState> tearDownChain;
        private readonly ActionChain<MbUnitTestState> beforeChildChain;
        private readonly ActionChain<MbUnitTestState> afterChildChain;
        private TimeSpan? timeout;

        /// <summary>
        /// Initializes a test initially without a parent.
        /// </summary>
        /// <param name="name">The name of the component</param>
        /// <param name="codeReference">The point of definition</param>
        /// <param name="templateBinding">The template binding that produced this test</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/>,
        /// <paramref name="codeReference"/> or <paramref name="templateBinding"/> is null</exception>
        public MbUnitTest(string name, CodeReference codeReference, MbUnitTemplateBinding templateBinding)
            : base(name, codeReference, templateBinding)
        {
            setUpChain = new ActionChain<MbUnitTestState>();
            executeChain = new ActionChain<MbUnitTestState>();
            tearDownChain = new ActionChain<MbUnitTestState>();
            beforeChildChain = new ActionChain<MbUnitTestState>();
            afterChildChain = new ActionChain<MbUnitTestState>();
        }

        /// <summary>
        /// Gets or sets the maximum amount of time the whole test including
        /// it setup, teardown and body should be permitted to run.  If the test
        /// runs any longer than this, it will be aborted by the framework.
        /// The timeout may be null to indicate the absence of a timeout.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="value"/>
        /// represents a negative time span</exception>
        public TimeSpan? Timeout
        {
            get { return timeout; }
            set
            {
                if (value.HasValue && value.Value.Ticks < 0)
                    throw new ArgumentOutOfRangeException(@"value");
                timeout = value;
            }
        }

        /// <summary>
        /// Gets the chain of actions to perform while the test
        /// is being set up prior to execution.  The chain may be
        /// extended to inject new behaviors.
        /// </summary>
        /// <remarks>
        /// The actions in the chain are executed as part of the
        /// <see cref="LifecyclePhase.SetUp" /> lifecycle phase
        /// of the test.
        /// </remarks>
        public ActionChain<MbUnitTestState> SetUpChain
        {
            get { return setUpChain; }
        }

        /// <summary>
        /// Gets the chain of actions to perform while the main body
        /// of the test is executing.  The chain may be
        /// extended to inject new behaviors.
        /// </summary>
        /// <remarks>
        /// The actions in the chain are executed as part of the
        /// <see cref="LifecyclePhase.Execute" /> lifecycle phase
        /// of the test.
        /// </remarks>
        public ActionChain<MbUnitTestState> ExecuteChain
        {
            get { return executeChain; }
        }

        /// <summary>
        /// Gets the chain of actions to perform while the test is being
        /// torn down after execution.  The chain may be
        /// extended to inject new behaviors.
        /// </summary>
        /// <remarks>
        /// The actions in the chain are executed as part of the
        /// <see cref="LifecyclePhase.TearDown" /> lifecycle phase
        /// of the test.
        /// </remarks>
        public ActionChain<MbUnitTestState> TearDownChain
        {
            get { return tearDownChain; }
        }

        /// <summary>
        /// Gets the chain of actions to perform before each nested test
        /// is executed.  The chain may be extended to inject new behaviors.
        /// </summary>
        /// <remarks>
        /// The actions in the chain are executed as part of the
        /// <see cref="LifecyclePhase.SetUp" /> lifecycle phase
        /// of the nested test before the test's own <see cref="SetUpChain" /> runs.
        /// </remarks>
        public ActionChain<MbUnitTestState> BeforeChildChain
        {
            get { return beforeChildChain; }
        }

        /// <summary>
        /// Gets the chain of actions to perform after each nested test
        /// is executed.  The chain may be extended to inject new behaviors.
        /// </summary>
        /// <remarks>
        /// The actions in the chain are executed as part of the
        /// <see cref="LifecyclePhase.TearDown" /> lifecycle phase
        /// of the nested test after the test's own <see cref="TearDownChain" /> runs.
        /// </remarks>
        public ActionChain<MbUnitTestState> AfterChildChain
        {
            get { return afterChildChain; }
        }
    }
}