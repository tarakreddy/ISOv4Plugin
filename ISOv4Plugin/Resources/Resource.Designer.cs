﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgGateway.ADAPT.ISOv4Plugin.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("AgGateway.ADAPT.ISOv4Plugin.Resources.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Snap-shot of the ISO 11783-11 online data base
        ///@ Copyright International Organization for Standardization, see: www.iso.org/iso/copyright.htm. No reproduction on networking 
        ///permitted without license from ISO. The export file from the online data base is supplied without liability. Hard and Saved 
        ///copies of this document are considered uncontrolled and represents a snap-shot of the ISO11783-11 online data base.
        ///
        ///
        ///DD Entity: 0 Data Dictionary Version
        ///Definition: This DDE is used to specify which versi [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string ddiExport {
            get {
                return ResourceManager.GetString("ddiExport", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;ddiToUnitOfMeasureMapping&gt;
        ///  &lt;mappings&gt;
        ///    &lt;mapping unit=&quot;kg&quot; domainId=&quot;kg&quot; /&gt;
        ///    &lt;mapping unit=&quot;L&quot; domainId=&quot;l&quot; /&gt;
        ///    &lt;mapping unit=&quot;mg/m²&quot; domainId=&quot;mg1[m2]-1&quot; /&gt;
        ///    &lt;mapping unit=&quot;ml/m²&quot; domainId=&quot;ml1[m2]-1&quot; /&gt;
        ///    &lt;mapping unit=&quot;mm&quot; domainId=&quot;mm&quot; /&gt;
        ///    &lt;mapping unit=&quot;mm³/s&quot; domainId=&quot;[mm3]1sec-1&quot; /&gt;
        ///    &lt;mapping unit=&quot;ppm&quot; domainId=&quot;ppm&quot; /&gt;
        ///    &lt;mapping unit=&quot;mg/s&quot; domainId=&quot;mg1sec-1&quot; /&gt;
        ///    &lt;mapping unit=&quot;/s&quot; domainId=&quot;sec-1&quot; /&gt;
        ///    &lt;mapping uni [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string IsoUnitOfMeasure {
            get {
                return ResourceManager.GetString("IsoUnitOfMeasure", resourceCulture);
            }
        }
    }
}
