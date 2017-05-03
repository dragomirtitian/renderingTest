using IRR2.WebUI.UnitTests.TestUtilities;
using RenderingTest.Controllers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;
using System.Xml.Serialization;

namespace Renderer
{
    class CustomVirtualPathProvider: VirtualPathProvider
    {

    }
    class Program
    {
        static void Main(string[] args)
        {

            Thread.GetDomain().SetData(".appDomain", "*");
            Thread.GetDomain().SetData(".appId", "12");
            Thread.GetDomain().SetData(".appPath", @"c:\users\titian.dragomir\documents\visual studio 2013\Projects\RenderingTest\RenderingTest\");
            Thread.GetDomain().SetData(".appVPath", "/");
            Thread.GetDomain().SetData(".domainId", "LM/W3SVC/12/ROOT-1-131381888751591619");

            typeof(System.Configuration.ConfigurationManager).GetField("s_initState", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, 0);
            var env = new HostingEnvironment();
            typeof(HostingEnvironment)
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                .First(_ => _.Name == "Initialize" && _.GetParameters().Length == 5)
                .Invoke(env, new object[]
                {
                    null,
                    new ApplicationHost(),
                    new ConfigMapPathFactory(),
                    null,
                    //Activator.CreateInstance(typeof(System.Web.Hosting.HostingEnvironment).Assembly.GetType("System.Web.Hosting.HostingEnvironmentParameters")),
                    null,
                });
            
            CallContext.SetData("__TemporaryVirtualPathProvider__", new CustomVirtualPathProvider());



            var c = CreateController<HomeController>();
            //var v = c.Index();
            RenderViewToString(c.ControllerContext, @"~\Views\Home\Index.cshtml");
        }
        static string RenderViewToString(ControllerContext context,
                                    string viewPath,
                                    object model = null,
                                    bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(context, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(context, viewPath, null);

            if (viewEngineResult == null)
                throw new FileNotFoundException("View cannot be found.");

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            context.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(context, view,
                                            context.Controller.ViewData,
                                            context.Controller.TempData,
                                            sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }

        public static T CreateController<T>(RouteData routeData = null) where T : Controller, new()
        {
            // create a disconnected controller instance
            T controller = new T();

            // get context wrapper from HttpContext if available
            HttpContextBase wrapper = new FakeHttpContext(new FakePrincipal(new FakeIdentity("uu"), new string[0]),
                new NameValueCollection(), new NameValueCollection(), new HttpCookieCollection(), new SessionStateItemCollection(), Load<RequestData>(requestData), Load<HttpBrowserCapabilitiesData>(browsercaps));

            if (routeData == null)
                routeData = new RouteData();

            // add the controller routing if not existing
            if (!routeData.Values.ContainsKey("controller") &&
                !routeData.Values.ContainsKey("Controller"))
                routeData.Values.Add("controller",
                                     controller.GetType()
                                               .Name.ToLower().Replace("controller", ""));

            controller.ControllerContext = new ControllerContext(wrapper, routeData, controller);
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
