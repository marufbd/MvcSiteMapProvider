#region CODE HISTORY
/* -------------------------------------------------------------------------------- 
 * Client Name: 
 * Project Name: MyFrameWork.Mvc.MvcSiteMapProvider
 * Module: 
 * Name: TitanSiteMapProvider
 * Purpose:              
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  1/20/2012 10:17:03 AM
 *  Description: Development Starts
 * -------------------------------------------------------------------------------- */
#endregion CODE HISTORY

#region REFERENCES

using System;
using System.Collections.Specialized;
using System.Web;

#endregion REFERENCES

namespace MvcSiteMapProvider.MultiModuleProvider
{
    /// <summary>
    /// Adds support for integrating MvcSitemap.sitemap file from multiple assemblies
    /// </summary>
    public class ZephyrSiteMapProvider : DefaultSiteMapProvider
    {
        public string SiteMapFileName
        {
            get
            {
                if (this.siteMapFile.StartsWith("~/"))
                    return this.siteMapFile.Substring(2);
                else
                    return this.siteMapFile;
            }
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection attributes)
        {
            base.Initialize(name, attributes);
            
            this.SiteMapProviderEventHandler = new SiteMapProviderEventHandler();
        }

        protected override MvcSiteMapNode CreateSiteMapNode(string key, NameValueCollection explicitResourceKeys, string implicitResourceKey)
        {
            return new ZephyrMvcSiteMapNode(this, key, explicitResourceKeys, implicitResourceKey);
        }


        //identify the mvcsitemapnodes with module attribute specified
        //and create a TitanMvcSiteMapNode with module attribute being set
        public override MvcSiteMapNode GetSiteMapNodeFromXmlElement(System.Xml.Linq.XElement node, SiteMapNode parentNode)
        {
            var siteMapNode = (ZephyrMvcSiteMapNode)base.GetSiteMapNodeFromXmlElement(node, parentNode);

            //setup for module
            var module = node.GetAttributeValue("moduleName");
            if (!String.IsNullOrEmpty(module))
            {
                siteMapNode.Module = module;
            }

            //setup for parentKey
            var parentKey = node.GetAttributeValue("parentKey");
            if (!String.IsNullOrEmpty(parentKey))
            {
                siteMapNode.ParentKey = parentKey;
            }

            return siteMapNode;
        }

        protected override void AddNode(SiteMapNode node, SiteMapNode parentNode)
        {
            var currentNode = node as ZephyrMvcSiteMapNode;

            if (!String.IsNullOrEmpty(currentNode.ParentKey))
            {
                parentNode = this.FindSiteMapNodeFromKey(currentNode.ParentKey) as ZephyrMvcSiteMapNode;
                currentNode.ParentNode = parentNode;
            }

            base.AddNode(node, parentNode);
        }
    }
}