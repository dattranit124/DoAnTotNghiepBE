﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DoAnTotNghiep_CORE.Helpers {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DoAnTotNghiep_CORE.Helpers.Resource", typeof(Resource).Assembly);
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
        ///   Looks up a localized string similar to Thêm thất bại.
        /// </summary>
        public static string FailAdd {
            get {
                return ResourceManager.GetString("FailAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Xóa thất bại.
        /// </summary>
        public static string FailDelete {
            get {
                return ResourceManager.GetString("FailDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tài khoản hoặc mật khẩu không chính xác !.
        /// </summary>
        public static string FailLogin {
            get {
                return ResourceManager.GetString("FailLogin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Tài khoản này đã tồn tại trong hệ thống, Hãy thử sử dụng email khác để đăng ký thành viên..
        /// </summary>
        public static string FailRegister {
            get {
                return ResourceManager.GetString("FailRegister", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sửa thất bại.
        /// </summary>
        public static string FailUpdate {
            get {
                return ResourceManager.GetString("FailUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Thêm thành công.
        /// </summary>
        public static string SuccessAdd {
            get {
                return ResourceManager.GetString("SuccessAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Xóa thành công.
        /// </summary>
        public static string SuccessDelete {
            get {
                return ResourceManager.GetString("SuccessDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Chúc mừng bạn đã đăng kí tài khoản thành công ! .
        /// </summary>
        public static string SuccessRegister {
            get {
                return ResourceManager.GetString("SuccessRegister", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sửa thành công.
        /// </summary>
        public static string SuccessUpdate {
            get {
                return ResourceManager.GetString("SuccessUpdate", resourceCulture);
            }
        }
    }
}
