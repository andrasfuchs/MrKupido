﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MrKupido.Web.Resources.Shared {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Validation {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Validation() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MrKupido.Web.Resources.Shared.Validation", typeof(Validation).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to the format of {} is invalid.
        /// </summary>
        public static string Format {
            get {
                return ResourceManager.GetString("Format", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {} is invalid.
        /// </summary>
        public static string General {
            get {
                return ResourceManager.GetString("General", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {} is required.
        /// </summary>
        public static string Missing {
            get {
                return ResourceManager.GetString("Missing", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {} is too high.
        /// </summary>
        public static string Overflow {
            get {
                return ResourceManager.GetString("Overflow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to this value.
        /// </summary>
        public static string ThisValue {
            get {
                return ResourceManager.GetString("ThisValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {} is too long.
        /// </summary>
        public static string TooLong {
            get {
                return ResourceManager.GetString("TooLong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {} is too low.
        /// </summary>
        public static string Underflow {
            get {
                return ResourceManager.GetString("Underflow", resourceCulture);
            }
        }
    }
}
