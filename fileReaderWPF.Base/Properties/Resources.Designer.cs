﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace fileReaderWPF.Base.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("fileReaderWPF.Base.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Please select at least one extension.
        /// </summary>
        public static string CouldNotFindAnyExtensions {
            get {
                return ResourceManager.GetString("CouldNotFindAnyExtensions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Could not find given phrase in files in the selected folder.
        /// </summary>
        public static string CouldNotFindPhrase {
            get {
                return ResourceManager.GetString("CouldNotFindPhrase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Given directory does not exist.
        /// </summary>
        public static string DirectoryDoesntExist {
            get {
                return ResourceManager.GetString("DirectoryDoesntExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Extensions cannot be empty.
        /// </summary>
        public static string EmptyExtensions {
            get {
                return ResourceManager.GetString("EmptyExtensions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Given folder does not contain any files with given extensions.
        /// </summary>
        public static string EmptyFolderException {
            get {
                return ResourceManager.GetString("EmptyFolderException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please select folder with desired files.
        /// </summary>
        public static string FolderBrowserDialogDescription {
            get {
                return ResourceManager.GetString("FolderBrowserDialogDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You have selected {0}.
        /// </summary>
        public static string ShowSelectedFolder {
            get {
                return ResourceManager.GetString("ShowSelectedFolder", resourceCulture);
            }
        }
    }
}
