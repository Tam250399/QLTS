//code from Telerik MVC Extensions
using System.Threading.Tasks;
using GS.Core.Domain.DanhMuc;
using GS.Core.Infrastructure;
using GS.Services.DanhMuc;
using GS.Services.HeThong;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Nop.Web.Framework.Menu;

namespace GS.Web.Framework.Menu
{
    /// <summary>
    /// XML sitemap
    /// </summary>
    public class XmlSiteMap : IXmlSiteMap
    {

        /// <summary>
        /// Ctor
        /// </summary>
        ///
        public XmlSiteMap(
            )
        {
            RootNode = new SiteMapNode();
        }

        /// <summary>
        /// Root node
        /// </summary>
        public SiteMapNode RootNode { get; set; }

        #region Load All site map
        /// <summary>
        /// Load sitemap
        /// </summary>
        /// <param name="physicalPath">Filepath to load a sitemap</param>
        public virtual async Task LoadFromAsync(string physicalPath)
        {
            var fileProvider = EngineContext.Current.Resolve<IGSFileProvider>();

            var filePath = fileProvider.MapPath(physicalPath);
            var content = fileProvider.ReadAllText(filePath, Encoding.UTF8);

            if (!string.IsNullOrEmpty(content))
            {
                using (var sr = new StringReader(content))
                {
                    using (var xr = XmlReader.Create(sr,
                            new XmlReaderSettings
                            {
                                CloseInput = true,
                                IgnoreWhitespace = true,
                                IgnoreComments = true,
                                IgnoreProcessingInstructions = true
                            }))
                    {
                        var doc = new XmlDocument();
                        doc.Load(xr);

                        if ((doc.DocumentElement != null) && doc.HasChildNodes)
                        {
                            var xmlRootNode = doc.DocumentElement.FirstChild;
                            IterateAsync(RootNode, xmlRootNode);
                        }
                    }
                }
            }
        }

        protected virtual async Task IterateAsync(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            await PopulateNodeAsync(siteMapNode, xmlNode);

            foreach (XmlNode xmlChildNode in xmlNode.ChildNodes)
            {
                if (xmlChildNode.LocalName.Equals("siteMapNode", StringComparison.InvariantCultureIgnoreCase))
                {
                    var siteMapChildNode = new SiteMapNode();
                    siteMapNode.ChildNodes.Add(siteMapChildNode);

                    IterateAsync(siteMapChildNode, xmlChildNode);
                }
            }
        }
        #endregion

        #region  Common
        protected virtual async Task PopulateNodeAsync(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            //Unit curent 
            var _workContext = EngineContext.Current.Resolve<Core.IWorkContext>(); ;



            //is type unit nhập liệu
            #region permission
            //permission name

            var permissionNames = GetStringValueFromAttribute(xmlNode, "PermissionNames");
            if (!string.IsNullOrEmpty(permissionNames))
            {
                var typeUnitNhapLieu = GetStringValueFromAttribute(xmlNode, "TypeUnitNhapLieu");
                var permissionService = EngineContext.Current.Resolve<IQuyenService>();


                siteMapNode.Visible = permissionNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Any(permissionName => permissionService.Authorize(permissionName.Trim()));

                //siteMapNode.Visible = permissionNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                //   .Any(permissionName => (permissionService.Authorize(permissionName.Trim()) && string.IsNullOrWhiteSpace(typeUnitNhapLieu)) ||
                //   (permissionService.Authorize(permissionName.Trim()) && !string.IsNullOrWhiteSpace(typeUnitNhapLieu) && Convert.ToBoolean(typeUnitNhapLieu).Equals(_workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU.GetValueOrDefault(false))));

                if (!string.IsNullOrWhiteSpace(typeUnitNhapLieu) && !_workContext.CurrentDonVi.IS_LA_DON_VI_NHAP_LIEU.GetValueOrDefault(false).Equals(Convert.ToBoolean(typeUnitNhapLieu)))
                       siteMapNode.Visible = false;

                if (siteMapNode.SystemName == "BaoCaoDoiChieuData" && !string.IsNullOrWhiteSpace(typeUnitNhapLieu) && Convert.ToBoolean(typeUnitNhapLieu) == false && _workContext.CurrentDonVi.TREE_LEVEL != 1)
                    siteMapNode.Visible = false;

            }
            else
            {
                siteMapNode.Visible = true;
            }

            //is type unit only tài sản dự án
            //Đơn vị chỉ quản lý riêng tài sản dự án, không quản lý tài sản công.
            var _donViService = EngineContext.Current.Resolve<IDonViService>();
            var typeUnitOnlyTaiSanDuAn = GetStringValueFromAttribute(xmlNode, "TypeUnitOnlyTaiSanDuAn");
            if (!string.IsNullOrEmpty(typeUnitOnlyTaiSanDuAn) && Convert.ToBoolean(typeUnitOnlyTaiSanDuAn))
            {
                var isBanQuanLyDuAn = _donViService.IsOnlyTaiSanDuAn(donViId: _workContext.CurrentDonVi.ID);
                if (!isBanQuanLyDuAn)
                {
                    siteMapNode.Visible = false;
                }
            }
            else if (!string.IsNullOrEmpty(typeUnitOnlyTaiSanDuAn) && !Convert.ToBoolean(typeUnitOnlyTaiSanDuAn))
            {
                if (_workContext.CurrentDonVi.LA_BAN_QL_DU_AN ?? false)
                {
                    var donVi = _donViService.GetDonViById(_workContext.CurrentDonVi.ID);
                    var maDonViVp = donVi.MA + "001";
                    DonVi donViVp = _donViService.GetDonViByMa(maDonViVp);
                    if (donViVp != null && (!donViVp.LA_BAN_QL_DU_AN.HasValue || donViVp.LA_BAN_QL_DU_AN == false))
                    {
                        siteMapNode.Visible = true;
                    }
                }
                //var isBanQuanLyDuAn = _donViService.IsOnlyTaiSanDuAn(donViId: _workContext.CurrentDonVi.ID);
                //if (isBanQuanLyDuAn)
                //{
                //    siteMapNode.Visible = false;
                //}
            }
            //đơn vị 
            //is type unit quản lý dự án
            var typeUnitQLDuAn = GetStringValueFromAttribute(xmlNode, "TypeUnitQLDuAn");
            if (!string.IsNullOrEmpty(typeUnitQLDuAn) && Convert.ToBoolean(typeUnitQLDuAn))
            {
                var isBanQuanLyDuAn = _donViService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
                if (!isBanQuanLyDuAn)
                {
                    siteMapNode.Visible = false;
                }
            }
            else if (!string.IsNullOrEmpty(typeUnitQLDuAn) && !Convert.ToBoolean(typeUnitQLDuAn))
            {
                var isBanQuanLyDuAn = _donViService.isHasChildDonViBanQLDA(donViId: _workContext.CurrentDonVi.ID);
                if (isBanQuanLyDuAn)
                {
                    siteMapNode.Visible = false;
                }
            }
            //is type unit đơn vị sự nghiêp
            var typeUnitDonViSuNghiep = GetStringValueFromAttribute(xmlNode, "TypeUnitDonViSuNghiep");
            if (!string.IsNullOrEmpty(typeUnitDonViSuNghiep) && Convert.ToBoolean(typeUnitDonViSuNghiep))
            {
                if (!_donViService.isDonViSuNghiep(donViId: _workContext.CurrentDonVi.ID))
                {
                    siteMapNode.Visible = false;
                }
            }
            else if (!string.IsNullOrEmpty(typeUnitDonViSuNghiep) && !Convert.ToBoolean(typeUnitDonViSuNghiep))
            {
                if (_donViService.isDonViSuNghiep(donViId: _workContext.CurrentDonVi.ID))
                {
                    siteMapNode.Visible = false;
                }
            }
            #endregion
            #region
            //system name
            siteMapNode.SystemName = GetStringValueFromAttribute(xmlNode, "SystemName");

            //title
            var nopResource = GetStringValueFromAttribute(xmlNode, "nopResource");
            var _nhanHienThiService = EngineContext.Current.Resolve<INhanHienThiService>();
            siteMapNode.Title = _nhanHienThiService.GetGiaTri(nopResource);

            //routes, url
            var controllerName = GetStringValueFromAttribute(xmlNode, "controller");
            var actionName = GetStringValueFromAttribute(xmlNode, "action");
            var url = GetStringValueFromAttribute(xmlNode, "url");
            if (!string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(actionName))
            {
                siteMapNode.ControllerName = controllerName;
                siteMapNode.ActionName = actionName;

                //apply admin area as described here - https://www.nopcommerce.com/boards/t/20478/broken-menus-in-admin-area-whilst-trying-to-make-a-plugin-admin-page.aspx
                //siteMapNode.RouteValues = new RouteValueDictionary { { "area", AreaNames.Admin } };
            }
            else if (!string.IsNullOrEmpty(url))
            {
                siteMapNode.Url = url;
            }

            //image URL
            siteMapNode.IconClass = GetStringValueFromAttribute(xmlNode, "IconClass");

            //them 2 thuoc tinh BadgeId, BadegCss de hien thi so luong
            //BadgeId
            siteMapNode.BadgeId = GetStringValueFromAttribute(xmlNode, "BadgeId");
            //BadgeCss
            siteMapNode.BadgeCss = GetStringValueFromAttribute(xmlNode, "BadgeCss");

            #endregion
            // Open URL in new tab
            var openUrlInNewTabValue = GetStringValueFromAttribute(xmlNode, "OpenUrlInNewTab");
            if (!string.IsNullOrWhiteSpace(openUrlInNewTabValue) && bool.TryParse(openUrlInNewTabValue, out bool booleanResult))
            {
                siteMapNode.OpenUrlInNewTab = booleanResult;
            }
            //thiet lap main menu
            var isMainMenu = GetStringValueFromAttribute(xmlNode, "IsMainMenu");
            if (!string.IsNullOrWhiteSpace(isMainMenu) && bool.TryParse(isMainMenu, out bool IsMainMenu))
            {
                siteMapNode.IsMainMenu = IsMainMenu;
            }
        }

        private static string GetStringValueFromAttribute(XmlNode node, string attributeName)
        {
            string value = null;

            if (node.Attributes != null && node.Attributes.Count > 0)
            {
                var attribute = node.Attributes[attributeName];

                if (attribute != null)
                {
                    value = attribute.Value;
                }
            }

            return value;
        }
        #endregion
    }
}
