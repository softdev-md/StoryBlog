using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Api.Application.Infrastructure
{
    /// <summary>
    /// Provides access to the singleton instance of the Nop engine.
    /// </summary>
    public class EngineContext
    {
        #region Methods

        /// <summary>
        /// Create a static instance of the app engine.
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Create()
        {
            //create WebAppEngine as engine
            return Singleton<IEngine>.Instance ?? (Singleton<IEngine>.Instance = new WebAppEngine());
        }

        /// <summary>
        /// Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.
        /// </summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }
        
        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton Nop engine used to access Nop services.
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Create();
                }

                return Singleton<IEngine>.Instance;
            }
        }

        #endregion
    }
}
