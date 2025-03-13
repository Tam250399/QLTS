using System;
using System.Linq;

namespace GS.Web.Framework.Menu
{
    /// <summary>
    /// Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Checks whether this node or child ones has a specified system name
        /// </summary>
        /// <param name="node">Node</param>
        /// <param name="systemName">System name</param>
        /// <returns>Result</returns>
        public static bool ContainsSystemName(this SiteMapNode node, string systemName)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (string.IsNullOrWhiteSpace(systemName))
                return false;

            if (systemName.Equals(node.SystemName, StringComparison.InvariantCultureIgnoreCase))
                return true;

            return node.ChildNodes.Any(cn => ContainsSystemName(cn, systemName));
        }
        /// <summary>
        /// Lay thong tin node menu chinh, tu sytemname de ve menu doc
        /// </summary>
        /// <param name="node"></param>
        /// <param name="systemName"></param>
        /// <returns></returns>
        public static SiteMapNode GetNodeRootMenu(this SiteMapNode node, string systemName)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (string.IsNullOrWhiteSpace(systemName))
                return null;
            //la muc 1 de tra ket qua
            foreach(var childNode in node.ChildNodes)
            {
                if(childNode.IsMainMenu)
                {
                    //neu dang la node hien tai thi return 
                    if (systemName.Equals(childNode.SystemName, StringComparison.InvariantCultureIgnoreCase))
                        return childNode;
                    //kiem tra xem co con dang active ko
                    if(childNode.ChildNodes.Any(cn => ContainsSystemName(cn, systemName)))
                        return childNode;
                }    
            }
            return null;
           
        }
    }
}
