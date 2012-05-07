#region Using directives

using System.Collections.Specialized;
using System.Web;

#endregion

namespace MvcSiteMapProvider.MultiModuleProvider
{
    /// <summary>
    /// MvcSiteMapNode class
    /// </summary>
    public class ZephyrMvcSiteMapNode
        : MvcSiteMapNode
    {
        /// <summary>
        /// Gets or sets the moduleName attribute.
        /// </summary>
        /// <value>Name of the module assembly</value>
        public string Module
        {
            get { return this["module"] ?? ""; }
            set { this["module"] = value; }
        }

        public string ParentKey
        {
            get { return this["parentKey"] ?? ""; }
            set { this["parentKey"] = value; }
        }

        public ZephyrMvcSiteMapNode(SiteMapProvider provider, string key, NameValueCollection explicitResourceKeys, string implicitResourceKey)
            : base(provider, key, explicitResourceKeys, implicitResourceKey)
        {
            //this.UrlResolver=new DefaultSiteMapNodeUrlResolver();
        }
    }
}