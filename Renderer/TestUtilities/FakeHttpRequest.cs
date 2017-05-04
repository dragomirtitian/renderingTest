using RenderingTest.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Routing;
using System.Threading.Tasks;
using System.Web.Caching;

namespace IRR2.WebUI.UnitTests.TestUtilities
{
   
    public class FakeHttpRequest : HttpRequestBase
    {
        private NameValueCollection _formParams;
        private NameValueCollection _queryStringParams;
        private HttpCookieCollection _cookies;
        private RequestData _data;
        private HttpBrowserCapabilitiesData _dataCaps;

        public FakeHttpRequest(HttpRequest httpRequest, NameValueCollection formParams, NameValueCollection queryStringParams, HttpCookieCollection cookies, RequestData data, HttpBrowserCapabilitiesData dataCaps)
        {
            _httpRequest = httpRequest;
            _formParams = formParams;
            _queryStringParams = queryStringParams;
            _cookies = cookies;
            _data = data;
            _dataCaps = dataCaps;
            _serverVariables = new NameValueCollection();
        }

        public override NameValueCollection Form
        {
            get
            {
                return _formParams;
            }
        }

        public override NameValueCollection QueryString
        {
            get
            {
                return _queryStringParams;
            }
        }
        public override NameValueCollection ServerVariables
        {
            get
            {
                return _serverVariables;
            }
        }

        public override HttpCookieCollection Cookies
        {
            get
            {
                return _cookies;
            }
        }

        public override void Abort()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override byte[] BinaryRead(int count)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override Stream GetBufferedInputStream()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override Stream GetBufferlessInputStream()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override Stream GetBufferlessInputStream(bool disableMaxRequestLength)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void InsertEntityBody()
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void InsertEntityBody(byte[] buffer, int offset, int count)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override int[] MapImageCoordinates(string imageFieldName)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override string MapPath(string overridePath)
        {
            return this._httpRequest.MapPath(overridePath);
        }

        public override string MapPath(string overridePath, string baseoverrideDir, bool allowCrossAppMapping)
        {
            return this._httpRequest.MapPath(overridePath, baseoverrideDir, allowCrossAppMapping);
        }

        public override double[] MapRawImageCoordinates(string imageFieldName)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void SaveAs(string filename, bool includeHeaders)
        {
            System.Diagnostics.Debugger.Break(); throw new NotImplementedException();
        }

        public override void ValidateInput()
        {
        }


        public override string[] AcceptTypes { get { return _data.AcceptTypes; } }
        public override string AnonymousID { get { return _data.AnonymousID; } }
        public override string ApplicationPath { get { return _data.ApplicationPath; } }

        string _AppRelativeCurrentExecutionFilePath;
        private HttpRequest _httpRequest;
        private NameValueCollection _serverVariables;

        public override string AppRelativeCurrentExecutionFilePath { get { return _AppRelativeCurrentExecutionFilePath ?? _data.AppRelativeCurrentExecutionFilePath; } }
        public override int ContentLength { get { return _data.ContentLength; } }
        public override string ContentType { get { return _data.ContentType; } }
        public override string CurrentExecutionFilePath { get { return _data.CurrentExecutionFilePath; } }
        public override string FilePath { get { return _data.FilePath; } }
        public override string HttpMethod { get { return _data.HttpMethod; } }
        public override bool IsAuthenticated { get { return _data.IsAuthenticated; } }
        public override bool IsLocal { get { return _data.IsLocal; } }
        public override bool IsSecureConnection { get { return _data.IsSecureConnection; } }
        public override string Path { get { return _data.Path; } }
        public override string PathInfo { get { return _data.PathInfo; } }
        public override string PhysicalApplicationPath { get { return _data.PhysicalApplicationPath; } }
        public override string PhysicalPath { get { return _data.PhysicalPath; } }
        public override string RawUrl { get { return _data.RawUrl; } }
        public override string RequestType { get { return _data.RequestType; } }
        public override int TotalBytes { get { return _data.TotalBytes; } }
        public override Uri Url { get { return new Uri(_data.Url); } }
        public override Uri UrlReferrer { get { return new Uri(_data.UrlReferrer); } }
        public override string UserAgent { get { return _data.UserAgent; } }
        public override string UserHostAddress { get { return _data.UserHostAddress; } }
        public override string UserHostName { get { return _data.UserHostName; } }
        public override string[] UserLanguages { get { return _data.UserLanguages; } }

        public override HttpBrowserCapabilitiesBase Browser
        {
            get
            {
                return new FakeHttpBrowserCapabilities(_dataCaps);
            }
        }

        internal string SwitchCurrentExecutionFilePath(string filePath)
        {
            var old = this.AppRelativeCurrentExecutionFilePath;
            this._AppRelativeCurrentExecutionFilePath = filePath;
            return old;
        }

        internal NameValueCollection SwitchForm(NameValueCollection nameValueCollection)
        {
            var old = this.Form;
            this._formParams = nameValueCollection;
            return old;
        }

        internal string QueryStringText
        {
            get { System.Diagnostics.Debugger.Break(); throw new NotImplementedException(); }
            set { System.Diagnostics.Debugger.Break(); throw new NotImplementedException(); }
        }
    }

    public class FakeHttpBrowserCapabilities: HttpBrowserCapabilitiesBase
    {
        private HttpBrowserCapabilitiesData _data;
        public FakeHttpBrowserCapabilities(HttpBrowserCapabilitiesData data)
        {
            _data = data;
        }
        public override bool ActiveXControls { get { return _data.ActiveXControls; } }
        //public override IDictionary Adapters { get { return _data.Adapters; } }
        public override bool AOL { get { return _data.AOL; } }
        public override bool BackgroundSounds { get { return _data.BackgroundSounds; } }
        public override bool Beta { get { return _data.Beta; } }
        public override string Browser { get { return _data.Browser; } }
        //public override ArrayList Browsers { get { return _data.Browsers; } }
        public override bool CanCombineFormsInDeck { get { return _data.CanCombineFormsInDeck; } }
        public override bool CanInitiateVoiceCall { get { return _data.CanInitiateVoiceCall; } }
        public override bool CanRenderAfterInputOrSelectElement { get { return _data.CanRenderAfterInputOrSelectElement; } }
        public override bool CanRenderEmptySelects { get { return _data.CanRenderEmptySelects; } }
        public override bool CanRenderInputAndSelectElementsTogether { get { return _data.CanRenderInputAndSelectElementsTogether; } }
        public override bool CanRenderMixedSelects { get { return _data.CanRenderMixedSelects; } }
        public override bool CanRenderOneventAndPrevElementsTogether { get { return _data.CanRenderOneventAndPrevElementsTogether; } }
        public override bool CanRenderPostBackCards { get { return _data.CanRenderPostBackCards; } }
        public override bool CanRenderSetvarZeroWithMultiSelectionList { get { return _data.CanRenderSetvarZeroWithMultiSelectionList; } }
        public override bool CanSendMail { get { return _data.CanSendMail; } }
        //public override IDictionary Capabilities { get; set; }
        public override bool CDF { get { return _data.CDF; } }
        public override Version ClrVersion { get { return _data.ClrVersion; } }
        public override bool Cookies { get { return _data.Cookies; } }
        public override bool Crawler { get { return _data.Crawler; } }
        public override int DefaultSubmitButtonLimit { get { return _data.DefaultSubmitButtonLimit; } }
        public override Version EcmaScriptVersion { get { return _data.EcmaScriptVersion; } }
        public override bool Frames { get { return _data.Frames; } }
        public override int GatewayMajorVersion { get { return _data.GatewayMajorVersion; } }
        public override double GatewayMinorVersion { get { return _data.GatewayMinorVersion; } }
        public override string GatewayVersion { get { return _data.GatewayVersion; } }
        public override bool HasBackButton { get { return _data.HasBackButton; } }
        public override bool HidesRightAlignedMultiselectScrollbars { get { return _data.HidesRightAlignedMultiselectScrollbars; } }
        public override string HtmlTextWriter { get; set; }
        public override string Id { get { return _data.Id; } }
        public override string InputType { get { return _data.InputType; } }
        public override bool IsColor { get { return _data.IsColor; } }
        public override bool IsMobileDevice { get { return _data.IsMobileDevice; } }
        public override bool JavaApplets { get { return _data.JavaApplets; } }
        public override Version JScriptVersion { get { return _data.JScriptVersion; } }
        public override int MajorVersion { get { return _data.MajorVersion; } }
        public override int MaximumHrefLength { get { return _data.MaximumHrefLength; } }
        public override int MaximumRenderedPageSize { get { return _data.MaximumRenderedPageSize; } }
        public override int MaximumSoftkeyLabelLength { get { return _data.MaximumSoftkeyLabelLength; } }
        public override double MinorVersion { get { return _data.MinorVersion; } }
        public override string MinorVersionString { get { return _data.MinorVersionString; } }
        public override string MobileDeviceManufacturer { get { return _data.MobileDeviceManufacturer; } }
        public override string MobileDeviceModel { get { return _data.MobileDeviceModel; } }
        public override Version MSDomVersion { get { return _data.MSDomVersion; } }
        public override int NumberOfSoftkeys { get { return _data.NumberOfSoftkeys; } }
        public override string Platform { get { return _data.Platform; } }
        public override string PreferredImageMime { get { return _data.PreferredImageMime; } }
        public override string PreferredRenderingMime { get { return _data.PreferredRenderingMime; } }
        public override string PreferredRenderingType { get { return _data.PreferredRenderingType; } }
        public override string PreferredRequestEncoding { get { return _data.PreferredRequestEncoding; } }
        public override string PreferredResponseEncoding { get { return _data.PreferredResponseEncoding; } }
        public override bool RendersBreakBeforeWmlSelectAndInput { get { return _data.RendersBreakBeforeWmlSelectAndInput; } }
        public override bool RendersBreaksAfterHtmlLists { get { return _data.RendersBreaksAfterHtmlLists; } }
        public override bool RendersBreaksAfterWmlAnchor { get { return _data.RendersBreaksAfterWmlAnchor; } }
        public override bool RendersBreaksAfterWmlInput { get { return _data.RendersBreaksAfterWmlInput; } }
        public override bool RendersWmlDoAcceptsInline { get { return _data.RendersWmlDoAcceptsInline; } }
        public override bool RendersWmlSelectsAsMenuCards { get { return _data.RendersWmlSelectsAsMenuCards; } }
        public override string RequiredMetaTagNameValue { get { return _data.RequiredMetaTagNameValue; } }
        public override bool RequiresAttributeColonSubstitution { get { return _data.RequiresAttributeColonSubstitution; } }
        public override bool RequiresContentTypeMetaTag { get { return _data.RequiresContentTypeMetaTag; } }
        public override bool RequiresControlStateInSession { get { return _data.RequiresControlStateInSession; } }
        public override bool RequiresDBCSCharacter { get { return _data.RequiresDBCSCharacter; } }
        public override bool RequiresHtmlAdaptiveErrorReporting { get { return _data.RequiresHtmlAdaptiveErrorReporting; } }
        public override bool RequiresLeadingPageBreak { get { return _data.RequiresLeadingPageBreak; } }
        public override bool RequiresNoBreakInFormatting { get { return _data.RequiresNoBreakInFormatting; } }
        public override bool RequiresOutputOptimization { get { return _data.RequiresOutputOptimization; } }
        public override bool RequiresPhoneNumbersAsPlainText { get { return _data.RequiresPhoneNumbersAsPlainText; } }
        public override bool RequiresSpecialViewStateEncoding { get { return _data.RequiresSpecialViewStateEncoding; } }
        public override bool RequiresUniqueFilePathSuffix { get { return _data.RequiresUniqueFilePathSuffix; } }
        public override bool RequiresUniqueHtmlCheckboxNames { get { return _data.RequiresUniqueHtmlCheckboxNames; } }
        public override bool RequiresUniqueHtmlInputNames { get { return _data.RequiresUniqueHtmlInputNames; } }
        public override bool RequiresUrlEncodedPostfieldValues { get { return _data.RequiresUrlEncodedPostfieldValues; } }
        public override int ScreenBitDepth { get { return _data.ScreenBitDepth; } }
        public override int ScreenCharactersHeight { get { return _data.ScreenCharactersHeight; } }
        public override int ScreenCharactersWidth { get { return _data.ScreenCharactersWidth; } }
        public override int ScreenPixelsHeight { get { return _data.ScreenPixelsHeight; } }
        public override int ScreenPixelsWidth { get { return _data.ScreenPixelsWidth; } }
        public override bool SupportsAccesskeyAttribute { get { return _data.SupportsAccesskeyAttribute; } }
        public override bool SupportsBodyColor { get { return _data.SupportsBodyColor; } }
        public override bool SupportsBold { get { return _data.SupportsBold; } }
        public override bool SupportsCacheControlMetaTag { get { return _data.SupportsCacheControlMetaTag; } }
        public override bool SupportsCallback { get { return _data.SupportsCallback; } }
        public override bool SupportsCss { get { return _data.SupportsCss; } }
        public override bool SupportsDivAlign { get { return _data.SupportsDivAlign; } }
        public override bool SupportsDivNoWrap { get { return _data.SupportsDivNoWrap; } }
        public override bool SupportsEmptyStringInCookieValue { get { return _data.SupportsEmptyStringInCookieValue; } }
        public override bool SupportsFontColor { get { return _data.SupportsFontColor; } }
        public override bool SupportsFontName { get { return _data.SupportsFontName; } }
        public override bool SupportsFontSize { get { return _data.SupportsFontSize; } }
        public override bool SupportsImageSubmit { get { return _data.SupportsImageSubmit; } }
        public override bool SupportsIModeSymbols { get { return _data.SupportsIModeSymbols; } }
        public override bool SupportsInputIStyle { get { return _data.SupportsInputIStyle; } }
        public override bool SupportsInputMode { get { return _data.SupportsInputMode; } }
        public override bool SupportsItalic { get { return _data.SupportsItalic; } }
        public override bool SupportsJPhoneMultiMediaAttributes { get { return _data.SupportsJPhoneMultiMediaAttributes; } }
        public override bool SupportsJPhoneSymbols { get { return _data.SupportsJPhoneSymbols; } }
        public override bool SupportsQueryStringInFormAction { get { return _data.SupportsQueryStringInFormAction; } }
        public override bool SupportsRedirectWithCookie { get { return _data.SupportsRedirectWithCookie; } }
        public override bool SupportsSelectMultiple { get { return _data.SupportsSelectMultiple; } }
        public override bool SupportsUncheck { get { return _data.SupportsUncheck; } }
        public override bool SupportsXmlHttp { get { return _data.SupportsXmlHttp; } }
        public override bool Tables { get { return _data.Tables; } }
        //public override Type TagWriter { get { return _data.TagWriter; } }
        public override string Type { get { return _data.Type; } }
        public override bool UseOptimizedCacheKey { get { return _data.UseOptimizedCacheKey; } }
        public override bool VBScript { get { return _data.VBScript; } }
        public override string Version { get { return _data.Version; } }
        public override Version W3CDomVersion { get { return _data.W3CDomVersion; } }
        public override bool Win16 { get { return _data.Win16; } }
        public override bool Win32 { get { return _data.Win32; } }
    }
}
