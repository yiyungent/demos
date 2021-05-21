using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AspNetMvc_MultiTemplateDemo.Infrastructure
{
    public class ThemeViewEngine : RazorViewEngine
    {
        internal Func<string, string> GetExtensionThunk;

        private readonly string[] _emptyLocations = null;

        public ThemeViewEngine(IViewPageActivator viewPageActivator)
            : base(viewPageActivator)
        {
            this.AreaViewLocationFormats = new[]
              {
        "~/Themes/{3}/Areas/{2}/Views/{1}/{0}.cshtml",
        "~/Themes/{3}/Areas/{2}/Views/{1}/{0}.vbhtml",
        "~/Themes/{3}/Areas/{2}/Views/Shared/{0}.cshtml",
        "~/Themes/{3}/Areas/{2}/Views/Shared/{0}.vbhtml",

        "~/Areas/{2}/Views/{1}/{0}.cshtml",
        "~/Areas/{2}/Views/{1}/{0}.vbhtml",
        "~/Areas/{2}/Views/Shared/{0}.cshtml",
        "~/Areas/{2}/Views/Shared/{0}.vbhtml"
      };
            this.AreaMasterLocationFormats = new[]
              {
        "~/Themes/{3}/Areas/{2}/Views/{1}/{0}.cshtml",
        "~/Themes/{3}/Areas/{2}/Views/{1}/{0}.vbhtml",
        "~/Themes/{3}/Areas/{2}/Views/Shared/{0}.cshtml",
        "~/Themes/{3}/Areas/{2}/Views/Shared/{0}.vbhtml",

        "~/Areas/{2}/Views/{1}/{0}.cshtml",
        "~/Areas/{2}/Views/{1}/{0}.vbhtml",
        "~/Areas/{2}/Views/Shared/{0}.cshtml",
        "~/Areas/{2}/Views/Shared/{0}.vbhtml"
      };
            this.AreaPartialViewLocationFormats = new[]
              { "~/Themes/{3}/Areas/{2}/Views/{1}/{0}.cshtml",
        "~/Themes/{3}/Areas/{2}/Views/{1}/{0}.vbhtml",
        "~/Themes/{3}/Areas/{2}/Views/Shared/{0}.cshtml",
        "~/Themes/{3}/Areas/{2}/Views/Shared/{0}.vbhtml",

        "~/Areas/{2}/Views/{1}/{0}.cshtml",
        "~/Areas/{2}/Views/{1}/{0}.vbhtml",
        "~/Areas/{2}/Views/Shared/{0}.cshtml",
        "~/Areas/{2}/Views/Shared/{0}.vbhtml"
      };
            this.ViewLocationFormats = new[]
              {
        "~/Themes/{2}/Views/{1}/{0}.cshtml",
        "~/Themes/{2}/Views/{1}/{0}.vbhtml",
        "~/Themes/{2}/Views/Shared/{0}.cshtml",
        "~/Themes/{2}/Views/Shared/{0}.vbhtml",

        "~/Views/{1}/{0}.cshtml",
        "~/Views/{1}/{0}.vbhtml",
        "~/Views/Shared/{0}.cshtml",
        "~/Views/Shared/{0}.vbhtml"
      };
            this.MasterLocationFormats = new[]
              {
        "~/Themes/{2}/Views/{1}/{0}.cshtml",
        "~/Themes/{2}/Views/{1}/{0}.vbhtml",
        "~/Themes/{2}/Views/Shared/{0}.cshtml",
        "~/Themes/{2}/Views/Shared/{0}.vbhtml",

        "~/Views/{1}/{0}.cshtml",
        "~/Views/{1}/{0}.vbhtml",
        "~/Views/Shared/{0}.cshtml",
        "~/Views/Shared/{0}.vbhtml"
      };
            this.PartialViewLocationFormats = new[]
              {
        "~/Themes/{2}/Views/{1}/{0}.cshtml",
        "~/Themes/{2}/Views/{1}/{0}.vbhtml",
        "~/Themes/{2}/Views/Shared/{0}.cshtml",
        "~/Themes/{2}/Views/Shared/{0}.vbhtml",

        "~/Views/{1}/{0}.cshtml",
        "~/Views/{1}/{0}.vbhtml",
        "~/Views/Shared/{0}.cshtml",
        "~/Views/Shared/{0}.vbhtml"
      };
            this.FileExtensions = new[]
              {
        "cshtml",
        "vbhtml"
      };

            GetExtensionThunk = new Func<string, string>(VirtualPathUtility.GetExtension);
        }

        protected virtual string GetPath(ControllerContext controllerContext, string[] locations, string[] areaLocations, string locationsPropertyName, string name, string controllerName, string cacheKeyPrefix, bool useCache, out string[] searchedLocations)
        {
            string theme = GetCurrentTheme(controllerContext);
            searchedLocations = _emptyLocations;
            if (string.IsNullOrEmpty(name))
                return string.Empty;
            string areaName = GetAreaName(controllerContext.RouteData);

            bool flag = !string.IsNullOrEmpty(areaName);
            List<ThemeViewLocation> viewLocations = GetViewLocations(locations, flag ? areaLocations : null);
            if (viewLocations.Count == 0)
            {
                throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Properties cannot be null or empty.", new object[] { locationsPropertyName }));
            }
            bool isSpecificPath = IsSpecificPath(name);
            string key = this.CreateCacheKey(cacheKeyPrefix, name, isSpecificPath ? string.Empty : controllerName, areaName, theme);
            if (useCache)
            {
                var cached = this.ViewLocationCache.GetViewLocation(controllerContext.HttpContext, key);
                if (cached != null)
                {
                    return cached;
                }
            }
            if (!isSpecificPath)
            {
                return this.GetPathFromGeneralName(controllerContext, viewLocations, name, controllerName, areaName, theme, key, ref searchedLocations);
            }
            return this.GetPathFromSpecificName(controllerContext, name, key, ref searchedLocations);
        }

        protected virtual List<ThemeViewLocation> GetViewLocations(string[] viewLocationFormats, string[] areaViewLocationFormats)
        {
            var list = new List<ThemeViewLocation>();
            if (areaViewLocationFormats != null)
            {
                list.AddRange(areaViewLocationFormats.Select(s => new ThemeAreaAwareViewLocation(s)).Cast<ThemeViewLocation>());
            }
            if (viewLocationFormats != null)
            {
                list.AddRange(viewLocationFormats.Select(s => new ThemeViewLocation(s)));
            }
            return list;
        }

        protected virtual string GetCurrentTheme(ControllerContext controllerContext)
        {
            var theme = controllerContext.RequestContext.HttpContext.Request["Theme"];
            return theme;
        }

        protected virtual bool IsSpecificPath(string name)
        {
            char ch = name[0];
            if (ch != '~')
            {
                return (ch == '/');
            }
            return true;
        }

        protected virtual string CreateCacheKey(string prefix, string name, string controllerName, string areaName, string theme)
        {
            return string.Format(CultureInfo.InvariantCulture, ":ViewCacheEntry:{0}:{1}:{2}:{3}:{4}:{5}", new object[] { base.GetType().AssemblyQualifiedName, prefix, name, controllerName, areaName, theme });
        }

        protected virtual string GetAreaName(RouteData routeData)
        {
            object obj2;
            if (routeData.DataTokens.TryGetValue("area", out obj2))
            {
                return (obj2 as string);
            }
            return GetAreaName(routeData.Route);
        }

        protected virtual string GetAreaName(RouteBase route)
        {
            var area = route as IRouteWithArea;
            if (area != null)
            {
                return area.Area;
            }
            var route2 = route as Route;
            if ((route2 != null) && (route2.DataTokens != null))
            {
                return (route2.DataTokens["area"] as string);
            }
            return null;
        }

        protected virtual string GetPathFromSpecificName(ControllerContext controllerContext, string name, string cacheKey, ref string[] searchedLocations)
        {
            string virtualPath = name;
            if (!this.FilePathIsSupported(name) || !this.FileExists(controllerContext, name))
            {
                virtualPath = string.Empty;
                searchedLocations = new string[] { name };
            }
            this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
            return virtualPath;
        }

        protected virtual bool FilePathIsSupported(string virtualPath)
        {
            if (this.FileExtensions == null)
            {
                return true;
            }
            string str = this.GetExtensionThunk(virtualPath).TrimStart(new char[] { '.' });
            return this.FileExtensions.Contains<string>(str, StringComparer.OrdinalIgnoreCase);
        }

        protected virtual string GetPathFromGeneralName(ControllerContext controllerContext, List<ThemeViewLocation> locations, string name, string controllerName, string areaName, string theme, string cacheKey, ref string[] searchedLocations)
        {
            string virtualPath = string.Empty;
            searchedLocations = new string[locations.Count];
            for (int i = 0; i < locations.Count; i++)
            {
                string str2 = locations[i].Format(name, controllerName, areaName, theme);
                if (this.FileExists(controllerContext, str2))
                {
                    searchedLocations = _emptyLocations;
                    virtualPath = str2;
                    this.ViewLocationCache.InsertViewLocation(controllerContext.HttpContext, cacheKey, virtualPath);
                    return virtualPath;
                }
                searchedLocations[i] = str2;
            }
            return virtualPath;
        }
    }

    public class ThemeAreaAwareViewLocation : ThemeViewLocation
    {
        public ThemeAreaAwareViewLocation(string virtualPathFormatString)
            : base(virtualPathFormatString)
        {
        }

        public override string Format(string viewName, string controllerName, string areaName, string theme)
        {
            return string.Format(CultureInfo.InvariantCulture, _virtualPathFormatString, viewName, controllerName, areaName, theme);
        }
    }

    public class ThemeViewLocation
    {
        protected readonly string _virtualPathFormatString;

        public ThemeViewLocation(string virtualPathFormatString)
        {
            _virtualPathFormatString = virtualPathFormatString;
        }

        public virtual string Format(string viewName, string controllerName, string areaName, string theme)
        {
            return string.Format(CultureInfo.InvariantCulture, _virtualPathFormatString, viewName, controllerName, theme);
        }
    }
}