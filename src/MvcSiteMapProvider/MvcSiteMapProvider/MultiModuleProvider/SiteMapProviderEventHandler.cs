#region CODE HISTORY
/* -------------------------------------------------------------------------------- 
 * Client Name: 
 * Project Name: MyFrameWork.Mvc.MvcSiteMapProvider
 * Module: 
 * Name: Class1
 * Purpose:              
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  1/20/2012 11:57:37 AM
 *  Description: Development Starts
 * -------------------------------------------------------------------------------- */
#endregion CODE HISTORY

#region REFERENCES

using System;
using System.Reflection;
using System.Xml.Linq;
using MvcSiteMapProvider.Extensibility;

#endregion REFERENCES

namespace MvcSiteMapProvider.MultiModuleProvider
{
    /// <summary>
    /// Event handler to add nodes from multiple assemblies Mvc.Sitemap files embedded resources
    /// </summary>
    public class SiteMapProviderEventHandler : ISiteMapProviderEventHandler
    {
        public bool OnAddingSiteMapNode(SiteMapProviderEventContext context)
        {
            var titanNode = (ZephyrMvcSiteMapNode)context.CurrentNode;
            var provider = (ZephyrSiteMapProvider)context.Provider;

            //if this is a module node go search the module assembly for xml sitemap embedded
            if (!String.IsNullOrEmpty(titanNode.Module))
            {
                System.IO.Stream xmlResStream = Assembly.Load(titanNode.Module).
                    GetManifestResourceStream(titanNode.Module + "." +
                                              provider.SiteMapFileName);
                if (xmlResStream == null)
                    throw new MvcSiteMapException(@"Error loading xml resource from module.
Possible reasons:
Module sitemap filename is not exactly in same case as configured in web.config.
Module assembly name does not match with sitemap module.");

                XDocument doc = XDocument.Load(xmlResStream);

                var rootElement = doc.Element(provider.SiteMapNamespace + provider.RootName);
                titanNode = (ZephyrMvcSiteMapNode)provider.GetSiteMapNodeFromXmlElement(rootElement, null);

                // Process our XML file, passing in the parentnode sitemap node and xml element.
                provider.ProcessXmlNodes(context.ParentNode, rootElement);


                //this node need not be added by the provider
                //as this is just a placeholder for module nodes
                return false;
            }

            return true;
        }

        public void OnAddedSiteMapNode(SiteMapProviderEventContext context)
        {
            //throw new NotImplementedException();
        }
    }
}