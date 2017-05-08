using IRR2.Common.Tests.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace RenderingTest.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController()
        {
        }
        //
        // GET: /Home/

        public ActionResult Index(ViewModel vm)
        {
            var x = Serializer.Serialize(System.Web.HttpContext.Current);
            //System.IO.File.WriteAllText(@"C:\Users\drago\Desktop\renderingTest\context.json", x);
            //new XmlSerializer(typeof(RequestData)).Serialize(new StringWriter(), new RequestData());
            return View(vm ?? new ViewModel());
        }

        public PartialViewResult Partial(int value = 0)
        {
            ViewBag.Value = 0;
            //new XmlSerializer(typeof(RequestData)).Serialize(new StringWriter(), new RequestData());
            return PartialView(new ViewModel
            {
                Value = value
            });
        }
    }

    public class ViewModel
    {
        public int Value { get; set; }
        public string StrValue { get; set; }
    }

    public class RequestData
    {
        public RequestData() { }
        public RequestData(HttpRequestBase request)
        {
            AcceptTypes = request.AcceptTypes;
            AnonymousID = request.AnonymousID;
            ApplicationPath = request.ApplicationPath;
            AppRelativeCurrentExecutionFilePath = request.AppRelativeCurrentExecutionFilePath;
            //X = request.Browser;
            ContentLength = request.ContentLength;
            ContentType = request.ContentType;
            CurrentExecutionFilePath = request.CurrentExecutionFilePath;
            FilePath = request.FilePath;
            HttpMethod = request.HttpMethod;
            IsAuthenticated = request.IsAuthenticated;
            IsLocal = request.IsLocal;
            IsSecureConnection = request.IsSecureConnection;
            Path = request.Path;
            PathInfo = request.PathInfo;
            PhysicalApplicationPath = request.PhysicalApplicationPath;
            PhysicalPath = request.PhysicalPath;
            RawUrl = request.RawUrl;
            RequestType = request.RequestType;
            TotalBytes = request.TotalBytes;
            Url = request.Url.ToString();
            UrlReferrer = request.UrlReferrer == null ? null : request.UrlReferrer.ToString();
            UserAgent = request.UserAgent;
            UserHostAddress = request.UserHostAddress;
            UserHostName = request.UserHostName;
            UserLanguages = request.UserLanguages;
        }
        public string[] AcceptTypes { get; set; }
        public string AnonymousID { get; set; }
        public string ApplicationPath { get; set; }
        public string AppRelativeCurrentExecutionFilePath { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
        public string CurrentExecutionFilePath { get; set; }
        public string FilePath { get; set; }
        public string HttpMethod { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsLocal { get; set; }
        public bool IsSecureConnection { get; set; }
        public string Path { get; set; }
        public string PathInfo { get; set; }
        public string PhysicalApplicationPath { get; set; }
        public string PhysicalPath { get; set; }
        public string RawUrl { get; set; }
        public string RequestType { get; set; }
        public int TotalBytes { get; set; }
        public string Url { get; set; }
        public string UrlReferrer { get; set; }
        public string UserAgent { get; set; }
        public string UserHostAddress { get; set; }
        public string UserHostName { get; set; }
        public string[] UserLanguages { get; set; }
    }

    public class HttpBrowserCapabilitiesData
    {
        public HttpBrowserCapabilitiesData() { }
        public HttpBrowserCapabilitiesData(HttpBrowserCapabilitiesBase cap)
        {
            this.ActiveXControls = cap.ActiveXControls;
            //this.Adapters = cap.Adapters;
            this.AOL = cap.AOL;
            this.BackgroundSounds = cap.BackgroundSounds;
            this.Beta = cap.Beta;
            this.Browser = cap.Browser;
            //this.Browsers = cap.Browsers;
            this.CanCombineFormsInDeck = cap.CanCombineFormsInDeck;
            this.CanInitiateVoiceCall = cap.CanInitiateVoiceCall;
            this.CanRenderAfterInputOrSelectElement = cap.CanRenderAfterInputOrSelectElement;
            this.CanRenderEmptySelects = cap.CanRenderEmptySelects;
            this.CanRenderInputAndSelectElementsTogether = cap.CanRenderInputAndSelectElementsTogether;
            this.CanRenderMixedSelects = cap.CanRenderMixedSelects;
            this.CanRenderOneventAndPrevElementsTogether = cap.CanRenderOneventAndPrevElementsTogether;
            this.CanRenderPostBackCards = cap.CanRenderPostBackCards;
            this.CanRenderSetvarZeroWithMultiSelectionList = cap.CanRenderSetvarZeroWithMultiSelectionList;
            this.CanSendMail = cap.CanSendMail;
            //this.Capabilities = cap.Capabilities;
            this.CDF = cap.CDF;
            this.ClrVersion = cap.ClrVersion;
            this.Cookies = cap.Cookies;
            this.Crawler = cap.Crawler;
            this.DefaultSubmitButtonLimit = cap.DefaultSubmitButtonLimit;
            this.EcmaScriptVersion = cap.EcmaScriptVersion;
            this.Frames = cap.Frames;
            this.GatewayMajorVersion = cap.GatewayMajorVersion;
            this.GatewayMinorVersion = cap.GatewayMinorVersion;
            this.GatewayVersion = cap.GatewayVersion;
            this.HasBackButton = cap.HasBackButton;
            this.HidesRightAlignedMultiselectScrollbars = cap.HidesRightAlignedMultiselectScrollbars;
            this.HtmlTextWriter = cap.HtmlTextWriter;
            this.Id = cap.Id;
            this.InputType = cap.InputType;
            this.IsColor = cap.IsColor;
            this.IsMobileDevice = cap.IsMobileDevice;
            this.JavaApplets = cap.JavaApplets;
            this.JScriptVersion = cap.JScriptVersion;
            this.MajorVersion = cap.MajorVersion;
            this.MaximumHrefLength = cap.MaximumHrefLength;
            this.MaximumRenderedPageSize = cap.MaximumRenderedPageSize;
            this.MaximumSoftkeyLabelLength = cap.MaximumSoftkeyLabelLength;
            this.MinorVersion = cap.MinorVersion;
            this.MinorVersionString = cap.MinorVersionString;
            this.MobileDeviceManufacturer = cap.MobileDeviceManufacturer;
            this.MobileDeviceModel = cap.MobileDeviceModel;
            this.MSDomVersion = cap.MSDomVersion;
            this.NumberOfSoftkeys = cap.NumberOfSoftkeys;
            this.Platform = cap.Platform;
            this.PreferredImageMime = cap.PreferredImageMime;
            this.PreferredRenderingMime = cap.PreferredRenderingMime;
            this.PreferredRenderingType = cap.PreferredRenderingType;
            this.PreferredRequestEncoding = cap.PreferredRequestEncoding;
            this.PreferredResponseEncoding = cap.PreferredResponseEncoding;
            this.RendersBreakBeforeWmlSelectAndInput = cap.RendersBreakBeforeWmlSelectAndInput;
            this.RendersBreaksAfterHtmlLists = cap.RendersBreaksAfterHtmlLists;
            this.RendersBreaksAfterWmlAnchor = cap.RendersBreaksAfterWmlAnchor;
            this.RendersBreaksAfterWmlInput = cap.RendersBreaksAfterWmlInput;
            this.RendersWmlDoAcceptsInline = cap.RendersWmlDoAcceptsInline;
            this.RendersWmlSelectsAsMenuCards = cap.RendersWmlSelectsAsMenuCards;
            this.RequiredMetaTagNameValue = cap.RequiredMetaTagNameValue;
            this.RequiresAttributeColonSubstitution = cap.RequiresAttributeColonSubstitution;
            this.RequiresContentTypeMetaTag = cap.RequiresContentTypeMetaTag;
            this.RequiresControlStateInSession = cap.RequiresControlStateInSession;
            this.RequiresDBCSCharacter = cap.RequiresDBCSCharacter;
            this.RequiresHtmlAdaptiveErrorReporting = cap.RequiresHtmlAdaptiveErrorReporting;
            this.RequiresLeadingPageBreak = cap.RequiresLeadingPageBreak;
            this.RequiresNoBreakInFormatting = cap.RequiresNoBreakInFormatting;
            this.RequiresOutputOptimization = cap.RequiresOutputOptimization;
            this.RequiresPhoneNumbersAsPlainText = cap.RequiresPhoneNumbersAsPlainText;
            this.RequiresSpecialViewStateEncoding = cap.RequiresSpecialViewStateEncoding;
            this.RequiresUniqueFilePathSuffix = cap.RequiresUniqueFilePathSuffix;
            this.RequiresUniqueHtmlCheckboxNames = cap.RequiresUniqueHtmlCheckboxNames;
            this.RequiresUniqueHtmlInputNames = cap.RequiresUniqueHtmlInputNames;
            this.RequiresUrlEncodedPostfieldValues = cap.RequiresUrlEncodedPostfieldValues;
            this.ScreenBitDepth = cap.ScreenBitDepth;
            this.ScreenCharactersHeight = cap.ScreenCharactersHeight;
            this.ScreenCharactersWidth = cap.ScreenCharactersWidth;
            this.ScreenPixelsHeight = cap.ScreenPixelsHeight;
            this.ScreenPixelsWidth = cap.ScreenPixelsWidth;
            this.SupportsAccesskeyAttribute = cap.SupportsAccesskeyAttribute;
            this.SupportsBodyColor = cap.SupportsBodyColor;
            this.SupportsBold = cap.SupportsBold;
            this.SupportsCacheControlMetaTag = cap.SupportsCacheControlMetaTag;
            this.SupportsCallback = cap.SupportsCallback;
            this.SupportsCss = cap.SupportsCss;
            this.SupportsDivAlign = cap.SupportsDivAlign;
            this.SupportsDivNoWrap = cap.SupportsDivNoWrap;
            this.SupportsEmptyStringInCookieValue = cap.SupportsEmptyStringInCookieValue;
            this.SupportsFontColor = cap.SupportsFontColor;
            this.SupportsFontName = cap.SupportsFontName;
            this.SupportsFontSize = cap.SupportsFontSize;
            this.SupportsImageSubmit = cap.SupportsImageSubmit;
            this.SupportsIModeSymbols = cap.SupportsIModeSymbols;
            this.SupportsInputIStyle = cap.SupportsInputIStyle;
            this.SupportsInputMode = cap.SupportsInputMode;
            this.SupportsItalic = cap.SupportsItalic;
            this.SupportsJPhoneMultiMediaAttributes = cap.SupportsJPhoneMultiMediaAttributes;
            this.SupportsJPhoneSymbols = cap.SupportsJPhoneSymbols;
            this.SupportsQueryStringInFormAction = cap.SupportsQueryStringInFormAction;
            this.SupportsRedirectWithCookie = cap.SupportsRedirectWithCookie;
            this.SupportsSelectMultiple = cap.SupportsSelectMultiple;
            this.SupportsUncheck = cap.SupportsUncheck;
            this.SupportsXmlHttp = cap.SupportsXmlHttp;
            this.Tables = cap.Tables;
            this.Type = cap.Type;
            this.UseOptimizedCacheKey = cap.UseOptimizedCacheKey;
            this.VBScript = cap.VBScript;
            this.Version = cap.Version;
            this.W3CDomVersion = cap.W3CDomVersion;
            this.Win16 = cap.Win16;
            this.Win32 = cap.Win32;
        }
        public bool ActiveXControls { get; set; }
        //public Dictionary<object, object> Adapters { get; set; }
        public bool AOL { get; set; }
        public bool BackgroundSounds { get; set; }
        public bool Beta { get; set; }
        public string Browser { get; set; }
        public string[] Browsers { get; set; }
        public bool CanCombineFormsInDeck { get; set; }
        public bool CanInitiateVoiceCall { get; set; }
        public bool CanRenderAfterInputOrSelectElement { get; set; }
        public bool CanRenderEmptySelects { get; set; }
        public bool CanRenderInputAndSelectElementsTogether { get; set; }
        public bool CanRenderMixedSelects { get; set; }
        public bool CanRenderOneventAndPrevElementsTogether { get; set; }
        public bool CanRenderPostBackCards { get; set; }
        public bool CanRenderSetvarZeroWithMultiSelectionList { get; set; }
        public bool CanSendMail { get; set; }
        //public Dictionary<object, object> Capabilities { get; set; }
        public bool CDF { get; set; }
        public Version ClrVersion { get; set; }
        public bool Cookies { get; set; }
        public bool Crawler { get; set; }
        public int DefaultSubmitButtonLimit { get; set; }
        public Version EcmaScriptVersion { get; set; }
        public bool Frames { get; set; }
        public int GatewayMajorVersion { get; set; }
        public double GatewayMinorVersion { get; set; }
        public string GatewayVersion { get; set; }
        public bool HasBackButton { get; set; }
        public bool HidesRightAlignedMultiselectScrollbars { get; set; }
        public string HtmlTextWriter { get; set; }
        public string Id { get; set; }
        public string InputType { get; set; }
        public bool IsColor { get; set; }
        public bool IsMobileDevice { get; set; }
        public bool JavaApplets { get; set; }
        public Version JScriptVersion { get; set; }
        public int MajorVersion { get; set; }
        public int MaximumHrefLength { get; set; }
        public int MaximumRenderedPageSize { get; set; }
        public int MaximumSoftkeyLabelLength { get; set; }
        public double MinorVersion { get; set; }
        public string MinorVersionString { get; set; }
        public string MobileDeviceManufacturer { get; set; }
        public string MobileDeviceModel { get; set; }
        public Version MSDomVersion { get; set; }
        public int NumberOfSoftkeys { get; set; }
        public string Platform { get; set; }
        public string PreferredImageMime { get; set; }
        public string PreferredRenderingMime { get; set; }
        public string PreferredRenderingType { get; set; }
        public string PreferredRequestEncoding { get; set; }
        public string PreferredResponseEncoding { get; set; }
        public bool RendersBreakBeforeWmlSelectAndInput { get; set; }
        public bool RendersBreaksAfterHtmlLists { get; set; }
        public bool RendersBreaksAfterWmlAnchor { get; set; }
        public bool RendersBreaksAfterWmlInput { get; set; }
        public bool RendersWmlDoAcceptsInline { get; set; }
        public bool RendersWmlSelectsAsMenuCards { get; set; }
        public string RequiredMetaTagNameValue { get; set; }
        public bool RequiresAttributeColonSubstitution { get; set; }
        public bool RequiresContentTypeMetaTag { get; set; }
        public bool RequiresControlStateInSession { get; set; }
        public bool RequiresDBCSCharacter { get; set; }
        public bool RequiresHtmlAdaptiveErrorReporting { get; set; }
        public bool RequiresLeadingPageBreak { get; set; }
        public bool RequiresNoBreakInFormatting { get; set; }
        public bool RequiresOutputOptimization { get; set; }
        public bool RequiresPhoneNumbersAsPlainText { get; set; }
        public bool RequiresSpecialViewStateEncoding { get; set; }
        public bool RequiresUniqueFilePathSuffix { get; set; }
        public bool RequiresUniqueHtmlCheckboxNames { get; set; }
        public bool RequiresUniqueHtmlInputNames { get; set; }
        public bool RequiresUrlEncodedPostfieldValues { get; set; }
        public int ScreenBitDepth { get; set; }
        public int ScreenCharactersHeight { get; set; }
        public int ScreenCharactersWidth { get; set; }
        public int ScreenPixelsHeight { get; set; }
        public int ScreenPixelsWidth { get; set; }
        public bool SupportsAccesskeyAttribute { get; set; }
        public bool SupportsBodyColor { get; set; }
        public bool SupportsBold { get; set; }
        public bool SupportsCacheControlMetaTag { get; set; }
        public bool SupportsCallback { get; set; }
        public bool SupportsCss { get; set; }
        public bool SupportsDivAlign { get; set; }
        public bool SupportsDivNoWrap { get; set; }
        public bool SupportsEmptyStringInCookieValue { get; set; }
        public bool SupportsFontColor { get; set; }
        public bool SupportsFontName { get; set; }
        public bool SupportsFontSize { get; set; }
        public bool SupportsImageSubmit { get; set; }
        public bool SupportsIModeSymbols { get; set; }
        public bool SupportsInputIStyle { get; set; }
        public bool SupportsInputMode { get; set; }
        public bool SupportsItalic { get; set; }
        public bool SupportsJPhoneMultiMediaAttributes { get; set; }
        public bool SupportsJPhoneSymbols { get; set; }
        public bool SupportsQueryStringInFormAction { get; set; }
        public bool SupportsRedirectWithCookie { get; set; }
        public bool SupportsSelectMultiple { get; set; }
        public bool SupportsUncheck { get; set; }
        public bool SupportsXmlHttp { get; set; }
        public bool Tables { get; set; }
        public string Type { get; set; }
        public bool UseOptimizedCacheKey { get; set; }
        public bool VBScript { get; set; }
        public string Version { get; set; }
        public Version W3CDomVersion { get; set; }
        public bool Win16 { get; set; }
        public bool Win32 { get; set; }
    }

}
