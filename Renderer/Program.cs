using IRR2.Common.Tests;
using IRR2.Common.Tests.Context;
using IRR2.Common.Tests.MvcRendering;
using IRR2.WebUI.UnitTests.TestUtilities;
using Newtonsoft.Json;
using Renderer.TestUtilities;
//using RenderingTest.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Caching;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using System.Web.WebPages.Scope;
using System.Xml.Serialization;

namespace Renderer
{
    class Program
    {   
        static void Main(string[] args)
        {
            var root = @"C:\tmp\irr2\IRR2.WebUI\";
            //var root = @"C:\Users\drago\Desktop\renderingTest\RenderingTest";
            AppDomain.CurrentDomain.AssemblyResolve += (s, e) =>
            {
                string assembliesDir = root + @"bin";
                var asmPath = Path.Combine(assembliesDir, e.Name + ".dll");
                if (!File.Exists(asmPath)) return null;
                Assembly asm = Assembly.LoadFrom(asmPath);
                return asm;
            };

            var env = RenderingEnviroment.Initialize(root);

            var data = JsonConvert.DeserializeObject(File.ReadAllText(@"C:\Users\drago\Desktop\renderingTest\context.json"));

            var worker = new FakeHttpWorkerRequest(data, env.ApplicationHost);
            HttpContext.Current = new HttpContext(worker);
            var globaltype = BuildManager.GetGlobalAsaxType();
            var global = (HttpApplication)Activator.CreateInstance(globaltype);
            globaltype.GetMethod("Application_Start", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(global, new object[0]);
            ScopeStorage.CurrentProvider = new RenderingScopeStorageProvider();
            GlobalFilters.Filters.Add(new HttpCaptureFilter());

            worker = new FakeHttpWorkerRequest(data, env.ApplicationHost);
            HttpContext.Current = new HttpContext(worker);

            //var c = CreateController<RenderingTest.Controllers.HomeController>(global, HttpContext.Current);
            var c = CreateController<IRR2.WebUI.Controllers.DashBoard.HomeController>(global, HttpContext.Current);

            //var index = c.Index(new ViewModel { StrValue = "D", Value = 1 });

            HttpContext.Current.Response.Flush();

            worker.CopyTo("result.html");
            //index.ExecuteResult(c.ControllerContext);

            //((FakeHttpResponse)c.ControllerContext.HttpContext.Response).CopyOutputTo(@"result.html");
            //RenderViewToString(c.ControllerContext, @"Index");
        }

        //static string RenderViewToString(ControllerContext context,
        //                            ViewResult result)
        //{
        //    ViewEngineResult result = null;
        //    if (this.View == null)
        //    {
        //        result = result.FindView(context);
        //        this.View = result.View;
        //    }
        //    TextWriter output = context.HttpContext.Response.Output;
        //    ViewContext viewContext = new ViewContext(context, this.View, this.ViewData, this.TempData, output);
        //    this.View.Render(viewContext, output);
        //    if (result != null)
        //    {
        //        result.ViewEngine.ReleaseView(context, this.View);
        //    }
        //}

        public static T CreateController<T>(HttpApplication app, HttpContext context) where T : Controller
        {
            var contextWrapper = new HttpContextWrapper(context);
            RouteData routeData = RouteTable.Routes.GetRouteData(contextWrapper);
            RequestContext requestContext = new RequestContext(contextWrapper, routeData);
            // create a disconnected controller instance
            T controller = (T)System.Web.Mvc.ControllerBuilder.Current.GetControllerFactory().CreateController(requestContext, "Home");


            ((IController)controller).Execute(requestContext);
            //controller.ControllerContext = new ControllerContext(contextWrapper, routeData, controller);
            return controller;
        }
        public static T Load<T>(string data)
        {
            var sw = new StringReader(data);
            return (T)new XmlSerializer(typeof(T)).Deserialize(sw);
        }

        static string requestData = @"<?xml version=""1.0"" encoding=""utf-16""?>
<RequestData xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <AcceptTypes>
    <string>text/html</string>
    <string>application/xhtml+xml</string>
    <string>application/xml;q=0.9</string>
    <string>image/webp</string>
    <string>*/*;q=0.8</string>
  </AcceptTypes>
  <ApplicationPath>/</ApplicationPath>
  <AppRelativeCurrentExecutionFilePath>~/</AppRelativeCurrentExecutionFilePath>
  <ContentLength>0</ContentLength>
  <ContentType />
  <CurrentExecutionFilePath>/</CurrentExecutionFilePath>
  <FilePath>/</FilePath>
  <HttpMethod>GET</HttpMethod>
  <IsAuthenticated>false</IsAuthenticated>
  <IsLocal>true</IsLocal>
  <IsSecureConnection>false</IsSecureConnection>
  <Path>/</Path>
  <PathInfo />
  <PhysicalApplicationPath>c:\users\titian.dragomir\documents\visual studio 2013\Projects\RenderingTest\RenderingTest\</PhysicalApplicationPath>
  <PhysicalPath>c:\users\titian.dragomir\documents\visual studio 2013\Projects\RenderingTest\RenderingTest</PhysicalPath>
  <RawUrl>/</RawUrl>
  <RequestType>GET</RequestType>
  <TotalBytes>0</TotalBytes>
  <Url>http://localhost:51116/</Url>
  <UserAgent>Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.133 Safari/537.36</UserAgent>
  <UserHostAddress>::1</UserHostAddress>
  <UserHostName>::1</UserHostName>
  <UserLanguages>
    <string>en-US</string>
    <string>en;q=0.8</string>
  </UserLanguages>
</RequestData>
";

        public static string browsercaps = @"<?xml version=""1.0"" encoding=""utf-16""?>
<HttpBrowserCapabilitiesData xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <ActiveXControls>false</ActiveXControls>
  <AOL>false</AOL>
  <BackgroundSounds>false</BackgroundSounds>
  <Beta>false</Beta>
  <Browser>Chrome</Browser>
  <CanCombineFormsInDeck>true</CanCombineFormsInDeck>
  <CanInitiateVoiceCall>false</CanInitiateVoiceCall>
  <CanRenderAfterInputOrSelectElement>true</CanRenderAfterInputOrSelectElement>
  <CanRenderEmptySelects>true</CanRenderEmptySelects>
  <CanRenderInputAndSelectElementsTogether>true</CanRenderInputAndSelectElementsTogether>
  <CanRenderMixedSelects>true</CanRenderMixedSelects>
  <CanRenderOneventAndPrevElementsTogether>true</CanRenderOneventAndPrevElementsTogether>
  <CanRenderPostBackCards>true</CanRenderPostBackCards>
  <CanRenderSetvarZeroWithMultiSelectionList>true</CanRenderSetvarZeroWithMultiSelectionList>
  <CanSendMail>true</CanSendMail>
  <CDF>false</CDF>
  <ClrVersion />
  <Cookies>true</Cookies>
  <Crawler>false</Crawler>
  <DefaultSubmitButtonLimit>1</DefaultSubmitButtonLimit>
  <EcmaScriptVersion />
  <Frames>true</Frames>
  <GatewayMajorVersion>0</GatewayMajorVersion>
  <GatewayMinorVersion>0</GatewayMinorVersion>
  <GatewayVersion>None</GatewayVersion>
  <HasBackButton>true</HasBackButton>
  <HidesRightAlignedMultiselectScrollbars>false</HidesRightAlignedMultiselectScrollbars>
  <Id>chrome</Id>
  <InputType>keyboard</InputType>
  <IsColor>true</IsColor>
  <IsMobileDevice>false</IsMobileDevice>
  <JavaApplets>true</JavaApplets>
  <JScriptVersion />
  <MajorVersion>57</MajorVersion>
  <MaximumHrefLength>10000</MaximumHrefLength>
  <MaximumRenderedPageSize>300000</MaximumRenderedPageSize>
  <MaximumSoftkeyLabelLength>5</MaximumSoftkeyLabelLength>
  <MinorVersion>0</MinorVersion>
  <MinorVersionString>0</MinorVersionString>
  <MobileDeviceManufacturer>Unknown</MobileDeviceManufacturer>
  <MobileDeviceModel>Unknown</MobileDeviceModel>
  <MSDomVersion />
  <NumberOfSoftkeys>0</NumberOfSoftkeys>
  <Platform>WinNT</Platform>
  <PreferredImageMime>image/gif</PreferredImageMime>
  <PreferredRenderingMime>text/html</PreferredRenderingMime>
  <PreferredRenderingType>html32</PreferredRenderingType>
  <RendersBreakBeforeWmlSelectAndInput>false</RendersBreakBeforeWmlSelectAndInput>
  <RendersBreaksAfterHtmlLists>true</RendersBreaksAfterHtmlLists>
  <RendersBreaksAfterWmlAnchor>false</RendersBreaksAfterWmlAnchor>
  <RendersBreaksAfterWmlInput>false</RendersBreaksAfterWmlInput>
  <RendersWmlDoAcceptsInline>true</RendersWmlDoAcceptsInline>
  <RendersWmlSelectsAsMenuCards>false</RendersWmlSelectsAsMenuCards>
  <RequiresAttributeColonSubstitution>false</RequiresAttributeColonSubstitution>
  <RequiresContentTypeMetaTag>false</RequiresContentTypeMetaTag>
  <RequiresControlStateInSession>false</RequiresControlStateInSession>
  <RequiresDBCSCharacter>false</RequiresDBCSCharacter>
  <RequiresHtmlAdaptiveErrorReporting>false</RequiresHtmlAdaptiveErrorReporting>
  <RequiresLeadingPageBreak>false</RequiresLeadingPageBreak>
  <RequiresNoBreakInFormatting>false</RequiresNoBreakInFormatting>
  <RequiresOutputOptimization>false</RequiresOutputOptimization>
  <RequiresPhoneNumbersAsPlainText>false</RequiresPhoneNumbersAsPlainText>
  <RequiresSpecialViewStateEncoding>false</RequiresSpecialViewStateEncoding>
  <RequiresUniqueFilePathSuffix>false</RequiresUniqueFilePathSuffix>
  <RequiresUniqueHtmlCheckboxNames>false</RequiresUniqueHtmlCheckboxNames>
  <RequiresUniqueHtmlInputNames>false</RequiresUniqueHtmlInputNames>
  <RequiresUrlEncodedPostfieldValues>false</RequiresUrlEncodedPostfieldValues>
  <ScreenBitDepth>8</ScreenBitDepth>
  <ScreenCharactersHeight>40</ScreenCharactersHeight>
  <ScreenCharactersWidth>80</ScreenCharactersWidth>
  <ScreenPixelsHeight>480</ScreenPixelsHeight>
  <ScreenPixelsWidth>640</ScreenPixelsWidth>
  <SupportsAccesskeyAttribute>true</SupportsAccesskeyAttribute>
  <SupportsBodyColor>true</SupportsBodyColor>
  <SupportsBold>true</SupportsBold>
  <SupportsCacheControlMetaTag>true</SupportsCacheControlMetaTag>
  <SupportsCallback>true</SupportsCallback>
  <SupportsCss>true</SupportsCss>
  <SupportsDivAlign>true</SupportsDivAlign>
  <SupportsDivNoWrap>false</SupportsDivNoWrap>
  <SupportsEmptyStringInCookieValue>true</SupportsEmptyStringInCookieValue>
  <SupportsFontColor>true</SupportsFontColor>
  <SupportsFontName>true</SupportsFontName>
  <SupportsFontSize>true</SupportsFontSize>
  <SupportsImageSubmit>true</SupportsImageSubmit>
  <SupportsIModeSymbols>false</SupportsIModeSymbols>
  <SupportsInputIStyle>false</SupportsInputIStyle>
  <SupportsInputMode>false</SupportsInputMode>
  <SupportsItalic>true</SupportsItalic>
  <SupportsJPhoneMultiMediaAttributes>false</SupportsJPhoneMultiMediaAttributes>
  <SupportsJPhoneSymbols>false</SupportsJPhoneSymbols>
  <SupportsQueryStringInFormAction>true</SupportsQueryStringInFormAction>
  <SupportsRedirectWithCookie>true</SupportsRedirectWithCookie>
  <SupportsSelectMultiple>true</SupportsSelectMultiple>
  <SupportsUncheck>true</SupportsUncheck>
  <SupportsXmlHttp>true</SupportsXmlHttp>
  <Tables>true</Tables>
  <Type>Chrome57</Type>
  <UseOptimizedCacheKey>true</UseOptimizedCacheKey>
  <VBScript>false</VBScript>
  <Version>57.0</Version>
  <W3CDomVersion />
  <Win16>false</Win16>
  <Win32>true</Win32>
</HttpBrowserCapabilitiesData>";
    }


}
