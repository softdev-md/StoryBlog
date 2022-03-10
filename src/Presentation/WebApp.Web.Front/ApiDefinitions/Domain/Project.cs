using System;

namespace WebApp.Web.Front.ApiDefinitions.Domain
{
    /// <summary>
    /// Represents project
    /// </summary>
    public partial class Project
    {
        #region Properties

        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        #endregion
    }
}