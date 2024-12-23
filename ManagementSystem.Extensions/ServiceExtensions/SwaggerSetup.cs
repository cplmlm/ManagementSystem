using ManagementSystem.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using static ManagementSystem.Extensions.CustomApiVersion;

namespace ManagementSystem.Extensions
{
    /// <summary>
    /// Swagger 启动服务
    /// </summary>
    public static class SwaggerSetup
    {
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var basePath = AppContext.BaseDirectory;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JST API", Version = "v1" });
                #region 
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "ManagementSystem.Web.xml");
                c.IncludeXmlComments(xmlPath, true);
                #endregion

                #region Token绑定到ConfigureServices
                // 开启小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                // 在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
                #endregion
            });
        }
    }

    /// <summary>
    /// 自定义版本
    /// </summary>
    public class CustomApiVersion
    {
        /// <summary>
        /// Api接口版本 自定义
        /// </summary>
        public enum ApiVersions
        {
            /// <summary>
            /// V1 版本
            /// </summary>
            V1 = 1,

            /// <summary>
            /// V2 版本
            /// </summary>
            V2 = 2,
        }
    }
}